import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import * as SharedStore from './shared-store';
import * as PackageService from '../services/package-service';
import { translate } from '@/lib/translate';
import { AppThunk } from '.';
import { createConnection } from '@/lib/signalr';

export interface PackageUpdateStore {
  packages: Models.Package.Model[];
}

export const initialState: PackageUpdateStore = {
  packages: []
};

const slice = createSlice({
  name: 'packageList',
  initialState,
  reducers: {
    setPackages: (state: PackageUpdateStore, action: PayloadAction<Models.Package.Model[]>) => {
      state.packages = action.payload;
    }
  }
});

export const { setPackages } = slice.actions;

export const reducer = slice.reducer;

// thunk actions
export const onInit = (): AppThunk => async (dispatch, store) => {
  dispatch(SharedStore.setTitle(translate('Page_Title_Packages', 'Packages')));
  dispatch(setupSignalRConnection());
};

export const onGetPackages = (): AppThunk => async (dispatch, store) => {
  try {
    const result = await PackageService.getPackages();
    if (result.data) {
      dispatch(setPackages(result.data));
    }
  } catch (err) {
    console.log(err);
  }
};

export const onUpdatePackage = (packageId: number, versionId: number): AppThunk => async (dispatch, store) => {
  try {
    const result = await PackageService.updatePackage(packageId, versionId);
    if (result.data) {
      const packages = store().packageList.packages;
      dispatch(setPackages(packages.map(x => (x.id === packageId ? result.data : x))));
    }
  } catch (err) {
    console.log(err);
  }
};

export const onUpdateAllPackages = (): AppThunk => async (dispatch, store) => {
  try {
    const result = await PackageService.updateAllPackages();
    if (result.data) {
      dispatch(setPackages(result.data));
    }
  } catch (err) {
    console.log(err);
  }
};

export const onResetPackages = (): AppThunk => async (dispatch, store) => {
  try {
    const result = await PackageService.resetPackages();
    if (result.data) {
      dispatch(setPackages(result.data));
    }
  } catch (err) {
    console.log(err);
  }
};

const setupSignalRConnection = (): AppThunk => async (dispatch, store) => {
  const connection = createConnection('hubs/package');
  try {
    await connection.start();
    connection.on('packageUpdated', (result: Models.Package.Model) => {
      const packages = store().packageList.packages;
      dispatch(setPackages(packages.map(x => (x.id === result.id ? result : x))));
    });
  } catch (err) {
    console.log(err);
  }
};

export default slice;
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import * as PackageService from '../services/package-service';
import { AppThunk } from '.';
import { toast } from 'react-toastify';

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
    },
    setModifiedPackages: (state: PackageUpdateStore, action: PayloadAction<Models.Package.Model[]>) => {
      state.packages = state.packages.map(x => action.payload.find(a => x.id === a.id) || x);
    }
  }
});

export const { setPackages, setModifiedPackages } = slice.actions;

export const reducer = slice.reducer;

// thunk actions
export const onGetPackages = (): AppThunk => async (dispatch) => {
  try {
    const result = await PackageService.getPackages();
    if (result.data) {
      dispatch(setPackages(result.data));
    }
  } catch (err) {
    console.log(err);
  }
};

export const onCreatePackage = (name: string, description: string, version: string, type: Enums.Type): AppThunk => async (dispatch, store) => {
  try {
    if(store().packageList.packages.find(x => x.name === name)) {
      toast('Package already exist');
      return;
    }

    const model: Models.Package.Create = {
      name,
      description,
      version,
      type
    };

    await PackageService.createPackage(model);
  } catch (err) {
    console.log(err);
  }
};

export const onUpdatePackage = (packageId: number): AppThunk => async () => {
  try {
    await PackageService.updatePackage(packageId);
  } catch (err) {
    console.log(err);
  }
};

export const onUpdateSelectedPackages = (ids: number[]): AppThunk => async () => {
  try {
    await PackageService.updateSelected(ids);
  } catch (err) {
    console.log(err);
  }
};

export const onDeleteSelectedPackages = (ids: number[]): AppThunk => async () => {
  try {
    await PackageService.deleteSelected(ids);
  } catch (err) {
    console.log(err);
  }
};

export const onImportPackages = (): AppThunk => async () => {
  try {
    await PackageService.importPackages();
  } catch (err) {
    console.log(err);
  }
};

export default slice;
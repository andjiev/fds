import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import * as PackageService from '../services/package-service';
import * as VersionService from '../services/version-service';
import { AppThunk } from '.';

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
    setPackage: (state: PackageUpdateStore, action: PayloadAction<Models.Package.Model>) => {
      state.packages = state.packages.map(x => (x.id === action.payload.id ? action.payload : x));
    }
  }
});

export const { setPackages, setPackage } = slice.actions;

export const reducer = slice.reducer;

// thunk actions
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

export const onUpdatePackage = (packageId: number): AppThunk => async (dispatch, store) => {
  try {
    const result = await PackageService.updatePackage(packageId);
    if (result.data) {
      dispatch(setPackage(result.data));
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

export const onCreatePackageVersion = (packageId: number, versionNumber: string): AppThunk => async (dispatch, store) => {
  try {
    const result = await VersionService.createVersion(packageId, versionNumber);
    if(result.data) {
      dispatch(setPackage(result.data));
    }
  } catch (err) {
    console.log(err);
  }
}

export default slice;
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
    setPackage: (state: PackageUpdateStore, action: PayloadAction<Models.Package.Model>) => {
      state.packages = state.packages.map(x => (x.id === action.payload.id ? action.payload : x));
    },
    setModifiedPackages: (state: PackageUpdateStore, action: PayloadAction<Models.Package.Model[]>) => {
      state.packages = state.packages.map(x => action.payload.find(a => x.id === a.id) || x);
    }
  }
});

export const { setPackages, setPackage, setModifiedPackages } = slice.actions;

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

export const onUpdateSelectedPackages = (ids: number[]): AppThunk => async (dispatch, store) => {
  try {
    const result = await PackageService.updateSelected(ids);
    if (result.data) {
      dispatch(setModifiedPackages(result.data));
    }
  } catch (err) {
    console.log(err);
  }
};

export const onDeleteSelectedPackages = (ids: number[]): AppThunk => async (dispatch, store) => {
  try {
    const result = await PackageService.deleteSelected(ids);
    if (result.data) {
      dispatch(setModifiedPackages(result.data));
    }
  } catch (err) {
    console.log(err);
  }
};

export const onImportPackages = (): AppThunk => async (dispatch, store) => {
  try {
    await PackageService.importPackages();
    toast('Import started');
  } catch (err) {
    console.log(err);
  }
};

export default slice;
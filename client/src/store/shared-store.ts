import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import * as UiStore from './ui-store';
import * as PackageStore from './package-store';
import { getCultureFromStorage, setCultureToStorage } from './helpers/language-helper';
import { initTranslations } from '../lib/translate';
import { AppThunk } from '.';
import { getTranslations } from '../services/translation-service';
import { FdsHubConnection } from '../lib/signalr';
import { toast } from 'react-toastify';

export interface SharedStore {
  title: string;
  culture: string;
}

const initialState: SharedStore = {
  title: '',
  culture: '',
};

const slice = createSlice({
  name: 'shared',
  initialState,
  reducers: {
    setTitle: (store: SharedStore, action: PayloadAction<string>) => {
      store.title = action.payload;
    },
    setCulture: (store: SharedStore, action: PayloadAction<string>) => {
      store.culture = action.payload;
    }
  }
});

export const { setTitle, setCulture } = slice.actions;

export const reducer = slice.reducer;

//thunk
export const bootstrapApp = (): AppThunk => async (dispatch, store) => {
  try {
    dispatch(UiStore.showInitialLoader());
    const translations = await getTranslations();

    const culture = getCultureFromStorage();
    dispatch(setCulture(culture));

    initTranslations(translations.data, culture);

    dispatch(UiStore.hideInitialLoader());
  } catch (err) {
    dispatch(UiStore.hideInitialLoader());
  }
};

export const changeCulture = (culture: string): AppThunk => async (dispatch, store) => {
  setCultureToStorage(culture);

  // refresh
  window.location.reload();
};

export const startSignalRConnection = (): AppThunk => async (dispatch, store) => {
  const connection = FdsHubConnection.getInstance('hubs/package')
  try {
    await connection.start();

    connection.on('packageUpdated', (result: Models.Package.Model) => {
      const packages = store().packageList.packages;
      dispatch(PackageStore.setPackages(packages.map(x => (x.id === result.id ? result : x))));
      toast(`${result.name} updated`);
    });

    connection.on('syncPackages', (result: Models.Package.Model[]) => {
      dispatch(PackageStore.setPackages(result));
      toast('Packages synced');
    });

    connection.on('packageInstalled', (result: Models.Package.Model) => {
      const packages = store().packageList.packages;
      dispatch(PackageStore.setPackages([...packages, result]));
      toast(`${result.name} installed`);
    });

    connection.on('packageDeleted', (id: number, name: string) => {
      const packages = store().packageList.packages;
      dispatch(PackageStore.setPackages(packages.filter(x => x.id !== id)));
      toast(`${name} deleted`);
    });
  } catch (err) {
    console.log(err);
  }
};

export const stopSignalRConnection = (): AppThunk => async (dispatch, store) => {
  const connection = FdsHubConnection.getInstance('hubs/package')
  try {
    await connection.stop();
  } catch (err) {
    console.log(err);
  }
}

export default slice;
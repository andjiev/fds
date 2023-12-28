import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import * as UiStore from './ui-store';
import * as PackageStore from './package-store';
import * as SettingsService from '../services/settings-service';
import { getCultureFromStorage, setCultureToStorage } from './helpers/language-helper';
import { initTranslations } from '../lib/translate';
import { AppThunk } from '.';
import { getTranslations } from '../services/translation-service';
import { FdsHubConnection } from '../lib/signalr';
import { toast } from 'react-toastify';
import { ImportState, Status } from '../lib/enums';

export interface SharedStore {
  title: string;
  culture: string;
  settings: Models.Settings.Model;
}

const initialState: SharedStore = {
  title: '',
  culture: '',
  settings: {
    state: ImportState.Initial
  }
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
    },
    setSettings: (store: SharedStore, action: PayloadAction<Models.Settings.Model>) => {
      store.settings = action.payload;
    }
  }
});

export const { setTitle, setCulture, setSettings } = slice.actions;

export const reducer = slice.reducer;

//thunk
export const bootstrapApp = (): AppThunk => async (dispatch) => {
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

// thunk actions
export const onGetSettings = (): AppThunk => async (dispatch) => {
  try {
    const result = await SettingsService.getSettings();
    if (result.data) {
      dispatch(setSettings(result.data));
    }
  } catch (err) {
    console.log(err);
  }
};

export const changeCulture = (culture: string): AppThunk => async () => {
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

    connection.on('importStarted', () => {
      dispatch(setSettings({ ...store().shared.settings, state: ImportState.Importing }));
      toast('Import started');
    });

    connection.on('importCompleted', (result: Models.Package.Model[]) => {
      dispatch(PackageStore.setPackages(result));
      dispatch(setSettings({ ...store().shared.settings, state: ImportState.Initial }));
      toast('Packages imported');
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

    connection.on('packagesModified', (ids: number[]) => {
      const packages = store().packageList.packages;
      const setModifiedPackages = packages.filter(x => ids.find(a => a === x.id)).map(x => ({ ...x, status: Status.Loading }));
      dispatch(PackageStore.setModifiedPackages(setModifiedPackages));
    });

  } catch (err) {
    console.log(err);
  }
};

export const stopSignalRConnection = (): AppThunk => async () => {
  const connection = FdsHubConnection.getInstance('hubs/package')
  try {
    await connection.stop();
  } catch (err) {
    console.log(err);
  }
}

export default slice;
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import * as UiStore from './ui-store';
import { getCultureFromStorage, setCultureToStorage } from './helpers/language-helper';
import { initTranslations } from '../lib/translate';
import { AppThunk } from '.';
import { getTranslations } from '@/services/translation-service';

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

export default slice;
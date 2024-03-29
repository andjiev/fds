import { CURRENT_ENVIRONMENT } from '../consts';
import i18next from 'i18next';
import { Environment } from './enums';

const initTranslations = (translations: Translations, culture: string) => {
  i18next.init({
    lng: culture,
    resources: translations
  });
};

const translate = (key: string, fallback: string = ''): string => {
  if (CURRENT_ENVIRONMENT == Environment.Development) {
    return i18next.t(key) || key;
  }

  return i18next.t(key, { defaultValue: fallback }) || fallback;
};

export { initTranslations, translate };

import { EN_CULTURE } from 'consts';

const key = 'culture';
const defaultCulture = EN_CULTURE;

const getCultureFromStorage = () => {
  const culture = sessionStorage.getItem(key);
  return culture ? culture : defaultCulture;
};

const setCultureToStorage = (culture: string) => {
  sessionStorage.setItem(key, culture);
};

export { getCultureFromStorage, setCultureToStorage };

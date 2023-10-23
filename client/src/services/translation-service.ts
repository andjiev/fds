import { AxiosResponse } from 'axios';
import httpService from './http-service';

const getTranslations = async (): Promise<AxiosResponse<Translations>> => {
  return httpService.get<Translations>('/translations.json', { baseURL: '/' });
};

export { getTranslations };

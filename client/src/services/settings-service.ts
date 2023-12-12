import { AxiosResponse } from 'axios';
import httpService from './http-service';

const getSettings = async (): Promise<AxiosResponse<Models.Settings.Model>> => {
  return httpService.get<Models.Settings.Model>(`/api/settings`);
};

export { getSettings };

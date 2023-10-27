import { AxiosResponse } from 'axios';
import httpService from './http-service';

const createVersion = async (packageId: number, versionNumber: string): Promise<AxiosResponse<Models.Package.Model>> => {
  return httpService.post<Models.Package.Model>(`/api/versions`, { packageId, versionNumber });
};

export { createVersion };

import { AxiosResponse } from 'axios';
import httpService from './http-service';

const getPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
  return httpService.get<Models.Package.Model[]>(`/api/packages`);
};

const createPackage = async (model: Models.Package.Create): Promise<AxiosResponse> => {
  return httpService.post(`/api/packages`, model);
};

const updatePackage = async (packageId: number): Promise<AxiosResponse<Models.Package.Model>> => {
  return httpService.put<Models.Package.Model>(`/api/packages/${packageId}`);
};

const updateAllPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
  return httpService.put<Models.Package.Model[]>(`/api/packages/updateAll`);
};

const syncPackages = async (): Promise<AxiosResponse> => {
  return httpService.put(`/api/packages/sync`);
};

export { getPackages, createPackage, updatePackage, updateAllPackages, syncPackages };

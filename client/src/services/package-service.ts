import { AxiosResponse } from 'axios';
import httpService from './http-service';

const getPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
  return httpService.get<Models.Package.Model[]>(`/api/packages`);
};

const updatePackage = async (packageId: number): Promise<AxiosResponse<Models.Package.Model>> => {
  return httpService.put<Models.Package.Model>(`/api/packages/${packageId}`);
};

const updateAllPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
  return httpService.put<Models.Package.Model[]>(`/api/packages/updateAll`);
};

const syncPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
  return httpService.put<Models.Package.Model[]>(`/api/packages/sync`);
};

export { getPackages, updatePackage, updateAllPackages, syncPackages };

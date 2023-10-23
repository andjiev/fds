import { AxiosResponse } from 'axios';
import httpService from './http-service';

const getPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
  return httpService.get<Models.Package.Model[]>(`/api/packages`);
};

const updatePackage = async (packageId: number, versionId: number): Promise<AxiosResponse<Models.Package.Model>> => {
  return httpService.put<Models.Package.Model>(`/api/packages/${packageId}`, { versionId });
};

const updateAllPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
  return httpService.put<Models.Package.Model[]>(`/api/packages/updateAll`);
};

const resetPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
  return httpService.put<Models.Package.Model[]>(`/api/packages/reset`);
};

export { getPackages, updatePackage, updateAllPackages, resetPackages };

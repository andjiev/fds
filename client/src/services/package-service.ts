import { AxiosResponse } from 'axios';
import httpService from './http-service';

const getPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
  return httpService.get<Models.Package.Model[]>(`/api/packages`);
};

const createPackage = async (model: Models.Package.Create): Promise<AxiosResponse> => {
  return httpService.post(`/api/packages`, model);
};

const updatePackage = async (packageId: number): Promise<AxiosResponse> => {
  return httpService.put(`/api/packages/${packageId}`);
};

const updateSelected = async (ids: number[]): Promise<AxiosResponse> => {
  return httpService.put(`/api/packages/updateSelected`, ids);
};

const deleteSelected = async (ids: number[]): Promise<AxiosResponse> => {
  return httpService.put(`/api/packages/deleteSelected`, ids);
};

const importPackages = async (): Promise<AxiosResponse> => {
  return httpService.put(`/api/packages/import`);
};

export { getPackages, createPackage, updatePackage, updateSelected, deleteSelected, importPackages };

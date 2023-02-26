import { AxiosResponse } from 'axios';
import httpService from './http-service';

const getPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
    return httpService.get<Models.Package.Model[]>(`/api/packages`);
};

const updatePackageVersion = async (packageId: number, versionId: number): Promise<AxiosResponse<Models.Package.Model>> => {
    return httpService.patch<Models.Package.Model>(`/api/packages/${packageId}/version/${versionId}`);
};

const resetPackages = async (): Promise<AxiosResponse<Models.Package.Model[]>> => {
    return httpService.patch<Models.Package.Model[]>(`/api/packages/reset`);
};

export { getPackages, updatePackageVersion, resetPackages };

declare module Models.Package {
  export interface Model {
    id: number;
    name: string;
    currentVersion: string;
    latestVersion: string;
    status: Enums.Status;
  }
}
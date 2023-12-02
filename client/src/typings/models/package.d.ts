declare module Models.Package {
  export interface Model {
    id: number;
    name: string;
    currentVersion: string;
    latestVersion: string;
    score?: number;
    url: string;
    description: string;
    status: Enums.Status;
    updatedOn: Date;
    type: Enums.Type;
  }

  export interface Create {
    name: string;
    version: string;
    description: string;
    type: Enums.Type;
  }
}
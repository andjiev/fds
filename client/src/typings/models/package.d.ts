declare module Models.Package {
  export interface Model {
    id: number;
    name: string;
    status: Enums.Status;
    version: Models.Version.Model;
    versionUpdate?: Models.Version.SimplifiedModel;
  }
}
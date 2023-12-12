declare module Enums {
  export enum Status {
    UpdateNeeded = 1,
    Loading = 2,
    UpToDate = 3
  }

  export enum Type {
    Prod = 1,
    Dev = 2
  }

  export enum ImportState {
    Initial = 1,
    Importing = 2
  }
}
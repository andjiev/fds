enum Environment {
  Development = 'development',
  Production = 'production'
}

enum Status {
  UpdateNeeded = 1,
  Loading = 2,
  UpToDate = 3
}

enum Type {
  Prod = 1,
  Dev = 2
}

enum ImportState {
  Initial = 1,
  Importing = 2
}

export { Environment, Status, Type, ImportState };

enum Environment {
  Development = 'development',
  Production = 'production'
}

enum Status {
  UpdateNeeded = 1,
  Updating = 2,
  UpToDate = 3
}

enum Type {
  Prod = 1,
  Dev = 2
}

export { Environment, Status, Type };

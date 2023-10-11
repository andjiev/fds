import * as UiStore from './ui-store';
import * as SharedStore from './shared-store';
import * as PackageListStore from './package-update-store';

const reducers = {
  ui: UiStore.reducer,
  shared: SharedStore.reducer,
  packageList: PackageListStore.reducer
};

export { reducers };

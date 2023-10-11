import { RouterState } from 'connected-react-router';

import * as UiStore from './ui-store';
import * as SharedStore from './shared-store';
import * as PackageList from './package-update-store';

export default interface ApplicationState {
  ui: UiStore.UiStore;
  router?: RouterState;

  shared: SharedStore.SharedStore;
  packageList: PackageList.PackageUpdateStore;
}

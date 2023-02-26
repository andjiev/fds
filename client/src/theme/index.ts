import { createMuiTheme } from '@material-ui/core/styles';

import './index.css';

const theme = createMuiTheme({
  palette: {
    primary: {
      main: '#333333'
    },
    grey: {
      300: '#F9F9F9'
    }
  },
  overrides: {}
});

export { theme };

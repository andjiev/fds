import { Suspense, lazy, useEffect } from 'react';

import Router from './router';
import { Box, Grid } from '@mui/material';
import { useAppDispatch, useAppSelector } from './hooks';
import { bootstrapApp } from './store/shared-store';
import Loading from './components/Loading';
import { onGetPackages } from './store/package-store';

const Menu = lazy(() => import('./components/Menu'));
const Navigation = lazy(() => import('./components/Navigation'));

const App = () => {
  const dispatch = useAppDispatch();
  const applicationBootstraped = useAppSelector(state => !state.ui.showInitialLoader);

  useEffect(() => {
    dispatch(bootstrapApp());
    dispatch(onGetPackages());
  }, []);

  const renderLoader = () => <Loading />;

  return (
    <>
      {applicationBootstraped ? (
        <Suspense fallback={renderLoader()}>
          <Box style={{ backgroundColor: 'rgb(237, 238, 240)' }}>
            <Grid container>
              <Grid item xs={12} md={4} lg={3} xl={2}>
                <Navigation />
              </Grid>
              <Grid item xs={12} md={8} lg={9} xl={10}>
                <Box>
                  <Menu />
                  <Box m={3}>
                    <Router />
                  </Box>
                </Box>
              </Grid>
            </Grid>
          </Box>
        </Suspense>
      ) : (
        renderLoader()
      )}
    </>
  );
};

export default App;

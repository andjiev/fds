import { Suspense, lazy, useEffect } from 'react';

import Router from './router';
import { Box, Grid } from '@mui/material';
import { useAppDispatch, useAppSelector } from './hooks';
import { bootstrapApp } from './store/shared-store';
import Loading from './components/Loading';
import { onGetPackages } from './store/package-store';
import { ToastContainer } from 'react-toastify';

const Menu = lazy(() => import('./components/Menu'));

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
            <Menu />
            <Box m={3}>
              <Router />
              <ToastContainer position="bottom-right" />
            </Box>
          </Box>
        </Suspense>
      ) : (
        renderLoader()
      )}
    </>
  );
};

export default App;

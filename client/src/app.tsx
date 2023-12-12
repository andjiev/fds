import { Suspense, lazy, useEffect } from 'react';

import Router from './router';
import { Box } from '@mui/material';
import { useAppDispatch, useAppSelector } from './hooks';
import { bootstrapApp, onGetSettings } from './store/shared-store';
import Loading from './components/Loading';
import { onGetPackages } from './store/package-store';
import { ToastContainer, toast } from 'react-toastify';
import Menu from './components/Menu';

// const Menu = lazy(() => import('./components/Menu'));

const App = () => {
  const dispatch = useAppDispatch();
  const applicationBootstraped = useAppSelector(state => !state.ui.showInitialLoader);

  useEffect(() => {
    dispatch(bootstrapApp());
    dispatch(onGetSettings());
    dispatch(onGetPackages());
  }, []);

  const renderLoader = () => <Loading />;

  return (
    <>
      {applicationBootstraped ? (
        // <Suspense fallback={renderLoader()}>
        <Box style={{ backgroundColor: 'rgb(237, 238, 240)', minHeight: '100vh' }}>
          <Menu />
          <Box ml={3} mr={3}>
            <Router />
            <ToastContainer position="top-right" theme="colored" />
          </Box>
        </Box>
        // </Suspense>
      ) : (
        renderLoader()
      )}
    </>
  );
};

export default App;

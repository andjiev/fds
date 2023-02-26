import React, { useEffect, lazy, Suspense } from 'react';
import { connect } from 'react-redux';
import { Route, Switch } from 'react-router';
import { AppDispatch } from 'index';
import ApplicationState from 'store/application-store';
import { bootstrapApp } from 'store/shared-store';

import { ROUTES } from './consts';
import { Box, Grid } from '@material-ui/core';
import LoadingScreen from 'react-loading-screen';

interface IApp {
  applicationBootstraped: boolean;

  bootstrapApp: () => void;
}

const _App: React.FC<IApp> = (props: IApp) => {
  useEffect(() => {
    props.bootstrapApp();
  }, []);

  const Menu = lazy(() => import('components/menu'));
  const Navigation = lazy(() => import('components/navigation'));
  const PackageListPage = lazy(() => import('pages/package-list'));

  const renderLoader = () => {
    return (
      <LoadingScreen loading bgColor="#333333" spinnerColor="#ffffff">
        <Box component="span"></Box>
      </LoadingScreen>
    );
  };

  return (
    <>
      {props.applicationBootstraped ? (
        <>
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
                      <Switch>
                        <Route exact path={ROUTES.PACKAGE_LIST} component={PackageListPage} />
                      </Switch>
                    </Box>
                  </Box>
                </Grid>
              </Grid>
            </Box>
          </Suspense>
        </>
      ) : (
          renderLoader()
        )}
    </>
  );
};

const mapDispatchToProps = (dispatch: AppDispatch) => ({
  bootstrapApp: () => {
    dispatch(bootstrapApp());
  }
});

const mapStateToProps = (state: ApplicationState) => {
  return {
    applicationBootstraped: !state.ui.showInitialLoader
  };
};

const App = connect(() => mapStateToProps, mapDispatchToProps)(_App);

export default App;

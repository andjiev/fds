import React, { useEffect } from 'react';
import { connect } from 'react-redux';

import { AppDispatch } from 'index';
import ApplicationState from 'store/application-store';

import { Grid, Box, TableContainer, Table, Paper, TableRow, TableCell, TableBody, IconButton, CircularProgress, Button } from '@material-ui/core';
import * as PackageListStore from 'store/package-update-store';
import { translate } from 'lib/translate';
import SystemUpdateAltIcon from '@material-ui/icons/SystemUpdateAlt';
import { StyledTableHead } from './package-update.styles';
import { Status } from 'lib/enums';

interface IPackageUpdateProps {
  packages: Models.Package.Model[];

  onPageInit: () => void;
  onGetPackages: () => void;
  onUpdatePackage: (packageId: number, versionId: number) => void;
  onUpdateAllPackages: () => void;
  onResetPackages: () => void;
}

const PackageUpdatePage = (props: IPackageUpdateProps) => {
  useEffect(() => {
    props.onPageInit();
  }, []);

  return (
    <>
      <Box mt={2}>
        <Grid container spacing={3}>
          <Grid item xs={6}>
            <TableContainer component={Paper}>
              <Table>
                <StyledTableHead>
                  <TableRow>
                    <TableCell>Name</TableCell>
                    <TableCell>Version</TableCell>
                    <TableCell>Update</TableCell>
                    <TableCell align="center">Action</TableCell>
                  </TableRow>
                </StyledTableHead>
                <TableBody>
                  {props.packages.map(item => {
                    return (
                      <TableRow key={item.id}>
                        <TableCell>{translate(item.name)}</TableCell>
                        <TableCell>{item.version.name}</TableCell>
                        <TableCell>{item.versionUpdate ? `${item.versionUpdate.name} is available` : item.status === Status.Updated ? 'Package is up to date' : 'Updating...'}</TableCell>
                        <TableCell align="center">
                          {item.versionUpdate ? (
                            <IconButton size="small" onClick={() => props.onUpdatePackage(item.id, item.versionUpdate.id)}>
                              <SystemUpdateAltIcon />
                            </IconButton>
                          ) : item.status === Status.Updated ? (
                            ''
                          ) : (
                            <CircularProgress size="20px" />
                          )}
                        </TableCell>
                      </TableRow>
                    );
                  })}
                </TableBody>
              </Table>
            </TableContainer>
          </Grid>
          <Grid item xs={6}>
            <Box>
              <Button variant="contained" color="primary" onClick={props.onUpdateAllPackages}>
                Update all packages
              </Button>
            </Box>
            <Box mt={3}>
              <Button variant="contained" color="secondary" onClick={props.onResetPackages}>
                Reset packages
              </Button>
            </Box>
          </Grid>
        </Grid>
      </Box>
    </>
  );
};

const mapDisptachToProps = (dispatch: AppDispatch) => ({
  onPageInit: () => {
    dispatch(PackageListStore.onInit());
    dispatch(PackageListStore.onGetPackages());
  },
  onGetPackages: () => {
    dispatch(PackageListStore.onGetPackages());
  },
  onUpdatePackage: (packageId: number, versionId: number) => {
    dispatch(PackageListStore.onUpdatePackage(packageId, versionId));
  },
  onUpdateAllPackages: () => {
    dispatch(PackageListStore.onUpdateAllPackages());
  },
  onResetPackages: () => {
    dispatch(PackageListStore.onResetPackages());
  }
});

const mapStateToProps = (state: ApplicationState) => {
  return {
    packages: state.packageList.packages
  };
};

const _PackageUpdatePage = connect(() => mapStateToProps, mapDisptachToProps)(PackageUpdatePage);

export default _PackageUpdatePage;

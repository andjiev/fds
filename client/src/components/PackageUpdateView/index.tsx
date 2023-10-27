import { useEffect } from 'react';

import { Grid, Box, TableContainer, Table, Paper, TableRow, TableCell, TableBody, IconButton, CircularProgress, Button } from '@mui/material';
import { onUpdateAllPackages, onUpdatePackage, onResetPackages } from '@/store/package-store';
import { StyledTableHead } from './styles';
import { useAppDispatch, useAppSelector } from '@/hooks';
import { translate } from '@/lib/translate';
import { Status } from '@/lib/enums';
import SystemUpdateAltIcon from '@mui/icons-material/SystemUpdateAlt';
import { setTitle } from '@/store/shared-store';

const PackageUpdateView = () => {
  const dispatch = useAppDispatch();
  const packages = useAppSelector(state => state.packageList.packages);

  useEffect(() => {
    dispatch(setTitle(translate('Page_Title_Packages', 'Packages')));
  }, []);

  const onUpdate = (packageId: number, versionId: number) => {
    dispatch(onUpdatePackage(packageId, versionId));
  }

  const onUpdateAll = () => {
    dispatch(onUpdateAllPackages());
  }

  const onReset = () => {
    dispatch(onResetPackages());
  }

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
                  {packages.map(item => {
                    return (
                      <TableRow key={item.id} style={{ height: 70 }}>
                        <TableCell>{translate(item.name)}</TableCell>
                        <TableCell>v.{item.version.name}</TableCell>
                        <TableCell>{item.versionUpdate ? `v.${item.versionUpdate.name} is available` : item.status === Status.Updated ? 'Up to date' : 'Updating...'}</TableCell>
                        <TableCell align="center">
                          {item.versionUpdate ? (
                            <IconButton size="small" onClick={() => onUpdate(item.id, item.versionUpdate.id)}>
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
              <Button variant="contained" color="primary" onClick={onUpdateAll}>
                Update all packages
              </Button>
            </Box>
            <Box mt={3}>
              <Button variant="contained" color="secondary" onClick={onReset}>
                Reset packages
              </Button>
            </Box>
          </Grid>
        </Grid>
      </Box>
    </>
  );
};

export default PackageUpdateView;

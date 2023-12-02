import { useEffect } from 'react';

import { Grid, Box, TableContainer, Table, Paper, TableRow, TableCell, TableBody, IconButton, CircularProgress, Button } from '@mui/material';
import { onUpdateAllPackages, onUpdatePackage, onSyncPackages } from '@/store/package-store';
import { StyledTableHead } from './styles';
import { useAppDispatch, useAppSelector } from '@/hooks';
import { translate } from '@/lib/translate';
import { Status } from '@/lib/enums';
import UpdateIcon from '@mui/icons-material/Update';
import { setTitle } from '@/store/shared-store';
import { ScoreBadge } from '../ScoreBadge';
import Search from '../Search';
import NpmImage from '../../assets/npm.png';
import { Type } from '@/lib/enums';

const PackageUpdateView = () => {
  const dispatch = useAppDispatch();
  const packages = useAppSelector(state => state.packageList.packages);

  useEffect(() => {
    dispatch(setTitle(translate('Page_Title_Packages', 'Packages')));
  }, []);

  const onUpdate = (packageId: number) => {
    dispatch(onUpdatePackage(packageId));
  }

  const onUpdateAll = () => {
    dispatch(onUpdateAllPackages());
  }

  const onSync = () => {
    dispatch(onSyncPackages());
  }

  return (
    <Box mt={5}>
      <Grid item display={'flex'} alignItems={'center'} justifyContent={'space-between'} width={'100%'} mt={4}>
        <Box>
          <Search />
        </Box>
        <Grid item display={'flex'} alignItems={'center'}>
          <Box ml={2}>
            <Button variant="contained" color="primary" onClick={onSync}>
              Sync
            </Button>
          </Box>
          <Box ml={2}>
            <Button variant="contained" color="secondary" onClick={onUpdateAll}>
              Update all
            </Button>
          </Box>
        </Grid>
      </Grid>
      <Grid item mt={5}>
        <TableContainer component={Paper}>
          <Table>
            <StyledTableHead>
              <TableRow>
                <TableCell></TableCell>
                <TableCell>Name</TableCell>
                <TableCell align="center">Score</TableCell>
                <TableCell>Description</TableCell>
                <TableCell>Type</TableCell>
                <TableCell>Current version</TableCell>
                <TableCell>Latest version</TableCell>
                <TableCell align="center">Action</TableCell>
              </TableRow>
            </StyledTableHead>
            <TableBody>
              {packages.map(item => {
                return (
                  <TableRow key={item.id} style={{ height: 70 }}>
                    <TableCell>
                      <img src={NpmImage} width={30} />
                    </TableCell>
                    <TableCell>{item.name}</TableCell>
                    <TableCell align="center">
                      <ScoreBadge
                        href={item.url}
                        score={item.score}
                        target="_blank"
                        title="Visit Snyk.io package details"
                      >
                        {item.score}%
                      </ScoreBadge>
                    </TableCell>
                    <TableCell>{item.description}</TableCell>
                    <TableCell>{item.type === Type.Dev ? 'dev' : 'prod'}</TableCell>
                    <TableCell>{item.currentVersion}</TableCell>
                    <TableCell>{item.latestVersion}</TableCell>
                    <TableCell align="center">
                      {item.status === Status.UpdateNeeded ? (
                        <IconButton size="small" onClick={() => onUpdate(item.id)}>
                          <UpdateIcon />
                        </IconButton>
                      ) : item.status === Status.UpToDate ? (
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
    </Box>
  );
};

export default PackageUpdateView;

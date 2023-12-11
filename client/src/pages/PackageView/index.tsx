import { useEffect, useState } from 'react';

import { Grid, Box, TableContainer, Table, Paper, TableRow, TableCell, TableBody, IconButton, CircularProgress, Button, Switch, FormControlLabel, Checkbox, ToggleButtonGroup, ToggleButton } from '@mui/material';
import { onUpdateSelectedPackages, onDeleteSelectedPackages, onUpdatePackage, onImportPackages } from '../../store/package-store';
import { StyledTableHead } from './styles';
import { useAppDispatch, useAppSelector } from '../../hooks';
import { translate } from '../../lib/translate';
import { Status, Type } from '../../lib/enums';
import UpdateIcon from '@mui/icons-material/Update';
import { setTitle } from '../../store/shared-store';
import { ScoreBadge } from '../../components/ScoreBadge';
import Search from '../../components/Search';
import NpmImage from '../../assets/npm.png';
import { PackageType } from '../../components/PackageType';
import AutoDeleteIcon from '@mui/icons-material/AutoDelete';
import PlaylistAddCheckIcon from '@mui/icons-material/PlaylistAddCheck';
import RadioButtonUncheckedIcon from '@mui/icons-material/RadioButtonUnchecked';

type ActionType = 'single' | 'multi-update' | 'multi-delete';

const PackageView = () => {
  const dispatch = useAppDispatch();
  const packages = useAppSelector(state => state.packageList.packages);
  const [action, setAction] = useState<ActionType>('single');
  const [selected, setSelected] = useState<number[]>([]);

  useEffect(() => {
    dispatch(setTitle(translate('Page_Title_Packages', 'Packages')));
  }, []);

  const onUpdate = (packageId: number) => {
    dispatch(onUpdatePackage(packageId));
  }

  const onUpdateSelected = () => {
    dispatch(onUpdateSelectedPackages(selected));
    setSelected([]);
    setAction('single');
  }

  const onDeleteSelected = () => {
    dispatch(onDeleteSelectedPackages(selected));
    setSelected([]);
    setAction('single');
  }

  const onImport = () => {
    dispatch(onImportPackages());
  }

  const renderActionCell = (item: Models.Package.Model) => {
    if (action === 'multi-delete') {
      return <Checkbox size="small" onChange={(checked) => {
        if (checked) {
          setSelected([...selected, item.id]);
        } else {
          setSelected(selected.filter(x => x !== item.id));
        }
      }} />;
    }

    if (action === 'multi-update' && item.status === Status.UpdateNeeded) {
      return <Checkbox size="small" onChange={(checked) => {
        if (checked) {
          setSelected([...selected, item.id]);
        } else {
          setSelected(selected.filter(x => x !== item.id));
        }
      }} />;
    }

    if (item.status === Status.UpdateNeeded) {
      return <IconButton size="small" onClick={() => onUpdate(item.id)}>
        <UpdateIcon />
      </IconButton>;
    }

    if (item.status === Status.UpToDate) {
      return '';
    }

    return <CircularProgress size="20px" />;
  }

  return (
    <Box mt={5}>
      <Grid item display={'flex'} alignItems={'center'} justifyContent={'space-between'} width={'100%'} mt={4}>
        <Box>
          <Search />
        </Box>
        <Grid item display={'flex'} alignItems={'center'}>
          <Box ml={2}>
            <ToggleButtonGroup
              value={action}
              exclusive
              onChange={(_, value) => {
                value ? setAction(value) : setAction('single');
                setSelected([]);
              }}
              aria-label="text alignment"
            >
              <ToggleButton value="single" aria-label="single">
                <RadioButtonUncheckedIcon />
              </ToggleButton>
              <ToggleButton value="multi-update" aria-label="multi update">
                <PlaylistAddCheckIcon />
              </ToggleButton>
              <ToggleButton value="multi-delete" aria-label="multi deleete">
                <AutoDeleteIcon />
              </ToggleButton>
            </ToggleButtonGroup>
          </Box>
          <Box ml={2}>
            <Button variant="contained" color="success" onClick={onUpdateSelected} disabled={!selected.length || action === 'multi-delete'}>
              Update({action === 'multi-update' ? selected.length : 0})
            </Button>
          </Box>
          <Box ml={2}>
            <Button variant="contained" color="error" onClick={onDeleteSelected} disabled={!selected.length || action === 'multi-update'}>
              Delete({action === 'multi-delete' ? selected.length : 0})
            </Button>
          </Box>
          <Box ml={2}>
            <Button variant="contained" color="primary" onClick={onImport}>
              Import
            </Button>
          </Box>
        </Grid>
      </Grid>
      <Grid item pt={5} pb={5}>
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
                    <TableCell><PackageType type={item.type}>{item.type === Type.Dev ? 'D' : 'P'}</PackageType></TableCell>
                    <TableCell>{item.currentVersion}</TableCell>
                    <TableCell>{item.latestVersion}</TableCell>
                    <TableCell align="center">
                      {renderActionCell(item)}
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

export default PackageView;

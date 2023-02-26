import React from 'react';
import { connect } from 'react-redux';

import { AppDispatch } from 'index';
import ApplicationState from 'store/application-store';

import * as SharedStore from 'store/shared-store';

import { StyledBox } from './menu.styles';
import { Grid, Box, Typography, FormControl, Select, MenuItem } from '@material-ui/core';
import { formatCulture, formatLanguage } from './menu.utils';

interface IMenuProps {
  title: string;
  language: string;

  onLanguageChange: (lang: string) => void;
}

const Menu = (props: IMenuProps) => {
  const languages = ['EN', 'MK'];

  return (
    <>
      <StyledBox boxShadow={3}>
        <Box p={3}>
          <Grid container>
            <Grid item xs={8}>
              <Typography variant="h5">{props.title}</Typography>
            </Grid>
            <Grid container item xs={4} justify="flex-end">
              <Grid item xs={12} md={2}>
                <FormControl fullWidth>
                  <Select
                    value={props.language}
                    onChange={(event: React.ChangeEvent<{ value: unknown }>) => props.onLanguageChange(event.target.value as string)}
                  >
                    {languages.map(lang => {
                      return <MenuItem key={lang} value={lang}>{lang}</MenuItem>;
                    })}
                  </Select>
                </FormControl>
              </Grid>
            </Grid>
          </Grid>
        </Box>
      </StyledBox>
    </>
  );
};

const mapDispatchToProps = (dispatch: AppDispatch) => ({
  onLanguageChange: (lang: string) => {
    dispatch(SharedStore.changeCulture(formatLanguage(lang)));
  }
});

const mapStateToProps = (state: ApplicationState) => {
  return {
    title: state.shared.title,
    language: formatCulture(state.shared.culture)
  };
};

const MenuContainer = connect(mapStateToProps, mapDispatchToProps)(Menu);

export default MenuContainer;

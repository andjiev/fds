import React from 'react';
import { push } from 'connected-react-router';
import { connect } from 'react-redux';

import { List, ListItem, ListItemText, Box, Grid } from '@material-ui/core';
import { StyledNavigationWrapper, WhiteIcons, StyledImage } from './navigation.styles';

import { AppDispatch } from 'index';
import ApplicationState from 'store/application-store';

import { ROUTES } from 'consts';
import AllInboxIcon from '@material-ui/icons/AllInbox';
import UpdateIcon from '@material-ui/icons/Update';
import { translate } from 'lib/translate';

interface INavigationProps {
  navigateTo: (url: string) => void;
}

const Navigation = (props: INavigationProps) => {
  return (
    <StyledNavigationWrapper>
      <Box p={3}>
        <Grid container justify="center">
          <Grid item>
            <StyledImage src="/static/assets/app_logo.png" />
          </Grid>
        </Grid>
      </Box>
      <List component="nav">
        <ListItem button alignItems="center" onClick={() => props.navigateTo(ROUTES.PACKAGE_UPDATE)}>
          <WhiteIcons>
            <UpdateIcon />
          </WhiteIcons>
          <ListItemText primary={translate('Page_Title_Packages', 'Packages')} />
        </ListItem>
        <ListItem button alignItems="center" onClick={() => props.navigateTo(ROUTES.PACKAGE_VERSION)}>
          <WhiteIcons>
            <AllInboxIcon />
          </WhiteIcons>
          <ListItemText primary={translate('Page_Title_Versions', 'Versions')} />
        </ListItem>
      </List>
    </StyledNavigationWrapper>
  );
};

const mapDisptachToProps = (disptach: AppDispatch) => ({
  navigateTo: (url: string) => {
    disptach(push(url));
  }
});

const mapStateToProps = (state: ApplicationState) => {
  return {};
};

const _Navigation = connect(() => mapStateToProps, mapDisptachToProps)(Navigation);

export default _Navigation;

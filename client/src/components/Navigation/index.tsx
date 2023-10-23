import { List, ListItem, ListItemText, Box, Grid, Typography } from '@mui/material';
import { StyledNavigationWrapper, WhiteIcons, StyledImage } from './styles';
import AllInboxIcon from '@mui/icons-material/AllInbox';
import UpdateIcon from '@mui/icons-material/Update';
import { translate } from '@/lib/translate';
import { NavLink } from 'react-router-dom';

const Navigation = () => {
  return (
    <StyledNavigationWrapper>
      <Box p={3}>
        <Grid container justifyContent="center">
          <Grid item>
            <StyledImage src="/app_logo.png" />
          </Grid>
        </Grid>
      </Box>
      <List component="nav">
        <NavLink to="/">
          <ListItem alignItems="center">
            <WhiteIcons>
              <UpdateIcon htmlColor="#fff" />
            </WhiteIcons>
            <ListItemText
              disableTypography
              primary={<Typography variant="body1" style={{ color: '#FFFFFF' }}>{translate('Page_Title_Packages', 'Packages')}</Typography>}
            />
          </ListItem>
        </NavLink>
        <NavLink to="/version">
          <ListItem alignItems="center">
            <WhiteIcons>
              <AllInboxIcon htmlColor="#fff" />
            </WhiteIcons>
            <ListItemText
              disableTypography
              primary={<Typography variant="body1" style={{ color: '#FFFFFF' }}>{translate('Page_Title_Versions', 'Versions')}</Typography>}
            />
          </ListItem>
        </NavLink>
      </List>
    </StyledNavigationWrapper>
  );
};

export default Navigation;

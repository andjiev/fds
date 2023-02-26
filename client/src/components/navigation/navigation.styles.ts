import { Box, ListItemIcon } from "@material-ui/core";
import styled from "styled-components";

const StyledNavigationWrapper = styled(Box)`
  background-color: ${props => props.theme.palette.primary.main};
  height: 100%;
  position: relative;
  color: white;

  ${props => props.theme.breakpoints.up('md')} {
    min-height: 100vh;
  }
`;

const WhiteIcons = styled(ListItemIcon)`
  color: white;
`;

const StyledImage = styled.img`
  width: 220px;
`;

export { StyledNavigationWrapper, WhiteIcons, StyledImage };
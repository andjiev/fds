import { Box, ListItemIcon } from "@mui/material";
import styled from "styled-components";

const StyledNavigationWrapper = styled(Box)`
  background-color: #333333;
  height: 100%;
  position: relative;
  color: white;
  min-height: 100vh;
`;

const WhiteIcons = styled(ListItemIcon)`
  color: white;
`;

const StyledImage = styled.img`
  width: 220px;
`;

export { StyledNavigationWrapper, WhiteIcons, StyledImage };
import { changeCulture } from '../../store/shared-store';
import { useAppDispatch, useAppSelector } from '../../hooks';

import { Box, FormControl, Grid, MenuItem, Select, Typography } from '@mui/material';
import { formatCulture, formatLanguage } from './menu.utils';
import { StyledBox } from './menu.styles';

const Menu = () => {
  const dispatch = useAppDispatch();
  const title = useAppSelector(state => state.shared.title);
  const language = useAppSelector(state => formatCulture(state.shared.culture));
  const languages = ['EN', 'MK'];

  const onLanguageChange = (lang: string) => {
    dispatch(changeCulture(formatLanguage(lang)));
  };

  return (
    <>
      <StyledBox boxShadow={3}>
        <Box p={3}>
          <Grid container alignItems="center">
            <Grid item xs={8}>
              <Typography variant="h5">{title}</Typography>
            </Grid>
            <Grid container item xs={4} justifyContent="flex-end">
              <Grid item xs={12} md={2}>
                <FormControl fullWidth>
                  {/* <Select
                    value={language}
                    onChange={(event: any) => onLanguageChange(event.target.value as string)}
                    style={{ height: 45 }}
                  >
                    {languages.map(lang => {
                      return <MenuItem key={lang} value={lang}>{lang}</MenuItem>;
                    })}
                  </Select> */}
                </FormControl>
              </Grid>
            </Grid>
          </Grid>
        </Box>
      </StyledBox>
    </>
  );
};

export default Menu;

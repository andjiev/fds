import * as React from 'react';
import Autocomplete from '@mui/material/Autocomplete';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import { debounce } from '@mui/material/utils';
import { StyledDiv, StyledTextInput } from './styles';
import axios from 'axios';
import { useEffect, useMemo } from 'react';
import NpmImage from '../../assets/npm.png';
import { Button } from '@mui/material';

interface PackageResponse {
  name: string;
  description: string;
  version: string;
}

const Search = () => {
  const [inputValue, setInputValue] = React.useState('');
  const [options, setOptions] = React.useState<readonly PackageResponse[]>([]);

  const fetch = useMemo(
    () =>
      debounce(
        (
          request: { input: string },
          callback: (results?: readonly PackageResponse[]) => void,
        ) => {
          axios.get(`https://api.npms.io/v2/search?from=0&size=25&q=${request.input}`).then((response) => {
            callback(response.data.results.map((result: any) => result.package));
          });
        },
        400,
      ),
    [],
  );

  useEffect(() => {
    let active = true;


    fetch({ input: inputValue }, (results?: readonly PackageResponse[]) => {
      if (active) {
        let newOptions: readonly PackageResponse[] = [];


        if (results) {
          newOptions = [...newOptions, ...results];
        }

        setOptions(newOptions);
      }
    });

    return () => {
      active = false;
    };
  }, [inputValue, fetch]);

  return (
    <Autocomplete
      id="search-bar"
      sx={{ width: 400 }}
      getOptionLabel={(option) =>
        typeof option === 'string' ? option : option.name
      }
      filterOptions={(x) => x}
      options={options}
      autoComplete
      includeInputInList
      filterSelectedOptions
      noOptionsText="No packages"
      disableCloseOnSelect
      disableClearable
      // onChange={(event: any, newValue: PackageResponse | null) => {
      //   setOptions(newValue ? [newValue, ...options] : options);
      // }}
      onInputChange={(event, newInputValue) => {
        setInputValue(newInputValue);
      }}
      renderInput={(params) => (
        <StyledTextInput {...params} label="Search new package" fullWidth />
      )}
      renderOption={(props, option) => {
        return (
          <StyledDiv>
            <Grid container alignItems="center" sx={{ display: 'flex' }}>
              <Grid item sx={{ display: 'flex', width: 44 }}>
                <img src={NpmImage} width={30} />
              </Grid>
              <Grid item sx={{ wordWrap: 'break-word' }}>
                <Typography variant="body2" color="text.secondary">
                  {option.name}
                </Typography>
              </Grid>
              <Grid item sx={{ display: 'flex', justifyContent: 'in' }}>
                <Typography variant="body2" color="text.secondary">
                  prod
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  dev
                </Typography>
              </Grid>
            </Grid>
          </StyledDiv>
        );
      }}
    />);
};

export default Search;
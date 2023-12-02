import * as React from 'react';
import Autocomplete from '@mui/material/Autocomplete';
import Typography from '@mui/material/Typography';
import { debounce } from '@mui/material/utils';
import { StyledDiv, StyledTextInput } from './styles';
import axios from 'axios';
import { useEffect, useMemo, useState } from 'react';
import NpmImage from '../../assets/npm.png';
import { PackageType } from '../PackageType';
import { Type } from '@/lib/enums';

interface PackageResponse {
  name: string;
  description: string;
  version: string;
}

const Search = () => {
  const [inputValue, setInputValue] = useState('');
  const [options, setOptions] = useState<readonly PackageResponse[]>([]);
  const [open, setOpen] = useState(false);

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

  const onSelected = (name: string, type: Type) => {
    // alert(name + ' ' + type);

    setTimeout(() => {
      setOpen(false);
    }, 200);
  };

  return (
    <Autocomplete
      id="search-bar"
      sx={{ width: 500 }}
      getOptionLabel={(option) =>
        typeof option === 'string' ? option : option.name
      }
      filterOptions={(x) => x}
      options={options}
      autoComplete
      includeInputInList
      filterSelectedOptions
      noOptionsText="No packages"
      open={open}
      // disableCloseOnSelect
      // disableClearable
      // onChange={(event: any, newValue: PackageResponse | null) => {
      //   setOptions(newValue ? [newValue, ...options] : options);
      // }}
      onInputChange={(event, newInputValue) => {
        setInputValue(newInputValue);
        if (newInputValue && inputValue !== newInputValue) {
          setTimeout(() => {
            setOpen(true);
          }, 200);
        } else {
          setOpen(false);
        }
      }}
      renderInput={(params) => (
        <StyledTextInput {...params} label="Search new package" fullWidth />
      )}
      renderOption={(props, option) => {
        return (
          <StyledDiv>
            <div style={{ display: 'flex' }}>
              <div style={{ width: 44 }}>
                <img src={NpmImage} width={30} />
              </div>
              <Typography variant="body2" color="text.secondary">
                {option.name}  <b>{option.version}</b>
              </Typography>
            </div>
            <div style={{ display: 'flex' }}>
              <Typography variant="body2" color="text.secondary" style={{ cursor: 'pointer' }} component={'span'} onClick={() => onSelected(option.name, Type.Prod)}>
                <PackageType type={Type.Prod}>P</PackageType>
              </Typography>
              <Typography variant="body2" color="text.secondary" style={{ cursor: 'pointer' }} ml={1} onClick={() => onSelected(option.name, Type.Dev)}>
                <PackageType type={Type.Dev}>D</PackageType>
              </Typography>
            </div>
          </StyledDiv>
        );
      }}
    />);
};

export default Search;
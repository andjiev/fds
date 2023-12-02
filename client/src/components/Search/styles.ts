import { TextField } from "@mui/material";
import styled from "styled-components";

const StyledTextInput = styled(TextField)`
    color: #000;
    background-color: #fff;

    & .MuiInputBase-input {
        color: #000;
    }
`;

const StyledDiv = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px;
`;

export { StyledDiv, StyledTextInput };

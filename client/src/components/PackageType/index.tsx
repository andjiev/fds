import styled from "styled-components";
import { CSSType } from "../../typings/styled";
import { Type } from "../../lib/enums";

export const PackageType = styled.a<{ type: Enums.Type }>`
  color: white;
  text-decoration: none;
  font-weight: 100;
  padding: 4px 8px;
  border-radius: 2px;

  ${({ type }): CSSType => type === Type.Prod && 'background: #833EB4;'}
  ${({ type }): CSSType => type === Type.Dev && 'background: #E05D45;'}
`;

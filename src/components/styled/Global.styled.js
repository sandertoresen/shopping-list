import { createGlobalStyle } from 'styled-components';

const GlobalStyles = createGlobalStyle`
  body {
    font-family: 'Roboto', sans-serif;
    background: ${props => props.theme.colors.background}
  }
`;

export default GlobalStyles;

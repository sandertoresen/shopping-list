import React from 'react'
import ReactDOM from 'react-dom/client'
import { ThemeProvider } from 'styled-components'
import App from './App.jsx'
import GlobalStyles from './components/styled/Global.styled.js'
import theme from './components/styled/theme.js'
import dotenv from 'dotenv';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <ThemeProvider theme={theme}>
      <GlobalStyles/>
      <App /> 
    </ThemeProvider>
  </React.StrictMode>,
)

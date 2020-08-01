import React from 'react';
import AgGrid from './agGrid';
import createGlobalStyle from './createGlobalStyle';
import theme from './theme';

const sbTrackColor = theme.color.background;
const sbThumbColor = '#a2a5b9';

// Why don't I use styled-component? It has way more features than I need and
// most of them are already available from material-ui's own styled api. But
// material-ui global styling api do not work when styling global scrollbar, so
// I have to copy the relavent code from styled-component to fix the issue. Also
// mimic styled-component api to have that syntax highlighting in vscode
const GlobalStyle = createGlobalStyle`
  ::selection {
    color: ${theme.color.white};
    background-color: ${theme.color.blue};
  }

  /* custom scrollbar for chrome and firefox */
  /* Currently there is no cross-platform support for custom css scrollbar */
  * {
    /* Firefox */
    /* https://stackoverflow.com/a/54101063/9449426 */
    scrollbar-width: thin;
    scrollbar-color: ${sbThumbColor} ${sbTrackColor};

    /* Chromium */
    /* https://stackoverflow.com/a/53739309/9449426 */
    ::-webkit-scrollbar {
      width: .8rem;
    }
    ::-webkit-scrollbar-track-piece {
      background-color: ${sbTrackColor};
    }
    ::-webkit-scrollbar-thumb:vertical {
      background-color: ${sbThumbColor};
    }
  }
`;

export default function GlobalStyles(): JSX.Element {
  return (
    <>
      <GlobalStyle />
      <AgGrid />
    </>
  );
}

import React from "react";
import { createGlobalStyle } from "styled-components";
import AgGrid from "./agGrid";
import theme from './theme'

const sbTrackColor = theme.color.background;
const sbThumbColor = '#a2a5b9';

const GlobalStyle = createGlobalStyle`
:root {
  --display-font: Poppins, Helvetica, sans-serif;
  --background-color: #f2f3f8;
}

::selection {
    color: var(--white);
    background-color: var(--blue);
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
  ::-webkit-scrollbar-track-piece  {
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

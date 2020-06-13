import React from "react";
import { createGlobalStyle } from "styled-components";
import AgGrid from './agGrid';

const GlobalStyle = createGlobalStyle`
::selection {
    color: var(--white);
    background-color: var(--blue);
}
`;

export default function GlobalStyles(): JSX.Element {
  return (
    <>
     <GlobalStyle/>
     <AgGrid/>
    </>
  )
}

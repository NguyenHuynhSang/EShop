import React from "react";
import { Route, Switch } from "react-router-dom";
import shuffle from "lodash/shuffle";
import ErrorPage1 from "./ErrorPage1";
import ErrorPage2 from "./ErrorPage2";
import ErrorPage3 from "./ErrorPage3";
import ErrorPage4 from "./ErrorPage4";
import ErrorPage5 from "./ErrorPage5";
import ErrorPage6 from "./ErrorPage6";

const errorPages = [
  ErrorPage1,
  ErrorPage2,
  ErrorPage3,
  ErrorPage4,
  ErrorPage5,
  ErrorPage6,
];

function RandomErrorPage() {
  const ErrorPage = shuffle(errorPages)[0];
  return <ErrorPage />;
}

export default function ErrorsPage() {
  return (
    <Switch>
      <Route path="/error" component={RandomErrorPage} />
      <Route path="/error/error-v1" component={ErrorPage1} />
      <Route path="/error/error-v2" component={ErrorPage2} />
      <Route path="/error/error-v3" component={ErrorPage3} />
      <Route path="/error/error-v4" component={ErrorPage4} />
      <Route path="/error/error-v5" component={ErrorPage5} />
      <Route path="/error/error-v6" component={ErrorPage6} />
    </Switch>
  );
}

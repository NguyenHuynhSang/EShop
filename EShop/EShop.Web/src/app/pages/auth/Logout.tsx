import React, { Component } from "react";
import * as auth from "../../store/ducks/auth.duck";
import { connect, ConnectedProps } from "react-redux";
import { Redirect } from "react-router-dom";
import { LayoutSplashScreen } from "../../../_metronic";
import { RootState } from "../../store/rootDuck";

class Logout extends Component<PropsFromRedux> {
  componentDidMount() {
    this.props.logout();
  }

  render() {
    const { hasAuthToken } = this.props;

    return hasAuthToken ? <LayoutSplashScreen /> : <Redirect to="/auth" />;
  }
}

const connector = connect(
  (state: RootState) => ({ hasAuthToken: Boolean(state.auth.authToken) }),
  auth.actions
);

type PropsFromRedux = ConnectedProps<typeof connector>;

export default connector(Logout);

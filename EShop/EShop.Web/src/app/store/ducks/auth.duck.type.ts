export interface AuthState {
  user?: string;
  authToken?: string;
}

export enum ActionType {
  Login = "[Login] Action",
  Logout = "[Logout] Action",
  Register = "[Register] Action",
  UserRequested = "[Request User] Action",
  UserLoaded = "[Load User] Auth API",
}

interface LoginAction {
  type: typeof ActionType.Login;
  payload: {
    authToken: string;
  };
}

interface RegisterAction {
  type: typeof ActionType.Register;
  payload: {
    authToken: string;
  };
}

interface LogoutAction {
  type: typeof ActionType.Logout;
}

interface UserRequestedAction {
  type: typeof ActionType.UserRequested;
  payload: {
    user: string;
  };
}

interface UserLoadedAction {
  type: typeof ActionType.UserLoaded;
  payload: {
    user: string;
  };
}

export type AuthActionTypes =
  | LoginAction
  | RegisterAction
  | LogoutAction
  | UserRequestedAction
  | UserLoadedAction;

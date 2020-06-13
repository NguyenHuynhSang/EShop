export interface AuthState {
  user?: string;
  authToken?: string;
}

export enum AuthAction {
  Login = "[Login] Action",
  Logout = "[Logout] Action",
  Register = "[Register] Action",
  UserRequested = "[Request User] Action",
  UserLoaded = "[Load User] Auth API",
}

interface LoginAction {
  type: typeof AuthAction.Login;
  payload: {
    authToken: string;
  };
}

interface RegisterAction {
  type: typeof AuthAction.Register;
  payload: {
    authToken: string;
  };
}

interface LogoutAction {
  type: typeof AuthAction.Logout;
}

interface UserRequestedAction {
  type: typeof AuthAction.UserRequested;
  payload: {
    user: string;
  };
}

interface UserLoadedAction {
  type: typeof AuthAction.UserLoaded;
  payload: {
    user: string;
  };
}

export type AuthActionType =
  | LoginAction
  | RegisterAction
  | LogoutAction
  | UserRequestedAction
  | UserLoadedAction;

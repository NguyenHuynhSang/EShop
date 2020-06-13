import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import { put, takeLatest } from "redux-saga/effects";
import { getUserByToken } from "../../crud/auth.crud";
import * as routerHelpers from "../../router/RouterHelpers";
import { AuthAction, AuthActionType, AuthState } from "./auth.duck.d";

export { AuthAction };

const initialState: AuthState = {
  user: undefined,
  authToken: undefined
};

export const reducer = persistReducer<AuthState, AuthActionType>(
    { storage, key: "auth", whitelist: ["user", "authToken"] },
    (state = initialState, action) => {
      switch (action.type) {
        case AuthAction.Login: {
          const { authToken } = action.payload;
          return { authToken, user: undefined };
        }

        case AuthAction.Register: {
          const { authToken } = action.payload;

          return { authToken, user: undefined };
        }

        case AuthAction.Logout: {
          routerHelpers.forgotLastLocation();
          return initialState;
        }

        case AuthAction.UserLoaded: {
          const { user } = action.payload;

          return { ...state, user };
        }

        default:
          return state;
      }
    }
);

export const actions = {
  login: (authToken): AuthActionType => ({ type: AuthAction.Login, payload: { authToken } }),
  register: (authToken): AuthActionType => ({
    type: AuthAction.Register,
    payload: { authToken }
  }),
  logout: (): AuthActionType => ({ type: AuthAction.Logout }),
  requestUser: (user): AuthActionType => ({ type: AuthAction.UserRequested, payload: { user } }),
  fulfillUser: (user): AuthActionType => ({ type: AuthAction.UserLoaded, payload: { user } })
};

export function* saga() {
  yield takeLatest(AuthAction.Login, function* loginSaga() {
    yield put(actions.requestUser(null));
  });

  yield takeLatest(AuthAction.Register, function* registerSaga() {
    yield put(actions.requestUser(null));
  });

  yield takeLatest(AuthAction.UserRequested, function* userRequested() {
    const { data: user } = yield getUserByToken();

    yield put(actions.fulfillUser(user));
  });
}

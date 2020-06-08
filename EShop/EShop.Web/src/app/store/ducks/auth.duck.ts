import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import { put, takeLatest } from "redux-saga/effects";
import { getUserByToken } from "../../crud/auth.crud";
import * as routerHelpers from "../../router/RouterHelpers";
import { ActionType, AuthActionTypes, AuthState } from "./auth.duck.type";

export { ActionType };

const initialAuthState: AuthState = {
  user: undefined,
  authToken: undefined
};

export const reducer = persistReducer<AuthState, AuthActionTypes>(
    { storage, key: "demo1-auth", whitelist: ["user", "authToken"] },
    (state = initialAuthState, action) => {
      switch (action.type) {
        case ActionType.Login: {
          const { authToken } = action.payload;
          return { authToken, user: undefined };
        }

        case ActionType.Register: {
          const { authToken } = action.payload;

          return { authToken, user: undefined };
        }

        case ActionType.Logout: {
          routerHelpers.forgotLastLocation();
          return initialAuthState;
        }

        case ActionType.UserLoaded: {
          const { user } = action.payload;

          return { ...state, user };
        }

        default:
          return state;
      }
    }
);

export const actions = {
  login: (authToken): AuthActionTypes => ({ type: ActionType.Login, payload: { authToken } }),
  register: (authToken): AuthActionTypes => ({
    type: ActionType.Register,
    payload: { authToken }
  }),
  logout: (): AuthActionTypes => ({ type: ActionType.Logout }),
  requestUser: (user): AuthActionTypes => ({ type: ActionType.UserRequested, payload: { user } }),
  fulfillUser: (user): AuthActionTypes => ({ type: ActionType.UserLoaded, payload: { user } })
};

export function* saga() {
  yield takeLatest(ActionType.Login, function* loginSaga() {
    yield put(actions.requestUser(null));
  });

  yield takeLatest(ActionType.Register, function* registerSaga() {
    yield put(actions.requestUser(null));
  });

  yield takeLatest(ActionType.UserRequested, function* userRequested() {
    const { data: user } = yield getUserByToken();

    yield put(actions.fulfillUser(user));
  });
}

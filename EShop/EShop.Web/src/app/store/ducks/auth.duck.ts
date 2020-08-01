import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { persistReducer, PersistConfig } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { put, takeLatest } from 'typed-redux-saga';
import { getUserByToken } from '../../crud/auth.crud';
import * as routerHelpers from '../../router/RouterHelpers';

export interface AuthState {
  user?: string;
  authToken?: string;
}

const initialState: AuthState = {
  user: undefined,
  authToken: undefined,
};

const slice = createSlice({
  initialState,
  name: 'auth',
  reducers: {
    login(state, action: PayloadAction<string>) {
      state.authToken = action.payload;
      state.user = undefined;
    },
    register(state, action: PayloadAction<string>) {
      state.authToken = action.payload;
      state.user = undefined;
    },
    logout(state) {
      routerHelpers.forgotLastLocation();
      state = initialState;
    },
    userRequested() {},
    userLoaded(state, action: PayloadAction<string>) {
      state.user = action.payload;
    },
  },
});

const persistConfig: PersistConfig<AuthState> = {
  storage,
  key: 'auth',
  whitelist: ['user', 'authToken'],
};

export const { actions } = slice;
export const reducer = persistReducer(persistConfig, slice.reducer);

export function* saga() {
  yield* takeLatest(actions.login.type, function* loginSaga() {
    yield* put(actions.userRequested());
  });

  yield* takeLatest(actions.register.type, function* registerSaga() {
    yield* put(actions.userRequested());
  });

  yield* takeLatest(actions.userRequested.type, function* userRequested() {
    const { data: user } = yield getUserByToken();

    yield* put(actions.userLoaded(user));
  });
}

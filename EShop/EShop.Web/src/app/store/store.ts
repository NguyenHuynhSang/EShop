import { configureStore, getDefaultMiddleware } from '@reduxjs/toolkit';
import createSagaMiddleware from 'redux-saga';
import {
  persistStore,
  REGISTER,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
} from 'redux-persist';
import {
  useSelector as useReduxSelector,
  TypedUseSelectorHook,
  useDispatch,
  shallowEqual,
} from 'react-redux';
import { rootSaga, reducer } from './rootDuck';
import { Middleware } from 'redux';

let middlewares: Middleware[] = [];

if (process.env.NODE_ENV === `development`) {
  const { createLogger } = require(`redux-logger`);
  const logger = createLogger({
    collapsed: true,
    duration: true,
  });

  middlewares.push(logger);
}

const sagaMiddleware = createSagaMiddleware();
const rtkMiddlewares = getDefaultMiddleware({
  thunk: false,
  serializableCheck: {
    // FIX: serialization issue when using redux-toolkit with redux-persist
    // https://github.com/reduxjs/redux-toolkit/issues/121#issuecomment-611641781
    ignoredActions: [
      FLUSH,
      REHYDRATE,
      PAUSE,
      PERSIST,
      PURGE,
      REGISTER,
      'builder/setHtmlClassService',
      // payload content is not serializable, but the final state after filtering is
      'errors/setError',
    ],
    ignoredPaths: ['builder.htmlClassServiceObjects'],
  },
  immutableCheck: {
    ignoredPaths: ['builder.htmlClassServiceObjects'],
  },
});

middlewares = middlewares.concat(rtkMiddlewares);
middlewares.push(sagaMiddleware);

const store = configureStore({
  reducer,
  middleware: middlewares,
});

/**
 * @see https://github.com/rt2zz/redux-persist#persiststorestore-config-callback
 * @see https://github.com/rt2zz/redux-persist#persistor-object
 */
export const persistor = persistStore(store);

sagaMiddleware.run(rootSaga);

export type RootState = ReturnType<typeof store.getState>;

const useSelector: TypedUseSelectorHook<RootState> = useReduxSelector;

export { useSelector, useDispatch, shallowEqual };

export default store;

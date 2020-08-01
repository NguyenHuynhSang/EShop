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

const sagaMiddleware = createSagaMiddleware();
const middleware = [
  ...getDefaultMiddleware({
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
  }),
  sagaMiddleware,
];
const store = configureStore({ reducer, middleware });

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

import { persistReducer, PersistConfig } from "redux-persist";
import storage from "redux-persist/lib/storage";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

type i18nState = {
  lang: string;
};

const initialState: i18nState = {
  lang: "en",
};

const slice = createSlice({
  initialState,
  name: "i18n",
  reducers: {
    setLanguage(state, action: PayloadAction<string>) {
      state.lang = action.payload;
    },
  },
});

const persistConfig: PersistConfig<i18nState> = {
  storage,
  key: "i18n",
};

export const { actions } = slice;
export const reducer = persistReducer(persistConfig, slice.reducer);

import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";

enum ActionType {
  SetLanguage = "i18n/SET_LANGUAGE",
};

type i18nState = {
  lang: string;
}

const initialState: i18nState = {
  lang: "en"
};

export const reducer = persistReducer<i18nState, AuthActionTypes>(
  { storage, key: "i18n" },
  (state = initialState, action) => {
    switch (action.type) {
      case ActionType.SetLanguage:
        return { ...state, lang: action.payload.lang };

      default:
        return state;
    }
  }
);

interface SetLanguageAction {
  type: typeof ActionType.SetLanguage;
  payload: {
    lang: string;
  }
}

type AuthActionTypes = SetLanguageAction;

export const actions = {
  setLanguage: lang => ({ type: ActionType.SetLanguage, payload: { lang } })
};

import objectPath from "object-path";
import { persistReducer, PersistConfig } from "redux-persist";
import storage from "redux-persist/lib/storage";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

import LayoutConfig, { initLayoutConfig } from "../layout/LayoutConfig";
import MenuConfig from "../layout/MenuConfig";

export const selectors = {
  getClasses: (store, params) => {
    const { htmlClassServiceObjects } = store.builder;

    return htmlClassServiceObjects
      ? htmlClassServiceObjects.getClasses(params.path, params.toString)
      : "";
  },

  getConfig: (state, path) => {
    const { layoutConfig } = state.builder;

    if (path) {
      // if path is specified, get the value within object
      return objectPath.get(layoutConfig, path);
    }

    return "";
  },

  getLogo: ({ builder: { layoutConfig } }) => {
    const menuAsideLeftSkin = objectPath.get(layoutConfig, "brand.self.skin");
    // set brand logo
    const logoObject = objectPath.get(layoutConfig, "self.logo");
    let logo;
    if (typeof logoObject === "string") {
      logo = logoObject;
    }

    if (typeof logoObject === "object") {
      logo = objectPath.get(logoObject, menuAsideLeftSkin + "");
    }

    if (typeof logo === "undefined") {
      try {
        const logos = objectPath.get(layoutConfig, "self.logo");
        logo = logos[Object.keys(logos)[0]];
      } catch (e) {}
    }
    return logo;
  },

  getStickyLogo: (store) => {
    const { layoutConfig } = store.builder;
    let logo = objectPath.get(layoutConfig, "self.logo.sticky");
    if (typeof logo === "undefined") {
      logo = selectors.getLogo(store);
    }
    return logo + "";
  },
};

type IMenuConfig = typeof MenuConfig;
type ILayoutConfig = typeof initLayoutConfig;
export type BuilderState = {
  menuConfig: IMenuConfig;
  layoutConfig: ILayoutConfig;
  htmlClassServiceObjects?: object;
};

const initialState: BuilderState = {
  menuConfig: MenuConfig,
  layoutConfig: LayoutConfig,
  htmlClassServiceObjects: undefined,
};

const slice = createSlice({
  initialState,
  name: "builder",
  reducers: {
    setMenuConfig(state, action: PayloadAction<IMenuConfig>) {
      state.menuConfig = action.payload;
    },
    setLayoutConfigs(state, action: PayloadAction<ILayoutConfig>) {
      state.layoutConfig = action.payload;
    },
    setLayoutConfigsWithPageRefresh(
      state,
      action: PayloadAction<ILayoutConfig>
    ) {
      state.layoutConfig = action.payload;
    },
    setHtmlClassService(state, action) {
      state.htmlClassServiceObjects = action.payload;
    },
  },
});

const persistConfig: PersistConfig<BuilderState> = {
  storage,
  key: "build-demo1",
  blacklist: ["htmlClassServiceObjects"],
};

export const { actions } = slice;
export const reducer = persistReducer(persistConfig, slice.reducer);

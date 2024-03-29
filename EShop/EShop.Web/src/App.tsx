/**
 * Entry application component used to compose providers and render Routes.
 * */

import React from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { PersistGate } from 'redux-persist/integration/react';
import { LastLocationProvider } from 'react-router-last-location';
import { setAutoFreeze } from '@reduxjs/toolkit/node_modules/immer';
import { LicenseManager } from 'ag-grid-enterprise';
import { Routes } from './app/router/Routes';
import SnackbarProvider from './app/providers/SnackbarProvider';
import { I18nProvider, LayoutSplashScreen, ThemeProvider } from './_metronic';
import GlobalStyles from './app/styles/global';
import ErrorNotificationProvider from './app/providers/ErrorNotificationProvider';

if (process.env.REACT_APP_AG_GRID_LICENSE_KEY)
  LicenseManager.setLicenseKey(process.env.REACT_APP_AG_GRID_LICENSE_KEY);

// fucking ag-grid keep messing with my data by mutating it internally.
setAutoFreeze(false);

export default function App({ store, Layout, persistor, basename }) {
  return (
    /* Provide Redux store */
    <Provider store={store}>
      {/* Asynchronously persist redux stores and show `SplashScreen` while it's loading. */}
      <PersistGate persistor={persistor}>
        {/* Add high level `Suspense` in case if was not handled inside the React tree. */}
        <React.Suspense fallback={<LayoutSplashScreen />}>
          {/* Override `basename` (e.g: `homepage` in `package.json`) */}
          <BrowserRouter basename={basename}>
            {/*This library only returns the location that has been active before the recent location change in the current window lifetime.*/}
            <LastLocationProvider>
              {/* Provide Metronic theme overrides. */}
              <ThemeProvider>
                <GlobalStyles />
                {/* Provide `react-intl` context synchronized with Redux state.  */}
                <I18nProvider>
                  {/* My custom SnackbarProvider to enable you to create Snackbar imperatively. The different
                  between this and notistack is this can only show one Snackbar at a time according to Mui specs */}
                  <SnackbarProvider>
                    <ErrorNotificationProvider />
                    {/* Render routes with provided `Layout`. */}
                    <Routes Layout={Layout} />
                  </SnackbarProvider>
                </I18nProvider>
              </ThemeProvider>
            </LastLocationProvider>
          </BrowserRouter>
        </React.Suspense>
      </PersistGate>
    </Provider>
  );
}

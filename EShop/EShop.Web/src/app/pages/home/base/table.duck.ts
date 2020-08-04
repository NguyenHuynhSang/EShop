import { persistReducer, PersistConfig } from 'redux-persist';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import storage from 'redux-persist/lib/storage';

export enum ExportFormat {
  Csv = 'Csv',
  Json = 'Json',
  Excel = 'Excel',
}
type TableState = {
  _global: {
    exportDialogOpen: boolean;
    exportFormat: ExportFormat;
  };
};

const initialState: TableState = {
  _global: {
    exportDialogOpen: false,
    exportFormat: ExportFormat.Csv,
  },
};

const slice = createSlice({
  initialState,
  name: 'table',
  reducers: {
    setExportDialogOpen(state, action: PayloadAction<ExportFormat>) {
      state._global.exportDialogOpen = true;
      state._global.exportFormat = action.payload;
    },
    setExportDialogClose(state) {
      state._global.exportDialogOpen = false;
    },
    setExportFormat(state, action: PayloadAction<ExportFormat>) {
      state._global.exportFormat = action.payload;
    },
  },
});

const persistConfig: PersistConfig<TableState> = {
  storage,
  key: 'table',
  whitelist: [],
};

export const { actions } = slice;
export const reducer = persistReducer(persistConfig, slice.reducer);

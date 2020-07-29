import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AxiosError } from "axios";

export type AppError = Error & {
  id: number;
};

type ErrorState = {
  errors: AppError[];
};

const initialState: ErrorState = { errors: [] };
let errorId = 0;

// https://github.com/axios/axios/issues/2013#issuecomment-605863332
function isAxiosError<T>(error: AxiosError | any): error is AxiosError<T> {
  return error && error.isAxiosError;
}

const slice = createSlice({
  initialState,
  name: "errors",
  reducers: {
    setError(state, action: PayloadAction<any>) {
      const error = action.payload;
      if (isAxiosError(error)) {
        state.errors.push({
          id: ++errorId,
          name: error.name,
          message: error.message,
          stack: error.stack,
        });
      }
    },
    removeError(state, action: PayloadAction<number>) {
      const errorId = action.payload;
      state.errors = state.errors.filter(e => e.id !== errorId);
    },
  },
});

export const { actions, reducer } = slice;

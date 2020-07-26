import { select as _select } from "typed-redux-saga";
import { RootState } from "./store";

export { takeLatest, put, call } from "typed-redux-saga";

// cannot infer even with typed-redux-saga for some reasons. This fixes it
export function* select<T>(fn: (state: RootState) => T) {
  return yield* _select(fn);
}

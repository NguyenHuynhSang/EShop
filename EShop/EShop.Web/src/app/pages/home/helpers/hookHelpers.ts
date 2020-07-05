import { useEffect, EffectCallback, useReducer } from "react";

export const useOnMount = (cb: EffectCallback) => useEffect(cb, []);

export const useForceUpdate = () => {
  const [, forceUpdate] = useReducer((x) => x + 1, 0);
  return forceUpdate;
};

import { useEffect, EffectCallback } from "react";

export const useEffectOnce = (cb: EffectCallback) => useEffect(cb, []);
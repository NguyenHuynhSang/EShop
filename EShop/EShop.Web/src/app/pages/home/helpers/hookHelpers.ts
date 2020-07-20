import { useEffect, EffectCallback, useReducer, useRef, useState } from "react";

export { useSnackbar } from "../../../providers/SnackbarProvider";

export const useOnMount = (cb: EffectCallback) => useEffect(cb, []);

export const useForceUpdate = () => {
  const [, forceUpdate] = useReducer((x) => x + 1, 0);
  return forceUpdate;
};

type EventType = keyof WindowEventMap;
type EventListener = (e: WindowEventMap[EventType]) => any;

// thank you https://usehooks.com/useEventListener/ very cool
export function useEventListener<K extends keyof WindowEventMap>(
  eventName: K,
  handler: EventListener,
  element = window
) {
  // Create a ref that stores handler
  const savedHandler = useRef<EventListener>();

  // Update ref.current value if handler changes.
  // This allows our effect below to always get latest handler ...
  // ... without us needing to pass it in effect deps array ...
  // ... and potentially cause effect to re-run every render.
  useEffect(() => {
    savedHandler.current = handler;
  }, [handler]);

  useEffect(
    () => {
      // Make sure element supports addEventListener
      // On
      const isSupported = element && element.addEventListener;
      if (!isSupported) return;

      // Create event listener that calls handler function stored in ref
      const eventListener: EventListener = (event) =>
        savedHandler.current && savedHandler.current(event);

      // Add event listener
      element.addEventListener(eventName, eventListener);

      // Remove event listener on cleanup
      return () => element.removeEventListener(eventName, eventListener);
    },

    [eventName, element] // Re-run if eventName or element changes
  );
}

export const useDialog = () => {
  const [open, setOpen] = useState(false);

  return {
    open,
    handleOpen: () => setOpen(true),
    handleClose: () => setOpen(false),
  };
};

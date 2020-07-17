import { ColumnApi, GridApi, GridReadyEvent } from "ag-grid-community";
import { useCallback, useRef } from "react";
import { useEventListener, useOnMount } from "./hookHelpers";

type GridReadyCb = (api: AgGridApi) => void;
export type AgGridApi = {
  grid?: GridApi;
  column?: ColumnApi;
};
type GridReadyFunc = (event: GridReadyEvent) => void;
type AutoSizeFunc = (columns?: string[]) => void;

let api: AgGridApi = {
  column: undefined,
  grid: undefined,
};

export function useAgGrid(
  onGridReadyCb?: GridReadyCb
): [AgGridApi, GridReadyFunc, AutoSizeFunc] {
  const onGridReady = useCallback(
    (params: GridReadyEvent) => {
      api.grid = params.api;
      api.column = params.columnApi;

      if (onGridReadyCb) onGridReadyCb(api);
    },
    [onGridReadyCb]
  );
  const autoSizeColumnsCb = useCallback<AutoSizeFunc>(
    (columns) => autoSizeColumns(api.column, columns),
    []
  );

  return [api, onGridReady, autoSizeColumnsCb];
}

export function useGridApi(): AgGridApi {
  return api;
}

export function autoSizeColumns(gridColumnApi?: ColumnApi, columns?: string[]) {
  const allColumnIds =
    columns || gridColumnApi?.getAllColumns().map((c) => c.getId()) || [];
  gridColumnApi?.autoSizeColumns(allColumnIds, false);
  // when using custom header component, autosize does not work on the first try
  // especially when there too many columns to fit on one screen
  setTimeout(() => {
    gridColumnApi?.autoSizeColumns(allColumnIds, false);
  });
}

export const useStickyHeader = () => {
  const headerElementRef = useRef<HTMLDivElement>();
  const bodyElementRef = useRef<HTMLDivElement>();
  const stickyRef = useRef(false);
  const originalStyles = useRef({ position: "", top: "", zIndex: "" });

  useOnMount(() => {
    headerElementRef.current = document.querySelector(
      '[ref="headerRoot"]'
    ) as HTMLDivElement;
    bodyElementRef.current = document.querySelector(
      '[ref="eBodyViewport"]'
    ) as HTMLDivElement;

    const header = headerElementRef.current;

    originalStyles.current.position = header.style.position;
    originalStyles.current.top = header.style.top;
    originalStyles.current.zIndex = header.style.zIndex;
  });

  const onScroll = useCallback(() => {
    const header = headerElementRef.current;
    const body = bodyElementRef.current;
    if (!header || !body) return;

    let shouldStick = false;
    let shouldUnstick = false;

    if (!stickyRef.current) {
      shouldStick = header.getBoundingClientRect().top <= 0;
      if (shouldStick) stickyRef.current = true;
    } else {
      shouldUnstick =
        body.getBoundingClientRect().top -
          header.getBoundingClientRect().height >
        0;
      if (shouldUnstick) stickyRef.current = false;
    }

    if (shouldStick) {
      header.style.position = "fixed";
      header.style.top = "0";
      header.style.zIndex = "2";
    }
    if (shouldUnstick) {
      const original = originalStyles.current;
      header.style.position = original.position;
      header.style.top = original.top;
      header.style.zIndex = original.zIndex;
    }
  }, []);

  useEventListener("scroll", onScroll);

  return stickyRef;
};

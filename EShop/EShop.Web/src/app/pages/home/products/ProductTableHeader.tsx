import React from "react";
import { IHeaderParams, Column, ColumnApi } from "ag-grid-community";
import {
  OverlayTrigger,
  Popover,
  ButtonGroup,
  Button,
  ListGroup,
} from "react-bootstrap";
import isArray from "lodash/isArray";
import isString from "lodash/isString";
import { IconButton } from "@material-ui/core";
import { useSelector, useDispatch, shallowEqual } from "../../../store/store";
import MenuIcon from "@material-ui/icons/Menu";
import { actions } from "./product.duck";
import pressKey, { VKey } from "../helpers/pressKey";
import ThemeProvider from "../../../../_metronic/materialUIThemeProvider/ThemeProvider";
import styled, { important } from "../../../styles/styled";

type HeaderWrapperProps = {
  isNumericColumn: boolean;
};

const HeaderWrapper = styled<HeaderWrapperProps>("div")({
  width: "100%",
  height: "100%",
  display: "flex",
  alignItems: "center",
  justifyContent: (props) => (props.isNumericColumn ? "end" : "start"),
});

enum SortMode {
  None = "none",
  Ascending = "asc",
  Descending = "desc",
}
type SortFunction = () => void;

const sortModes = [SortMode.None, SortMode.Ascending, SortMode.Descending];

const useSort = (
  enableSorting: boolean,
  sortBy?: string
): [SortMode, SortFunction] => {
  const dispatch = useDispatch();
  const sortIndexRef = React.useRef<number>(0);
  const [sortMode, setSortMode] = React.useState(
    sortModes[sortIndexRef.current]
  );

  const cycleSort = () => {
    if (!enableSorting) return;

    sortIndexRef.current =
      sortIndexRef.current >= sortModes.length - 1
        ? 0
        : sortIndexRef.current + 1;

    const nextSortMode = sortModes[sortIndexRef.current];
    setSortMode(nextSortMode);
    dispatch(actions.getAllRequest({ sort: nextSortMode, sortBy }));
  };

  return [sortMode, cycleSort];
};

const useColumnMenu = (column: Column, columnApi: ColumnApi) => {
  const colDef = column.getColDef();
  const setPin = (pinned: "left" | "right" | null) => {
    columnApi.setColumnPinned(column.getColId(), pinned || "");
  };

  if (
    (isArray(colDef.type) && colDef.type.find((t) => t === "currency")) ||
    (isString(colDef.type) && colDef.type === "currency")
  ) {
    return (
      // prevent this column from being sorted if clicking this button
      <div onClick={(e) => e.stopPropagation()}>
        <ThemeProvider>
          <OverlayTrigger
            trigger="click"
            rootClose
            placement="top"
            overlay={
              <Popover id="popover-positioned-top">
                <ListGroup variant="flush">
                  <ListGroup.Item>
                    <ButtonGroup>
                      <Button
                        variant="secondary"
                        onClick={() => setPin("left")}
                      >
                        Pin Left
                      </Button>
                      <Button
                        variant="secondary"
                        onClick={() => setPin(null)}
                        autoFocus
                      >
                        No Pin
                      </Button>
                      <Button
                        variant="secondary"
                        onClick={() => setPin("right")}
                      >
                        Pin Right
                      </Button>
                    </ButtonGroup>
                  </ListGroup.Item>
                  <ListGroup.Item
                    action
                    onClick={() => {
                      columnApi.autoSizeColumn(column.getColId());
                      pressKey("keyup", VKey.Escape);
                    }}
                  >
                    Autosize This Column
                  </ListGroup.Item>
                </ListGroup>
              </Popover>
            }
          >
            <IconButton size="small" color="primary">
              <MenuIcon fontSize="inherit" />
            </IconButton>
          </OverlayTrigger>
        </ThemeProvider>
      </div>
    );
  }
  return null;
};

const getSortIndicator = (sortMode: SortMode) => {
  if (sortMode === SortMode.None) return null;

  const arrowClassModifier = sortMode === SortMode.Ascending ? "up" : "down";
  return (
    <span>
      &nbsp;
      <i className={"fa fa-long-arrow-alt-" + arrowClassModifier} />
    </span>
  );
};

export default function ProductTableHeader(props: IHeaderParams) {
  const { displayName, column, enableSorting, columnApi } = props;
  const colDef = column.getColDef();
  const isNumericColumn =
    colDef.type !== undefined && colDef.type?.indexOf("numericColumn") !== -1;
  const [sortMode, cycleSort] = useSort(enableSorting, colDef.field);

  return (
    <HeaderWrapper
      isNumericColumn={isNumericColumn}
      role="button"
      onClick={() => cycleSort()}
    >
      {displayName}
      {useColumnMenu(column, columnApi)}
      {enableSorting && getSortIndicator(sortMode)}
    </HeaderWrapper>
  );
}

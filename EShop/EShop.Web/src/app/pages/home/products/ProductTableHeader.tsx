import React from "react";
import { IHeaderParams, Column, ColumnApi } from "ag-grid-community";
import {
  OverlayTrigger,
  Popover,
  ButtonGroup,
  Button,
  ListGroup,
  ListGroupItemProps,
} from "react-bootstrap";
import isArray from "lodash/isArray";
import isString from "lodash/isString";
import { IconButton } from "@material-ui/core";
import ThemeProvider from "../../../../_metronic/materialUIThemeProvider/ThemeProvider";
import MenuIcon from "@material-ui/icons/Menu";
import { useDispatch, useSelector } from "../../../store/store";
import { actions } from "./product.duck";
import pressKey, { VKey } from "../helpers/pressKey";
import styled from "../../../styles/styled";
import { SortMode, Pinned } from "./product.duck.d";

type HeaderWrapperProps = {
  isNumericColumn: boolean;
};

const HeaderWrapper = styled<HeaderWrapperProps>((props) => {
  // TODO: need a better way, props filter is hard to read
  // https://github.com/styled-components/styled-components/pull/3006
  const { isNumericColumn, ...rest } = props;
  return <div {...rest} />;
})({
  width: "100%",
  height: "100%",
  display: "flex",
  alignItems: "center",
  justifyContent: (props) => (props.isNumericColumn ? "end" : "start"),

  "& .fas.fa-thumbtack": {
    marginLeft: "auto",
  },
});

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
const usePinStatus = (column?: string) =>
  useSelector(
    (state) =>
      state.products.columnInfos.find((c) => c.field === column)?.pinned
  );

export function hasType(column: Column, type: string) {
  const colDef = column.getColDef();
  return (
    (isArray(colDef.type) && colDef.type.indexOf(type) !== -1) ||
    (isString(colDef.type) && colDef.type === type)
  );
}

type ActionButtonProps = ListGroupItemProps & {
  onClick: () => void;
  children: React.ReactNode;
};
function ActionButton(props: ActionButtonProps) {
  const { onClick, ...rest } = props;
  return (
    <ListGroup.Item
      action
      onClick={() => {
        onClick();
        pressKey("keyup", VKey.Escape);
      }}
      {...rest}
    />
  );
}

type ColumnMenuProps = {
  column: Column;
  columnApi: ColumnApi;
};
function ColumnMenu(props: ColumnMenuProps) {
  const { column, columnApi } = props;
  const field = column.getColDef().field!;
  const dispatch = useDispatch();
  const pinned = usePinStatus(field);
  const setPin = (pinned: Pinned) => {
    columnApi.setColumnPinned(column.getColId(), pinned ?? "");

    if (pinned === undefined) {
      // onColumnPinned from ag-grid doesn't fire when unpinning column
      dispatch(actions.setPinned({ column: field, pinned }));
    }
  };
  const autoSizeThisColumn = () => {
    columnApi.autoSizeColumn(column.getColId(), false);
  };
  const autoSizeAll = () => {
    const allColumnIds = columnApi.getAllColumns().map((c) => c.getId());
    columnApi.autoSizeColumns(allColumnIds, false);
  };
  const hideThisColumn = () => {
    columnApi.setColumnVisible(column.getColId(), false);
    dispatch(actions.setColumnVisible({ column: field, visible: false }));
  };

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
                      autoFocus={pinned === "left"}
                    >
                      Pin Left
                    </Button>
                    <Button
                      variant="secondary"
                      onClick={() => setPin(undefined)}
                      autoFocus={pinned === undefined}
                    >
                      No Pin
                    </Button>
                    <Button
                      variant="secondary"
                      onClick={() => setPin("right")}
                      autoFocus={pinned === "right"}
                    >
                      Pin Right
                    </Button>
                  </ButtonGroup>
                </ListGroup.Item>
                <ActionButton onClick={autoSizeThisColumn}>
                  Autosize this column
                </ActionButton>
                <ActionButton onClick={autoSizeAll}>
                  Autosize all columns
                </ActionButton>
                <ActionButton onClick={hideThisColumn}>
                  Hide this column
                </ActionButton>
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

const getColumnMenu = (column: Column, columnApi: ColumnApi) => {
  const colDef = column.getColDef();

  // if (hasType(column, "currency")) {
  if (colDef.field !== "id") {
    return <ColumnMenu column={column} columnApi={columnApi} />;
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
  const isNumericColumn = colDef.cellClass === "ag-right-aligned-cell";
  const [sortMode, cycleSort] = useSort(enableSorting, colDef.field);
  const pinned = usePinStatus(colDef.field);

  return (
    <HeaderWrapper
      isNumericColumn={isNumericColumn}
      role="button"
      onClick={cycleSort}
    >
      {displayName}
      {getColumnMenu(column, columnApi)}
      {enableSorting && getSortIndicator(sortMode)}
      {pinned && <i className="fas fa-thumbtack fa-sm" />}
    </HeaderWrapper>
  );
}

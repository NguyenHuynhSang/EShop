import React from "react";
import { IHeaderParams, Column, ColumnApi } from "ag-grid-community";
import OverlayTrigger from "react-bootstrap/OverlayTrigger";
import Popover from "react-bootstrap/Popover";
import ButtonGroup from "react-bootstrap/ButtonGroup";
import Button from "react-bootstrap/Button";
import ListGroup from "react-bootstrap/ListGroup";
import { ListGroupItemProps } from "react-bootstrap/ListGroupItem";
import isArray from "lodash/isArray";
import isString from "lodash/isString";
import IconButton from "@material-ui/core/IconButton";
import MenuIcon from "@material-ui/icons/Menu";
import ThemeProvider from "../../../../_metronic/materialUIThemeProvider/ThemeProvider";
import { useDispatch, useSelector } from "../../../store/store";
import { actions, SortMode, Pinned, WeightUnit } from "./product.duck";
import pressKey, { VKey } from "../helpers/pressKey";
import { theme, makeStyles } from "../../../styles";

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

const useMenuStyles = makeStyles({
  buttonGroup: {
    display: "flex",
    "& > *": {
      flex: 1,
    },
  },
});

type ColumnMenuProps = {
  className?: string;
  column: Column;
  columnApi: ColumnApi;
};
function ColumnMenu(props: ColumnMenuProps) {
  const { className, column, columnApi } = props;
  const field = column.getColDef().field!;
  const dispatch = useDispatch();
  const pinned = usePinStatus(field);
  const weightUnit = useSelector((state) => state.products.weightUnit);
  const isWeight = hasType(column, "weight");
  const styles = useMenuStyles();
  const setPin = (pinned: Pinned) => () => {
    columnApi.setColumnPinned(column.getColId(), pinned ?? "");

    if (pinned === undefined) {
      // onColumnPinned from ag-grid doesn't fire when unpinning column
      dispatch(actions.setPinned({ column: field, pinned }));
    }
  };
  const setWeight = (w: string) => () => {
    dispatch(actions.setWeightUnit(WeightUnit[w]));
    pressKey("keyup", VKey.Escape);
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
    <div className={className} onClick={(e) => e.stopPropagation()}>
      <ThemeProvider>
        <OverlayTrigger
          trigger="click"
          rootClose
          flip
          overlay={
            <Popover id={field + "-column-menu"}>
              <ListGroup variant="flush">
                <ListGroup.Item>
                  <ButtonGroup className={styles.buttonGroup}>
                    <Button
                      variant="secondary"
                      onClick={setPin("left")}
                      disabled={pinned === "left"}
                    >
                      Pin Left
                    </Button>
                    <Button
                      variant="secondary"
                      onClick={setPin(undefined)}
                      disabled={pinned === undefined}
                    >
                      No Pin
                    </Button>
                    <Button
                      variant="secondary"
                      onClick={setPin("right")}
                      disabled={pinned === "right"}
                    >
                      Pin Right
                    </Button>
                  </ButtonGroup>
                </ListGroup.Item>
                {isWeight && (
                  <ListGroup.Item>
                    <ButtonGroup className={styles.buttonGroup}>
                      {Object.keys(WeightUnit).map((w) => (
                        <Button
                          key={w}
                          variant="secondary"
                          onClick={setWeight(w)}
                          disabled={weightUnit === WeightUnit[w]}
                        >
                          {w}
                        </Button>
                      ))}
                    </ButtonGroup>
                  </ListGroup.Item>
                )}
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

  if (colDef.field === "id") {
    return null;
  }
  return (
    <ColumnMenu className="columnMenu" column={column} columnApi={columnApi} />
  );
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

type HeaderWrapperProps = {
  isNumericColumn: boolean;
};
const useStyles = makeStyles<HeaderWrapperProps>({
  root: {
    width: "100%",
    height: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: (props) => (props.isNumericColumn ? "end" : "start"),

    "& .fas.fa-thumbtack": {
      marginLeft: theme.spacing.sm,
    },
    "& .columnMenu": {
      marginRight: "auto",
    },
  },
});

export default function ProductTableHeader(props: IHeaderParams) {
  const { displayName, column, enableSorting, columnApi } = props;
  const colDef = column.getColDef();
  const isNumericColumn = colDef.cellClass === "ag-right-aligned-cell";
  const [sortMode, cycleSort] = useSort(enableSorting, colDef.field);
  const pinned = usePinStatus(colDef.field);
  const style = useStyles({ isNumericColumn });

  return (
    <div className={style.root} role="button" onClick={cycleSort}>
      {displayName}
      {getColumnMenu(column, columnApi)}
      {enableSorting && getSortIndicator(sortMode)}
      {pinned && <i className="fas fa-thumbtack fa-sm" />}
    </div>
  );
}

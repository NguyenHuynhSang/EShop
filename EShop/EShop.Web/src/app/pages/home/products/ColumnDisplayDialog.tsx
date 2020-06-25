import React from "react";
import {
  Button,
  Checkbox,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControlLabel,
  FormGroup,
} from "@material-ui/core";
import { ColumnInfo } from "./product.duck.d";
import { useDispatch } from "../../../store/store";
import { actions } from "./product.duck";

type ColumnDisplayDialogProps = {
  open: boolean;
  handleClose: () => void;
  initialValue: ColumnInfo[];
};

export default function ColumnDisplayDialog(props: ColumnDisplayDialogProps) {
  const { open, handleClose, initialValue: columnInfos } = props;
  const dispatch = useDispatch();
  const [columnDisplay, setColumnDisplay] = React.useState(columnInfos);

  const onChangeCheckbox = (name) => (e) => {
    const colIndex = columnDisplay.findIndex((c) => c.columnName === name);
    columnDisplay[colIndex].visible = e.target.checked;
    setColumnDisplay([...columnDisplay]);
  };

  const onSave = () => {
    dispatch(actions.setColumnDisplay(columnDisplay));
    handleClose();
  };

  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Chọn những cột cần hiển thị</DialogTitle>
      <DialogContent>
        <FormGroup>
          {columnDisplay.map((c) => (
            <FormControlLabel
              key={c.columnName}
              disabled={c.alwaysVisible || false}
              control={
                <Checkbox
                  checked={c.visible}
                  onChange={onChangeCheckbox(c.columnName)}
                />
              }
              label={c.columnName}
            />
          ))}
        </FormGroup>
      </DialogContent>
      <DialogActions>
        <Button onClick={onSave} color="primary">
          Lưu thay đổi
        </Button>
      </DialogActions>
    </Dialog>
  );
}

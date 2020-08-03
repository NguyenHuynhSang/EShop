import React from 'react';
import Button from '@material-ui/core/Button';
import Checkbox from '@material-ui/core/Checkbox';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import FormGroup from '@material-ui/core/FormGroup';
import { useDispatch, useSelector, shallowEqual } from '../../../store/store';
import { actions, ColumnSettings } from './product.duck';
import { colDefs } from './useColumnDefs';
import immer from 'immer';
import { useGridApi } from '../helpers/agGridHelpers';

type ColumnDisplayDialogProps = {
  name: string;
  open: boolean;
  handleClose: () => void;
};

export default function ColumnDisplayDialog(props: ColumnDisplayDialogProps) {
  const { name, open, handleClose } = props;
  const api = useGridApi(name);
  const dispatch = useDispatch();
  const columnSettings = useSelector(
    state => state.products.columnSettings,
    shallowEqual
  );
  const [draft, setDraft] = React.useState<ColumnSettings[]>([]);

  React.useEffect(() => {
    if (open) {
      // reset unsaved changes when opening again
      setDraft(columnSettings);
    }
  }, [open, columnSettings]);

  const onChangeCheckbox = (name: string) => (_: any, checked: boolean) => {
    const hide = !checked;

    setDraft(
      immer(draft, _draft => {
        const column = _draft.find(c => c.field === name);

        if (column) {
          column.hide = hide;
          if (hide) column.pinned = undefined;
        }
      })
    );
  };

  const onSave = () => {
    dispatch(actions.setColumnDisplay(draft));
    // change colDef externally, need to notify ag-grid to update the changes
    api.column?.setColumnState(draft.map(c => ({ ...c, colId: c.field })));
    handleClose();
  };

  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Chọn những cột cần hiển thị</DialogTitle>
      <DialogContent>
        <FormGroup>
          {draft.map(c => {
            return (
              <FormControlLabel
                key={c.field}
                disabled={c.alwaysVisible || false}
                control={
                  <Checkbox
                    checked={!c.hide}
                    onChange={onChangeCheckbox(c.field)}
                  />
                }
                label={colDefs[c.field].headerName}
              />
            );
          })}
        </FormGroup>
      </DialogContent>
      <DialogActions>
        <Button onClick={onSave} color='primary'>
          Lưu thay đổi
        </Button>
      </DialogActions>
    </Dialog>
  );
}

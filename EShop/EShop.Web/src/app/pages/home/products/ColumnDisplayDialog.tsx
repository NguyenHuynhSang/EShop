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
import { actions, ColumnInfo } from './product.duck';
import { colDefs } from './useColumnDefs';
import immer from 'immer';

type ColumnDisplayDialogProps = {
  open: boolean;
  handleClose: () => void;
};

export default function ColumnDisplayDialog(props: ColumnDisplayDialogProps) {
  const { open, handleClose } = props;
  const dispatch = useDispatch();
  const columnInfos = useSelector(
    state => state.products.columnInfos,
    shallowEqual
  );
  const [draftColumnInfo, setDraftColumnInfo] = React.useState<ColumnInfo[]>(
    []
  );

  React.useEffect(() => {
    if (open) {
      // reset unsaved changes when opening again
      setDraftColumnInfo(columnInfos);
    }
  }, [open, columnInfos]);

  const onChangeCheckbox = (name: string) => (_: any, checked: boolean) => {
    const hide = !checked;

    setDraftColumnInfo(
      immer(draftColumnInfo, draft => {
        const column = draft.find(c => c.field === name);

        if (column) {
          column.hide = hide;
          if (hide) column.pinned = undefined;
        }
      })
    );
  };

  const onSave = () => {
    dispatch(actions.setColumnDisplay(draftColumnInfo));
    handleClose();
  };

  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Chọn những cột cần hiển thị</DialogTitle>
      <DialogContent>
        <FormGroup>
          {draftColumnInfo.map(c => {
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

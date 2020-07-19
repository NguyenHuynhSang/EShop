import React from "react";
import Button from "@material-ui/core/Button";
import IconButton from "@material-ui/core/IconButton";
import GetAppIcon from "@material-ui/icons/GetApp";
import VisibilityIcon from "@material-ui/icons/Visibility";
import "ag-grid-enterprise";
import { SelectButton } from "../../../widgets/Common";
import { actions, ExportFormat } from "../base/table.duck";
import { makeStyles, theme } from "../../../styles";
import { useSelector, useDispatch } from "../../../store/store";
import { useExportDownload } from "../helpers/agGridHelpers";

const useExportOptionStyles = makeStyles((theme) => ({
  root: {
    display: "flex",
  },
  text: {
    width: "100%",
    padding: "8px 15px",
    transition: "color .25s ease",
    // left-align text
    display: "flex",
    justifyContent: "flex-start",
    "&:hover": {
      backgroundColor: "transparent",
      color: theme.palette.primary.main,
    },
  },
  previewButton: {
    padding: 0,
    marginLeft: "auto",
    width: "35px",
  },
}));
type ExportOptionProps = { children: React.ReactNode; format: ExportFormat };
function ExportOption({ children, format }: ExportOptionProps) {
  const download = useExportDownload(format);
  const dispatch = useDispatch();
  const styles = useExportOptionStyles();

  return (
    <div className={styles.root}>
      <Button
        className={styles.text}
        disableRipple
        disableFocusRipple
        onClick={download}
      >
        {children}
      </Button>
      <IconButton
        className={styles.previewButton}
        onClick={() => dispatch(actions.setExportDialogOpen(format))}
      >
        <VisibilityIcon htmlColor={theme.color.blue} />
      </IconButton>
    </div>
  );
}

const useExportButtonStyles = makeStyles({
  root: {
    "& [class$='-option']": {
      padding: 0,
      // remove selection color of the first option when opening
      backgroundColor: "transparent",
    },
  },
});
export default function ExportButton({ iconSize }) {
  const styles = useExportButtonStyles();
  const selectedRows = useSelector((state) => state.products.rowsSelected);
  const selectedRowsText = selectedRows > 0 ? "(" + selectedRows + ")" : "";

  return (
    <SelectButton
      className={styles.root}
      startIcon={<GetAppIcon style={{ fontSize: iconSize }} />}
      variant="contained"
      color="primary"
      size="large"
      options={Object.keys(ExportFormat).map((f) => ({
        label: <ExportOption format={ExportFormat[f]}>{f}</ExportOption>,
        value: f,
        // Disable react-select Option's onClick handler, because we already have one in ExportButton.
        // This will prevent the error: <button> cannot appear as a descendant of <button>
        isDisabled: true,
      }))}
    >
      Export {selectedRowsText}
    </SelectButton>
  );
}

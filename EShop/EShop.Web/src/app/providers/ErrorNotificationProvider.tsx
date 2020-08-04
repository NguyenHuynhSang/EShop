import React from 'react';
import Button from '@material-ui/core/Button';
import { useSnackbar } from './SnackbarProvider';
import { shallowEqual, useDispatch, useSelector } from '../store/store';
import { AppError, actions } from '../pages/home/base/errors/error.duck';

function useThrow(error?: AppError) {
  const dispatch = useDispatch();
  const [createSnackbar, closeSnackbar] = useSnackbar();

  if (error) {
    setTimeout(() => {
      const { id, message = 'Oops! Something went wrong.' } = error;
      const key = id.toString();

      createSnackbar(message, {
        key,
        variant: 'error',
        autoHideDuration: 5000,
        onClose: () => {
          dispatch(actions.removeError(id));
        },
        // TODO: log to server
        // action: (
        //   <Button
        //     onClick={async () => {
        //       createSnackbar("Error report has been sent", {
        //         variant: "success",
        //       });
        //       closeSnackbar(key);
        //       dispatch(actions.removeError(id));
        //     }}
        //   >
        //     Report
        //   </Button>
        // ),
      });
    });
  }
}

const ErrorNotificationProvider = () => {
  const errors = useSelector(state => state.errors.errors, shallowEqual);
  const displayedError = errors.length > 0 ? errors[0] : undefined;
  useThrow(displayedError);

  return null;
};

export default ErrorNotificationProvider;

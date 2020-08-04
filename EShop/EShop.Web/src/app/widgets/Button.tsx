import React from 'react';
import BsButton from 'react-bootstrap/Button';
import { makeStyles, important } from '../styles';

const useStyles = makeStyles({
  root: {
    height: important('40px'),
    whiteSpace: 'nowrap',
  },
});
export const Button = ({ children, className, ...rest }) => {
  const styles = useStyles();
  return (
    <BsButton {...rest} className={styles.root}>
      {children}
    </BsButton>
  );
};

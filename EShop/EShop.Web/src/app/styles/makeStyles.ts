import muiMakeStyles from '@material-ui/styles/makeStyles';
import { Styles, ClassNameMap } from '@material-ui/styles';
import { Theme as DefaultTheme } from '@material-ui/core/styles';

// switch Props generic type to first because I use it a lot more than the others
export default function makeStyles<
  Props extends {} = any,
  Theme = DefaultTheme,
  ClassKey extends string = string
>(
  style: Styles<Theme, Props, ClassKey>
): (props?: Props) => ClassNameMap<ClassKey> {
  return muiMakeStyles(style) as any;
}

export function important<T extends unknown>(cssValue: T) {
  return (cssValue + '!important') as T;
}

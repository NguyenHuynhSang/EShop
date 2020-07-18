import { makeStyles as muiMakeStyles } from "@material-ui/core";
import { Styles, ClassNameMap } from "@material-ui/styles";
import { Theme as DefaultTheme } from "@material-ui/core/styles/createMuiTheme";

// switch Props generic type to first because I use it a lot more than the others
export default function makeStyles<
  Props extends {} = {},
  Theme = DefaultTheme,
  ClassKey extends string = string
>(
  style: Styles<Theme, Props, ClassKey>
): (props?: Props) => ClassNameMap<ClassKey> {
  return muiMakeStyles(style) as any;
}

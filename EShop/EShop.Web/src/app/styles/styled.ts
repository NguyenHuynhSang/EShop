import { styled as muiStyled, Theme as DefaultTheme } from "@material-ui/core";
import { CreateCSSProperties } from "@material-ui/styles";
import theme from "./theme";

export type CssInJs<Props extends {} = {}, Theme = DefaultTheme> =
  | CreateCSSProperties<Props>
  | ((props: { theme: Theme } & Props) => CreateCSSProperties<Props>);

// make material-ui styled api easier to use
//
// before:
// styled(Component)<any, ComponentProps>({
//   display: "block",
// });
//
// after: (similar to styled-component)
// styled<ComponentProps>(Component)({
//   display: "block",
// });
function styled<Props extends {}, Theme = DefaultTheme>(
  Component: React.ElementType
) {
  return (css: CssInJs<Props, Theme>) =>
    muiStyled(Component)<Theme, Props>(css);
}

export function important<T extends unknown>(cssValue: T) {
  return (cssValue + "!important") as T;
}

export { theme };

export default styled;

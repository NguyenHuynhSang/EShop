import React from "react";
// Have to import like this or ThemeProvider only works in some cases instead of
// import { createMuiTheme } from "@material-ui/core";
// Otherwise, in some cases the theme is not applied to components
// https://stackoverflow.com/a/58467651/9449426
// eslint-disable-next-line no-restricted-imports
import { createMuiTheme } from "@material-ui/core/styles";
import { ThemeProvider as MuiThemeProvider } from "@material-ui/styles";
import themeConfig from "../../app/styles/theme";

const theme = createMuiTheme(
  /**
   * @see https://material-ui.com/customization/themes/#theme-configuration-variables
   */
  {
    // direction: "rtl",
    typography: {
      fontFamily: ["Poppins, Helvetica, sans-serif"].join(","),
    },

    palette: {
      contrastThreshold: 2,
      primary: {
        light: themeConfig.color.primaryLight,
        main: themeConfig.color.primary,
        // dark: will be calculated from palette.primary.main,
        // contrastText: will be calculated to contrast with palette.primary.main
      },
      secondary: {
        light: themeConfig.color.secondaryLight,
        main: themeConfig.color.secondary,
        // dark: will be calculated from palette.primary.main,
        contrastText: "#ffffff",
      },
      error: {
        // light: will be calculated from palette.primary.main,
        main: "#fd397a",
        // dark: will be calculated from palette.primary.main,
        // contrastText: will be calculated to contrast with palette.primary.main
      },
    },

    /**
     * @see https://material-ui.com/customization/globals/#default-props
     */
    props: {
      // Set default elevation to 1 for popovers.
      MuiPopover: {
        elevation: 1,
      },
      MuiButton: {
        disableElevation: true,
      },
    },

    overrides: {
      MuiButton: {
        root: {
          // height: 35,
          color: "#646c9a",
          "&.MuiButton-outlined": {
            borderColor: themeConfig.color.grey2,
          },
          "&.MuiButton-outlined:hover": {
            backgroundColor: themeConfig.color.grey2,
          },
          "&.MuiButton-outlinedPrimary": {
            borderColor: themeConfig.color.primaryLight,
            color: themeConfig.color.primary,
          },
          "&.MuiButton-outlinedPrimary:hover": {
            borderColor: themeConfig.color.primaryLight,
            backgroundColor: themeConfig.color.primaryLight,
          },
          "&.MuiButton-containedPrimary:hover": {
            backgroundColor: themeConfig.color.primaryDark1,
          },
        },
      },
    },
  }
);

export type ThemeProviderProps = {
  children: React.ReactNode;
};

export default function ThemeProvider(props: ThemeProviderProps) {
  const { children } = props;

  return <MuiThemeProvider theme={theme}>{children}</MuiThemeProvider>;
}

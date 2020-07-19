"use strict";

module.exports = {
  extends: "eslint-config-react-app",
  rules: {
    // "no-script-url": "warn",
    "jsx-a11y/anchor-is-valid": "warn",
    "no-restricted-imports": [
      "error",
      {
        // https://material-ui.com/guides/minimizing-bundle-size/#option-1
        patterns: ["@material-ui/*/*/*", "!@material-ui/core/test-utils/*"],
      },
    ],
  },
};

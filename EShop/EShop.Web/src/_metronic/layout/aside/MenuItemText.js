import React from "react";
import { FormattedMessage } from "react-intl";
import { makeStyles } from "@material-ui/core";
import classNames from "classnames";
import { important } from "../../../app/styles";

const useStyles = makeStyles({
  menuLinkIcon: {
    color: important("#c4cff9"),
    transition: "color ease .2s",

    ".kt-menu__link:hover &": {
      color: important("var(--blue)"),
    },
  },
});

export default function MenuItemText(props) {
  const { item, parentItem } = props;
  const styles = useStyles();

  return (
    <>
      {item.icon && (
        <i
          className={classNames(
            "kt-menu__link-icon",
            item.icon,
            styles.menuLinkIcon
          )}
        />
      )}

      {parentItem && parentItem.bullet === "dot" && (
        <i className="kt-menu__link-bullet kt-menu__link-bullet--dot">
          <span />
        </i>
      )}

      {parentItem && parentItem.bullet === "line" && (
        <i className="kt-menu__link-bullet kt-menu__link-bullet--line">
          <span />
        </i>
      )}

      <span className="kt-menu__link-text" style={{ whiteSpace: "nowrap" }}>
        {!item.translate ? (
          item.title
        ) : (
          <FormattedMessage id={item.translate} defaultMessage={item.title} />
        )}
      </span>

      {item.badge && (
        <span className="kt-menu__link-badge">
          <span className={`kt-badge ${item.badge.type}`}>
            {item.badge.value}
          </span>
        </span>
      )}

      {item.submenu && <i className="kt-menu__ver-arrow la la-angle-right" />}
    </>
  );
}

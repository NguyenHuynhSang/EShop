import React, { forwardRef, useEffect, useState, ReactNode } from "react";
import clsx from "clsx";
import { isFragment } from "react-is";

type PortletBaseProps = {
  className?: string;
  children: React.ReactNode;
};

export const PortletHeaderIcon = forwardRef(
  (props: PortletBaseProps, ref: React.Ref<HTMLSpanElement>) => (
    <span
      ref={ref}
      className={clsx("kt-portlet__head-icon", props.className)}
    />
  )
);

export const PortletHeaderTitle = forwardRef(
  (props: PortletBaseProps, ref: React.Ref<HTMLHeadingElement>) => {
    const { className, children, ...rest } = props;
    return (
      <h3
        {...rest}
        ref={ref}
        className={clsx("kt-portlet__head-title", className)}
      >
        {children}
      </h3>
    );
  }
);

export const PortletHeaderToolbar = forwardRef(
  (props: PortletBaseProps, ref: React.Ref<HTMLDivElement>) => {
    const { className, ...rest } = props;

    return (
      <div
        {...rest}
        ref={ref}
        className={clsx("kt-portlet__head-toolbar", className)}
      />
    );
  }
);

type PortletHeaderProps = {
  className?: string;
  icon?: ReactNode;
  title: React.ReactNode;
  toolbar: JSX.Element;
  sticky?: boolean;
  labelRef?: string;
};

export const PortletHeader = forwardRef(
  (props: PortletHeaderProps, ref: React.Ref<HTMLDivElement>) => {
    const { icon, title, toolbar, className, sticky = false, labelRef } = props;
    const [top, setTop] = useState(0);
    const [windowHeight, setWindowHeight] = useState(0);

    useEffect(() => {
      handleResize();

      function handleResize() {
        setWindowHeight(window.innerWidth);
      }

      window.addEventListener("resize", handleResize);

      return () => {
        window.removeEventListener("resize", handleResize);
      };
    });

    useEffect(() => {
      // Skip if sticky is disabled or on initial render when we don't know about window height.
      if (!sticky || windowHeight === 0) {
        return;
      }

      const headerElement = document.querySelector(
        ".kt-header"
      ) as HTMLDivElement;
      const subheaderElement = document.querySelector(
        ".kt-subheader"
      ) as HTMLDivElement;
      const headerMobileElement = document.querySelector(
        ".kt-header-mobile"
      ) as HTMLDivElement;

      let nextMarginTop = 0;

      // mobile header
      if (window.getComputedStyle(headerElement).height === "0px") {
        nextMarginTop = headerMobileElement.offsetHeight;
      } else {
        // desktop header
        if (document.body.classList.contains("kt-header--minimize-topbar")) {
          // hardcoded minimized header height
          nextMarginTop = 60;
        } else {
          // normal fixed header
          if (document.body.classList.contains("kt-header--fixed")) {
            nextMarginTop += headerElement.offsetHeight;
          }

          if (document.body.classList.contains("kt-subheader--fixed")) {
            nextMarginTop += subheaderElement.offsetHeight;
          }
        }
      }

      setTop(nextMarginTop);
    }, [sticky, windowHeight]);

    return (
      <div
        ref={ref}
        className="kt-portlet__head"
        style={
          !sticky
            ? undefined
            : { top, position: "sticky", backgroundColor: "#fff" }
        }
      >
        {title != null && (
          <div
            ref={labelRef}
            className={clsx("kt-portlet__head-label", className)}
          >
            {icon}

            {/* Wrap string and fragments in PortletHeaderTitle */
            typeof title === "string" || isFragment(title) ? (
              <PortletHeaderTitle>{title}</PortletHeaderTitle>
            ) : (
              title
            )}
          </div>
        )}

        {toolbar}
      </div>
    );
  }
);

type PortletBodyProps = {
  fit?: boolean;
  fluid?: boolean;
  className?: string;
  children: React.ReactNode;
};

export const PortletBody = forwardRef(
  (props: PortletBodyProps, ref: React.Ref<HTMLDivElement>) => {
    const { fit, fluid, className, ...rest } = props;
    return (
      <div
        {...rest}
        ref={ref}
        className={clsx(
          "kt-portlet__body",
          {
            "kt-portlet__body--fit": fit,
            "kt-portlet__body--fluid": fluid,
          },
          className
        )}
      />
    );
  }
);

export const PortletFooter = forwardRef(
  (props: PortletBaseProps, ref: React.Ref<HTMLDivElement>) => {
    const { className, ...rest } = props;
    return (
      <div
        {...rest}
        ref={ref}
        className={clsx("kt-portlet__foot", className)}
      />
    );
  }
);

type PortletProps = {
  id?: string;
  className?: string;
  fluidHeight?: boolean;
  children: React.ReactNode;
};

export const Portlet = forwardRef(
  (props: PortletProps, ref: React.Ref<HTMLDivElement>) => {
    const { fluidHeight, className, ...rest } = props;
    return (
      <div
        {...rest}
        ref={ref}
        className={clsx(
          "kt-portlet",
          { "kt-portlet--height-fluid": fluidHeight },
          className
        )}
      />
    );
  }
);

// Set display names for debugging.
if (process.env.NODE_ENV !== "production") {
  Portlet.displayName = "Portlet";

  PortletHeader.displayName = "PortletHeader";
  PortletHeaderIcon.displayName = "PortletHeaderIcon";
  PortletHeaderTitle.displayName = "PortletHeaderTitle";
  PortletHeaderToolbar.displayName = "PortletHeaderToolbar";

  PortletBody.displayName = "PortletBody";
  PortletFooter.displayName = "PortletFooter";
}

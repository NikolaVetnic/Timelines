import React from "react";
import "./Button.css";

const Button = ({
  text = "",
  onClick = () => {},
  variant = "primary",
  size = "medium",
  shape = "rectangle",
  disabled = false,
  className = "",
  type = "button",
  icon = null,
  iconOnly = false,
  noBackground = false,
  ...props
}) => {
  const buttonClasses = [
    "btn",
    `btn-${variant}`,
    `btn-${size}`,
    `btn-${shape}`,
    disabled ? "btn-disabled" : "",
    iconOnly ? "btn-icon-only" : "",
    noBackground ? "btn-no-bg" : "",
    className,
  ]
    .filter(Boolean)
    .join(" ");

  const content = (
    <>
      {icon && <span className="btn-icon">{icon}</span>}
      {text && !iconOnly && shape !== "circle" && (
        <span className="btn-text">{text}</span>
      )}
      {shape === "circle" && !icon && <span>{String(text).charAt(0)}</span>}
    </>
  );

  return (
    <button
      className={buttonClasses}
      onClick={onClick}
      disabled={disabled}
      type={type}
      {...props}
    >
      {content}
    </button>
  );
};

export default Button;

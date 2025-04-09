import React from "react";
import "./IconButton.css";

const IconButton = ({
  onClick,
  icon,
  title,
  color = "white",
  bgColor = "var(--enmOrange)",
  hoverColor = "#cf5a22",
  size = "27px",
  iconSize = "22px",
  borderRadius = "4px",
  className = "",
  style = {},
  disabled = false,
  hoverTransform = "scale(1.1)",
}) => {
  const buttonStyle = {
    "--bg-color": disabled ? "#d6d6d6" : bgColor,
    "--color": disabled ? "#888" : color,
    "--hover-bg-color": hoverColor,
    "--hover-transform": hoverTransform,
    width: size,
    height: size,
    borderRadius,
    ...style,
  };

  return (
    <button
      className={`icon-button ${className}`}
      onClick={onClick}
      title={title}
      disabled={disabled}
      style={buttonStyle}
    >
      {React.cloneElement(icon, {
        style: {
          width: iconSize,
          height: iconSize,
        },
      })}
    </button>
  );
};

export default IconButton;

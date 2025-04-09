import React from "react";
import "./TextButton.css";

const TextButton = ({
  onClick,
  disabled = false,
  text,
  color = "#007bff",
  hoverColor = "#0056b3",
  textColor = "white",
  borderRadius = "5px",
  padding = "5px 10px",
  fontSize = "12px",
  fontWeight = "bold",
  width = "auto",
  height = "30px",
  className = "",
  style = {},
}) => {
  const buttonStyle = {
    backgroundColor: disabled ? "#d6d6d6" : color,
    color: disabled ? "#888" : textColor,
    borderRadius,
    padding,
    fontSize,
    fontWeight,
    width,
    height,
    ...style,
  };

  const hoverStyle = {
    backgroundColor: hoverColor,
  };

  return (
    <button
      className={`text-button ${className}`}
      onClick={onClick}
      disabled={disabled}
      style={buttonStyle}
      data-hover-style={JSON.stringify(hoverStyle)}
    >
      {text}
    </button>
  );
};

export default TextButton;

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
  fullWidth = false,
  tooltip = "",
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
    fullWidth ? "btn-full-width" : "",
    className,
  ]
    .filter(Boolean)
    .join(" ");

  const tooltipText = tooltip || text;

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
    <div className="btn-wrapper">
      <button
        className={buttonClasses}
        onClick={onClick}
        disabled={disabled}
        type={type}
        aria-label={tooltipText}
        {...props}
      >
        {content}
      </button>
      {iconOnly && tooltipText && (
        <span className="btn-tooltip">{tooltipText}</span>
      )}
    </div>
  );
};

export default Button;

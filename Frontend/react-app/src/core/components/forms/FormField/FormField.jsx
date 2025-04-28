import "./FormField.css";

const FormField = ({
  label,
  type,
  name,
  value,
  onChange,
  placeholder,
  min,
  max,
  required,
  error,
}) => (
  <div className="form-field">
    <label>
      {label}
      {required && <span className="required-asterisk">*</span>}
    </label>
    {type === "textarea" ? (
      <textarea
        name={name}
        value={value}
        onChange={onChange}
        placeholder={placeholder}
        className={error ? "error-input" : ""}
      />
    ) : (
      <input
        type={type}
        name={name}
        value={value}
        onChange={onChange}
        placeholder={placeholder}
        min={min}
        max={max}
        className={error ? "error-input" : ""}
      />
    )}
    {error && <div className="form-field-error">{error}</div>}
  </div>
);

export default FormField;

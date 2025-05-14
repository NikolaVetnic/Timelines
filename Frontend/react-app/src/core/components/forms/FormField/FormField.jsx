import { useState } from "react";
import { FiChevronDown, FiChevronUp } from "react-icons/fi";
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
  options,
}) => {
  const [isSelectOpen, setIsSelectOpen] = useState(false);

  return (
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
      ) : type === "select" ? (
        <div className="select-wrapper">
          <select
            name={name}
            value={value}
            onChange={onChange}
            className={error ? "error-input" : ""}
            onFocus={() => setIsSelectOpen(true)}
            onBlur={() => setIsSelectOpen(false)}
          >
            {placeholder && <option value="">{placeholder}</option>}
            {options?.map((option) => (
              <option key={option.value} value={option.value}>
                {option.label}
              </option>
            ))}
          </select>
          <span className="select-arrow">
            {isSelectOpen ? <FiChevronUp /> : <FiChevronDown />}
          </span>
        </div>
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
};

export default FormField;

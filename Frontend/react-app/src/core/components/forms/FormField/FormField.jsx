import Fuse from 'fuse.js';
import { useEffect, useRef, useState } from "react";
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
  disabled,
}) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredOptions, setFilteredOptions] = useState(options || []);
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const dropdownRef = useRef(null);
  const inputRef = useRef(null);
  const dropdownListRef = useRef(null);

  const selectedOption = options?.find(option => option.value === value);
  const displayValue = selectedOption?.label || searchTerm;

  useEffect(() => {
    if (options) {
      if (searchTerm.trim() === '') {
        setFilteredOptions(options);
      } else {
        const fuse = new Fuse(options, {
          keys: ['label'],
          threshold: 0.3,
          includeScore: true,
        });
        const results = fuse.search(searchTerm);
        setFilteredOptions(results.map(result => result.item));
      }
    }
  }, [searchTerm, options]);

  useEffect(() => {
    if (isDropdownOpen && dropdownListRef.current && value) {
      const selectedElement = dropdownListRef.current.querySelector('.selected');
      if (selectedElement) {
        selectedElement.scrollIntoView({ block: 'nearest' });
      }
    }
  }, [isDropdownOpen, value]);

  const handleSelect = (selectedValue, selectedLabel) => {
    onChange({ target: { value: selectedValue } });
    setSearchTerm(selectedLabel);
    setIsDropdownOpen(false);
  };

  const handleInputChange = (e) => {
    const newValue = e.target.value;
    setSearchTerm(newValue);
    setIsDropdownOpen(true);
  };

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
        setIsDropdownOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  return (
    <div className="form-field" ref={dropdownRef}>
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
          className={error ? "form-field-error-input" : ""}
        />
      ) : options ? (
        <div className="form-field-search-select">
          <input
            ref={inputRef}
            type="text"
            name={name}
            value={displayValue}
            onChange={handleInputChange}
            placeholder={placeholder}
            className={error ? "error-input" : ""}
            onFocus={() => setIsDropdownOpen(true)}
            disabled={disabled}
          />
          {isDropdownOpen && (
            <div 
              className="form-field-search-select-dropdown"
              style={{
                width: inputRef.current?.offsetWidth
              }}
            >
              <div 
                className="form-field-dropdown-options"
                ref={dropdownListRef}
              >
                {filteredOptions.length > 0 ? (
                  filteredOptions.map((option) => (
                    <div
                      key={option.value}
                      className={`form-field-dropdown-option ${
                        value === option.value ? "selected" : ""
                      }`}
                      onClick={() => handleSelect(option.value, option.label)}
                    >
                      {option.label}
                    </div>
                  ))
                ) : (
                  <div className="form-field-dropdown-option no-results">
                    No timelines found
                  </div>
                )}
              </div>
            </div>
          )}
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
          className={error ? "form-field-error-input" : ""}
        />
      )}
      {error && <div className="form-field-error">{error}</div>}
    </div>
  );
};

export default FormField;

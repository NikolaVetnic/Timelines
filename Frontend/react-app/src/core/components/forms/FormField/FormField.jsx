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
  searchable = false,
}) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredOptions, setFilteredOptions] = useState(options || []);
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const dropdownRef = useRef(null);
  const inputRef = useRef(null);
  const dropdownListRef = useRef(null);

  const selectedOption = options?.find(option => option.value === value);
  const displayValue = selectedOption?.label || searchTerm;

  // Filter options based on search term
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

  // Scroll to selected option when dropdown opens
  useEffect(() => {
    if (isDropdownOpen && dropdownListRef.current && value) {
      const selectedElement = dropdownListRef.current.querySelector('.selected');
      if (selectedElement) {
        selectedElement.scrollIntoView({ block: 'nearest' });
      }
    }
  }, [isDropdownOpen, value]);

  // Close dropdown when clicking outside
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

  const handleSelect = (selectedValue, selectedLabel) => {
    onChange({ target: { name, value: selectedValue } });
    setSearchTerm(selectedLabel);
    setIsDropdownOpen(false);
  };

  const handleInputChange = (e) => {
    const newValue = e.target.value;
    setSearchTerm(newValue);
    setIsDropdownOpen(true);
    
    if (newValue === '') {
      onChange({ target: { name, value: '' } });
    }
  };

  const handleClassicSelectChange = (e) => {
    onChange({ target: { name, value: e.target.value } });
  };

  return (
    <div className="form-field" ref={dropdownRef}>
      {label && (
        <label>
          {label}
          {required && <span className="required-asterisk">*</span>}
        </label>
      )}
      
      {type === "textarea" ? (
        <textarea
          name={name}
          value={value}
          onChange={onChange}
          placeholder={placeholder}
          className={error ? "form-field-error-input" : ""}
          disabled={disabled}
        />
      ) : options ? (
        searchable ? (
          // Searchable dropdown
          <div className="form-field-search-select">
            <input
              ref={inputRef}
              type="text"
              name={name}
              value={displayValue}
              onChange={handleInputChange}
              placeholder={placeholder}
              className={error ? "form-field-error-input" : ""}
              onFocus={() => setIsDropdownOpen(true)}
              disabled={disabled}
              autoComplete="off"
            />
            {isDropdownOpen && (
              <div 
                className="form-field-search-select-dropdown"
                style={{ width: inputRef.current?.offsetWidth }}
              >
                <div className="form-field-dropdown-options" ref={dropdownListRef}>
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
                      No options found
                    </div>
                  )}
                </div>
              </div>
            )}
          </div>
        ) : (
          // Classic dropdown
          <div className="form-field-classic-select">
            <select
              name={name}
              value={value}
              onChange={handleClassicSelectChange}
              className={error ? "form-field-error-input" : ""}
              disabled={disabled}
            >
              {placeholder && <option value="">{placeholder}</option>}
              {options.map((option) => (
                <option key={option.value} value={option.value}>
                  {option.label}
                </option>
              ))}
            </select>
          </div>
        )
      ) : (
        <input
          type={type}
          name={name}
          value={value}
          onChange={onChange}
          placeholder={placeholder}
          min={min}
          max={max}
          required={required}
          className={error ? "form-field-error-input" : ""}
          disabled={disabled}
        />
      )}
      {error && <div className="form-field-error">{error}</div>}
    </div>
  );
};

export default FormField;

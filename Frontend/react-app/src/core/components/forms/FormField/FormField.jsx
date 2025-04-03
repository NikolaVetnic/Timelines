const FormField = ({ label, type, name, value, onChange, placeholder, min, max }) => (
    <div className="form-field">
        <label>{label}:</label>
        {type === "textarea" ? (
            <textarea name={name} value={value} onChange={onChange} placeholder={placeholder} />
        ) : (
            <input type={type} name={name} value={value} onChange={onChange} placeholder={placeholder} min={min} max={max} />
        )}
    </div>
);

export default FormField;

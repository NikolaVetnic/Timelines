import './ColorPicker.css';

const ColorPicker = ({ color, onChange }) => {
  return (
    <div className="color-picker-wrapper">
      <input
        type="color"
        value={color}
        onChange={(e) => onChange(e.target.value)}
        className="color-picker-input"
      />
      <span className="color-picker-value">{color.toUpperCase()}</span>
    </div>
  );
};

export default ColorPicker;

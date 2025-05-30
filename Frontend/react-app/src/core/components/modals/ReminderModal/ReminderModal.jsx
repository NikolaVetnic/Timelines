import { useEffect, useState } from 'react';
import { PRIORITY } from "../../../../data/constants";
import Button from "../../buttons/Button/Button";
import "./ReminderModal.css";

const ReminderModal = ({ isOpen, onClose, reminder }) => {
  const [countdown, setCountdown] = useState(5);
  const [isButtonDisabled, setIsButtonDisabled] = useState(false);

  useEffect(() => {
    if (isOpen && reminder?.priority === 3) {
      setIsButtonDisabled(true);
      setCountdown(5);
    } else {
      setIsButtonDisabled(false);
    }
  }, [isOpen, reminder]);

  useEffect(() => {
    let timer;
    if (isButtonDisabled && countdown > 0) {
      timer = setTimeout(() => setCountdown(countdown - 1), 1000);
    } else if (countdown === 0) {
      setIsButtonDisabled(false);
    }
    return () => clearTimeout(timer);
  }, [countdown, isButtonDisabled]);

  if (!isOpen || !reminder) {
    return null;
  }

  const pulseColor = reminder.colorHex || '#e76a29';
  
  const hexToRgba = (hex, opacity) => {
    const r = parseInt(hex.slice(1, 3), 16);
    const g = parseInt(hex.slice(3, 5), 16);
    const b = parseInt(hex.slice(5, 7), 16);
    return `rgba(${r}, ${g}, ${b}, ${opacity})`;
  };

  const pulseStyle = {
    '--pulse-color': pulseColor,
    '--pulse-color-100': hexToRgba(pulseColor, 1),
    '--pulse-color-80': hexToRgba(pulseColor, 0.8),
    '--pulse-color-70': hexToRgba(pulseColor, 0.7),
    '--pulse-color-40': hexToRgba(pulseColor, 0.4),
    '--pulse-color-00': hexToRgba(pulseColor, 0),
    borderColor: hexToRgba(pulseColor, 1),
  };

  return (
    <div 
      className={`reminder-modal-overlay ${reminder.priority === 3 ? 'high-priority-pulse-overlay' : ''}`}
      style={reminder.priority === 3 ? pulseStyle : {}}
    >
      <div 
        className={`reminder-modal-content ${reminder.priority === 3 ? 'high-priority-pulse' : ''}`}
        style={reminder.priority === 3 ? pulseStyle : {}}
      >
        <h3 className="reminder-modal-header">
          REMINDER: {reminder.title}
        </h3>
        
        <div className="reminder-modal-message">
          <p><strong>Description:</strong> {reminder.description || "No additional details"}</p>
          <p><strong>Priority:</strong> {PRIORITY[reminder.priority]}</p>
        </div>

        <div className="reminder-modal-actions">
          <Button 
            text={isButtonDisabled ? `Dismiss (${countdown})` : 'Dismiss'} 
            variant={reminder.priority === 3 ? 'danger' : 'primary'} 
            onClick={onClose}
            disabled={isButtonDisabled}
          />
        </div>
      </div>
    </div>
  );
};

export default ReminderModal;

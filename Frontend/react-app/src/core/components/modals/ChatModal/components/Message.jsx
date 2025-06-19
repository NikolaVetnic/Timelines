import { FaRobot, FaUser } from "react-icons/fa";

export const Message = ({ message }) => (
  <div className={`message-container ${message.sender}`}>
    <div className="message-icon">
      {message.sender === 'user' ? <FaUser /> : <FaRobot />}
    </div>
    <div className={`message ${message.sender}`}>
      {message.text}
      {message.isTyping && <span className="typing-cursor">|</span>}
    </div>
  </div>
);

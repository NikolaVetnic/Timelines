import { FaTimes } from "react-icons/fa";

export const ChatHeader = ({ onClose }) => (
  <div className="chat-header">
    <div className="header-content">
      <h3>File Analysis Assistant</h3>
    </div>
    <button className="close-button" onClick={onClose}>
      <FaTimes />
    </button>
  </div>
);

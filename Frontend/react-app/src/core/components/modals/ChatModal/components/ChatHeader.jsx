import { FaCompress, FaExpand, FaTimes } from "react-icons/fa";

export const ChatHeader = ({ onClose, isFullScreen, toggleFullScreen }) => (
  <div className="chat-header">
    <div className="header-content">
      <h3>File Analysis Assistant</h3>
    </div>
    <div className="header-actions">
      <button className="fullscreen-button" onClick={toggleFullScreen}>
        {isFullScreen ? <FaCompress /> : <FaExpand />}
      </button>
      <button className="close-button" onClick={onClose}>
        <FaTimes />
      </button>
    </div>
  </div>
);

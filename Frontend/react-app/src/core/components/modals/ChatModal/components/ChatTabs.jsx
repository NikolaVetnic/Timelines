import { FaFileAlt, FaHome, FaTimes } from "react-icons/fa";

export const ChatTabs = ({ tabs, switchTab, closeTab }) => (
  <div className="chat-tabs">
    {tabs.map(tab => (
      <div 
        key={tab.id}
        className={`tab ${tab.active ? 'active' : ''}`}
        onClick={() => switchTab(tab.id)}
      >
        {tab.type === 'home' ? <FaHome className="tab-icon" /> : <FaFileAlt className="tab-icon" />}
        <span className="tab-title">{tab.title}</span>
        {tab.id !== 'home' && (
          <button 
            className="tab-close"
            onClick={(e) => closeTab(tab.id, e)}
          >
            <FaTimes />
          </button>
        )}
      </div>
    ))}
  </div>
);

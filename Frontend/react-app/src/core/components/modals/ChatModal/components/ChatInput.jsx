import { CiPaperplane } from "react-icons/ci";

export const ChatInput = ({ 
  inputValue, 
  setInputValue, 
  handleSendMessage, 
  activeTab,
  isTyping
}) => (
  <div className="chat-input-container">
    <input
      type="text"
      value={inputValue}
      onChange={(e) => setInputValue(e.target.value)}
      placeholder={activeTab?.type === 'file' ? `Ask about ${activeTab.file.name}...` : "Select a file first..."}
      onKeyPress={(e) => e.key === 'Enter' && handleSendMessage()}
      disabled={activeTab?.type !== 'file'}
    />
    <button 
      onClick={handleSendMessage} 
      disabled={isTyping || activeTab?.type !== 'file'}
      className="send-button"
    >
      <CiPaperplane />
    </button>
  </div>
);

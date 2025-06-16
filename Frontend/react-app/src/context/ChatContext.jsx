import { createContext, useContext, useState } from 'react';
import ChatModal from '../core/components/modals/ChatModal/ChatModal';

const ChatContext = createContext();

export const ChatProvider = ({ children }) => {
  const [isChatOpen, setIsChatOpen] = useState(false);
  const [chatContext, setChatContext] = useState(null);

  const openChat = (context) => {
    setChatContext(context);
    setIsChatOpen(true);
  };

  const closeChat = () => {
    setIsChatOpen(false);
    setChatContext(null);
  };

  return (
    <ChatContext.Provider value={{ isChatOpen, openChat, closeChat, chatContext }}>
      {children}
      
      {isChatOpen && (
        <div className="chat-modal-wrapper" style={{
          position: 'fixed',
          bottom: '80px',
          right: '20px',
          zIndex: 1000
        }}>
          <ChatModal 
            onClose={closeChat}
            initialFile={chatContext?.file || null}
          />
        </div>
      )}
    </ChatContext.Provider>
  );
};

export const useChat = () => useContext(ChatContext);

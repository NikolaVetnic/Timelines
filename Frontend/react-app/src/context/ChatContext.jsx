import { createContext, useContext, useState } from 'react';
import ChatModal from '../core/components/modals/ChatModal/ChatModal';

const ChatContext = createContext();

export const ChatProvider = ({ children }) => {
  const [isChatOpen, setIsChatOpen] = useState(false);
  const [chatContext, setChatContext] = useState(null);
  const [initialFile, setInitialFile] = useState(null);

  const openChat = (context) => {
    setChatContext(context);
    setInitialFile(context?.file || null);
    setIsChatOpen(true);
  };

  const closeChat = () => {
    setIsChatOpen(false);
    setChatContext(null);
    setInitialFile(null);
  };

  const clearInitialFile = () => {
    setInitialFile(null);
  };

  return (
    <ChatContext.Provider value={{ 
      isChatOpen, 
      openChat, 
      closeChat, 
      chatContext,
      clearInitialFile
    }}>
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
            initialFile={initialFile}
            clearInitialFile={clearInitialFile}
          />
        </div>
      )}
    </ChatContext.Provider>
  );
};

export const useChat = () => useContext(ChatContext);

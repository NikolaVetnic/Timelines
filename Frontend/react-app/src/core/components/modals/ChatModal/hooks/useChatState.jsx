import { useCallback, useEffect, useRef, useState } from 'react';

const STORAGE_KEY = 'fileAnalysisChat';
const CHAT_EXPIRY_DAYS = 7;

export function useChatState(initialFile) {
  const [tabs, setTabs] = useState(() => {
    const savedState = loadChatState();
    
    if (initialFile && !savedState?.tabs?.some(tab => tab.file?.id === initialFile.id)) {
      return [
        { id: 'home', title: 'Home', type: 'home', active: false },
        { 
          id: `file-${initialFile.id}`, 
          title: initialFile.name, 
          type: 'file', 
          file: initialFile, 
          active: true 
        }
      ];
    }
    
    return savedState?.tabs || [{ id: 'home', title: 'Home', type: 'home', active: true }];
  });
  
  const [messages, setMessages] = useState(() => {
    const savedState = loadChatState();
    return savedState?.messages || {};
  });

  const tabsRef = useRef(tabs);

  function loadChatState() {
    try {
      const savedData = localStorage.getItem(STORAGE_KEY);
      if (!savedData) return null;
      
      const parsedData = JSON.parse(savedData);
      
      if (new Date().getTime() > parsedData.expiry) {
        localStorage.removeItem(STORAGE_KEY);
        return null;
      }
      
      return parsedData.data;
    } catch (e) {
      console.error("Failed to load chat state", e);
      return null;
    }
  }

  const saveChatState = useCallback(() => {
    const chatState = {
      tabs,
      messages,
      lastUpdated: new Date().getTime()
    };
    
    const expiryDate = new Date();
    expiryDate.setDate(expiryDate.getDate() + CHAT_EXPIRY_DAYS);
    
    const storageData = {
      data: chatState,
      expiry: expiryDate.getTime()
    };
    
    try {
      localStorage.setItem(STORAGE_KEY, JSON.stringify(storageData));
    } catch (e) {
      console.error("Failed to save chat state", e);
    }
  }, [tabs, messages]);

  useEffect(() => {
    saveChatState();
  }, [tabs, messages, saveChatState]);

  useEffect(() => {
    tabsRef.current = tabs;
  }, [tabs]);

  return { tabs, setTabs, messages, setMessages, tabsRef };
}

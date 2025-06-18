import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import "./ChatModal.css";
import { ChatHeader } from './components/ChatHeader';
import { ChatInput } from './components/ChatInput';
import { ChatMessages } from './components/ChatMessages';
import { ChatTabs } from './components/ChatTabs';
import { FileSelection } from './components/FileSelection';
import { useChatState } from './hooks/useChatState';
import { useFileAnalysis } from './hooks/useFileAnalysis';
import { useTypingEffect } from './hooks/useTypingEffect';
import { addMessage, getBotResponse } from './services/ChatService';

const ChatModal = ({ onClose, initialFile, clearInitialFile }) => {
  const { tabs, setTabs, messages, setMessages, tabsRef } = useChatState();
  const { simulateTyping } = useTypingEffect();
  const handledFiles = useRef(new Set());
  const [inputValue, setInputValue] = useState('');
  const { 
    files, 
    isLoadingFiles, 
    pagination, 
    fetchFiles, 
    handlePageChange 
  } = useFileAnalysis();

  const messagesEndRef = useRef(null);

  const activeTab = useMemo(() => tabs.find(tab => tab.active), [tabs]);
  const activeMessages = useMemo(() => messages[activeTab?.id] || [], [messages, activeTab?.id]);
  const isTyping = useMemo(() => 
    activeMessages.length > 0 && activeMessages[activeMessages.length-1].isTyping, 
    [activeMessages]
  );

  const scrollToBottom = useCallback(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, []);

  useEffect(() => {
    scrollToBottom();
  }, [activeMessages, scrollToBottom]);

  useEffect(() => {
    fetchFiles();
  }, [fetchFiles]);

  useEffect(() => {
    if (initialFile && !handledFiles.current.has(initialFile.id)) {
      const existingTab = tabsRef.current.find(tab => 
        tab.type === 'file' && tab.file?.id === initialFile.id
      );
      
      if (!existingTab) {
        const newTab = {
          id: `file-${initialFile.id}`,
          title: initialFile.name,
          type: 'file',
          file: initialFile,
          active: true
        };
        
        setTabs(prevTabs => [
          ...prevTabs.map(tab => ({ ...tab, active: false })),
          newTab
        ]);
        
        simulateTyping(setMessages, newTab.id, `Great! I'm ready to analyze "${initialFile.name}". What would you like to know?`, 'bot');
      } else {
        setTabs(prevTabs => 
          prevTabs.map(tab => ({
            ...tab,
            active: tab.id === existingTab.id
          }))
        );
      }
      
      handledFiles.current.add(initialFile.id);
    }
  }, [initialFile, setTabs, simulateTyping, tabsRef, setMessages]);

  useEffect(() => {
    if (files.length > 0 && (!messages['home'] || messages['home'].length === 0)) {
      addMessage(messages, setMessages, 'home', "Hello! Please select a file to analyze:", 'bot');
    }
  }, [files, messages, setMessages]);

  const handleSendMessage = useCallback(() => {
    if (!inputValue.trim() || !activeTab) return;
    
    addMessage(messages, setMessages, activeTab.id, inputValue, 'user');
    setInputValue('');
    
    setTimeout(() => {
      const botResponse = getBotResponse(inputValue, activeTab);
      simulateTyping(setMessages, activeTab.id, botResponse, 'bot');
    }, 1000);
  }, [activeTab, inputValue, messages, setMessages, simulateTyping]);

  const handleFileSelect = useCallback((file) => {
    const existingTab = tabs.find(tab => tab.type === 'file' && tab.file.id === file.id);
    
    if (existingTab) {
      setTabs(tabs.map(tab => ({
        ...tab,
        active: tab.id === existingTab.id
      })));
    } else {
      const newTab = {
        id: `file-${file.id}`,
        title: file.name,
        type: 'file',
        file: file,
        active: true
      };
      
      setTabs(prevTabs => [
        ...prevTabs.map(tab => ({ ...tab, active: false })),
        newTab
      ]);
      
      simulateTyping(setMessages, newTab.id, `Great! I'm ready to analyze "${file.name}". What would you like to know?`, 'bot');
    }
  }, [tabs, setTabs, simulateTyping, setMessages]);

  const switchTab = useCallback((tabId) => {
    setTabs(tabs.map(tab => ({
      ...tab,
      active: tab.id === tabId
    })));
  }, [tabs, setTabs]);

  const closeTab = (tabId, e) => {
    e.stopPropagation();
    
    if (tabs.length <= 1) return;
    
    setTabs(prevTabs => {
      const tabIndex = prevTabs.findIndex(tab => tab.id === tabId);
      const tabToClose = prevTabs[tabIndex];
      const isActive = tabToClose?.active;
      const newTabs = prevTabs.filter(tab => tab.id !== tabId);
      
      if (isActive) {
        const homeTabIndex = newTabs.findIndex(tab => tab.id === 'home');
        const newActiveIndex = homeTabIndex >= 0 ? homeTabIndex : 0;
        
        return newTabs.map((tab, index) => ({
          ...tab,
          active: index === newActiveIndex
        }));
      }
      
      return newTabs;
    });
  
    if (tabId.startsWith('file-') && initialFile?.id === tabId.replace('file-', '')) {
      clearInitialFile (null);
    }
  
    handledFiles.current.delete(initialFile?.id);
  };


  return (
    <div className="chat-modal">
      <ChatHeader onClose={onClose} />
      <ChatTabs tabs={tabs} switchTab={switchTab} closeTab={closeTab} />
      
      {activeTab?.type === 'home' ? (
        <>
          <ChatMessages activeMessages={activeMessages} messagesEndRef={messagesEndRef} />
          <FileSelection 
            files={files}
            isLoadingFiles={isLoadingFiles}
            pagination={pagination}
            handleFileSelect={handleFileSelect}
            handlePageChange={handlePageChange}
          />
        </>
      ) : (
        <ChatMessages activeMessages={activeMessages} messagesEndRef={messagesEndRef} />
      )}
      
      <ChatInput 
        inputValue={inputValue}
        setInputValue={setInputValue}
        handleSendMessage={handleSendMessage}
        activeTab={activeTab}
        isTyping={isTyping}
      />
    </div>
  );
};

export default ChatModal;

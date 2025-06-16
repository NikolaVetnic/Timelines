
export function useTypingEffect() {
  const simulateTyping = (setMessages, tabId, text, sender) => {
    const messageId = Date.now();
    setMessages(prev => ({
      ...prev,
      [tabId]: [...(prev[tabId] || []), { id: messageId, text: '', sender, isTyping: true }]
    }));
    
    let i = 0;
    const typingInterval = setInterval(() => {
      setMessages(prev => {
        const currentMessages = prev[tabId] || [];
        const messageIndex = currentMessages.findIndex(msg => msg.id === messageId);
        
        if (messageIndex === -1 || i >= text.length) {
          clearInterval(typingInterval);
          if (messageIndex !== -1) {
            return {
              ...prev,
              [tabId]: currentMessages.map(msg => 
                msg.id === messageId 
                  ? { ...msg, isTyping: false } 
                  : msg
              )
            };
          }
          return prev;
        }
        
        return {
          ...prev,
          [tabId]: currentMessages.map(msg => 
            msg.id === messageId 
              ? { ...msg, text: text.substring(0, i + 1) } 
              : msg
          )
        };
      });
      i++;
    }, 20);
  };

  return { simulateTyping };
}

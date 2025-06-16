const mockedAnalysis = {
    "pdf": "This PDF contains contract documentation. Key clauses found: termination, liability, confidentiality.",
    "xlsx": "Financial spreadsheet shows revenue data. Top performing products listed.",
    "pptx": "Presentation deck with key metrics slides. Recommendation: Update competitor analysis.",
    "jpg": "Image file with detected objects and color distribution.",
    "csv": "Dataset with multiple rows. Shows average transaction values and data quality."
  };
  
  export const getBotResponse = (inputValue, activeTab) => {
    if (!activeTab || activeTab.type !== 'file') {
      return "I can help you analyze files. First, select a file from the options above.";
    }
  
    const file = activeTab.file;
    let botResponse = "I can analyze your file content. Try asking:\n- What's in this file?\n- Show key points\n- Analyze data patterns";
    
    if (inputValue.toLowerCase().includes('analyze') || 
        inputValue.toLowerCase().includes('what') ||
        inputValue.toLowerCase().includes('show')) {
      const fileExtension = file.name.split('.').pop().toLowerCase();
      botResponse = mockedAnalysis[fileExtension] || 
        `Analysis for ${file.name}: This file appears to contain business data. For detailed analysis, please view the actual file.`;
    }
    
    return botResponse;
  };
  
  export const addMessage = (messages, setMessages, tabId, text, sender) => {
    if (sender === 'bot' && (text.startsWith("Hello!") || text.startsWith("Great! I'm ready to analyze"))) {
      const existingMessages = messages[tabId] || [];
      const hasSimilarGreeting = existingMessages.some(msg => 
        msg.sender === 'bot' && 
        (msg.text.startsWith("Hello!") || msg.text.startsWith("Great! I'm ready to analyze"))
      );
      
      if (hasSimilarGreeting) {
        return;
      }
    }
    
    const message = { id: Date.now(), text, sender };
    setMessages(prev => ({
      ...prev,
      [tabId]: [...(prev[tabId] || []), message]
    }));
  };

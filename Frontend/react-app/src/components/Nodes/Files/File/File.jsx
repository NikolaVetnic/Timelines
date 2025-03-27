import * as pdfjsLib from 'pdfjs-dist';
import React, { useCallback, useEffect, useState } from "react";
import { useDropzone } from "react-dropzone";
import { toast } from "react-toastify";

import TextButton from "../../../../core/components/buttons/TextButton/TextButton";
import { LOCAL_STORAGE_KEY, MAX_FILE_SIZE } from "../../../../data/constants";
import { KEYWORDS } from "../../../../data/keywords/keywrods";

import "react-toastify/dist/ReactToastify.css";
import "./File.css";

// Configure PDF.js worker
pdfjsLib.GlobalWorkerOptions.workerSrc = new URL(
  "pdfjs-dist/build/pdf.worker.min.js",
  import.meta.url
).toString();

const File = ({ nodeId, timelineId, onToggle }) => {
  const root = "file";
  const [files, setFiles] = useState([]);
  const [isExpanded, setIsExpanded] = useState(false);
  const [selectedPdf, setSelectedPdf] = useState(null);
  const [extractedText, setExtractedText] = useState("");
  const [isAnalyzing, setIsAnalyzing] = useState(false);
  const [analysisResult, setAnalysisResult] = useState("");
  const [extractedNames, setExtractedNames] = useState({ firstName: "", lastName: "" });
  const [customKeywords, setCustomKeywords] = useState("");
  const [highlightedText, setHighlightedText] = useState("");
  const [hasAnalyzed, setHasAnalyzed] = useState(false);

  // todo: connect to backend
  useEffect(() => {
    const storedData = localStorage.getItem(LOCAL_STORAGE_KEY);
    if (storedData) {
      const parsedData = JSON.parse(storedData);
      const timeline = parsedData.find((t) => t.id === timelineId);
      const node = timeline?.nodes.find((n) => n.id === nodeId);
      if (node) {
        setFiles(node.files || []);
      }
    }
  }, [timelineId, nodeId]);

  // todo: connect to backend
  const updateLocalStorage = useCallback((updatedFiles) => {
    const storedData = localStorage.getItem(LOCAL_STORAGE_KEY);
    if (storedData) {
      const parsedData = JSON.parse(storedData);
      const timelineIndex = parsedData.findIndex((t) => t.id === timelineId);
      if (timelineIndex !== -1) {
        const nodeIndex = parsedData[timelineIndex].nodes.findIndex((n) => n.id === nodeId);
        if (nodeIndex !== -1) {
          parsedData[timelineIndex].nodes[nodeIndex].files = updatedFiles;
          localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(parsedData));
        }
      }
    }
  }, [timelineId, nodeId]);

  const fileToBase64 = (file) => {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });
  };
  
  const onDrop = useCallback(
    async (acceptedFiles) => {
      const validFiles = acceptedFiles.filter(file => {
        if (file.size > MAX_FILE_SIZE) {
          toast.error(`❌ File "${file.name}" exceeds the 10MB limit.`);
          return false;
        }
        return true;
      });
  
      if (validFiles.length === 0) return;
  
      const newFiles = await Promise.all(
        validFiles.map(async (file) => {
          const base64 = await fileToBase64(file);
          return {
            id: new Date().getTime().toString(),
            name: file.name,
            size: file.size,
            type: file.type,
            url: base64,
          };
        })
      );
  
      const updatedFiles = [...files, ...newFiles];
      setFiles(updatedFiles);
      updateLocalStorage(updatedFiles);
      toast.success(`📂 ${newFiles.length} file(s) uploaded successfully!`);
      setTimeout(() => onToggle(), 0);
    },
    [files, onToggle, updateLocalStorage]
  );
  
  const { getRootProps, getInputProps } = useDropzone({
    onDrop,
    accept: {
      "image/*": [".png", ".jpg", ".jpeg", ".gif", ".webp"],
      "application/pdf": [".pdf"],
      "application/msword": [".doc", ".docx"],
      "application/vnd.ms-excel": [".xls", ".xlsx"],
      "application/vnd.ms-powerpoint": [".ppt", ".pptx"],
      "text/plain": [".txt"],
    },
    multiple: true,
  });

  const handleRemoveFile = (id) => {
    const fileToRemove = files.find(file => file.id === id);
    const updatedFiles = files.filter((file) => file.id !== id);
    setFiles(updatedFiles);
    updateLocalStorage(updatedFiles);
    toast.warning(`🗑️ File "${fileToRemove?.name}" removed.`);
    setTimeout(() => onToggle(), 0);
  };

  const toggleExpansion = () => {
    setIsExpanded((prev) => !prev);
    setTimeout(() => onToggle(), 0);
  };

  const handleDownload = (file) => {
    const link = document.createElement("a");
    link.href = file.url;
    link.download = file.name;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    toast.success(`⬇️ File "${file.name}" downloaded!`);
  };

  const handlePreview = (file) => {
    if (file.type.startsWith("image/")) {
      const previewWindow = window.open();
      previewWindow.document.writeln(`<img src="${file.url}" style="max-width:100%;" />`);
    } 
    else if (file.type === "application/pdf") {
      const previewWindow = window.open();
      previewWindow.document.writeln(`
        <embed src="${file.url}" type="application/pdf" width="100%" height="100%" style="border:none;">
      `);
    }
    else if (file.type === "text/plain") {
      fetch(file.url)
        .then(response => response.text())
        .then(text => {
          const previewWindow = window.open();
          previewWindow.document.writeln(`<pre style="white-space: pre-wrap;">${text}</pre>`);
        })
        .catch(() => toast.error("❌ Error loading text file."));
    }
    else if (
      file.name.endsWith(".doc") || file.name.endsWith(".docx") || 
      file.name.endsWith(".xls") || file.name.endsWith(".xlsx") || 
      file.name.endsWith(".ppt") || file.name.endsWith(".pptx")
    ) {
      const googleViewerUrl = `https://docs.google.com/gview?url=${encodeURIComponent(file.url)}&embedded=true`;
      window.open(googleViewerUrl, "_blank");
    } 
    else {
      toast.error("❌ Preview not supported for this file type.");
    }
  };

  const extractNamesFromText = (text) => {
    const namePatterns = [
      /(?:name|first name|first)[:\s]+([a-z]+)[\s]+(?:last name|last|surname)[:\s]+([a-z]+)/i,
      /^([A-Z][a-z]+)\s+([A-Z][a-z]+)/,
      /([A-Z][a-z]+),\s+([A-Z][a-z]+)/
    ];

    for (const pattern of namePatterns) {
      const match = text.match(pattern);
      if (match) {
        if (pattern.toString().includes(',')) {
          return { firstName: match[2], lastName: match[1] };
        }
        return { firstName: match[1], lastName: match[2] };
      }
    }

    return { firstName: "", lastName: "" };
  };

  const extractTextFromPdf = async (pdfUrl) => {
    try {
      const base64Data = pdfUrl.startsWith('data:application/pdf;base64,') 
        ? pdfUrl.split(',')[1] 
        : pdfUrl;
  
      const binaryString = atob(base64Data);
      const bytes = new Uint8Array(binaryString.length);
      for (let i = 0; i < binaryString.length; i++) {
        bytes[i] = binaryString.charCodeAt(i);
      }
  
      const loadingTask = pdfjsLib.getDocument({
        data: bytes,
        disableAutoFetch: true,
        disableStream: true
      });
      
      const pdf = await loadingTask.promise;
      let text = "";
    
    for (let i = 1; i <= pdf.numPages; i++) {
      const page = await pdf.getPage(i);
      const content = await page.getTextContent();
      
      const pageText = content.items
        .map(item => {
          let str = item.str
            .replace(/\u0000/g, '')
            .replace(/\s+/g, ' ')
            .trim();
            
          return item.hasEOL ? str + '\n' : str + ' ';
        })
        .join('')
        .replace(/(\S)-\n(\S)/g, '$1$2')
        .replace(/\n+/g, '\n');
      
      text += pageText + '\n\n';
    }

    text = text
      .replace(/(\w+)(\u00AD)/g, '$1')
      .replace(/\s\s+/g, ' ')
      .replace(/(\S)\s*-\s*(\S)/g, '$1-$2')
      .replace(/�/g, '')
      .replace(//g, '')
      .replace(/(\r\n|\n|\r)/gm, ' ');

    const names = extractNamesFromText(text);
    setExtractedNames(names);
    
    return text;
  } catch (error) {
    console.error("Error extracting text from PDF:", error);
    toast.error("❌ Error extracting text from PDF");
    return "";
  }
  };
  
  const handleAnalyzePdf = async (file) => {
    if (file.type !== "application/pdf") {
      toast.error("❌ Only PDF files can be analyzed");
      return;
    }
  
    try {
      setSelectedPdf(file);
      setIsAnalyzing(true);
      setExtractedText("");
      setAnalysisResult("");
      setExtractedNames({ firstName: "", lastName: "" });
      setHighlightedText("");
      setCustomKeywords("");
      setHasAnalyzed(false);
      
      toast.info("⏳ Extracting text from PDF...", { autoClose: false });
      
      const text = await extractTextFromPdf(file.url);
      
      if (!text || text.trim() === "") {
        throw new Error("No text content found in PDF");
      }
      
      setExtractedText(text);
      setHighlightedText(text);
      toast.dismiss();
      toast.success("✅ PDF text extracted successfully!");
    } catch (error) {
      console.error("PDF processing error:", error);
      toast.dismiss();
      toast.error(`❌ Error processing PDF: ${error.message}`);
    } finally {
      setIsAnalyzing(false);
    }
  };
  
  const handleAiAnalysis = async () => {
    if (!extractedText) {
      toast.error("❌ No text to analyze");
      return;
    }
  
    try {
      setIsAnalyzing(true);
      toast.info("🔍 Analyzing document for keywords...", { autoClose: false });
  
      const foundKeywords = KEYWORDS.filter(keyword => {
        const regex = new RegExp(`\\b${keyword}\\b`, 'i');
        return regex.test(extractedText);
      });
  
      const textWithHighlights = KEYWORDS.reduce((text, keyword) => {
        const regex = new RegExp(`(\\b${keyword}\\b)(?![^<]*>|</mark>)`, 'gi');
        return text.replace(regex, '<mark class="preloaded">$1</mark>');
      }, extractedText);
  
      const keywordsReport = `Predefined Keywords Found:\n` +
        `• ${foundKeywords.join("\n• ")}\n\n` +
        "The highlighted text shows where these keywords appear in the document.";
  
      setHighlightedText(textWithHighlights);
      setAnalysisResult(`Analysis for ${extractedNames.firstName} ${extractedNames.lastName}:\n\n${keywordsReport}`);
  
      toast.dismiss();
      toast.success("✅ Analysis complete!");
      setHasAnalyzed(true);
    } catch (error) {
      console.error("AI analysis error:", error);
      toast.error("❌ Error during AI analysis");
    } finally {
      setIsAnalyzing(false);
    }
  };

  const highlightCustomKeywords = () => {
    if (!customKeywords.trim()) {
      setAnalysisResult(prev => {
        if (!prev) return prev;
        return prev.replace(/Custom Keywords:[\s\S]*?\n\n/, '');
      });
      
      const textWithPredefinedHighlights = KEYWORDS.reduce((text, keyword) => {
        const regex = new RegExp(`(\\b${keyword}\\b)(?![^<]*>|</mark>)`, 'gi');
        return text.replace(regex, '<mark class="preloaded">$1</mark>');
      }, extractedText);
      
      setHighlightedText(textWithPredefinedHighlights);
      return;
    }
    
    try {
      const userKeywords = customKeywords.split(',')
      .map(k => k.trim())
      .filter(k => k.length > 0);

    const cleanText = extractedText.replace(/<mark class=".*?">|<\/mark>/g, '');

    let textWithHighlights = KEYWORDS.reduce((text, keyword) => {
      const regex = new RegExp(`(\\b${keyword}\\b)(?![^<]*>|</mark>)`, 'gi');
      return text.replace(regex, '<mark class="preloaded">$1</mark>');
    }, cleanText);

    textWithHighlights = userKeywords.reduce((text, keyword) => {
      const sanitizedKeyword = keyword.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
      const regexPattern = `(\\b${sanitizedKeyword}(?:-[\\w]+)*\\b)`;
      const regex = new RegExp(regexPattern, 'gi');
      
      const unmarkedText = text.replace(
        new RegExp(`<mark class=".*?">${sanitizedKeyword}</mark>`, 'gi'),
        keyword
      );
      
      return unmarkedText.replace(regex, '<mark class="custom">$1</mark>');
    }, textWithHighlights);

      const customKeywordsReport = `Custom Keywords:\n` +
        `• ${userKeywords.join("\n• ")}\n\n`;
  
      setHighlightedText(textWithHighlights);
      
      setAnalysisResult(prev => {
        const baseAnalysis = prev ? prev.replace(/Custom Keywords:[\s\S]*?\n\n/, '') : '';
        
        if (baseAnalysis.includes('Predefined Keywords Found:')) {
          return baseAnalysis.replace(
            /^(Analysis for .*?\n\n)/,
            `$1${customKeywordsReport}`
          );
        }

        return `Analysis for ${extractedNames.firstName} ${extractedNames.lastName}:\n\n` +
          `${customKeywordsReport}` +
          "The highlighted text shows where these keywords appear in the document.";
      });
      
      toast.success(`✅ Highlighted ${userKeywords.length} custom keywords!`);
    } catch (error) {
      console.error("Highlighting error:", error);
      toast.error("❌ Error highlighting keywords");
    }
  };
  
    return (
      <div className={`${root}-section`}>
        <button className={`${root}-header ${isExpanded ? "open" : "closed"}`} onClick={toggleExpansion}>
          <h4>Files</h4>
          <span>{isExpanded ? "-" : "+"}</span>
        </button>
  
        {isExpanded && (
          <div className={`${root}-content`}>
            <div className={`${root}-dropzone-container`} {...getRootProps()}>
              <input {...getInputProps()} />
              <p>Drag & drop files here, or click to select files</p>
            </div>
  
            <div className={`${root}-container`}>
              {files.length > 0 ? (
                files.map((file) => (
                  <div key={file.id} className={`${root}-item`}>
                    <div className={`${root}-content`}>
                      <p>{file.name}</p>
                      <p><strong>Size:</strong> {Math.round(file.size / 1024)} KB</p>
  
                      {file.type.startsWith("image/") && (
                        <img src={file.url} alt={file.name} className={`${root}-preview`} />
                      )}
  
                      {file.type === "application/pdf" && (
                        <embed src={file.url} type="application/pdf" className={`${root}-preview-pdf`} />
                      )}
  
                      {file.type === "text/plain" && (
                        <iframe title="Preview a document/picture." src={file.url} className={`${root}-preview-text`}></iframe>
                      )}
                    </div>
  
                    <div className={`${root}-buttons`}>
                      {file.type === "application/pdf" && (
                        <button 
                          className={`${root}-analyze-button`} 
                          onClick={() => handleAnalyzePdf(file)}
                          title="Extract and analyze text"
                        >
                          🪄
                        </button>
                      )}
                      <button className={`${root}-preview-button`} onClick={() => handlePreview(file)}>
                        Preview
                      </button>
                      <button className={`${root}-download-button`} onClick={() => handleDownload(file)}>
                        Download
                      </button>
                      <TextButton onClick={() => handleRemoveFile(file.id)} text="X" color="red" />
                    </div>
                  </div>
                ))
              ) : (
                <p>There are no available files.</p>
              )}
            </div>
  
            {selectedPdf && (
              <div className={`${root}-text-modal`}>
                <div className={`${root}-text-modal-content`}>
                  <div className={`${root}-text-modal-header`}>
                    <h3>PDF Text: {selectedPdf.name}</h3>
                    <button 
                      className={`${root}-text-modal-close`}
                      onClick={() => {
                        setSelectedPdf(null);
                        setExtractedText("");
                        setAnalysisResult("");
                        setExtractedNames({ firstName: "", lastName: "" });
                        setHighlightedText("");
                        setCustomKeywords("");
                        setHasAnalyzed(false);
                      }}
                    >
                      &times;
                    </button>
                  </div>
                  
                  <div className={`${root}-text-modal-body`}>
                    <div className={`${root}-text-actions`}>
                      <button 
                        className={`${root}-text-copy`}
                        onClick={() => {
                          navigator.clipboard.writeText(extractedText);
                          toast.success('Text copied to clipboard!');
                        }}
                      >
                        Copy Text
                      </button>
                      <button 
                        className={`${root}-text-analyze`}
                        onClick={handleAiAnalysis}
                        disabled={isAnalyzing}
                      >
                        {isAnalyzing ? 'Analyzing...' : 'Analyze with AI'}
                      </button>
                    </div>
                    
                    {extractedNames.firstName && (
                      <div className={`${root}-extracted-names`}>
                        <p><strong>First Name:</strong> {extractedNames.firstName}</p>
                        <p><strong>Last Name:</strong> {extractedNames.lastName}</p>
                      </div>
                    )}

                    {hasAnalyzed && (
                      <div className={`${root}-custom-keywords`}>
                        <input
                          type="text"
                          placeholder="Enter custom keywords (comma separated)"
                          value={customKeywords}
                          onChange={(e) => setCustomKeywords(e.target.value)}
                        />
                        <button 
                          className={`${root}-highlight-button`}
                          onClick={highlightCustomKeywords}
                          disabled={isAnalyzing}
                        >
                          Highlight Custom Keywords
                        </button>
                      </div>
                    )}
                                  
                    <pre 
                      className={`${root}-text-content`}
                      dangerouslySetInnerHTML={{ __html: highlightedText || extractedText }} 
                    />
                    
                    {analysisResult && (
                      <div className={`${root}-analysis-result`}>
                        <h4>AI Analysis Result:</h4>
                        <div className={`${root}-analysis-text`}>
                          {analysisResult.split('\n').map((line, i) => (
                            <p key={i}>{line}</p>
                          ))}
                        </div>
                      </div>
                    )}
                  </div>
                </div>
              </div>
            )}
          </div>
        )}
      </div>
    );
};

export default File;

import React, { useCallback, useEffect, useState } from "react";
import { useDropzone } from "react-dropzone";
import { toast } from "react-toastify";

import {
  ACCEPTED_FILE_TYPES,
  LOCAL_STORAGE_KEY,
  KEYWORDS,
  MAX_FILE_SIZE,
} from "../../../../data/constants";
import { extractTextFromPdf } from "../../../../core/utils/ExtractTextFromPDF";
import OCRModal from "../../../../core/components/modals/OCRModal/OCRModal";
import FilesList from "../FileList/FileList";
import { extractNamesFromText } from "../../../../core/utils/ExtractNameFromText";
import { extractTextFromImage } from "../../../../core/utils/ExtractTextFromImage";

import "react-toastify/dist/ReactToastify.css";
import "./File.css";

const File = ({ nodeId, timelineId, onToggle }) => {
  const root = "file";
  const [files, setFiles] = useState([]);
  const [isExpanded, setIsExpanded] = useState(false);
  const [selectedPdf, setSelectedPdf] = useState(null);
  const [extractedText, setExtractedText] = useState("");
  const [isAnalyzing, setIsAnalyzing] = useState(false);
  const [analysisResult, setAnalysisResult] = useState("");
  const [extractedNames, setExtractedNames] = useState({
    firstName: "",
    lastName: "",
  });
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
  const updateLocalStorage = useCallback(
    (updatedFiles) => {
      const storedData = localStorage.getItem(LOCAL_STORAGE_KEY);
      if (storedData) {
        const parsedData = JSON.parse(storedData);
        const timelineIndex = parsedData.findIndex((t) => t.id === timelineId);
        if (timelineIndex !== -1) {
          const nodeIndex = parsedData[timelineIndex].nodes.findIndex(
            (n) => n.id === nodeId
          );
          if (nodeIndex !== -1) {
            parsedData[timelineIndex].nodes[nodeIndex].files = updatedFiles;
            localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(parsedData));
          }
        }
      }
    },
    [timelineId, nodeId]
  );

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
      const validFiles = acceptedFiles.filter((file) => {
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
    accept: ACCEPTED_FILE_TYPES,
    multiple: true,
  });

  const handleRemoveFile = (id) => {
    const fileToRemove = files.find((file) => file.id === id);
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
      previewWindow.document.writeln(
        `<img src="${file.url}" style="max-width:100%;" />`
      );
    } else if (file.type === "application/pdf") {
      const previewWindow = window.open();
      previewWindow.document.writeln(`
        <embed src="${file.url}" type="application/pdf" width="100%" height="100%" style="border:none;">
      `);
    } else if (file.type === "text/plain") {
      fetch(file.url)
        .then((response) => response.text())
        .then((text) => {
          const previewWindow = window.open();
          previewWindow.document.writeln(
            `<pre style="white-space: pre-wrap;">${text}</pre>`
          );
        })
        .catch(() => toast.error("❌ Error loading text file."));
    } else if (
      file.name.endsWith(".doc") ||
      file.name.endsWith(".docx") ||
      file.name.endsWith(".xls") ||
      file.name.endsWith(".xlsx") ||
      file.name.endsWith(".ppt") ||
      file.name.endsWith(".pptx")
    ) {
      const googleViewerUrl = `https://docs.google.com/gview?url=${encodeURIComponent(
        file.url
      )}&embedded=true`;
      window.open(googleViewerUrl, "_blank");
    } else {
      toast.error("❌ Preview not supported for this file type.");
    }
  };

  // OCR part
  const handleAnalyzeFile = async (file) => {
    const supportedTypes = [
      "application/pdf",
      "image/jpeg",
      "image/png",
      "image/gif",
    ];
    if (!supportedTypes.includes(file.type)) {
      toast.error(
        "❌ Only PDF and image files (JPEG, PNG, GIF) can be analyzed"
      );
      return;
    }

    // Common initialization
    setSelectedPdf(file);
    setIsAnalyzing(true);
    setExtractedText("");
    setAnalysisResult("");
    setExtractedNames({ firstName: "", lastName: "" });
    setHighlightedText("");
    setCustomKeywords("");
    setHasAnalyzed(false);

    try {
      let text = "";
      const extractionToastId = toast.info("⏳ Preparing analysis...", {
        autoClose: false,
      });

      if (file.type === "application/pdf") {
        toast.update(extractionToastId, {
          render: "⏳ Extracting text from PDF...",
        });
        text = await extractTextFromPdf(file.url);
      } else if (file.type.startsWith("image/")) {
        toast.update(extractionToastId, {
          render: "⏳ Extracting text from image...",
        });
        text = await extractTextFromImage(file.url, (status, progress) => {
          toast.update(extractionToastId, {
            render: `⏳ Extracting text (${Math.round(progress * 100)}%)`,
          });
        });
      }

      if (!text || text.trim() === "") {
        throw new Error("No text content found in file");
      }

      const names = extractNamesFromText(text);
      setExtractedNames(names);
      setExtractedText(text);
      setHighlightedText(text);

      toast.dismiss(extractionToastId);
      toast.success("✅ Text extracted successfully!");
    } catch (error) {
      console.error("File processing error:", error);
      setExtractedText("");
      toast.error(`❌ Error processing file: ${error.message}`);
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

      // 1. Perform keyword analysis
      const keywordResults = KEYWORDS.map((keyword) => {
        const matches = extractedText.match(new RegExp(keyword, "gi")) || [];
        return {
          keyword,
          found: matches.length > 0,
          count: matches.length,
          examples: matches.slice(0, 3),
        };
      });

      const foundKeywords = keywordResults.filter((k) => k.found);

      // 2. Generate highlighted text
      const textWithHighlights = KEYWORDS.reduce((text, keyword) => {
        const regex = new RegExp(`(\\b${keyword}\\b)(?![^<]*>|</mark>)`, "gi");
        return text.replace(regex, '<mark class="preloaded">$1</mark>');
      }, extractedText);

      // 3. Create text report
      const keywordsReport =
        `Predefined Keywords Found:\n` +
        `• ${foundKeywords
          .map((k) => `${k.keyword} (${k.count}x)`)
          .join("\n• ")}\n\n` +
        "The highlighted text shows where these keywords appear in the document.";

      // 4. Generate structured JSON data in the exact desired format
      const analysisJson = {
        drzavljanstvo: "Црна Гора", // You'll need to extract this from the text
        zastupnici: [
          {
            ime: "Мироје", // Replace with actual extracted data
            prezime: "Јованивић",
          },
          {
            ime: "Драшен",
            prezime: "Медојевић",
          },
        ],
        nadlezni: [
          {
            naziv: "Апелациони суд Црне Горе", // Replace with actual extracted data
          },
          {
            naziv: "Виши суд у Подгорици",
          },
        ],
      };

      // 5. Update state with both formats
      setHighlightedText(textWithHighlights);
      setAnalysisResult({
        textReport: `Analysis for ${extractedNames.firstName} ${extractedNames.lastName}:\n\n${keywordsReport}`,
        jsonData: analysisJson,
      });

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

  const modalState = {
    root,
    selectedPdf,
    setSelectedPdf,
    extractedText,
    setExtractedText,
    analysisResult,
    setAnalysisResult,
    extractedNames,
    setExtractedNames,
    highlightedText,
    setHighlightedText,
    customKeywords,
    setCustomKeywords,
    hasAnalyzed,
    setHasAnalyzed,
    isAnalyzing,
    handleAiAnalysis,
    KEYWORDS,
  };

  return (
    <div className={`${root}-section`}>
      <button
        className={`${root}-header ${isExpanded ? "open" : "closed"}`}
        onClick={toggleExpansion}
      >
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
            <div className={`${root}-container`}>
              <FilesList
                root={root}
                files={files}
                isAnalyzing={isAnalyzing}
                handleAnalyzeFile={handleAnalyzeFile}
                handlePreview={handlePreview}
                handleDownload={handleDownload}
                handleRemoveFile={handleRemoveFile}
              />
            </div>
          </div>

          {selectedPdf && <OCRModal {...modalState} />}
        </div>
      )}
    </div>
  );
};

export default File;

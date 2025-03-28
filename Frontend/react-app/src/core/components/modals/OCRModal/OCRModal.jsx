import React from "react";
import { toast } from "react-toastify";
import { highlightCustomKeywords } from "../../../utils/HighlightCustomKeywords";
import "./OCRModal.css";

const formatJson = (json) => {
  if (!json) return "";
  return JSON.stringify(json, null, 2);
};

const OCRModal = ({
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
}) => {
  const handleCopyJson = () => {
    if (analysisResult?.jsonData) {
      navigator.clipboard.writeText(formatJson(analysisResult.jsonData));
      toast.success("JSON copied to clipboard!");
    }
  };

  return (
    <div className={`${root}-text-modal`}>
      <div className={`${root}-text-modal-content`}>
        <div className={`${root}-text-modal-header`}>
          <h3>
            {selectedPdf.type.startsWith("image/") ? "Image" : "PDF"} Text:{" "}
            {selectedPdf.name}
          </h3>
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
                toast.success("Text copied to clipboard!");
              }}
            >
              Copy Text
            </button>
            <button
              className={`${root}-text-analyze`}
              onClick={handleAiAnalysis}
              disabled={isAnalyzing}
            >
              {isAnalyzing ? "Analyzing..." : "Analyze with AI"}
            </button>
          </div>

          {extractedNames.firstName && (
            <div className={`${root}-extracted-names`}>
              <p>
                <strong>First Name:</strong> {extractedNames.firstName}
              </p>
              <p>
                <strong>Last Name:</strong> {extractedNames.lastName}
              </p>
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
                onClick={() =>
                  highlightCustomKeywords({
                    customKeywords,
                    extractedText,
                    extractedNames,
                    predefinedKeywords: KEYWORDS,
                    setAnalysisResult,
                    setHighlightedText,
                    toast,
                  })
                }
                disabled={isAnalyzing}
              >
                Highlight Custom Keywords
              </button>
            </div>
          )}

          <pre
            className={`${root}-text-content`}
            dangerouslySetInnerHTML={{
              __html: highlightedText || extractedText,
            }}
          />

          {analysisResult && (
            <>
              {/* Text Report - Only for PDF */}
              {selectedPdf.type === "application/pdf" &&
                analysisResult.textReport && (
                  <div className={`${root}-analysis-result`}>
                    <h4>Text Analysis Report:</h4>
                    <div className={`${root}-analysis-text`}>
                      {analysisResult.textReport.split("\n").map((line, i) => (
                        <p key={`text-${i}`}>{line}</p>
                      ))}
                    </div>
                  </div>
                )}
              {/* JSON Data - Only for Images */}
              {selectedPdf.type.startsWith("image/") &&
                analysisResult.jsonData && (
                  <div className={`${root}-json-result`}>
                    <h4>JSON Analysis Report:</h4>
                    <div className={`${root}-json-container`}>
                      <pre className={`${root}-json-view`}>
                        {JSON.stringify(analysisResult.jsonData, null, 2)}
                      </pre>
                    </div>
                    <button
                      className={`${root}-json-copy`}
                      onClick={handleCopyJson}
                    >
                      Copy JSON
                    </button>
                  </div>
                )}
            </>
          )}
        </div>
      </div>
    </div>
  );
};

export default OCRModal;

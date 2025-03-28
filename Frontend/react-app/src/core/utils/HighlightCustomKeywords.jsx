export const highlightCustomKeywords = ({
  customKeywords,
  extractedText,
  extractedNames,
  predefinedKeywords,
  setAnalysisResult,
  setHighlightedText,
  toast,
}) => {
  if (!customKeywords.trim()) {
    setAnalysisResult((prev) => {
      if (!prev) return prev;

      // Handle both string (legacy) and object formats
      if (typeof prev === "string") {
        return {
          textReport: prev.replace(/Custom Keywords:[\s\S]*?\n\n/, ""),
          jsonData: prev.jsonData || null,
        };
      } else {
        return {
          ...prev,
          textReport: prev.textReport.replace(
            /Custom Keywords:[\s\S]*?\n\n/,
            ""
          ),
        };
      }
    });

    const textWithPredefinedHighlights = predefinedKeywords.reduce(
      (text, keyword) => {
        // Modified regex to support Cyrillic word boundaries
        const regex = new RegExp(
          `(\\b${keyword}\\b|[^\\w]${keyword}[^\\w])`,
          "gi"
        );
        return text.replace(regex, (match) => {
          // Preserve the non-word characters if they were matched
          if (match.startsWith(keyword)) {
            return `<mark class="preloaded">${keyword}</mark>`;
          } else {
            return (
              match[0] +
              `<mark class="preloaded">${keyword}</mark>` +
              match.slice(-1)
            );
          }
        });
      },
      extractedText
    );

    setHighlightedText(textWithPredefinedHighlights);
    return;
  }

  try {
    const userKeywords = customKeywords
      .split(",")
      .map((k) => k.trim())
      .filter((k) => k.length > 0);

    const cleanText = extractedText.replace(/<mark class=".*?">|<\/mark>/g, "");

    // Highlight predefined keywords (supporting Cyrillic)
    let textWithHighlights = predefinedKeywords.reduce((text, keyword) => {
      const regex = new RegExp(
        `(\\b${keyword}\\b|[^\\w]${keyword}[^\\w])`,
        "gi"
      );
      return text.replace(regex, (match) => {
        if (match.toLowerCase() === keyword.toLowerCase()) {
          return `<mark class="preloaded">${match}</mark>`;
        } else {
          return (
            match[0] +
            `<mark class="preloaded">${match.slice(1, -1)}</mark>` +
            match.slice(-1)
          );
        }
      });
    }, cleanText);

    // Highlight user keywords (supporting Cyrillic)
    textWithHighlights = userKeywords.reduce((text, keyword) => {
      const sanitizedKeyword = keyword.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
      // Modified regex pattern for Cyrillic support
      const regexPattern = `(\\b${sanitizedKeyword}\\b|[^\\w]${sanitizedKeyword}[^\\w])`;
      const regex = new RegExp(regexPattern, "gi");

      // First remove existing marks for this keyword
      const unmarkedText = text.replace(
        new RegExp(`<mark class=".*?">${sanitizedKeyword}</mark>`, "gi"),
        keyword
      );

      // Then apply new marks
      return unmarkedText.replace(regex, (match) => {
        if (match.toLowerCase() === keyword.toLowerCase()) {
          return `<mark class="custom">${match}</mark>`;
        } else {
          return (
            match[0] +
            `<mark class="custom">${match.slice(1, -1)}</mark>` +
            match.slice(-1)
          );
        }
      });
    }, textWithHighlights);

    const customKeywordsReport = `Custom Keywords:\n• ${userKeywords.join(
      "\n• "
    )}\n\n`;

    setHighlightedText(textWithHighlights);

    setAnalysisResult((prev) => {
      // Handle both string (legacy) and object formats
      let baseText = "";
      let jsonData = null;

      if (typeof prev === "string") {
        baseText = prev;
        jsonData = null;
      } else {
        baseText = prev?.textReport || "";
        jsonData = prev?.jsonData || null;
      }

      const cleanedText = baseText.replace(/Custom Keywords:[\s\S]*?\n\n/, "");

      let newTextReport;
      if (cleanedText.includes("Predefined Keywords Found:")) {
        newTextReport = cleanedText.replace(
          /^(Analysis for .*?\n\n)/,
          `$1${customKeywordsReport}`
        );
      } else {
        newTextReport =
          `Analysis for ${extractedNames.firstName} ${extractedNames.lastName}:\n\n` +
          `${customKeywordsReport}` +
          "The highlighted text shows where these keywords appear in the document.";
      }

      return {
        textReport: newTextReport,
        jsonData: jsonData,
      };
    });

    toast.success(`✅ Highlighted ${userKeywords.length} custom keywords!`);
  } catch (error) {
    console.error("Highlighting error:", error);
    toast.error("❌ Error highlighting keywords");
  }
};

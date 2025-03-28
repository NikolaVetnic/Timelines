import * as pdfjsLib from "pdfjs-dist";

// Configure PDF.js worker once
pdfjsLib.GlobalWorkerOptions.workerSrc = new URL(
  "pdfjs-dist/build/pdf.worker.min.js",
  import.meta.url
).toString();

export const extractTextFromPdf = async (pdfUrl) => {
  try {
    if (!pdfUrl) {
      throw new Error("No PDF URL provided");
    }

    const base64Data = pdfUrl.startsWith("data:application/pdf;base64,")
      ? pdfUrl.split(",")[1]
      : pdfUrl;

    const binaryString = atob(base64Data);
    const bytes = new Uint8Array(binaryString.length);
    for (let i = 0; i < binaryString.length; i++) {
      bytes[i] = binaryString.charCodeAt(i);
    }

    const loadingTask = pdfjsLib.getDocument({
      data: bytes,
      disableAutoFetch: true,
      disableStream: true,
    });

    const pdf = await loadingTask.promise;
    let text = "";

    for (let i = 1; i <= pdf.numPages; i++) {
      const page = await pdf.getPage(i);
      const content = await page.getTextContent();

      const pageText = content.items
        .map((item) => {
          let str = item.str
            .replace(/\u0000/g, "")
            .replace(/\s+/g, " ")
            .trim();
          return item.hasEOL ? str + "\n" : str + " ";
        })
        .join("")
        .replace(/(\S)-\n(\S)/g, "$1$2")
        .replace(/\n+/g, "\n");

      text += pageText + "\n\n";
    }

    return (
      text
        .replace(/(\w+)(\u00AD)/g, "$1")
        .replace(/\s\s+/g, " ")
        .replace(/(\S)\s*-\s*(\S)/g, "$1-$2")
        .replace(/�/g, "")
        .replace(//g, "")
        .replace(/(\r\n|\n|\r)/gm, " ") || ""
    );
  } catch (error) {
    console.error("Error extracting text from PDF:", error);
    throw error;
  }
};

import React, { useCallback, useEffect, useState } from "react";
import { useDropzone } from "react-dropzone";
import { toast } from "react-toastify";

import { LOCAL_STORAGE_KEY, MAX_FILE_SIZE } from "../../../../data/constants";

import "react-toastify/dist/ReactToastify.css";
import "./File.css";

const File = ({ nodeId, timelineId, onToggle }) => {
  const root = "file";
  const [files, setFiles] = useState([]);
  const [isExpanded, setIsExpanded] = useState(false);

  // todo: connect to backend when it is ready
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

  // todo: connect to backend when it is ready
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
            {files.length > 0 ? (
              files.map((file) => (
                <div key={file.id} className={`${root}-item`}>
                  <div className={`${root}-content`}>
                    <p>{file.name}</p>
                    <p>
                      <strong>Size:</strong> {Math.round(file.size / 1024)} KB
                    </p>

                    {file.type.startsWith("image/") && (
                      <img
                        src={file.url}
                        alt={file.name}
                        className={`${root}-preview`}
                      />
                    )}

                    {file.type === "application/pdf" && (
                      <embed
                        src={file.url}
                        type="application/pdf"
                        className={`${root}-preview-pdf`}
                      />
                    )}

                    {file.type === "text/plain" && (
                      <iframe
                        title="Preview a document/picture."
                        src={file.url}
                        className={`${root}-preview-text`}
                      ></iframe>
                    )}
                  </div>

                  <div className={`${root}-buttons`}>
                    <button
                      className={`${root}-preview-button`}
                      onClick={() => handlePreview(file)}
                    >
                      Preview
                    </button>
                    <button
                      className={`${root}-download-button`}
                      onClick={() => handleDownload(file)}
                    >
                      Download
                    </button>
                    {/* <TextButton onClick={() => handleRemoveFile(file.id)} text="X" color="red" /> */}
                  </div>
                </div>
              ))
            ) : (
              <p>There are no available files.</p>
            )}
          </div>
        </div>
      )}
    </div>
  );
};

export default File;

import React from "react";
import TextButton from "../../../../core/components/buttons/TextButton/TextButton";

const FilesList = ({
    root,
    files,
    isAnalyzing,
    handleAnalyzeFile,
    handlePreview,
    handleDownload,
    handleRemoveFile,
}) => {
    if (files.length === 0) {
        return <p>Nema dostupnih fajlova</p>;
    }

    return files.map((file) => (
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
                {file.type === "application/pdf" && (
                    <button
                        className={`${root}-analyze-button`}
                        onClick={() => handleAnalyzeFile(file)}
                        title="Extract and analyze text"
                        disabled={isAnalyzing}
                    >
                        {isAnalyzing ? "⏳ Processing" : "🔍 Analyze"}
                    </button>
                )}
                {file.type.startsWith("image/") && (
                    <button
                        className={`${root}-analyze-button`}
                        onClick={() => handleAnalyzeFile(file)}
                        title="Extract text from image"
                    >
                        🖼️ Digitalizovati
                    </button>
                )}
                <button
                    className={`${root}-preview-button`}
                    onClick={() => handlePreview(file)}
                >
                    Pregledati
                </button>
                <button
                    className={`${root}-download-button`}
                    onClick={() => handleDownload(file)}
                >
                    Preuzeti
                </button>
                <TextButton
                    onClick={() => handleRemoveFile(file.id)}
                    text="X"
                    color="red"
                />
            </div>
        </div>
    ));
};

export default FilesList;

import { useMemo } from 'react';

export const FileViewer = ({ file }) => {
  const fileContent = useMemo(() => {
    if (!file) return null;
    
    // Safely check file type and extension
    const fileType = typeof file.type === 'string' ? file.type.toLowerCase() : '';
    const fileName = typeof file.name === 'string' ? file.name.toLowerCase() : '';
    
    // Text files
    if (fileType.startsWith('text/') || fileName.endsWith('.txt') || fileName.endsWith('.csv')) {
      return (
        <pre className="file-content-text">
          {file.content || 'File content not available'}
        </pre>
      );
    }
    
    // Image files
    if (fileType.startsWith('image/')) {
      try {
        const imgSrc = file.url || (file instanceof Blob ? URL.createObjectURL(file) : null);
        return (
          <div className="file-content-image">
            {imgSrc ? (
              <img 
                src={imgSrc} 
                alt={file.name || 'Image'} 
                style={{ maxWidth: '100%', maxHeight: '100%' }}
                onLoad={() => {
                  if (file instanceof Blob) {
                    URL.revokeObjectURL(imgSrc);
                  }
                }}
              />
            ) : (
              <p>Image source not available</p>
            )}
          </div>
        );
      } catch (error) {
        console.error('Error loading image:', error);
        return <p>Error loading image</p>;
      }
    }
    
    // PDF files
    if (fileType === 'application/pdf' || fileName.endsWith('.pdf')) {
      return (
        <div className="file-content-pdf">
          <p>PDF viewer would be displayed here</p>
          <p>File: {file.name || 'Unknown PDF'}</p>
        </div>
      );
    }
    
    // Unsupported files
    return (
      <div className="file-content-unsupported">
        <p>Preview not available for this file type</p>
        <p>File: {file.name || 'Unknown file'}</p>
      </div>
    );
  }, [file]);

  return (
    <div className="file-viewer">
      {file ? (
        <>
          <div className="file-viewer-header">
            <h4>{file.name || 'Untitled file'}</h4>
          </div>
          <div className="file-viewer-content">
            {fileContent}
          </div>
        </>
      ) : (
        <div className="file-viewer-empty">
          <p>No file selected</p>
        </div>
      )}
    </div>
  );
};

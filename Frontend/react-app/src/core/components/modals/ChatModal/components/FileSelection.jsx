import { FaFileAlt, FaSpinner } from "react-icons/fa";

export const FileSelection = ({ 
  files, 
  isLoadingFiles, 
  pagination, 
  handleFileSelect, 
  handlePageChange 
}) => {
  return (
    <div className="chat-file-selection">
      {isLoadingFiles ? (
        <div className="loading-files">
          <FaSpinner className="spinner" />
          <span>Loading files...</span>
        </div>
      ) : (
        <>
          <div className="predefined-files">
            {files.map((file, index) => (
              <button 
                key={index} 
                className="file-chip"
                onClick={() => handleFileSelect(file)}
              >
                <FaFileAlt className="file-chip-icon" />
                {file.name}
              </button>
            ))}
          </div>
          <div className="pagination-controls">
            <button 
              disabled={pagination.pageIndex === 0}
              onClick={() => handlePageChange(pagination.pageIndex - 1)}
            >
              Previous
            </button>
            <span>Page {pagination.pageIndex + 1} of {pagination.totalPages}</span>
            <button 
              disabled={pagination.pageIndex >= pagination.totalPages - 1}
              onClick={() => handlePageChange(pagination.pageIndex + 1)}
            >
              Next
            </button>
          </div>
        </>
      )}
    </div>
  );
};

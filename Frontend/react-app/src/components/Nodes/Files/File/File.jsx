import { useCallback, useEffect, useState } from "react";
import { useDropzone } from "react-dropzone";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useChat } from '../../../../context/ChatContext';
import FileList from "../../../../core/components/lists/FileList/FileList";
import DeleteModal from "../../../../core/components/modals/DeleteModal/DeleteModal";
import Pagination from "../../../../core/components/pagination/Pagination";
import { FILE_TYPES, MAX_FILE_SIZE } from "../../../../data/constants";
import FileService from "../../../../services/FileService";
import "./File.css";

const File = ({ node, onToggle }) => {
  const root = "file";
  const [files, setFiles] = useState([]);
  const [isExpanded, setIsExpanded] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [fileToDelete, setFileToDelete] = useState(null);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  
  const { openChat } = useChat();

  const [currentPage, setCurrentPage] = useState(0);
  const [itemsPerPage, setItemsPerPage] = useState(2);
  const [totalCount, setTotalCount] = useState(0);
  const itemsPerPageOptions = [2, 4, 6, 8];

  const fetchFiles = useCallback(async () => {
    if (isExpanded && node.id) {
      setIsLoading(true);
      try {
        const response = await FileService.getFilesByNode(
          node.id,
          currentPage,
          itemsPerPage
        );
        setFiles(response.items || []);
        setTotalCount(response.totalCount || 0);
      } finally {
        setIsLoading(false);
      }
    }
  }, [isExpanded, node.id, currentPage, itemsPerPage]);

  useEffect(() => {
    fetchFiles();
  }, [fetchFiles]);

  const totalPages = Math.ceil(totalCount / itemsPerPage) || 1;

  const handlePageChange = (page) => {
    setCurrentPage(page - 1);
  };

  const handleItemsPerPageChange = (size) => {
    setItemsPerPage(size);
    setCurrentPage(0);
  };

  const onDrop = useCallback(
    async (acceptedFiles) => {
      const validFiles = acceptedFiles.filter((file) => {
        if (file.size > MAX_FILE_SIZE) {
          toast.error(`File "${file.name}" exceeds the 10MB limit.`);
          return false;
        }
        return true;
      });

      if (validFiles.length === 0) return;

      setIsLoading(true);
      const uploadPromises = validFiles.map(async (file) => {
        const response = await FileService.uploadFile({
          nodeId: node.id,
          file,
          title: file.name,
          description: "",
        });

        return {
          ...response,
          url: URL.createObjectURL(file),
          name: file.name,
          size: file.size,
          type: file.type,
          nodeId: node.id
        };
      });

      const results = await Promise.all(uploadPromises);
      const successfulUploads = results.filter(result => result !== null);
      
      if (successfulUploads.length > 0) {
        await fetchFiles();
      }
      setIsLoading(false);
      onToggle();
    },
    [node.id, onToggle, fetchFiles]
  );

  const { getRootProps, getInputProps } = useDropzone({
    onDrop,
    accept: FILE_TYPES,
    multiple: true,
  });

  const toggleExpansion = () => {
    setIsExpanded((prev) => !prev);
    onToggle();
  };

  const confirmDelete = async () => {
    if (!fileToDelete) return;
    try {
      setIsLoading(true);
      await FileService.deleteFile(fileToDelete.id);
      await fetchFiles();
    } finally {
      setIsLoading(false);
      setIsDeleteModalOpen(false);
      setFileToDelete(null);
    }
  };

  const cancelDelete = () => {
    setIsDeleteModalOpen(false);
    setFileToDelete(null);
  };

  const handleDownload = async (file) => {
    await FileService.downloadFile(file.id);
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
    } else {
      toast.error("Preview not supported for this file type.");
    }
  };

  const handleOpenChat = (file) => {
    openChat({ file });
  };

  return (
    <div className={`${root}-section`}>
      <button
        className={`${root}-header ${
          isExpanded ? "file-header-open" : "file-header-closed"
        }`}
        onClick={toggleExpansion}
      >
        <h4>Files</h4>
        <span>{isExpanded ? "-" : "+"}</span>
      </button>

      {isExpanded && (
        <div className={`${root}-content`}>
          <div className={`${root}-container`}>
            <div className={`${root}-inner-header`}>
              <h4>Your Files</h4>
            </div>

            <div className={`${root}-dropzone-container`} {...getRootProps()}>
              <input {...getInputProps()} />
              <p>Drag & drop files here, or click to select files</p>
            </div>

            {isLoading ? (
              <div className={`${root}-loading`}>Loading...</div>
            ) : (
              <>
                <FileList
                  files={files}
                  root={root}
                  handlePreview={handlePreview}
                  handleDownload={handleDownload}
                  setFileToDelete={setFileToDelete}
                  setIsDeleteModalOpen={setIsDeleteModalOpen}
                  handleOpenChat={handleOpenChat}
                />
                {totalCount > 0 && (
                  <Pagination
                    currentPage={currentPage + 1}
                    totalPages={totalPages}
                    itemsPerPage={itemsPerPage}
                    onPageChange={handlePageChange}
                    onItemsPerPageChange={handleItemsPerPageChange}
                    itemsPerPageOptions={itemsPerPageOptions}
                  />
                )}
              </>
            )}
          </div>
        </div>
      )}

      <DeleteModal
        isOpen={isDeleteModalOpen}
        onClose={cancelDelete}
        onConfirm={confirmDelete}
        itemType="file"
        itemTitle={fileToDelete?.name || "Untitled File"}
      />
    </div>
  );
};

export default File;

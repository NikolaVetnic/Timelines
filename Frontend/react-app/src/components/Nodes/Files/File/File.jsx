import React, { useCallback, useEffect, useState } from "react";
import { useDropzone } from "react-dropzone";
import { toast } from "react-toastify";
import { IoMdDownload } from "react-icons/io";
import { MdDelete, MdOutlinePreview } from "react-icons/md";
import FileService from "../../../../services/FileService";
import DeleteModal from "../../../../core/components/modals/DeleteModal/DeleteModal";
import Button from "../../../../core/components/buttons/Button/Button";
import Pagination from "../../../../core/components/pagination/Pagination";
import { MAX_FILE_SIZE } from "../../../../data/constants";
import "react-toastify/dist/ReactToastify.css";
import "./File.css";

const File = ({ nodeId, onToggle }) => {
  const root = "file";
  const [files, setFiles] = useState([]);
  const [isExpanded, setIsExpanded] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [fileToDelete, setFileToDelete] = useState(null);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);

  // Pagination state
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(2);
  const itemsPerPageOptions = [2, 4, 6, 8];

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        setIsLoading(true);
        const response = await FileService.getFilesByNode(nodeId);
        setFiles(response.items || []);
      } catch (error) {
        toast.error("Failed to load files");
        console.error("Error fetching files:", error);
      } finally {
        setIsLoading(false);
      }
    };

    if (isExpanded && nodeId) {
      fetchFiles();
    }
  }, [isExpanded, nodeId]);

  // Pagination calculations
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentFiles = files.slice(indexOfFirstItem, indexOfLastItem);
  const totalPages = Math.ceil(files.length / itemsPerPage);

  const handlePageChange = (page) => {
    setCurrentPage(page);
  };

  const handleItemsPerPageChange = (size) => {
    setItemsPerPage(size);
    setCurrentPage(1);
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

      try {
        setIsLoading(true);
        const uploadPromises = validFiles.map(async (file) => {
          const response = await FileService.uploadFile({
            nodeId,
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
          };
        });

        const uploadedFiles = await Promise.all(uploadPromises);
        setFiles((prevFiles) => [...prevFiles, ...uploadedFiles]);
        toast.success(
          `📂 ${uploadedFiles.length} file(s) uploaded successfully!`
        );
      } catch (error) {
        console.error("Error uploading files:", error);
        toast.error("Failed to upload some files");
      } finally {
        setIsLoading(false);
        setTimeout(() => onToggle(), 0);
      }
    },
    [nodeId, onToggle]
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

  const toggleExpansion = () => {
    setIsExpanded((prev) => !prev);
    setTimeout(() => onToggle(), 0);
  };

  const confirmDelete = async () => {
    if (!fileToDelete) return;
    try {
      setIsLoading(true);
      await FileService.deleteFile(fileToDelete.id);
      setFiles((prev) => prev.filter((f) => f.id !== fileToDelete.id));
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
    try {
      await FileService.downloadFile(file.id);
      toast.success(`⬇️ Downloading "${file.name}"...`);
    } catch (error) {
      console.error("Error downloading file:", error);
      toast.error("Failed to download file");
    }
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

  return (
    <div className={`${root}-section`}>
      <button
        className={`${root}-header ${
          isExpanded ? "header-open" : "header-closed"
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
                {files.length > 0 ? (
                  <>
                    <div className={`${root}-grid`}>
                      {currentFiles.map((file) => (
                        <div key={file.id} className={`${root}-card`}>
                          <div className={`${root}-card-content`}>
                            <h5 className={`${root}-card-title`}>
                              {file.name}
                            </h5>
                            <p className={`${root}-card-date`}>
                              <strong>Size:</strong>{" "}
                              {Math.round(file.size / 1024)} KB
                            </p>
                            {file.description && (
                              <p className={`${root}-card-priority`}>
                                <strong>Description:</strong> {file.description}
                              </p>
                            )}
                            {file.type.startsWith("image/") && (
                              <img
                                src={file.url}
                                alt={file.name}
                                className={`${root}-preview`}
                              />
                            )}
                          </div>
                          <div className={`${root}-card-actions`}>
                            <Button
                              icon={<MdOutlinePreview />}
                              iconOnly
                              shape="square"
                              variant="primary"
                              size="small"
                              onClick={() => handlePreview(file)}
                              disabled={!file.url}
                            />
                            <Button
                              icon={<IoMdDownload />}
                              iconOnly
                              shape="square"
                              variant="primary"
                              size="small"
                              onClick={() => handleDownload(file)}
                            />
                            <Button
                              icon={<MdDelete />}
                              iconOnly
                              variant="danger"
                              shape="square"
                              size="small"
                              onClick={() => {
                                setFileToDelete(file);
                                setIsDeleteModalOpen(true);
                              }}
                            />
                          </div>
                        </div>
                      ))}
                    </div>
                    {files.length > itemsPerPage && (
                      <Pagination
                        currentPage={currentPage}
                        totalPages={totalPages}
                        itemsPerPage={itemsPerPage}
                        onPageChange={handlePageChange}
                        onItemsPerPageChange={handleItemsPerPageChange}
                        itemsPerPageOptions={itemsPerPageOptions}
                      />
                    )}
                  </>
                ) : (
                  <div className={`${root}-empty-state`}>
                    <p>No files yet. Upload your first file!</p>
                  </div>
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

import React from "react";
import { IoMdDownload } from "react-icons/io";
import { MdDelete, MdOutlinePreview } from "react-icons/md";
import Button from "../../../../core/components/buttons/Button/Button";

const FileList = ({ 
  files, 
  root, 
  handlePreview, 
  handleDownload, 
  setFileToDelete, 
  setIsDeleteModalOpen 
}) => {
  if (files.length === 0) {
    return (
      <div className={`${root}-empty-state`}>
        <p>No files yet. Upload your first file!</p>
      </div>
    );
  }

  return (
    <div className={`${root}-grid`}>
      {files.map((file) => (
        <div key={file.id} className={`${root}-card`}>
          <div className={`${root}-card-content`}>
            <h5 className={`${root}-card-title`}>{file.name}</h5>
            <p className={`${root}-card-date`}>
              <strong>Size:</strong> {Math.round(file.size / 1024)} KB
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
  );
};

export default FileList;

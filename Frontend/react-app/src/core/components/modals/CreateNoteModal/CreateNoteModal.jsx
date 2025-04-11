import MDEditor from "@uiw/react-md-editor";
import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom";
import Button from "../../buttons/Button/Button";

import "./CreateNoteModal.css";

const CreateNoteModal = ({ isOpen, closeModal, saveNote }) => {
  const root = "create-note-modal";
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");

  useEffect(() => {
    if (isOpen) {
      setTitle("");
      setContent("");
    }
  }, [isOpen]);

  if (!isOpen) return null;

  const handleSave = () => {
    if (title.trim()) {
      saveNote({
        id: Date.now().toString(),
        title,
        content,
        timestamp: new Date(),
        related: [],
        sharedWith: [],
        isPublic: false,
      });
      closeModal();
    }
  };

  return ReactDOM.createPortal(
    <div className={`${root}-overlay`}>
      <div className={`${root}-content`}>
        <h2 className={`${root}-header`}>Create New Note</h2>
        
        <input 
          type="text" 
          placeholder="Note Title" 
          value={title} 
          onChange={(e) => setTitle(e.target.value)} 
          className={`${root}-title-input`}
        />

        <div className={`${root}-editor-container`}>
          <MDEditor value={content} onChange={setContent} height="100%" />
        </div>

        <div className={`${root}-buttons`}>
          <Button 
            text="Cancel"
            onClick={closeModal}
          />
          <Button 
            text="Create"
            variant="success"
            onClick={handleSave}
          />
        </div>
      </div>
    </div>,
    document.body
  );
};

export default CreateNoteModal;

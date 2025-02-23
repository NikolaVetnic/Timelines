import MDEditor from "@uiw/react-md-editor";
import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom";

import "./CreateNoteModal.css";

const CreateNoteModal = ({ isOpen, closeModal, saveNote }) => {
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
    <div className="create-note-modal-overlay">
      <div className="create-note-modal-content">
        <h2 className="create-note-modal-header">Create New Note</h2>
        
        <input 
          type="text" 
          placeholder="Note Title" 
          value={title} 
          onChange={(e) => setTitle(e.target.value)} 
          className="note-title-input"
        />

        <div className="editor-container">
          <MDEditor value={content} onChange={setContent} height="100%" />
        </div>

        <div className="modal-buttons">
          <button className="close-button" onClick={closeModal}>Cancel</button>
          <button className="save-button" onClick={handleSave}>Create</button>
        </div>
      </div>
    </div>,
    document.body
  );
};

export default CreateNoteModal;

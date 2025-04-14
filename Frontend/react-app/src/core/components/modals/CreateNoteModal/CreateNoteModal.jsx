import MDEditor from "@uiw/react-md-editor";
import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom";
import { FaTimes } from "react-icons/fa";
import Button from "../../buttons/Button/Button";
import FormField from './../../forms/FormField/FormField';
import "./CreateNoteModal.css";

const CreateNoteModal = ({ isOpen, closeModal, saveNote }) => {
  const root = "create-note-modal";
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [showModal, setShowModal] = useState(false);

  useEffect(() => {
    if (isOpen) {
      setTitle("");
      setContent("");
      // Trigger animation after component mounts
      requestAnimationFrame(() => {
        setShowModal(true);
      });
    } else {
      setShowModal(false);
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
    <div className={`${root}-overlay ${showModal ? 'show' : ''}`}>
      <div className={`${root}-content ${showModal ? 'show' : ''}`}>
        <div className={`${root}-close`}>
          <Button
            icon={<FaTimes />}
            iconOnly
            noBackground
            variant="danger"
            onClick={closeModal}
          />
        </div>
        
        <h2 className={`${root}-header`}>Create New Note</h2>
      
        <FormField 
          type={"text"}
          placeholder={"Enter note title"}
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />

        <div className={`${root}-editor-container`}>
          <MDEditor value={content} onChange={setContent} height="100%" />
        </div>

        <div className={`${root}-actions`}>
          <Button 
            text="Cancel"
            variant="secondary"
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

import MDEditor from "@uiw/react-md-editor";
import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom";
import { FaTimes } from "react-icons/fa";
import Button from "../../buttons/Button/Button";
import "./NoteEditorModal.css";

const NoteEditor = ({ 
  selectedNote, 
  editorContent, 
  setEditorContent, 
  handleSaveNote, 
  closeNoteEditor 
}) => {
  const root = "note-editor-modal";
  const [showModal, setShowModal] = useState(false);

  useEffect(() => {
    if (selectedNote) {
      requestAnimationFrame(() => {
        setShowModal(true);
      });
    }
  }, [selectedNote]);

  if (!selectedNote) return null;

  return ReactDOM.createPortal(
    <div className={`${root}-overlay ${showModal ? 'show' : ''}`}>
      <div className={`${root}-content ${showModal ? 'show' : ''}`}>
        <div className={`${root}-close`}>
          <Button
            icon={<FaTimes />}
            iconOnly
            noBackground
            variant="danger"
            onClick={closeNoteEditor}
          />
        </div>
        
        <div className={`${root}-header`}>
          <h3>{selectedNote.title}</h3>
        </div>
        
        <div className={`${root}-editor`}>
          <MDEditor
            value={editorContent}
            onChange={(val) => setEditorContent(val?.toString() || "")}
            height="100%"
          />
        </div>
        
        <div className={`${root}-actions`}>
          <Button
            text="Cancel"
            variant="secondary"
            onClick={closeNoteEditor}
          />
          <Button 
            text="Save"
            variant="success"
            onClick={handleSaveNote}
          />
        </div>
      </div>
    </div>,
    document.body
  );
};

export default NoteEditor;

import MDEditor from "@uiw/react-md-editor";
import React from "react";
import ReactDOM from "react-dom";

import Button from "../../buttons/Button/Button";
import "./NoteEditorModal.css";

const NoteEditor = ({ selectedNote, editorContent, setEditorContent, handleSaveNote, closeNoteEditor }) => {
  const root = "note-modal";

  if (!selectedNote) return null;

  return ReactDOM.createPortal(
    <div className={`${root}-overlay`}>
      <div className={`${root}-overlay-content`}>
        <div className={`${root}-header`}>
          <h2>{selectedNote.title}</h2>
        </div>
        
        <div className="editor-container">
          <MDEditor
            value={editorContent}
            onChange={(val) => setEditorContent(val?.toString() || "")}
            height="100%"
          />
        </div>
        <div className={`${root}-buttons`}>
            <Button
              text="Cancel"
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

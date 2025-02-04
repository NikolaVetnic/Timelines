import MDEditor from "@uiw/react-md-editor";
import React from "react";
import ReactDOM from "react-dom";
import "./NoteEditorModal.css";

const NoteEditor = ({ selectedNote, editorContent, setEditorContent, handleSaveNote, closeNoteEditor }) => {
  if (!selectedNote) return null;

  return ReactDOM.createPortal(
    <div className="note-overlay">
      <div className="note-overlay-content">
        <div className="note-header">
          <h2>{selectedNote.title}</h2>
          <div className="note-buttons">
            <button onClick={handleSaveNote}>Save</button>
            <button onClick={closeNoteEditor}>Close</button>
          </div>
        </div>
        
        <div className="editor-container">
          <MDEditor
            value={editorContent}
            onChange={(val) => setEditorContent(val?.toString() || "")}
            height="100%"
          />
        </div>
      </div>
    </div>,
    document.body
  );
};

export default NoteEditor;

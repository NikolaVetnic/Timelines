import MDEditor from "@uiw/react-md-editor";
import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom";
import Button from "../../buttons/Button/Button";
import FormField from "../../forms/FormField/FormField";
import "./NoteEditorModal.css";

const NoteEditor = ({
  selectedNote,
  editorContent,
  setEditorContent,
  handleSaveNote,
  closeNoteEditor,
}) => {
  const root = "note-editor-modal";
  const [showModal, setShowModal] = useState(false);
  const [title, setTitle] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    if (selectedNote) {
      setTitle(selectedNote.title || "");
      setEditorContent(selectedNote.content || "");
      requestAnimationFrame(() => {
        setShowModal(true);
      });
    }
  }, [selectedNote, setEditorContent]);

  const handleTitleChange = (e) => {
    setTitle(e.target.value);
    if (error) setError("");
  };

  const handleSave = () => {
    if (!title.trim()) {
      setError("Title is required");
      return;
    }
    handleSaveNote(title.trim(), editorContent);
  };

  if (!selectedNote) return null;

  return ReactDOM.createPortal(
    <div className={`${root}-overlay ${showModal ? "show" : ""}`}>
      <div className={`${root}-content ${showModal ? "show" : ""}`}>
        <div className={`${root}-title`}><h3>Edit Note</h3></div>
        <div className={`${root}-header`}>
          <FormField
          className={`${root}-form-field`}
            label="Note Title"
            type="text"
            name="title"
            value={title}
            onChange={handleTitleChange}
            placeholder="Enter note title"
            required
            error={error}
          />
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
            size="small"
            onClick={closeNoteEditor}
          />
          <Button
            text="Save"
            variant="success"
            size="small"
            onClick={handleSave}
            disabled={!title.trim()}
          />
        </div>
      </div>
    </div>,
    document.body
  );
};

export default NoteEditor;

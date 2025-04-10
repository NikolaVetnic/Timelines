import MDEditor from "@uiw/react-md-editor";
import React from "react";
import ReactDOM from "react-dom";

import "./NoteEditorModal.css";

const NoteEditor = ({
    selectedNote,
    editorContent,
    setEditorContent,
    handleSaveNote,
    closeNoteEditor,
}) => {
    const root = "note-modal";

    if (!selectedNote) return null;

    return ReactDOM.createPortal(
        <div className={`${root}-overlay`}>
            <div className={`${root}-overlay-content`}>
                <div className={`${root}-header`}>
                    <h2>{selectedNote.title}</h2>
                    <div className={`${root}-buttons`}>
                        <button onClick={handleSaveNote}>Sačuvati</button>
                        <button onClick={closeNoteEditor}>Zatvoriti</button>
                    </div>
                </div>

                <div className="editor-container">
                    <MDEditor
                        value={editorContent}
                        onChange={(val) =>
                            setEditorContent(val?.toString() || "")
                        }
                        height="100%"
                    />
                </div>
            </div>
        </div>,
        document.body
    );
};

export default NoteEditor;

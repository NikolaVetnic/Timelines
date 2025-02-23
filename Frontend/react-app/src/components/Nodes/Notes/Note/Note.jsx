import React, { useState } from "react";

import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import RemoveButton from "../../../../core/components/buttons/RemoveButton/RemoveButton";
import CreateNoteModal from "../../../../core/components/modals/CreateNoteModal/CreateNoteModal";
import NoteEditor from "../../../../core/components/modals/NoteEditorModal/NoteEditorModal";
import useLocalNotes from "../../../../core/hooks/Note/UseLocalNotes";

import "./Note.css";

const Note = ({ nodeId, timelineId, onToggle }) => {
  const { notes, setNotes, updateLocalStorage } = useLocalNotes(timelineId, nodeId);
  const [isNotesExpanded, setIsNotesExpanded] = useState(false);
  const [selectedNote, setSelectedNote] = useState(null);
  const [editorContent, setEditorContent] = useState("");
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);

  const toggleNotesSection = () => {
    setIsNotesExpanded(prev => !prev);
    onToggle();
  };

  // todo: connect to backend
  const handleSaveNote = () => {
    if (selectedNote) {
      const updatedNotes = notes.map(note =>
        note.id === selectedNote.id ? { ...note, content: editorContent } : note
      );
      setNotes(updatedNotes);
      updateLocalStorage(updatedNotes);
    }
    closeNoteEditor();
  };

  const handleRemoveNote = (id) => {
    const updatedNotes = notes.filter(note => note.id !== id);
    setNotes(updatedNotes);
    updateLocalStorage(updatedNotes);
    if (selectedNote?.id === id) {
      setSelectedNote(null);
    }
    setTimeout(onToggle, 0);
  };

  const openNoteEditor = (note) => {
    setSelectedNote(note);
    setEditorContent(note.content || "");
  };

  const closeNoteEditor = () => setSelectedNote(null);

  const openCreateModal = (e) => {
    e.stopPropagation();
    setCreateModalOpen(true);
  };

  const closeCreateModal = () => setCreateModalOpen(false);

  const saveNewNote = (newNote) => {
    const updatedNotes = [...notes, newNote];
    setTimeout(onToggle, 0);
    setNotes(updatedNotes);
    updateLocalStorage(updatedNotes);
  };

  return (
    <div className="notes-section">
      <button
        className={`notes-header ${isNotesExpanded ? "notes-header-opened" : "notes-header-closed"}`}
        onClick={toggleNotesSection}
      >
        <h4>Notes</h4>
        <span>{isNotesExpanded ? "-" : "+"}</span>
      </button>

      {isNotesExpanded && (
        <div className="notes-container">
          <button className="add-note-button" onClick={openCreateModal}>+</button>
          {notes.length > 0 ? (
            notes.map((note) => (
              <div key={note.id} className="note-item">
                <div className="note-content">
                  <p>{note.title}</p>
                  <div className="note-content-button-area">
                    <RemoveButton id={note.id} onRemove={() => handleRemoveNote(note.id)} message="Note removed." />
                    <EditButton onClick={() => openNoteEditor(note)} />
                  </div>
                </div>
              </div>
            ))
          ) : (
            <p>There are no available notes.</p>
          )}
        </div>
      )}

      <CreateNoteModal isOpen={isCreateModalOpen} closeModal={closeCreateModal} saveNote={saveNewNote} />
      <NoteEditor
        selectedNote={selectedNote}
        editorContent={editorContent}
        setEditorContent={setEditorContent}
        handleSaveNote={handleSaveNote}
        closeNoteEditor={closeNoteEditor}
      />
    </div>
  );
};

export default Note;

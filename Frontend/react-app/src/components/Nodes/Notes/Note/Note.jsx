import React, { useState } from "react";

import NotesList from "../../../../core/components/lists/NotesList/NotesList";
import CreateNoteModal from "../../../../core/components/modals/CreateNoteModal/CreateNoteModal";
import NoteEditor from "../../../../core/components/modals/NoteEditorModal/NoteEditorModal";
import useLocalNotes from "../../../../core/hooks/Note/UseLocalNotes";

import DeleteModal from "../../../../core/components/modals/DeleteModal/DeleteModal";
import "./Note.css";

const Note = ({ nodeId, timelineId, onToggle }) => {
  const root = "note";
  const { notes, setNotes, updateLocalStorage } = useLocalNotes(
    timelineId,
    nodeId
  );
  const [isNotesExpanded, setIsNotesExpanded] = useState(false);
  const [selectedNote, setSelectedNote] = useState(null);
  const [noteToDelete, setNoteToDelete] = useState(null);
  const [editorContent, setEditorContent] = useState("");
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);

  const toggleNotesSection = () => {
    setIsNotesExpanded((prev) => !prev);
    onToggle();
  };

  // todo: connect to backend when it is ready
  const handleSaveNote = () => {
    if (selectedNote) {
      const updatedNotes = notes.map((note) =>
        note.id === selectedNote.id ? { ...note, content: editorContent } : note
      );
      setNotes(updatedNotes);
      updateLocalStorage(updatedNotes);
    }
    closeNoteEditor();
  };

  const handleRemoveNote = (id) => {
    const note = notes.find((n) => n.id === id);
    setNoteToDelete(note);
    setIsDeleteModalOpen(true);
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

  const confirmDelete = () => {
    if (noteToDelete) {
      const updatedNotes = notes.filter((note) => note.id !== noteToDelete.id);
      setNotes(updatedNotes);
      updateLocalStorage(updatedNotes);

      if (selectedNote?.id === noteToDelete.id) {
        setSelectedNote(null);
      }

      setIsDeleteModalOpen(false);
      setNoteToDelete(null);
      setTimeout(onToggle, 0);
    }
  };

  const cancelDelete = () => {
    setIsDeleteModalOpen(false);
    setNoteToDelete(null);
  };

  return (
    <div className={`${root}-section`}>
      {/*This is a special button */}
      <button
        className={`${root}-header ${root}-${
          isNotesExpanded ? "header-opened" : "header-closed"
        }`}
        onClick={toggleNotesSection}
      >
        <h4>Notes</h4>
        <span>{isNotesExpanded ? "-" : "+"}</span>
      </button>

      {isNotesExpanded && (
        <NotesList
          root={root}
          notes={notes}
          openCreateModal={openCreateModal}
          handleRemoveNote={handleRemoveNote}
          openNoteEditor={openNoteEditor}
        />
      )}

      <DeleteModal
        isOpen={isDeleteModalOpen}
        onClose={cancelDelete}
        onConfirm={confirmDelete}
        itemTitle={noteToDelete?.title || "Untitled Note"}
      />
      <CreateNoteModal
        isOpen={isCreateModalOpen}
        closeModal={closeCreateModal}
        saveNote={saveNewNote}
      />
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

import React, { useEffect, useState } from "react";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import CreateNoteModal from "../../../../core/components/modals/CreateNoteModal/CreateNoteModal";
import NoteEditor from "../../../../core/components/modals/NoteEditorModal/NoteEditorModal";
import "./Note.css";

const LOCAL_STORAGE_KEY = "timelineData";

const Note = ({ nodeId, timelineId, onToggle }) => {
  const [isNotesExpanded, setIsNotesExpanded] = useState(false);
  const [selectedNote, setSelectedNote] = useState(null);
  const [editorContent, setEditorContent] = useState(""); 
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [notes, setNotes] = useState([]);

  useEffect(() => {
    const storedData = localStorage.getItem(LOCAL_STORAGE_KEY);
    if (storedData) {
      const parsedData = JSON.parse(storedData);
      const timeline = parsedData.find(t => t.id === timelineId);
      const node = timeline?.nodes.find(n => n.id === nodeId);
      if (node) {
        setNotes(node.notes || []);
      }
    }
  }, [timelineId, nodeId]);

  const updateLocalStorage = (updatedNotes) => {
    const storedData = localStorage.getItem(LOCAL_STORAGE_KEY);
    if (storedData) {
      const parsedData = JSON.parse(storedData);
      const timelineIndex = parsedData.findIndex(t => t.id === timelineId);
      if (timelineIndex !== -1) {
        const nodeIndex = parsedData[timelineIndex].nodes.findIndex(n => n.id === nodeId);
        if (nodeIndex !== -1) {
          parsedData[timelineIndex].nodes[nodeIndex].notes = updatedNotes;
          localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(parsedData));
        }
      }
    }
  };

  const toggleNotesSection = () => {
    setIsNotesExpanded(prev => !prev);
    onToggle();
  };

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
    setTimeout(() => onToggle(), 0);
  };

  const openNoteEditor = (note) => {
    setSelectedNote(note);
    setEditorContent(note.content || ""); 
  };

  const closeNoteEditor = () => {
    setSelectedNote(null);
  };

  const openCreateModal = (e) => {
    e.stopPropagation();
    setCreateModalOpen(true);
  };

  const closeCreateModal = () => {
    setCreateModalOpen(false);
  };

  const saveNewNote = (newNote) => {
    const updatedNotes = [...notes, newNote];
    setNotes(updatedNotes);
    updateLocalStorage(updatedNotes);
  };

  return (
    <div className="notes-section">
      <button
      className={`notes-header ${
        isNotesExpanded ? "notes-header-opened" : "notes-header-closed"
      }`}
      onClick={toggleNotesSection}
    >
      <h4>Notes</h4>
      <span>{isNotesExpanded ? "-" : "+"}</span>
    </button>

      {isNotesExpanded && (
        <div className="notes-container">
          <button className="add-note-button" onClick={openCreateModal}>+</button>
          {notes.map((note) => (
            <div key={note.id} className="note-item">
              <div className="note-content">
                <p>{note.title}</p>
                <div className="note-content-button-area">
                  <button className="remove-note-button" onClick={() => handleRemoveNote(note.id)}>✖</button>
                  <EditButton onClick={() => openNoteEditor(note)} />
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

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

import React, { useCallback, useEffect, useState } from "react";
import NotesList from "../../../../core/components/lists/NotesList/NotesList";
import CreateNoteModal from "../../../../core/components/modals/CreateNoteModal/CreateNoteModal";
import DeleteModal from "../../../../core/components/modals/DeleteModal/DeleteModal";
import NoteEditor from "../../../../core/components/modals/NoteEditorModal/NoteEditorModal";
import Pagination from "../../../../core/components/pagination/Pagination";
import NoteService from "../../../../services/NoteService";
import "./Note.css";

const Note = ({ nodeId, onToggle }) => {
  const root = "note";
  const [notes, setNotes] = useState([]);
  const [isNotesExpanded, setIsNotesExpanded] = useState(false);
  const [selectedNote, setSelectedNote] = useState(null);
  const [noteToDelete, setNoteToDelete] = useState(null);
  const [editorContent, setEditorContent] = useState("");
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  
  const [currentPage, setCurrentPage] = useState(0);
  const [itemsPerPage, setItemsPerPage] = useState(2);
  const [totalCount, setTotalCount] = useState(0);
  const itemsPerPageOptions = [2, 4, 6, 8];

  const fetchNotes = useCallback(async () => {
    if (isNotesExpanded && nodeId) {
      setIsLoading(true);
      try {
        const notesData = await NoteService.getNotesByNode(
          nodeId,
          currentPage,
          itemsPerPage
        );
        setNotes(notesData.items || []);
        setTotalCount(notesData.totalCount || 0);
      } finally {
        setIsLoading(false);
      }
    }
  }, [isNotesExpanded, nodeId, currentPage, itemsPerPage]);

  useEffect(() => {
    fetchNotes();
  }, [fetchNotes]);

  const totalPages = Math.ceil(totalCount / itemsPerPage) || 1;

  const handlePageChange = (page) => {
    setCurrentPage(page - 1);
  };

  const handleItemsPerPageChange = (size) => {
    setItemsPerPage(size);
    setCurrentPage(0);
  };

  const toggleNotesSection = () => {
    if (!isNotesExpanded) {
      fetchNotes();
    }
    setIsNotesExpanded((prev) => !prev);
    onToggle();
  };

  const handleSaveNote = async () => {
    if (selectedNote) {
      setIsLoading(true);
      try {
        await NoteService.updateNote(selectedNote.id, { content: editorContent });
        await fetchNotes();
        closeNoteEditor();
      } finally {
        setIsLoading(false);
      }
    }
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

  const saveNewNote = async (newNote) => {
    try {
      setIsLoading(true);
      const noteWithNodeId = { 
        ...newNote, 
        nodeId,
        isPublic: false,
        sharedWith: [],
        owner: "current-user-id"
      };
      
      await NoteService.createNote(noteWithNodeId);
      await fetchNotes();
      closeCreateModal();
      onToggle();
    } finally {
      setIsLoading(false);
    }
  };

  const confirmDelete = async () => {
    if (!noteToDelete) return;

    try {
      setIsLoading(true);
      await NoteService.deleteNote(noteToDelete.id);
      await fetchNotes();
      
      if (selectedNote?.id === noteToDelete.id) {
        setSelectedNote(null);
      }

      setIsDeleteModalOpen(false);
      setNoteToDelete(null);
      onToggle();
    } finally {
      setIsLoading(false);
    }
  };

  const cancelDelete = () => {
    setIsDeleteModalOpen(false);
    setNoteToDelete(null);
  };

  return (
    <div className={`${root}-section`}>
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
        <div className={`${root}-content`}>
          {isLoading ? (
            <div className={`${root}-loading`}>Loading notes...</div>
          ) : (
            <>
              <NotesList
                root={root}
                notes={notes}
                openCreateModal={openCreateModal}
                handleRemoveNote={handleRemoveNote}
                openNoteEditor={openNoteEditor}
              />
              {totalCount > 0 && (
                <Pagination
                  currentPage={currentPage + 1}
                  totalPages={totalPages}
                  itemsPerPage={itemsPerPage}
                  onPageChange={handlePageChange}
                  onItemsPerPageChange={handleItemsPerPageChange}
                  itemsPerPageOptions={itemsPerPageOptions}
                />
              )}
            </>
          )}
        </div>
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

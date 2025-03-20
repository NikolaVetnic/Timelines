import React from "react";
import { CiEdit } from "react-icons/ci";

import IconButton from "../../buttons/IconButton/IconButton";
import TextButton from "../../buttons/TextButton/TextButton";

const NotesList = ({ root, notes, openCreateModal, handleRemoveNote, openNoteEditor }) => {
  return (
    <div className={`${root}-container`}>
      <button className={`${root}-add-button`} onClick={openCreateModal}>+</button>
      {notes.length > 0 ? (
        notes.map((note) => (
          <div key={note.id} className={`${root}-item`}>
            <div className={`${root}-content`}>
              <p>{note.title}</p>
              <div className={`${root}-content-button-area`}>
                <TextButton onClick={() => handleRemoveNote(note.id)} text="X" color="red" />
                <IconButton onClick={() => openNoteEditor(note)} icon={<CiEdit />} title="Edit" />
              </div>
            </div>
          </div>
        ))
      ) : (
        <p>There are no available notes.</p>
      )}
    </div>
  );
};

export default NotesList;

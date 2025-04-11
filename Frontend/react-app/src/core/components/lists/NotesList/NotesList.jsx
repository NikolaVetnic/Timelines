import { format } from 'date-fns'; // You'll need to install date-fns
import React from "react";
import { CiEdit } from "react-icons/ci";
import { IoIosAdd } from "react-icons/io";
import { MdDelete } from "react-icons/md";
import Button from "../../buttons/Button/Button";

const NotesList = ({ root, notes, openCreateModal, handleRemoveNote, openNoteEditor }) => {
  return (
    <div className={`${root}-container`}>
      <div className={`${root}-inner-header`}>
        <h4>Your Notes</h4>
        <Button 
          icon={<IoIosAdd />} 
          iconOnly
          variant="primary" 
          shape="square"
          size="little"
          onClick={openCreateModal}
          className={`${root}-add-button`}
        />
      </div>

      {notes.length > 0 ? (
        <div className={`${root}-grid`}>
          {notes.map((note) => (
            <div key={note.id} className={`${root}-card`}>
              <div className={`${root}-card-content`}>
                <h5 className={`${root}-card-title`}>{note.title}</h5>
                {note.createdAt && (
                  <p className={`${root}-card-date`}>
                    {format(new Date(note.createdAt), 'MMM dd, yyyy - h:mm a')}
                  </p>
                )}
                <p className={`${root}-card-preview`}>
                  {note.content?.substring(0, 100)}{note.content?.length > 100 ? '...' : ''}
                </p>
              </div>
              <div className={`${root}-card-actions`}>
                <Button 
                  icon={<CiEdit />} 
                  iconOnly
                  variant="info" 
                  shape="square"
                  size="little"
                  onClick={() => openNoteEditor(note)}
                />
                <Button 
                  icon={<MdDelete />} 
                  iconOnly
                  variant="danger" 
                  shape="square"
                  size="little"
                  onClick={() => handleRemoveNote(note.id)}
                />
              </div>
            </div>
          ))}
        </div>
      ) : (
        <div className={`${root}-empty-state`}>
          <p>No notes yet. Create your first note!</p>
        </div>
      )}
    </div>
  );
};

export default NotesList;

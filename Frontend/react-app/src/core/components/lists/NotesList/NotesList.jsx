import React from "react";
import { CiEdit, CiTrash } from "react-icons/ci";

import IconButton from "../../buttons/IconButton/IconButton";

const NotesList = ({
    root,
    notes,
    openCreateModal,
    handleRemoveNote,
    openNoteEditor,
}) => {
    return (
        <div className={`${root}-container`}>
            <button className={`${root}-add-button`} onClick={openCreateModal}>
                ➕ Nova beleška
            </button>
            {notes.length > 0 ? (
                notes.map((note) => (
                    <div key={note.id} className={`${root}-item`}>
                        <div
                            className={`${root}-content`}
                            style={{ backgroundColor: "#e6e6e6" }}
                        >
                            <p>{note.title}</p>
                            <div className={`${root}-content-button-area`}>
                                <IconButton
                                    onClick={() => handleRemoveNote(note.id)}
                                    icon={<CiTrash />}
                                    title="Remove"
                                />
                                &nbsp;&nbsp;
                                <IconButton
                                    onClick={() => openNoteEditor(note)}
                                    icon={<CiEdit />}
                                    title="Edit"
                                />
                            </div>
                        </div>
                    </div>
                ))
            ) : (
                <p>Nema dostupnih beležaka</p>
            )}
        </div>
    );
};

export default NotesList;

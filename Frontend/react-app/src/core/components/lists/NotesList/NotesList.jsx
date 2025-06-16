import { format, parseISO } from "date-fns";
import { CiEdit } from "react-icons/ci";
import { IoIosAdd } from "react-icons/io";
import { MdDelete } from "react-icons/md";
import Button from "../../buttons/Button/Button";

const NotesList = ({
  root,
  notes,
  openCreateModal,
  handleRemoveNote,
  openNoteEditor,
}) => {
  const formatDate = (dateString) => {
    if (!dateString) return "N/A";
    try {
      return format(parseISO(dateString), "MMM dd, yyyy - h:mm a");
    } catch (e) {
      try {
        return format(new Date(dateString), "MMM dd, yyyy - h:mm a");
      } catch (e) {
        console.error("Failed to parse date:", dateString);
        return "Invalid date";
      }
    }
  };

  return (
    <div className={`${root}-container`}>
      <div className={`${root}-inner-header`}>
        <h4>Your Notes</h4>
        <Button
          icon={<IoIosAdd />}
          iconOnly
          variant="success"
          shape="square"
          size="little"
          onClick={openCreateModal}
        />
      </div>

      {notes.length > 0 ? (
        <div className={`${root}-grid`}>
          {notes.map((note) => (
            <div key={note.id} className={`${root}-card`}>
              <div className={`${root}-card-content`}>
                <h5 className={`${root}-card-title`}>{note.title}</h5>
                <p className={`${root}-card-preview`}>
                  Content: {note.content?.substring(0, 20)}{note.content?.length > 20 ? "..." : ""}
                </p>
                <div className={`${root}-card-dates`}>
                  <p className={`${root}-card-date`}>
                    Created: {formatDate(note.createdAt)}
                  </p>
                  {note.lastModifiedAt && note.createdAt !== note.lastModifiedAt && (
                    <p className={`${root}-card-date`}>
                      Modified: {formatDate(note.lastModifiedAt)}
                    </p>
                  )}
                </div>
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

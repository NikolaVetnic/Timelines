import React from "react";
import { CiTrash } from "react-icons/ci";

import IconButton from "../../buttons/IconButton/IconButton";

const RemindersList = ({
    root,
    reminders,
    openCreateModal,
    handleRemoveReminder,
}) => {
    return (
        <div className={`${root}-container`}>
            <button className={`${root}-add-button`} onClick={openCreateModal}>
                ➕ Novi podsetnik
            </button>
            {reminders.length > 0 ? (
                reminders.map((reminder) => (
                    <div key={reminder.id} className={`${root}-item`}>
                        <div className={`${root}-content`}>
                            <p>{reminder.title}</p>
                            <p>
                                <strong>Vreme:</strong>{" "}
                                {new Date(reminder.notifyAt).toLocaleString()}
                            </p>
                            <p>
                                <strong>Prioritet:</strong> {reminder.priority}
                            </p>
                        </div>
                        <IconButton
                            onClick={handleRemoveReminder}
                            icon={<CiTrash />}
                            title="Remove"
                        />
                    </div>
                ))
            ) : (
                <p>Nema dostupnih podsetnika</p>
            )}
        </div>
    );
};

export default RemindersList;

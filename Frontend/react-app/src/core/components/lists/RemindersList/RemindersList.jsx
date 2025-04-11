import React from "react";

const RemindersList = ({ root, reminders, openCreateModal, handleRemoveReminder }) => {
  return (
    <div className={`${root}-container`}>
      <button className={`${root}-add-button`} onClick={openCreateModal}>+</button>
      {reminders.length > 0 ? (
        reminders.map((reminder) => (
          <div key={reminder.id} className={`${root}-item`}>
            <div className={`${root}-content`}>
              <p>{reminder.title}</p>
              <p>
                <strong>Notify At:</strong> {new Date(reminder.notifyAt).toLocaleString()}
              </p>
              <p>
                <strong>Priority:</strong> {reminder.priority}
              </p>
            </div>
            {/* <TextButton onClick={handleRemoveReminder} text="X" color="red" /> */}
          </div>
        ))
      ) : (
        <p>There are no current reminders.</p>
      )}
    </div>
  );
};

export default RemindersList;

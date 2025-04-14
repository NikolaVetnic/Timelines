import React from "react";
import { IoIosAdd } from "react-icons/io";
import Button from './../../buttons/Button/Button';

const RemindersList = ({ root, reminders, openCreateModal, handleRemoveReminder }) => {
  return (
    <div className={`${root}-container`}>
      <div className={`${root}-button`}>
        <Button
          icon={<IoIosAdd />}
          iconOnly
          variant="success"
          shape="square"
          size="little"
          onClick={openCreateModal}
        />
      </div>
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
          </div>
        ))
      ) : (
        <p>There are no current reminders.</p>
      )}
    </div>
  );
};

export default RemindersList;

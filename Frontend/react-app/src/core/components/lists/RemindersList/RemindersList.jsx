import { format } from "date-fns";
import React from "react";
import { IoIosAdd } from "react-icons/io";
import { MdDelete } from "react-icons/md";
import Button from './../../buttons/Button/Button';

const RemindersList = ({ 
  root, 
  reminders, 
  openCreateModal, 
  setReminderToDelete,
  setIsDeleteModalOpen
}) => {
  return (
    <div className={`${root}-container`}>
      <div className={`${root}-inner-header`}>
        <h4>Your Reminders</h4>
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
        <div className={`${root}-grid`}>
          {reminders.map((reminder) => (
            <div key={reminder.id} className={`${root}-card`}>
              <div className={`${root}-card-content`}>
                <h5 className={`${root}-card-title`}>{reminder.title}</h5>
                {reminder.notificationTime && (
                  <p className={`${root}-card-date`}>
                    {format(new Date(reminder.notificationTime), "MMM dd, yyyy - h:mm a")}
                  </p>
                )}
                <p className={`${root}-card-priority`}>
                  <strong>Priority:</strong> {reminder.priority}
                </p>
              </div>
              <div className={`${root}-card-actions`}>
                <Button
                  icon={<MdDelete />}
                  iconOnly
                  variant="danger"
                  shape="square"
                  size="little"
                  onClick={() => {
                    setReminderToDelete(reminder);
                    setIsDeleteModalOpen(true);
                  }}
                />
              </div>
            </div>
          ))}
        </div>
      ) : (
        <div className={`${root}-empty-state`}>
          <p>No reminders yet. Create your first reminder!</p>
        </div>
      )}
    </div>
  );
};

export default RemindersList;

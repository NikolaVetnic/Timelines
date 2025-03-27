import React, { useState } from "react";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Post from "../../../api/post";
import "./CreateTimelineModal.css";

const CreateTimelineModal = ({ onClose }) => {
  const [title, setTitle] = useState("");
//   const [description, setDescription] = useState("");

  const handleTitleChange = (e) => {
    setTitle(e.target.value);
  };

//   const handleDescriptionChange = (e) => {
//     setDescription(e.target.value);
//   };

  const handleSaveTimelineData = async () => {
    if (!title.trim()) { //|| !description.trim()
      toast.error("Please enter both title and description.");
      return;
    }

    const dataToPost = {
      Timeline: {
        Title: title,
        // Description: description,
      },
    };

    try {
      const apiUrl = "http://localhost:26001";
      await Post(apiUrl, "/Timelines", dataToPost);

      toast.success("Timeline created successfully!");
      onClose();
    } catch (error) {
      toast.error("Failed to create timeline.");
    }
  };

  return (
    <div className="create-timeline-modal">
      <div className="create-timeline-modal-content">
        <div className="create-timeline-modal-input">
          <label htmlFor="title">Title:</label>
          <input
            id="title"
            type="text"
            value={title}
            onChange={handleTitleChange}
            placeholder="Enter title"
          />
        </div>

        {/* <div className="create-timeline-modal-input">
          <label htmlFor="description">Description:</label>
          <input
            id="description"
            type="text"
            value={description}
            onChange={handleDescriptionChange}
            placeholder="Enter description"
          />
        </div> */}

        <div>
          <button
            className="create-timeline-modal-button-close"
            onClick={onClose}
          >
            Close
          </button>
          <button
            className="create-timeline-modal-button"
            onClick={handleSaveTimelineData}
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
};

export default CreateTimelineModal;

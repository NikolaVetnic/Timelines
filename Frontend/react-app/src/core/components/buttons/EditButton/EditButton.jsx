import React from "react";
import { CiEdit } from "react-icons/ci";
import "./EditButton.css";

const EditButton = ({ onClick }) => {
    return (
        <button className="edit-button" onClick={onClick} title="Edit">
            <CiEdit />
        </button>
    );
};

export default EditButton;

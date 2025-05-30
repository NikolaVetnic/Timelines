import { useState } from "react";
import Button from "../../buttons/Button/Button";
import FormField from "../../forms/FormField/FormField";
import "./CreatePhysicalPersonModal.css";

const INITIAL_PERSON_DATA = {
  firstName: "",
  middleName: "",
  lastName: "",
  parentName: "",
  birthDate: new Date().toISOString().slice(0, 10), // Default to today's date
  streetAddress: "",
  personalIdNumber: "",
  idCardNumber: "",
  emailAddress: "",
  phoneNumber: "",
  bankAccountNumber: "",
  comment: ""
};

const CreatePhysicalPersonModal = ({ isOpen, onClose, onSubmit }) => {
  const [personData, setPersonData] = useState(INITIAL_PERSON_DATA);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setPersonData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);
    try {
      await onSubmit(personData);
      onClose();
    } finally {
      setIsSubmitting(false);
    }
  };

  if (!isOpen) return null;

  return (
    <div className="create-physical-person-modal-overlay">
      <div className="create-physical-person-modal-content">
        <div className="create-physical-person-modal-header">
          <h3>Add Physical Person</h3>
          <button className="create-physical-person-modal-close-x" onClick={onClose}>
            &times;
          </button>
        </div>

        <form onSubmit={handleSubmit}>
          <FormField
            label="First Name"
            name="firstName"
            value={personData.firstName}
            onChange={handleChange}
            required
          />
          
          <FormField
            label="Middle Name"
            name="middleName"
            value={personData.middleName}
            onChange={handleChange}
          />
          
          <FormField
            label="Last Name"
            name="lastName"
            value={personData.lastName}
            onChange={handleChange}
            required
          />
          
          <FormField
            label="Parent Name"
            name="parentName"
            value={personData.parentName}
            onChange={handleChange}
          />
          
          <FormField
            label="Birth Date"
            type="date"
            name="birthDate"
            value={personData.birthDate}
            onChange={handleChange}
          />
          
          <FormField
            label="Personal ID Number"
            name="personalIdNumber"
            value={personData.personalIdNumber}
            onChange={handleChange}
          />

          <FormField
            label="Street Address"
            name="streetAddress"
            value={personData.streetAddress}
            onChange={handleChange}
          />

          <FormField
            label="Email Address"
            type="email"
            name="emailAddress"
            value={personData.emailAddress}
            onChange={handleChange}
          />

          <FormField
            label="Phone Number"
            type="tel"
            name="phoneNumber"
            value={personData.phoneNumber}
            onChange={handleChange}
          />

          <div className="create-physical-person-modal-actions">
            <Button 
              type="button" 
              onClick={onClose}
              disabled={isSubmitting}
            >
              Cancel
            </Button>
            <Button 
              type="submit" 
              variant="success"
              disabled={isSubmitting}
            >
              {isSubmitting ? "Creating..." : "Create"}
            </Button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default CreatePhysicalPersonModal;

import { useEffect, useState } from "react";
import { FaArrowLeft } from "react-icons/fa";
import { useMatches, useNavigate, useParams } from "react-router-dom";
import Button from "../../../core/components/buttons/Button/Button";
import PhysicalPersonService from "../../../services/PhysicalPersonService";
import "./PhysicalPersonDetails.css";

const PhysicalPersonDetails = () => {
  const { personId } = useParams();
  const navigate = useNavigate();
  const [person, setPerson] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  const matches = useMatches();
  const parentMatch = matches[matches.length - 2]; 

  useEffect(() => {
    const fetchPerson = async () => {
      try {
        setIsLoading(true);
        const data = await PhysicalPersonService.getPhysicalPersonById(personId);
        setPerson(data);
      } finally {
        setIsLoading(false);
      }
    };
    
    fetchPerson();
  }, [personId]);

  if (isLoading) return (
    <div className="loading-container">
      <div className="loading-spinner"></div>
      <p>Loading person details...</p>
    </div>
  );
  
  if (!person) return (
    <div className="not-found-container">
      <h2>Person not found</h2>
      <p>We couldn't find the person you're looking for.</p>
      <Button onClick={() => navigate(-1)}>Go Back</Button>
    </div>
  );

  return (
    <div className="physical-person-details">
      <div className="header-section">
        <Button
          className="back-button"
          icon={<FaArrowLeft />}
          iconOnly
          noBackground
          onClick={() => {
            if (parentMatch?.pathname) {
              navigate(parentMatch.pathname);
            } else {
              navigate(-1);
            }
          }}
        />
        <div className="person-header">
          <h2>{person.firstName} {person.lastName}</h2>
          <p className="person-subtitle">Personal ID: {person.personalIdNumber || 'Not specified'}</p>
        </div>
      </div>
      
      <div className="person-details-card">
        <div className="person-details-grid">
          <div className="detail-item">
            <span className="detail-label">Middle Name</span>
            <span className="detail-value">{person.middleName || '-'}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">Parent Name</span>
            <span className="detail-value">{person.parentName || '-'}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">Birth Date</span>
            <span className="detail-value">{person.birthDate || '-'}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">ID Card Number</span>
            <span className="detail-value">{person.idCardNumber || '-'}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">Email</span>
            <span className="detail-value">{person.emailAddress || '-'}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">Phone</span>
            <span className="detail-value">{person.phoneNumber || '-'}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">Address</span>
            <span className="detail-value">{person.streetAddress || '-'}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">Bank Account</span>
            <span className="detail-value">{person.bankAccountNumber || '-'}</span>
          </div>
          {person.comment && (
            <div className="detail-item full-width">
              <span className="detail-label">Comments</span>
              <span className="detail-value">{person.comment}</span>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default PhysicalPersonDetails;

  import { useCallback, useEffect, useState } from "react";
import { FaChevronLeft, FaChevronRight } from "react-icons/fa";
import { IoMdAdd } from "react-icons/io";
import { useNavigate } from "react-router";
import Button from "../../../core/components/buttons/Button/Button";
import CreatePhysicalPersonModal from "../../../core/components/modals/CreatePhysicalPerson/CreatePhysicalPersonModal";
import PhysicalPersonService from "../../../services/PhysicalPersonService";
import "./PhysicalPerson.css";

  const PhysicalPersonPanel = ({ timelineId }) => {
    const navigate = useNavigate();
    const [isOpen, setIsOpen] = useState(false);
    const [physicalPersons, setPhysicalPersons] = useState([]);
    const [showCreatePersonModal, setShowCreatePersonModal] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    const handleCreatePhysicalPerson = async (personData) => {
      try {
        setIsLoading(true);
        await PhysicalPersonService.createPhysicalPerson(personData, timelineId);
        const updatedPersons = await PhysicalPersonService.getPhysicalPersonsByTimelineWithoutPagination(timelineId);
        setPhysicalPersons(updatedPersons);
        setShowCreatePersonModal(false);
      } finally {
        setIsLoading(false);
      }
    };

  const fetchPhysicalPersons = useCallback(async () => {
      try {
        setIsLoading(true);
        const persons = await PhysicalPersonService.getPhysicalPersonsByTimelineWithoutPagination(timelineId);
        setPhysicalPersons(persons);
      } finally {
        setIsLoading(false);
      }
    }, [timelineId]);

    useEffect(() => {
      if (timelineId) {
        fetchPhysicalPersons();
      }
    }, [timelineId, fetchPhysicalPersons]);

    return (
      <>
        <button 
          className={`physical-person-toggle ${isOpen ? 'open' : ''}`}
          onClick={() => setIsOpen(!isOpen)}
          aria-label={isOpen ? 'Close panel' : 'Open panel'}
        >
          {isOpen ? 
            <FaChevronRight className="toggle-icon" /> : 
            <FaChevronLeft className="toggle-icon" />
          }
        </button>

        <div className={`physical-person-panel ${isOpen ? 'open' : ''}`}>
          <div className="physical-person-header">
            <h3>Physical Persons</h3>
            <Button
              icon={<IoMdAdd />}
              iconOnly
              onClick={() => setShowCreatePersonModal(true)}
              variant="success"
              tooltip="Add Physical Person"
              size="small"
              disabled={isLoading}
            />
          </div>
          
          <div className="physical-person-list">
          {isLoading ? (
              <p>Loading...</p>
          ) : physicalPersons.length > 0 ? (
              physicalPersons.map((person) => (
              <Button
                  key={person.id}
                  className="physical-person-button"
                  onClick={() => navigate(`/timelines/${timelineId}/physical-persons/${person.id}`)}
                  text={`${person.firstName} ${person.lastName}`}
                  variant="secondary"
                  fullWidth
              />
              ))
          ) : (
              <p>No physical persons added yet</p>
          )}
          </div>
        </div>

        <CreatePhysicalPersonModal
          isOpen={showCreatePersonModal}
          onClose={() => setShowCreatePersonModal(false)}
          onSubmit={handleCreatePhysicalPerson}
          isLoading={isLoading}
        />
      </>
    );
  };

  export default PhysicalPersonPanel;

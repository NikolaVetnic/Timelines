import { useState } from "react";
import { FaBug } from "react-icons/fa";
import { Outlet, useNavigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import BugReportModal from "../../core/components/modals/BugReportModal/BugReportModal";
import ReminderNotifier from "../../core/utils/ReminderNotifier";
import TimelineService from "../../services/TimelineService";
import HeaderBar from "../HeaderBar/HeaderBar";
import "./AppLayout.css";

function AppLayout() {
  const navigate = useNavigate();
  const [isBugReportOpen, setIsBugReportOpen] = useState(false);
  const [searchResults, setSearchResults] = useState([]);

  const handleSearch = async (query) => {
    if (query.trim() === "") {
      setSearchResults([]);
      return;
    }
    
    try {
      const response = await TimelineService.searchTimelines(query);
      setSearchResults(response.items);
    } catch (error) {
      console.error("Search error:", error);
      setSearchResults([]);
    }
  };

  const handleResultClick = (timeline) => {
    navigate(`/timelines/${timeline.id}`);
  };

  return (
    <div className="app-container">
      <ReminderNotifier />
      
      <HeaderBar 
        onSearch={handleSearch}
        searchResults={searchResults}
        onResultClick={handleResultClick}
      />

      <div className="app-content-wrapper">
        <div className="app-content">
          <div className="content-wrapper">
            <Outlet />
            <ToastContainer />
          </div>
        </div>
      </div>

      <button
        className="bug-report-button"
        onClick={() => setIsBugReportOpen(true)}
        aria-label="Report a bug"
      >
        <FaBug size={20} />
      </button>

      {isBugReportOpen && (
        <BugReportModal
          isOpen={isBugReportOpen}
          onClose={() => setIsBugReportOpen(false)}
        />
      )}
    </div>
  );
}

export default AppLayout;

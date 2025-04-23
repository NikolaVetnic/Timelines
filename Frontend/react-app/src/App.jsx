import React, { useState } from "react";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import BugReportModal from "./core/components/modals/BugReportModal/BugReportModal";
import { FaBug } from "react-icons/fa";
import TimelinePage from "./components/Pages/TimelinePage/TimelinePage";
import "./styles/App.css";

function App() {
  const [isBugReportOpen, setIsBugReportOpen] = useState(false);

  return (
    <div className="app-container">
      <ToastContainer />
      <div className="app-content">
        <TimelinePage />
      </div>

      <button
        className="bug-report-button"
        onClick={() => setIsBugReportOpen(true)}
        aria-label="Report a bug"
      >
        <FaBug size={20} />
      </button>

      {isBugReportOpen && (
        <BugReportModal setIsBugReportOpen={setIsBugReportOpen} />
      )}
    </div>
  );
}

export default App;

import { default as React, default as React, useState } from "react";
import { FaBug } from "react-icons/fa";
import { BrowserRouter, Route, Routes } from "react-router";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Timeline from "./components/Timelines/Timeline/Timeline";
import TimelineList from "./core/components/lists/TimelineList/TimelineList";
import BugReportModal from "./core/components/modals/BugReportModal/BugReportModal";
import ReminderNotifier from "./core/utils/ReminderNotifier";
import "./styles/App.css";

function App() {
  const [isBugReportOpen, setIsBugReportOpen] = useState(false);

  return (
    <BrowserRouter>
      <div className="app-container">
        <ReminderNotifier />

        <div className="app-content">
          <div className="content-wrapper">
            <Routes>
              <Route path="/timelines/:id" element={<Timeline />} />
              <Route path="/" element={<TimelineList />} />
            </Routes>
            <ToastContainer />
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
        <BugReportModal setIsBugReportOpen={setIsBugReportOpen} />
      )}
      </div>
    </BrowserRouter>
  );
}

export default App;

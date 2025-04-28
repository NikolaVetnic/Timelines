import { useState } from "react";
import { FaBug } from "react-icons/fa";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Timeline from "./components/Timelines/Timeline/Timeline";
import TimelineList from "./core/components/lists/TimelineList/TimelineList";
import BugReportModal from "./core/components/modals/BugReportModal/BugReportModal";
import ReminderNotifier from "./core/utils/ReminderNotifier";
import { AuthProvider } from "./context/AuthContext";
import AuthGuard from "./core/auth/AuthGuard";
import LoginPage from "./components/Pages/LoginPage";
import ErrorPage from "./components/Pages/ErrorPage";
import "./styles/App.css";

function ProtectedApp() {
  const [isBugReportOpen, setIsBugReportOpen] = useState(false);

  return (
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
        <BugReportModal
          isOpen={isBugReportOpen}
          onClose={() => setIsBugReportOpen(false)}
        />
      )}
    </div>
  );
}

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route
            path="/*"
            element={
              <AuthGuard>
                <ProtectedApp />
              </AuthGuard>
            }
          />
          <Route path="/error" element={<ErrorPage />} />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;

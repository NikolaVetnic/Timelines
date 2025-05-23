import { useState } from "react";
import { FaBug } from "react-icons/fa";
import {
  createBrowserRouter,
  createRoutesFromElements,
  Outlet,
  Route,
  RouterProvider,
} from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Breadcrumbs from "./components/Breadcrumbs/Breadcrumbs";
import ErrorPage from "./components/Pages/ErrorPage";
import LoginPage from "./components/Pages/LoginPage";
import Timeline from "./components/Timelines/Timeline/Timeline";
import { AuthProvider } from "./context/AuthContext";
import AuthGuard from "./core/auth/AuthGuard";
import TimelineList from "./core/components/lists/TimelineList/TimelineList";
import BugReportModal from "./core/components/modals/BugReportModal/BugReportModal";
import ReminderNotifier from "./core/utils/ReminderNotifier";
import "./styles/App.css";

function AppLayout() {
  const [isBugReportOpen, setIsBugReportOpen] = useState(false);

  return (
    <div className="app-container">
      <ReminderNotifier />
      <div className="app-content">
         <Breadcrumbs />
        <div className="content-wrapper">
          <Outlet />
          <ToastContainer />
        </div>
      </div>

      {/* this is a temporary button only for testing an application and reporting bugs to the developers */}
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

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route errorElement={<ErrorPage />}>
      <Route path="/login" element={<LoginPage />} />
      <Route
        path="/"
        element={<AuthGuard><AppLayout /></AuthGuard>}
        handle={{ crumb: () => "Home" }}
      >
        <Route
          index
          element={<TimelineList />}
        />
        <Route
          path="timelines/:id"
          element={<Timeline />}
          handle={{ crumb: ({ params, matches }) => `${params.id}` }}
        />
      </Route>
      <Route path="*" element={<ErrorPage />} />
    </Route>
  )
);

function App() {
  return (
    <AuthProvider>
      <RouterProvider router={router} />
    </AuthProvider>
  );
}

export default App;

import { useEffect, useState } from "react";
import { FaBug } from "react-icons/fa";
import { Outlet, useMatches, useNavigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import BugReportModal from "../../core/components/modals/BugReportModal/BugReportModal";
import ReminderNotifier from "../../core/utils/ReminderNotifier";
import PhysicalPersonService from "../../services/PhysicalPersonService";
import TimelineService from "../../services/TimelineService";
import Breadcrumbs from "../Breadcrumbs/Breadcrumbs";
import Footer from "../Footer/Footer";
import HeaderBar from "../HeaderBar/HeaderBar";
import "./AppLayout.css";

function AppLayout() {
  const navigate = useNavigate();
  const matches = useMatches();
  const [isBugReportOpen, setIsBugReportOpen] = useState(false);
  const [searchResults, setSearchResults] = useState([]);
  const [breadcrumbs, setBreadcrumbs] = useState([]);

  const footerHeight = process.env.REACT_APP_FOOTER === 'true' ? '150px' : '68px';

useEffect(() => {
  const buildBreadcrumbs = async () => {
    const crumbs = await Promise.all(
      matches
        .filter(match => match.handle?.crumb)
        .map(async (match) => {
          const crumbInfo = typeof match.handle.crumb === 'function' 
            ? match.handle.crumb({ params: match.params }) 
            : match.handle.crumb;

          if (match.pathname === '/') {
            return {
              ...crumbInfo,
              path: match.pathname
            };
          }

          if (match.params.id && crumbInfo.idParam === 'id') {
            try {
              const timeline = await TimelineService.getTimelineById(match.params.id);
              return {
                ...crumbInfo,
                title: timeline?.title || crumbInfo.title,
                path: match.pathname
              };
            } catch (error) {
              console.error("Failed to fetch timeline:", error);
              return crumbInfo;
            }
          }

          if (match.params.personId && crumbInfo.personIdParam === 'personId') {
            try {
              const person = await PhysicalPersonService.getPhysicalPersonById(match.params.personId);
              return {
                ...crumbInfo,
                title: person ? `${person.firstName} ${person.lastName}` : 'Person',
                path: match.pathname
              };
            } catch (error) {
              console.error("Failed to fetch person:", error);
              return crumbInfo;
            }
          }

          return crumbInfo;
        })
    );
    setBreadcrumbs(crumbs.filter(crumb => crumb));
  };

  buildBreadcrumbs();
}, [matches]);

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
        <div className="app-content" style={{
        display: 'flex',
        flexDirection: 'column',
        width: '900px',
        maxWidth: '100%',
        backgroundColor: '#fff',
        boxShadow: '0 4px 10px rgba(0, 0, 0, 0.1)',
        padding: '20px 20px 0 20px',
        height: `calc(100vh - ${footerHeight})`,
        overflowY: 'auto'
      }}>
          <Breadcrumbs crumbs={breadcrumbs} />
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
      <Footer />
    </div>
  );
}

export default AppLayout;

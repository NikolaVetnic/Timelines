import { useEffect, useRef, useState } from "react";
import { FaBars, FaBug, FaSignOutAlt, FaTimes } from "react-icons/fa";
import { Outlet, useNavigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useAuth } from "../../context/AuthContext";
import Button from "../../core/components/buttons/Button/Button";
import BugReportModal from "../../core/components/modals/BugReportModal/BugReportModal";
import ReminderNotifier from "../../core/utils/ReminderNotifier";
import SearchService from "../../services/SearchService";
import SearchBar from "../SearchBar/SearchBar";
import "./AppLayout.css";

function AppLayout() {
  const navigate = useNavigate();
  const [isBugReportOpen, setIsBugReportOpen] = useState(false);
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const [searchResults, setSearchResults] = useState([]);
  const [isMobile, setIsMobile] = useState(false);
  const { logout } = useAuth();
  const menuRef = useRef(null);

  useEffect(() => {
    const checkIfMobile = () => {
      setIsMobile(window.innerWidth <= 768);
    };
    
    checkIfMobile();
    window.addEventListener("resize", checkIfMobile);
    return () => window.removeEventListener("resize", checkIfMobile);
  }, []);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (menuRef.current && !menuRef.current.contains(event.target) && 
          !event.target.closest('.mobile-menu-button')) {
        setIsMobileMenuOpen(false);
      }
    };

    if (isMobileMenuOpen) {
      document.addEventListener('mousedown', handleClickOutside);
    } else {
      document.removeEventListener('mousedown', handleClickOutside);
    }

    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [isMobileMenuOpen]);

  const handleSearch = async (query) => {
    if (query.trim() === "") {
      setSearchResults([]);
      return;
    }
    
    try {
      const response = await SearchService.searchTimelines(query);
      setSearchResults(response.items || []);
    } catch (error) {
      console.error("Search error:", error);
      setSearchResults([]);
    }
  };

  const handleResultClick = (timeline) => {
    navigate(`/timelines/${timeline.id}`);
  };

  const toggleMobileMenu = () => {
    setIsMobileMenuOpen(!isMobileMenuOpen);
  };

  const closeMobileMenu = () => {
    setIsMobileMenuOpen(false);
  };

  const handleLogout = () => {
    logout();
    closeMobileMenu();
  };

  return (
    <div className="app-container">
      <ReminderNotifier />
      
      <header className="app-header">
        <div className="header-content">
          <h2 className="app-layout-list-title">Timelines</h2>
          <div className="search-container">
            <SearchBar 
              onSearch={handleSearch}
              searchResults={searchResults}
              onResultClick={handleResultClick}
            />
          </div>
          
          {isMobile ? (
            <>
              <button 
                className="mobile-menu-button"
                onClick={toggleMobileMenu}
                aria-label="Menu"
              >
                {isMobileMenuOpen ? "" : <FaBars />}
              </button>
              
              <div 
                className={`mobile-menu ${isMobileMenuOpen ? 'open' : ''}`}
                ref={menuRef}
              >
                <button 
                  className="mobile-menu-close"
                  onClick={closeMobileMenu}
                  aria-label="Close menu"
                >
                  <FaTimes />
                </button>
                <div className="mobile-menu-content">
                    <Button
                    text="Logout"
                    size="small"
                    onClick={handleLogout}
                    fullWidth
                    />
                </div>
              </div>
              {isMobileMenuOpen && (
                <div 
                  className="mobile-menu-overlay" 
                  onClick={closeMobileMenu}
                />
              )}
            </>
          ) : (
            <Button
              icon={<FaSignOutAlt />}
              iconOnly
              size="small"
              onClick={logout}
            />
          )}
        </div>
      </header>

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

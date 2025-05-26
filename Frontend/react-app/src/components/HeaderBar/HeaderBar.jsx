import { useEffect, useRef, useState } from "react";
import { FaBars, FaSignOutAlt, FaTimes } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import Button from "../../core/components/buttons/Button/Button";
import SearchBar from "../SearchBar/SearchBar";
import "./HeaderBar.css";

const HeaderBar = ({ onSearch, searchResults, onResultClick }) => {
  const { logout } = useAuth();
  const [isMobile, setIsMobile] = useState(false);
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const navigate = useNavigate(); 
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
    <header className="headbar">
      <h2 
        className="headbar-title"
        onClick={() => navigate('/')}
        style={{ cursor: 'pointer' }}
      >
        Timelines
      </h2>
      
      <div className="headbar-search">
        <SearchBar 
          onSearch={onSearch}
          searchResults={searchResults}
          onResultClick={onResultClick}
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
          className="headbar-logout"
        />
      )}
    </header>
  );
};

export default HeaderBar;

import { useState, useEffect } from "react";
import { FaSearch, FaTimes } from "react-icons/fa";
import Button from "../../core/components/buttons/Button/Button";
import "./SearchBar.css";

const SearchBar = ({ onSearch, searchResults, onResultClick }) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [isMobileSearchOpen, setIsMobileSearchOpen] = useState(false);
  const [isMobile, setIsMobile] = useState(false);

  useEffect(() => {
    const checkIfMobile = () => {
      setIsMobile(window.innerWidth <= 768);
    };
    checkIfMobile();
    window.addEventListener("resize", checkIfMobile);
    return () => window.removeEventListener("resize", checkIfMobile);
  }, []);

  const handleSearch = (e) => {
    e.preventDefault();
    onSearch(searchTerm);
  };

  const handleMobileSearchToggle = () => {
    setIsMobileSearchOpen(!isMobileSearchOpen);
    if (!isMobileSearchOpen) {
      setSearchTerm("");
    }
  };

  const handleResultClick = (timeline) => {
    onResultClick(timeline);
    setIsMobileSearchOpen(false);
    setSearchTerm("");
  };

  return (
    <>
      {isMobile ? (
        <>
          <Button
            icon={<FaSearch />}
            iconOnly
            size="small"
            onClick={handleMobileSearchToggle}
            className="mobile-search-toggle"
          />

          {isMobileSearchOpen && (
            <div className="mobile-search-modal">
              <div className="mobile-search-header">
                <form onSubmit={handleSearch} className="mobile-search-form">
                  <input
                    type="text"
                    placeholder="Search timelines..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    className="mobile-search-input"
                    autoFocus
                  />
                  <Button
                    icon={<FaTimes />}
                    iconOnly
                    size="small"
                    onClick={handleMobileSearchToggle}
                    className="mobile-search-close"
                  />
                </form>
              </div>

              {searchResults.length > 0 && (
                <div className="mobile-search-results">
                  {searchResults.map((timeline) => (
                    <div
                      key={timeline.id}
                      className="search-result-item"
                      onClick={() => handleResultClick(timeline)}
                    >
                      <h4>{timeline.title}</h4>
                      <p>{timeline.description || "No description"}</p>
                    </div>
                  ))}
                </div>
              )}
            </div>
          )}
        </>
      ) : (
        <form className="search-bar" onSubmit={handleSearch}>
          <div className="search-bar-input-container">
            <input
              type="text"
              placeholder="Search timelines..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="search-bar-input"
            />
          </div>
          <Button
            type="submit"
            size="small"
            icon={<FaSearch />}
            iconOnly
            className="search-bar-button"
          />
        </form>
      )}
    </>
  );
};

export default SearchBar;

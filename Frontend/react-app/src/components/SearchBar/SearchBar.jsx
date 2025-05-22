import { useEffect, useState } from "react";
import { FaSearch, FaTimes } from "react-icons/fa";
import Button from "../../core/components/buttons/Button/Button";
import "./SearchBar.css";

const SearchBar = ({ onSearch, searchResults, onResultClick }) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [isMobileSearchOpen, setIsMobileSearchOpen] = useState(false);
  const [isMobile, setIsMobile] = useState(false);
  const [showResults, setShowResults] = useState(false);

  useEffect(() => {
    const checkIfMobile = () => {
      setIsMobile(window.innerWidth <= 768);
    };
    checkIfMobile();
    window.addEventListener("resize", checkIfMobile);
    return () => window.removeEventListener("resize", checkIfMobile);
  }, []);

  const handleSearch = async (e) => {
    e.preventDefault();
    if (searchTerm.trim()) {
      await onSearch(searchTerm);
      setShowResults(true);
    }
  };

  const handleInputChange = async (e) => {
    setSearchTerm(e.target.value);
    if (e.target.value.trim()) {
      await onSearch(e.target.value);
      setShowResults(true);
    } else {
      setShowResults(false);
    }
  };

  const handleResultClick = (timeline) => {
    onResultClick(timeline);
    setSearchTerm("");
    setShowResults(false);
    if (isMobile) setIsMobileSearchOpen(false);
  };

  const handleMobileSearchToggle = () => {
    setIsMobileSearchOpen(!isMobileSearchOpen);
    if (!isMobileSearchOpen) {
      setSearchTerm("");
      setShowResults(false);
    }
  };

  const clearResults = () => {
    setSearchTerm("");
    setShowResults(false);
    onSearch("");
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
                    onChange={handleInputChange}
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

              {showResults && searchResults.length > 0 && (
                <div className="mobile-search-results">
                  <div className="search-results-header">
                    <span className="search-results-text">Search Results</span>
                    <button 
                      className="clear-results-button"
                      onClick={clearResults}
                    >
                      Clear
                    </button>
                  </div>
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
        <div className="desktop-search-container">
          <form className="search-bar" onSubmit={handleSearch}>
            <div className="search-bar-input-container">
              <input
                type="text"
                placeholder="Search timelines..."
                value={searchTerm}
                onChange={handleInputChange}
                className="search-bar-input"
              />
              <Button
                type="submit"
                size="small"
                icon={<FaSearch />}
                iconOnly
                disabled
                className="search-bar-button"
              />
            </div>
          </form>

          {showResults && searchResults.length > 0 && (
            <div className="desktop-search-results">
              <div className="search-results-header">
                <span className="search-results-text">Search Result</span>
                <button 
                  className="clear-results-button"
                  onClick={clearResults}
                >
                  Clear
                </button>
              </div>
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
  );
};

export default SearchBar;

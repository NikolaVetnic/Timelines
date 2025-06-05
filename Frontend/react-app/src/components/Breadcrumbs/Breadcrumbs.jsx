import { useEffect, useState } from "react";
import { Link } from "react-router";
import { useMatches, useNavigate } from "react-router-dom";
import "./Breadcrumbs.css";

const Breadcrumbs = ({ crumbs = [] }) => {
  const matches = useMatches();
  const navigate = useNavigate();
  const [isMobile, setIsMobile] = useState(false);

  useEffect(() => {
    const checkIsMobile = () => {
      setIsMobile(window.innerWidth <= 768);
    };
    checkIsMobile();
    window.addEventListener("resize", checkIsMobile);
    return () => window.removeEventListener("resize", checkIsMobile);
  }, []);

  const displayCrumbs = crumbs.length > 0 ? crumbs : 
    matches
      .filter(match => match.handle?.crumb)
      .map(match => ({
        ...(typeof match.handle.crumb === 'function' ? 
          match.handle.crumb({ params: match.params, matches }) : 
          match.handle.crumb),
        path: match.pathname
      }));

  if (displayCrumbs.length <= 1) return null;

  if (isMobile) {
    return (
      <div className="breadcrumb-dropdown-container">
        <select
          className="breadcrumb-select"
          value={displayCrumbs[displayCrumbs.length - 1].path}
          onChange={(e) => navigate(e.target.value)}
        >
          {displayCrumbs.map((crumb, idx) => (
            <option key={idx} value={crumb.path}>
              {crumb.title || crumb.name || 'Untitled'}
            </option>
          ))}
        </select>
      </div>
    );
  }

  return (
    <nav className="breadcrumb-nav">
      {displayCrumbs.map((crumb, idx) => {
        const isLast = idx === displayCrumbs.length - 1;
        return (
          <span key={idx} className={`breadcrumb-item ${isLast ? "active" : ""}`}>
            {!isLast ? (
              <Link to={crumb.path}>
                {crumb.title || crumb.name || 'Untitled'}
              </Link>
            ) : (
              crumb.title || crumb.name || 'Untitled'
            )}
            {!isLast && <span className="separator"> / </span>}
          </span>
        );
      })}
    </nav>
  );
};

export default Breadcrumbs;

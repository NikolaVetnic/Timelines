import { useEffect, useState } from "react";
import { Link, useMatches, useNavigate } from "react-router-dom";
import "./Breadcrumbs.css";

const Breadcrumbs = () => {
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

  const allBreadcrumbs = matches
    .filter((match) => match.handle?.crumb)
    .map((match) => {
      const crumb = typeof match.handle.crumb === "function"
        ? match.handle.crumb({ params: match.params, matches })
        : match.handle.crumb;

      return {
        name: crumb,
        path: match.pathname,
      };
    });

  if (allBreadcrumbs.length === 1 && allBreadcrumbs[0].path === "/") {
    return null;
  }

  const breadcrumbs = allBreadcrumbs.slice(-5);
  const lastIndex = breadcrumbs.length - 1;

  if (isMobile) {
    return (
      <div className="breadcrumb-dropdown-container">
        <select
          className="breadcrumb-select"
          value={breadcrumbs[lastIndex].path}
          onChange={(e) => navigate(e.target.value)}
        >
          {breadcrumbs.map(({ name, path }, idx) => (
            <option key={idx} value={path}>
              {name}
            </option>
          ))}
        </select>
      </div>
    );
  }

  return (
    <nav className="breadcrumb-nav">
      {breadcrumbs.map(({ name, path }, idx) => {
        const isLast = idx === lastIndex;
        return (
          <span key={idx} className={`breadcrumb-item ${isLast ? "active" : ""}`}>
            {!isLast ? <Link to={path}>{name}</Link> : name}
            {!isLast && <span className="separator"> / </span>}
          </span>
        );
      })}
    </nav>
  );
};

export default Breadcrumbs;

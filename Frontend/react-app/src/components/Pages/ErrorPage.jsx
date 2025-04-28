import { useRouteError, useNavigate } from "react-router-dom";
import { FaHome, FaRedo } from "react-icons/fa";
import "./PagesStyle/ErrorPage.css";

const ErrorPage = () => {
  const error = useRouteError();
  const navigate = useNavigate();

  // Determine if this is a 404 route error
  const isRouteError =
    error.status === 404 ||
    (error instanceof Error &&
      error.message.includes("The Page you are looking for does not exist."));

  return (
    <div className="error-page">
      <div className="error-illustration">
        <svg viewBox="0 0 200 200" xmlns="http://www.w3.org/2000/svg">
          <path
            fill="#FFEBEE"
            d="M40,-74.4C52.5,-69.2,63.8,-59.1,71.7,-46.3C79.6,-33.5,84,-18,83.5,-2.8C83,12.4,77.5,24.7,68.5,35.5C59.5,46.2,47,55.3,33.6,63.1C20.2,70.9,5.9,77.4,-8.8,78.3C-23.5,79.2,-47,74.5,-61.3,63.6C-75.6,52.7,-80.8,35.6,-81.5,18.8C-82.2,2,-78.4,-14.5,-70.1,-28.8C-61.8,-43.1,-49,-55.2,-35.6,-59.9C-22.2,-64.6,-8.3,-61.9,5.6,-57.9C19.5,-53.9,39,-48.6,40,-74.4Z"
            transform="translate(100 100)"
          />
          <text
            x="100"
            y="100"
            fontFamily="Arial"
            fontSize="40"
            fontWeight="bold"
            textAnchor="middle"
            fill="#D32F2F"
          >
            {isRouteError ? "404" : "!"}
          </text>
        </svg>
      </div>
      <h1>{isRouteError ? "Page Not Found" : "Oops! Something went wrong"}</h1>
      <div className="error-message">
        <p>
          {isRouteError
            ? "The page you're looking for doesn't exist."
            : error.statusText ||
              error.message ||
              "An unexpected error occurred"}
        </p>
        {error.status && !isRouteError && <p>Status code: {error.status}</p>}
      </div>
      <div className="error-actions">
        {!isRouteError && (
          <button
            className="error-retry-button"
            onClick={() => window.location.reload()}
          >
            <FaRedo /> Try Again
          </button>
        )}
        <button className="error-home-button" onClick={() => navigate("/")}>
          <FaHome /> Return Home
        </button>
      </div>
    </div>
  );
};

export default ErrorPage;

import { useRouteError } from "react-router-dom";
import "./PagesStyle/ErrorPage.css";

const ErrorPage = () => {
  const error = useRouteError();

  return (
    <div className="error-page">
      <h1>Oops!</h1>
      <p>Sorry, an unexpected error has occurred.</p>
      <p>
        <i>{error.statusText || error.message}</i>
      </p>
      <button onClick={() => window.location.reload()}>Retry</button>
    </div>
  );
};

export default ErrorPage;

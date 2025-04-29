import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import Button from "../../core/components/buttons/Button/Button";
import "./PagesStyle/LoginPage.css";

const LoginPage = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const { login, error, sessionExpired } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const success = await login({ username, password });
    if (success) {
      navigate("/");
    }
  };

  return (
    <div className="login-container">
      <h1>Login to Timelines</h1>

      {sessionExpired && (
        <div className="session-expired-message">
          <h3>Session Expired</h3>
          <p>Your session has expired. Please login again to continue.</p>
        </div>
      )}

      {error && (
        <div className="error-message">
          <p>{error}</p>
          <Button
            className="error-retry-button"
            icon={<FaRedo />}
            text="Try Again"
            variant="danger"
            onClick={() => window.location.reload()}
          />
        </div>
      )}

      <form className="login-form" onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="username">Username:</label>
          <input
            id="username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="password">Password:</label>
          <input
            id="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <button type="submit" className="login-button">
          Login
        </button>
      </form>
    </div>
  );
};

export default LoginPage;

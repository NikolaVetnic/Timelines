import { useState } from "react";
import { FaRedo } from "react-icons/fa";
import { useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import Button from "../../core/components/buttons/Button/Button";
import FormField from "../../core/components/forms/FormField/FormField";
import "./PagesStyle/LoginPage.css";

const INITIAL_FORM_DATA = {
  username: "",
  password: "",
};

const INITIAL_ERRORS = {
  username: "",
  password: "",
};

const LoginPage = () => {
  const [formData, setFormData] = useState(INITIAL_FORM_DATA);
  const [errors, setErrors] = useState(INITIAL_ERRORS);
  const { login, error: authError, sessionExpired } = useAuth();
  const location = useLocation();
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));

    if (errors[name]) {
      setErrors((prev) => ({ ...prev, [name]: "" }));
    }
  };

  const validateForm = () => {
    const newErrors = { ...INITIAL_ERRORS };
    let isValid = true;

    if (!formData.username.trim()) {
      newErrors.username = "Username is required";
      isValid = false;
    }

    if (!formData.password.trim()) {
      newErrors.password = "Password is required";
      isValid = false;
    }

    setErrors(newErrors);
    return isValid;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    if (!validateForm()) {
      return;
    }
  
    try {
      const success = await login(formData);
      if (success) {
        const from = location.state?.from?.pathname || "/";
        navigate(from, { replace: true });
      }
    } catch (error) {
      if (error.message.includes('Network Error')) {
        setErrors({ 
          ...errors, 
          form: 'Unable to connect to server. Please check your connection.' 
        });
      }
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

      {authError && (
        <div className="error-message">
          <p>{authError}</p>
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
        <FormField
          label="Username"
          type="text"
          name="username"
          value={formData.username}
          onChange={handleChange}
          required
          error={errors.username}
        />
        <FormField
          label="Password"
          type="password"
          name="password"
          value={formData.password}
          onChange={handleChange}
          required
          error={errors.password}
        />
        <Button type="submit" variant="login" text="Login" />
      </form>
    </div>
  );
};

export default LoginPage;

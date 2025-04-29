import { createContext, useContext, useState, useEffect } from "react";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [error, setError] = useState(null);
  const [sessionExpired, setSessionExpired] = useState(false);

  useEffect(() => {
    const checkTokenExpiration = () => {
      if (token && !isAuthenticated()) {
        handleLogout(true);
      }
    };

    const interval = setInterval(checkTokenExpiration, 1800000); // todo: find appropriate time frame for checking expired tokens currently it is set to 30 minutes
    return () => clearInterval(interval);
  }, [token]);

  const handleLogout = (expired = false) => {
    localStorage.removeItem("token");
    setToken(null);
    setUser(null);
    setSessionExpired(expired);
  };

  const login = async (credentials) => {
    try {
      setError(null);
      setSessionExpired(false);

      const mockUser = {
        id: "mock-user-123",
        username: credentials.username || "MockUser",
        name: credentials.name || "Mock User",
        role: credentials.role || "user",
      };

      const mockToken = `mock.${btoa(
        JSON.stringify({
          user: mockUser,
          exp: Math.floor(Date.now() / 1000) + 60 * 60, // 1 hour expiration
        })
      )}`;

      localStorage.setItem("token", mockToken);
      setToken(mockToken);
      setUser(mockUser);
      return true;
    } catch (err) {
      setError(err.message || "Login failed");
      return false;
    }
  };

  const logout = () => {
    handleLogout(false);
  };

  const isAuthenticated = () => {
    if (!token) return false;

    if (token.startsWith("mock.")) {
      try {
        const payload = JSON.parse(atob(token.split(".")[1]));
        return payload.exp > Math.floor(Date.now() / 1000);
      } catch {
        return false;
      }
    }

    return true;
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        token,
        error,
        sessionExpired,
        login,
        logout,
        isAuthenticated,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);

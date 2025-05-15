import { createContext, useContext, useEffect, useState } from "react";
import { Post } from "../core/api/post";
import API_BASE_URL from "../data/constants";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [error, setError] = useState(null);
  const [sessionExpired, setSessionExpired] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    const checkTokenExpiration = () => {
      if (token && !isAuthenticated()) {
        handleLogout(true);
      }
    };
    
    const interval = setInterval(checkTokenExpiration, 1800000);
    return () => clearInterval(interval);
  }, [token]);

useEffect(() => {
  const checkAuth = async () => {
    if (token && !isAuthenticated()) {
      const refreshed = await refreshToken();
      if (!refreshed) {
        handleLogout(true);
      }
    }
  };
  
  const interval = setInterval(checkAuth, 300000);
  return () => clearInterval(interval);
}, [token]);

const refreshToken = async () => {
  try {
    const refreshToken = localStorage.getItem("refresh_token");
    if (!refreshToken) return false;

    const data = await Post(API_BASE_URL, '/Auth/RefreshToken', {
      grant_type: "refresh_token",
      refresh_token: refreshToken
    }, true);

    if (data.access_token) {
      localStorage.setItem("token", data.access_token);
      setToken(data.access_token);
      
      const expiresAt = Date.now() + (data.expires_in || 3600) * 1000;
      localStorage.setItem("expires_at", expiresAt);

      return true;
    }

    return false;
  } catch {
    return false;
  }
};

const login = async (credentials) => {
  try {
    setIsLoading(true);
    setError(null);
    setSessionExpired(false);

    const requestData = {
      grant_type: "password",
      username: credentials.username,
      password: credentials.password,
      scope: "openid profile email offline_access api"
    };

    const data = await Post(API_BASE_URL, '/Auth/Token', requestData, true);
    
    if (!data.access_token) {
      throw new Error(data.error_description || 'Authentication failed');
    }

    localStorage.setItem("token", data.access_token);
    localStorage.setItem("refresh_token", data.refresh_token);
    
    const expiresAt = Date.now() + data.expires_in * 1000;
    localStorage.setItem("expires_at", expiresAt);

    let userInfo = {};
    if (data.id_token) {
      try {
        const idTokenParts = data.id_token.split('.');
        if (idTokenParts.length === 3) {
          const payload = JSON.parse(
            atob(
              idTokenParts[1].replace(/-/g, '+').replace(/_/g, '/')
            )
          );
          userInfo.username = payload.username || credentials.username;
        }
      } catch (error) {
        console.error('Error decoding id_token:', error);
        userInfo.username = credentials.username;
      }
    } else {
      userInfo.username = credentials.username;
    }

    setToken(data.access_token);
    setUser(userInfo);

    return true;
  } catch (err) {
    setError(err.error_description || err.message || "Login failed");
    return false;
  } finally {
    setIsLoading(false);
  }
};

const isAuthenticated = () => {
  const expiresAt = localStorage.getItem("expires_at");
  const storedToken = localStorage.getItem("token");
  
  return !!storedToken && !!expiresAt && Date.now() < Number(expiresAt);
};

  const logout = () => {
    handleLogout(false);
  };

  const handleLogout = (expired = false) => {
    localStorage.removeItem("token");
    setToken(null);
    setUser(null);
    setSessionExpired(expired);
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        token,
        error,
        sessionExpired,
        isLoading,
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

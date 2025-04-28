// import { createContext, useContext, useState, useEffect } from "react";
// import { useNavigate } from "react-router-dom";
// import jwt_decode from "jwt-decode";

// const AuthContext = createContext();

// export const AuthProvider = ({ children }) => {
//   const [user, setUser] = useState(null);
//   const [token, setToken] = useState(localStorage.getItem("token"));
//   const [error, setError] = useState(null);
//   const navigate = useNavigate();

//   useEffect(() => {
//     if (token) {
//       try {
//         const decoded = jwt_decode(token);
//         setUser(decoded);
//       } catch (err) {
//         logout();
//       }
//     }
//   }, [token]);

//   const login = async (credentials) => {
//     try {
//       setError(null);
//       const response = await fetch("/api/auth/login", {
//         method: "POST",
//         headers: {
//           "Content-Type": "application/json",
//         },
//         body: JSON.stringify(credentials),
//       });

//       if (!response.ok) {
//         const errorData = await response.json();
//         throw new Error(errorData.message || "Login failed");
//       }

//       const data = await response.json();
//       localStorage.setItem("token", data.token);
//       setToken(data.token);
//       navigate("/");
//     } catch (err) {
//       setError(err.message);
//     }
//   };

//   const logout = () => {
//     localStorage.removeItem("token");
//     setToken(null);
//     setUser(null);
//     navigate("/login");
//   };

//   const isAuthenticated = () => {
//     return !!token;
//   };

//   return (
//     <AuthContext.Provider
//       value={{ user, token, error, login, logout, isAuthenticated }}
//     >
//       {children}
//     </AuthContext.Provider>
//   );
// };

// export const useAuth = () => useContext(AuthContext);

import { createContext, useContext, useState } from "react";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [error, setError] = useState(null);

  // Mock login that stores a token but doesn't call any API
  const login = async (credentials) => {
    try {
      setError(null);
      
      // Mock authentication - create a fake token
      const mockUser = {
        id: "mock-user-123",
        name: credentials.name || "Mock User",
        email: credentials.email,
        role: credentials.role || "user",
      };
      
      const mockToken = `mock.${btoa(JSON.stringify({
        user: mockUser,
        exp: Math.floor(Date.now() / 1000) + 60 * 60 // 1 hour expiration
      }))}`;
      
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
    localStorage.removeItem("token");
    setToken(null);
    setUser(null);
  };

  const isAuthenticated = () => {
    if (!token) return false;
    
    // For mock tokens, just verify format
    if (token.startsWith("mock.")) {
      try {
        const payload = JSON.parse(atob(token.split(".")[1]));
        return payload.exp > Math.floor(Date.now() / 1000);
      } catch {
        return false;
      }
    }
    
    return true; // Assume real tokens are valid
  };

  return (
    <AuthContext.Provider
      value={{ user, token, error, login, logout, isAuthenticated }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
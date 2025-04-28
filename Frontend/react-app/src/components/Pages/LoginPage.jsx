// import { useState } from "react";
// import { useNavigate } from "react-router-dom";
// import { useAuth } from "./AuthContext";

// const LoginPage = () => {
//   const [email, setEmail] = useState("");
//   const [password, setPassword] = useState("");
//   const { login, error } = useAuth();
//   const navigate = useNavigate();

//   const handleSubmit = async (e) => {
//     e.preventDefault();
//     await login({ email, password });
//   };

//   return (
//     <div className="login-container">
//       <h1>Login to Timelines</h1>
//       {error && (
//         <div className="error-message">
//           {error}{" "}
//           <button onClick={() => window.location.reload()}>Retry</button>
//         </div>
//       )}
//       <form onSubmit={handleSubmit}>
//         <div className="form-group">
//           <label>Email:</label>
//           <input
//             type="email"
//             value={email}
//             onChange={(e) => setEmail(e.target.value)}
//             required
//           />
//         </div>
//         <div className="form-group">
//           <label>Password:</label>
//           <input
//             type="password"
//             value={password}
//             onChange={(e) => setPassword(e.target.value)}
//             required
//           />
//         </div>
//         <button type="submit">Login</button>
//       </form>
//     </div>
//   );
// };

// export default LoginPage;
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import "./PagesStyle/LoginPage.css";

const LoginPage = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { login, error } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const success = await login({ email, password });
    if (success) {
      navigate("/"); // Handle navigation here
    }
  };

  const handleMockLogin = async (role) => {
    const mockUsers = {
      user: {
        email: "user@example.com",
        password: "password123",
        name: "Regular User",
        role: "user",
      },
      admin: {
        email: "admin@example.com",
        password: "admin123",
        name: "Admin User",
        role: "admin",
      },
    };

    const user = mockUsers[role];
    setEmail(user.email);
    setPassword(user.password);
    const success = await login(user);
    if (success) {
      navigate("/"); // Handle navigation here
    }
  };

  return (
    <div className="login-container">
      <h1>Login to Timelines</h1>
      {error && (
        <div className="error-message">
          <p>{error}</p>
          <button
            className="error-retry-button"
            onClick={() => window.location.reload()}
          >
            Retry
          </button>
        </div>
      )}
      <form className="login-form" onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="email">Email:</label>
          <input
            id="email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
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

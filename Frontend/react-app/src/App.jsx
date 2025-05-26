import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import "react-toastify/dist/ReactToastify.css";
import Breadcrumbs from "./components/Breadcrumbs/Breadcrumbs";
import AppLayout from "./components/AppLayout/AppLayout";
import Timeline from "./components/Timelines/Timeline/Timeline";
import { AuthProvider } from "./context/AuthContext";
import AuthGuard from "./core/auth/AuthGuard";
import ErrorPage from "./pages/ErrorPage";
import HomePage from "./pages/HomePage";
import LoginPage from "./pages/LoginPage";
import "./styles/App.css";


const router = createBrowserRouter(
  createRoutesFromElements(
    <Route errorElement={<ErrorPage />}>
      <Route path="/login" element={<LoginPage />} />
      <Route
        path="/"
        element={<AuthGuard><AppLayout /></AuthGuard>}
        handle={{ crumb: () => "Home" }}
      >
        <Route
          index
          element={<TimelineList />}
        />
        <Route
          path="timelines/:id"
          element={<Timeline />}
          handle={{ crumb: ({ params, matches }) => `${params.id}` }}
        />
      </Route>
      <Route path="*" element={<ErrorPage />} />
    </Route>
  )
);

function App() {
  return (
    <AuthProvider>
      <RouterProvider router={router} />
    </AuthProvider>
  );
}

export default App;

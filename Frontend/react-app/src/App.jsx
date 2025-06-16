import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import "react-toastify/dist/ReactToastify.css";
import AppLayout from "./components/AppLayout/AppLayout";
import PhysicalPersonDetails from "./components/Timelines/PhysicalPersonDetails/PhysicalPersonDetails";
import Timeline from "./components/Timelines/Timeline/Timeline";
import { AuthProvider } from "./context/AuthContext";
import { ChatProvider } from "./context/ChatContext";
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
        element={
          <AuthGuard>
            <AppLayout />
          </AuthGuard>
        }
        handle={{ 
          crumb: { 
            title: "Home",
            static: true
          } 
        }}
      >
        <Route index element={<HomePage />} />
        <Route
          path="timelines/:id"
          element={<Timeline />}
          handle={{ 
            crumb: ({ params }) => ({
              title: "Timeline",
              dynamic: true,
              idParam: 'id'
            }) 
          }}
        >
          <Route
            path="physical-persons/:personId"
            element={<PhysicalPersonDetails />}
            handle={{
              crumb: ({ params }) => ({
                title: params.personId,
                dynamic: true,
                personIdParam: 'personId',
                type: 'person'
              })
            }}
          />
        </Route>
      </Route>
      <Route path="*" element={<ErrorPage />} />
    </Route>
  )
);

function App() {
  return (
    <AuthProvider>
      <ChatProvider>
        <RouterProvider router={router} />
      </ChatProvider>
    </AuthProvider>
  );
}

export default App;

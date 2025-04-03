import React from "react";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import TimelinePage from "./components/Pages/TimelinePage/TimelinePage";
import "./styles/App.css";

function App() {
    return (
        <div className="app-container">
            <ToastContainer />
            <div className="app-content">
                <TimelinePage />
            </div>
        </div>
    );
}

export default App;

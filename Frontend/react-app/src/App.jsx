import React from "react";
import { BrowserRouter, Route, Routes } from "react-router";
import Timeline from "./components/Timelines/Timeline/Timeline";
import TimelineList from "./components/Timelines/TimelineList/TimelineList";
import "./styles/App.css";

function App() {
    return (
        <BrowserRouter>
            <div className="app-container">
                <div className="app-content">
                    <div className="content-wrapper">
                    <Routes>
                        <Route path="/timelines/:id" element={<Timeline />} />
                        <Route path="/" element={<TimelineList />} />
                    </Routes>
                    </div>
                </div>
            </div>
        </BrowserRouter>
    );
}

export default App;

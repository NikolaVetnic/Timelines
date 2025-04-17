import React from "react";
import { BrowserRouter, Route, Routes } from "react-router";
import { ToastContainer } from 'react-toastify';
import Timeline from "./components/Timelines/Timeline/Timeline";
import TimelineList from "./components/Timelines/TimelineList/TimelineList";
import ReminderNotifier from "./core/utils/ReminderNotifier";
import "./styles/App.css";

function App() {
  return (
    <BrowserRouter>
      <div className="app-container">
        <ReminderNotifier />
        
        <div className="app-content">
          <div className="content-wrapper">
            <Routes>
              <Route path="/timelines/:id" element={<Timeline />} />
              <Route path="/" element={<TimelineList />} />
            </Routes>
            <ToastContainer />
          </div>
        </div>
      </div>
    </BrowserRouter>
  );
}

export default App;

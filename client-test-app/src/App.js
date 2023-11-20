import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import './App.css';
import Login from "./pages/login/Login";

function App() {
  return (
    <div className="d-flex flex-column min-vh-100">
    <Router>
      <Routes>
      <Route path="/" Component={Login} />
      </Routes>
    </Router>
  </div>
  );
}

export default App;

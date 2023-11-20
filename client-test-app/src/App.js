import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./App.css";
import Login from "./pages/login/Login";
import UserTests from "./pages/userTests/UserTests";
import About from "./pages/about/About";
import ViewCompletedTest from "./pages/viewCompleteTest/ViewCompletedTest";
import PassTheTest from "./pages/passTheTest/PassTheTest";

function App() {
  return (
    <div className="d-flex flex-column min-vh-100">
      <Router>
        <Routes>
          <Route path="/" Component={Login} />
          <Route path="/UserTests" Component={UserTests} />
          <Route path="/About" Component={About} />
          <Route path="/ViewCompletedTest/:userTestId" Component={ViewCompletedTest} />
          <Route path="/PassTheTest/:testId/:userTestId" Component={PassTheTest} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;

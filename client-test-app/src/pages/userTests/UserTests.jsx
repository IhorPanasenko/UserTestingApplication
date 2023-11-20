import React, { useState, useEffect } from "react";
import axios from "axios";

import { jwtDecode } from "jwt-decode";
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import "./UserTests.css";
import { useNavigate } from "react-router-dom";

const GET_USER_TESTS_URL =
  "https://localhost:7256/api/UserTests/GetUserTests?userId=";

function UserTests() {
  const [tests, setTests] = useState([]);
  const [error, setError] = useState(null);
  const [userId, setUserId] = useState("");
  const [isAuthorized, setisAuthorized] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const navigate = useNavigate();

  const getUserTests = async (currentId) => {
    try {
      const response = await axios.get(`${GET_USER_TESTS_URL}${currentId}`);
      setTests(response.data);
      setError("");
      setIsLoading(false);
    } catch (error) {
      setError(error.message);
    }
  };

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      setisAuthorized(true);
      const decoded = jwtDecode(token);
      console.log(decoded);

      const userId = decoded.UserId;
      setUserId(userId);
      console.log(userId);
      getUserTests(userId);
    } else {
      setisAuthorized(false);
    }

    setIsLoading(false);
  }, []);

  const viewResultsClick = () =>{
    navigate("/ViewCompletedTest")
  }

  const startTheTestClick = () => {
    navigate("/PassTheTest")
  }

  return (
    <>
      <Header />
      <div className="d-flex flex-column justify-content-between flex-grow-1">
        {!isAuthorized ? (
          <div className="text-center m-5 bg-info p-5  border border-dark border-3 rounded">
            <h1>Only authorized users can see thier tests</h1>
          </div>
        ) : (
          <main className="m-5 mt-3 mb-3 userTestsMain">
            <h1 className="text-center mb-5"> Your Tests</h1>
            {error && <p>Error: {error}</p>}
            {isLoading && !error && <p>Loading...</p>}
            {tests?.length == 0 && !error && (
              <div className="Container text-center">
                <h1>There are no tests available for you</h1>
              </div>
            )}
            {tests.length > 0 && (
              <table className="table table-bordered">
                <thead>
                  <tr className="text-center">
                    <th>Test Title</th>
                    <th>Number of Questions</th>
                    <th>Max Mark</th>
                    <th>Is Completed</th>
                    <th>Mark</th>
                    <th>Action</th>
                  </tr>
                </thead>
                <tbody>
                  {tests
                    .slice()
                    .reverse()
                    .map((test) =>
                      test.isCompleted ? (
                        <tr className="text-center table-info" key={test.userTestId}>
                          <td>{test.testTitle}</td>
                          <td>{test.numberOfQuestions}</td>
                          <td>{test.maxMark}</td>
                          <td>{test.isCompleted ? "Yes" : "No"}</td>
                          <td>
                            {test.mark !== null
                              ? `${test.mark}/${test.maxMark}`
                              : "N/A"}
                          </td>
                          <td className="text-center">
                            <button className="btn btn-primary p-4 pt-2 pb-2 m-2 border border-3 border-dark rounded fs-3"
                            onClick={viewResultsClick}
                            >
                                
                              View Results
                            </button>
                          </td>
                        </tr>
                      ) : (
                        <tr className="text-center table-warning" key={test.userTestId}>
                          <td>{test.testTitle}</td>
                          <td>{test.numberOfQuestions}</td>
                          <td>{test.maxMark}</td>
                          <td>{test.isCompleted ? "Yes" : "No"}</td>
                          <td>
                            {test.mark !== null
                              ? `${test.mark}/${test.maxMark}`
                              : "N/A"}
                          </td>
                          <td className="text-center">
                            <button className="btn btn-success p-4 pt-2 pb-2 m-2 border border-3 border-dark rounded fs-3"
                            onClick={startTheTestClick}
                            >
                              Start The Test
                            </button>
                          </td>
                        </tr>
                      )
                    )}
                </tbody>
              </table>
            )}
          </main>
        )}
      </div>
      <Footer />
    </>
  );
}

export default UserTests;

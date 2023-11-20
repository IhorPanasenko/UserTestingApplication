import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import axios from "axios";
import { Form } from "react-bootstrap";

const GET_COMPLETED_USER_TEST_URL =
  "https://localhost:7256/api/UserTests/GetCompletedTest?userTestId=";

function ViewCompletedTest() {
  const { userTestId } = useParams();
  const [testDetails, setTestDetails] = useState(null);

  const fetchData = async () => {
    var token = localStorage.getItem("token")
    console.log(token);
    try {
      const response = await axios.get(
        `${GET_COMPLETED_USER_TEST_URL}${userTestId}`,
        {
            headers:{
                'Authorization': 'Bearer ' + token,
            }
        }
      );
      setTestDetails(response.data);
    } catch (error) {
      console.error("Error fetching test details:", error);
    }
  };

  useEffect(() => {
    fetchData();
    console.log(testDetails);
  }, []);

  let i = 1;


  console.log(testDetails);
  return (
    <>
      <Header />
      <div className="d-flex flex-column justify-content-between flex-grow-1">
        <main className="fs-2">
          {!testDetails ? (
            <h1 className="text-center">Loading test details...</h1>
          ) : (
            <div className="m-5 mt-2  mb-0 bg-info border border-3 border-primary rounded">
              <h1 className="text-center pt-4">{testDetails.testName}</h1>
              <p className="text-end m-5 mt-0 mb-0">
                Mark: {testDetails.mark}/{testDetails.maxMark}
              </p>
              <Form className="fs-2 mt-4 p-4 bg-secondary ">
                {testDetails.questions.map((question) => (
                  <div
                    key={question.questionId}
                    className={`m-5 mb-4 mt-0 p-3 rounded border border-3 border-primary ${
                      question.isCorrectAnswered ? "bg-success" : "bg-danger"
                    }`}
                  >
                    <div className="d-flex justify-content-between">
                      <h3>Question {i++}</h3>
                      {question.isCorrectAnswered ? (
                        <div>Correct answer</div>
                      ) : (
                        <div>Wrong answer</div>
                      )}
                    </div>

                    <p>{question.questionText}</p>

                    {question.options.map((option) => (
                      <Form.Group key={option.optionId}>
                        {question.questionType === 2 ? (
                          <Form.Check
                            type="checkbox"
                            id={`option-${option.optionId}`}
                            checked={option.isChoosen}
                            label={option.optionText}
                            disabled
                          />
                        ) : question.questionType === 1 ? (
                          // For single-choice and open-answer questions
                          <Form.Check
                            type = "radio"
                            id={`option-${option.optionId}`}
                            checked={option.isChoosen}
                            label={option.optionText}
                            disabled
                          />
                        ) : (
                          <>
                            <Form.Control
                            className="fs-2"
                              type = "text"
                              id={`option-${option.optionId}`}
                              value = {option.enteredText}
                              readonly
                            />
                            <div className="text-end mt-2">Correct answer: {option.optionText}</div>
                          </> 
                        )}
                      </Form.Group>
                    ))}
                  </div>
                ))}
              </Form>
            </div>
          )}
        </main>
      </div>
      <Footer />
    </>
  );
}

export default ViewCompletedTest;

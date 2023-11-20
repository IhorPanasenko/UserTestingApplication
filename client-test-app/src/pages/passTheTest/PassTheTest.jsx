import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Form, Button } from "react-bootstrap";
import axios from "axios";
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

const GET_TEST_BY_ID_URL = "https://localhost:7256/api/Test/GetById?testId=";
const PASS_THE_TEST_URL = "https://localhost:7256/api/UserTests/PassTheTest";

function PassTheTest() {
  const { testId, userTestId } = useParams();
  const [testDetails, setTestDetails] = useState(null);
  const [userAnswers, setUserAnswers] = useState([]);
  const navigate = useNavigate();
  const [userId, setUserId] = useState("")
  const [isAuthorized, setIsAuthorized] = useState(false);

  const fetchData = async (appUserId) => {
    try {
      const response = await axios.get(`${GET_TEST_BY_ID_URL}${testId}&userId=${appUserId}`);
      setTestDetails(response.data);
    } catch (error) {
      console.error("Error fetching test details:", error);
    }
  };

  useEffect(() => {
    
    const token = localStorage.getItem("token");
    if (token) {
      setIsAuthorized(true);
      const decoded = jwtDecode(token);
      console.log(decoded);

      const appUserId = decoded.UserId;
      setUserId(appUserId);
      console.log(appUserId);
      fetchData(appUserId);
    } else {
      setIsAuthorized(false);
    }

    console.log(testDetails);
  }, [testId]);

  const handleSingleAnswerQuestionChange = (questionId, optionId) => {
    setUserAnswers((prevUserAnswers) => {
      const updatedAnswers = prevUserAnswers.filter(
        (answer) => answer.questionId !== questionId
      );

      updatedAnswers.push({
        userAnswerText: "",
        userTestId: parseInt(userTestId),
        questionId,
        userAnswerOptionId: optionId,
      });

      console.log(updatedAnswers);
      return updatedAnswers;
    });
  };

  const handleMultipleAnswerQuestionChange = (
    questionId,
    optionId,
    isChecked
  ) => {
    setUserAnswers((prevUserAnswers) => {
      if (isChecked) {
        return [
          ...prevUserAnswers,
          {
            userAnswerText: "",
            userTestId: parseInt(userTestId),
            questionId,
            userAnswerOptionId: optionId,
          },
        ];
      } else {
        return prevUserAnswers.filter(
          (answer) =>
            !(
              answer.questionId === questionId &&
              answer.userAnswerOptionId === optionId
            )
        );
      }
    });
    console.log(userAnswers);
  };

  const handleOpenAnswerQuestionChanged = (
    questionId,
    optionId,
    currentValue
  ) => {
    setUserAnswers((prevUserAnswers) => {
      const existingAnswerIndex = prevUserAnswers.findIndex(
        (answer) => answer.questionId === questionId
      );

      if (existingAnswerIndex !== -1) {
        return prevUserAnswers.map((answer, index) =>
          index === existingAnswerIndex
            ? { ...answer, userAnswerText: currentValue }
            : answer
        );
      } else {
        return [
          ...prevUserAnswers,
          {
            userAnswerText: currentValue,
            userTestId: parseInt(userTestId),
            questionId,
            userAnswerOptionId: optionId,
          },
        ];
      }
    });
  };

  const handleSubmit = async () => {
    const requestBody = {
      userTestId: parseInt(userTestId),
      appUserId: userId,
      testId: parseInt(testId),
      userAnswers,
    };

    console.log(requestBody)

    try {
      const response = await axios.put(PASS_THE_TEST_URL, requestBody);
      console.log("Test submitted successfully:", response.data);
      alert("All questions were submited");
      navigate("/UserTests");
    } catch (error) {
      console.error("Error submitting test:", error);
      alert(`Soory, error happened:\n${error}`);
      navigate("/UserTests");
    }
  };

  let i = 1;
  console.log(userAnswers);
  return (
    <>
      <Header />
      <div className="d-flex flex-column justify-content-between flex-grow-1">
        <main className="fs-2">
            {!isAuthorized && (<h2 className="text-center">Only authorized users can have access to completin tests</h2>)}
          {!testDetails ? (
            <h1 className="text-center">Loading test details...</h1>
          ) : (
            <div className="m-5 mt-2  mb-0 bg-info border border-3 border-primary rounded">
              <h1 className="text-center pt-4">{testDetails.testName}</h1>
              <Form className="fs-2 mt-4 p-4 bg-secondary">
                {testDetails.questions.length == 0 && (
                  <h2 className="fs-1 text-center">
                    There are no questions in test
                  </h2>
                )}
                {testDetails.questions.map((question) => (
                  <div
                    key={question.questionId}
                    className={`m-5 mb-4 mt-0 p-3 rounded border border-3 border-primary`}
                  >
                    <h3>Question {i++}</h3>
                    <p>{question.questionText}</p>

                    {question.options.map((option) => (
                      <Form.Group key={option.optionId}>
                        {question.questionType == 2 ? (
                          <Form.Check
                            type="checkbox"
                            id={`option-${option.optionId}`}
                            label={option.optionText}
                            checked={userAnswers.some(
                              (answer) =>
                                answer.questionId === question.questionId &&
                                answer.userAnswerOptionId === option.optionId
                            )}
                            onChange={(e) =>
                              handleMultipleAnswerQuestionChange(
                                question.questionId,
                                option.optionId,
                                e.target.checked
                              )
                            }
                          />
                        ) : question.questionType == 1 ? (
                          // For single-choice questions
                          <Form.Check
                            type="radio"
                            name={`radioButton-${question.questionId}`}
                            id={`option-${option.optionId}`}
                            label={option.optionText}
                            checked={option.isChoosen}
                            onChange={() =>
                              handleSingleAnswerQuestionChange(
                                question.questionId,
                                option.optionId
                              )
                            }
                          />
                        ) : (
                          <>
                            <Form.Control
                              className="fs-2"
                              type="text"
                              id={`option-${question.questionId}`}
                              placeholder="Your answer"
                              value={
                                userAnswers.find(
                                  (answer) =>
                                    answer.questionId === question.questionId
                                )?.userAnswerText || ""
                              }
                              onChange={(e) =>
                                handleOpenAnswerQuestionChanged(
                                  option.questionId,
                                  option.optionId,
                                  e.target.value
                                )
                              }
                            />
                          </>
                        )}
                      </Form.Group>
                    ))}
                  </div>
                ))}
              </Form>
              <div className="text-center p-3">
                <Button
                  className="fs-1 m-5 mt-3 mb-3"
                  variant="primary"
                  onClick={handleSubmit}
                >
                  Submit
                </Button>
              </div>
            </div>
          )}
        </main>
      </div>
      <Footer />
    </>
  );
}

export default PassTheTest;

/*setUserAnswers((prevUserAnswers) => {
    const updatedAnswers = prevUserAnswers.filter(
      (answer) => answer.questionId !== questionId
    );

    updatedAnswers.push({
      userAnswerText: enteredText,
      userTestId: 0, // You may need to replace this with the actual userTestId
      questionId,
      userAnswerOptionId: optionId,
    });

    return updatedAnswers;
  });*/

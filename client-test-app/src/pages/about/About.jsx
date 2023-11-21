import React from "react";
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import { Container } from "react-bootstrap";
import "./About.css";

function About() {
  return (
    <>
      <Header />
      <div className="d-flex flex-column justify-content-between flex-grow-1">
      <main>
        <Container className="bg-dark border rounded">
          <h1 className="text-center about_title m-3">About this application</h1>
          <article className="text-start m-3 p-3 border border-3 border-light solid">
            <span>
              This application is intended for users to pass the tests assigned
              to them. <br/> The test can be taken only 1 time.
            </span>
            <span>
              After passing the test, <br/>the user will be able to see his score and
              review the questions (without the possibility to change the
              answer) to find out which questions were answered correctly.
            </span>
          </article>
        </Container>
      </main>
      </div>
      <Footer />
    </>
  );
}

export default About;

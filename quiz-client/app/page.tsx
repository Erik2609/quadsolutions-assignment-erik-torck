"use client";

import Form from "next/form";
import { useState } from "react";
import { FormSubmitHandler } from "./FormSubmitHandler";
import { QuestionViewModelMapper } from "./mappers/QuestionViewModelMapper";
import { CheckAnswerModel } from "./models/CheckAnswerModel";
import { IsAnswerCorrectModel } from "./models/IsAnswerCorrectModel";
import { QuestionModel } from "./models/QuestionModel";

export default function Home() {
  const [questions, setQuestions] = useState<QuestionModel[]>([]);
  const [checkAnswerModels, setCheckAnswerModels] = useState<
    CheckAnswerModel[]
  >([]);
  const [isAnswerCorrectModels, setAnswers] = useState<IsAnswerCorrectModel[]>(
    [],
  );
  const [errors, setErrors] = useState<string[]>([]);

  // Fetch questions on initial load
  if (questions.length === 0 && errors.length === 0) {
    fetch("http://localhost:5183/questions")
      .then((data) => {
        data
          .json()
          .then((json) => {
            setQuestions(json);
          })
          .catch(() => {
            setErrors((prev) => [...prev, "Failed to parse JSON"]);
          });
      })
      .catch((err) => {
        setErrors((prev) => [...prev, err.message]);
      });
  }

  // Show loading state if questions are still being fetched
  if (questions.length === 0 && errors.length === 0) {
    return <div>Loading...</div>;
  }

  // Show errors if there are any
  if (errors.length > 0) {
    return (
      <div>
        <h1>Errors:</h1>
        <ul>
          {errors.map((error, index) => (
            <li key={index}>{error}</li>
          ))}
        </ul>
      </div>
    );
  }

  const questionViewModels = questions.map((question) =>
    QuestionViewModelMapper.toViewModel(question),
  );
  questionViewModels.forEach((viewModel) => {
    if (!!checkAnswerModels) {
      QuestionViewModelMapper.addGivenAnswerToViewModel(
        viewModel,
        checkAnswerModels,
      );
    }
    if (!!isAnswerCorrectModels) {
      QuestionViewModelMapper.addAnswerToViewModel(
        viewModel,
        isAnswerCorrectModels,
      );
    }
  });

  return (
    <Form
      action={(formData) =>
        FormSubmitHandler(formData, setCheckAnswerModels, setAnswers, setErrors)
      }
    >
      {questionViewModels.map((question) => (
        <>
          <fieldset key={question.id.toString()}>
            <legend>{decodeURIComponent(question.question)}</legend>
            {question.possibleAnswers.map((answer) => (
              <div key={answer}>
                <input
                  type="radio"
                  id={question.id.toString() + answer}
                  name={question.id.toString()}
                  value={answer}
                  disabled={question.givenAnswer !== undefined}
                  defaultChecked={question.givenAnswer === answer}
                />
                <label htmlFor={question.id.toString() + answer}>
                  {decodeURIComponent(answer)}
                </label>
              </div>
            ))}
          </fieldset>
          {question.correctAnswer &&
            question.givenAnswer === question.correctAnswer && (
              <div>Correct!</div>
            )}
          {question.correctAnswer &&
            question.givenAnswer !== question.correctAnswer && (
              <div>
                Incorrect! The correct answer is{" "}
                {decodeURIComponent(question.correctAnswer)}
              </div>
            )}
        </>
      ))}

      <button type="submit">Submit</button>
    </Form>
  );
}

"use client";

import { useState } from "react";
import { FormComponent } from "./form/FormComponent";
import { formSubmitHandler } from "./formSubmitHandler";
import { questionViewModelMapper } from "./mappers/questionViewModelMapper";
import { checkAnswerModel } from "./models/checkAnswerModel";
import { isAnswerCorrectModel } from "./models/isAnswerCorrectModel";
import { questionModel } from "./models/questionModel";

export default function Home() {
  const [questions, setQuestions] = useState<questionModel[]>([]);
  const [checkAnswerModels, setCheckAnswerModels] = useState<
    checkAnswerModel[]
  >([]);
  const [isAnswerCorrectModels, setAnswers] = useState<isAnswerCorrectModel[]>(
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
        setErrors((prev) => [
          ...prev,
          err.message,
          "(During the demo, this probably means the server is not running)",
        ]);
      });
  }

  // Show loading state if questions are still being fetched
  if (questions.length === 0 && errors.length === 0) {
    return <h1>Loading...</h1>;
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
        <button
          className="btn btn-warning"
          onClick={() => window.location.reload()}
        >
          Reload
        </button>
      </div>
    );
  }

  const questionViewModels = questions.map((question) =>
    questionViewModelMapper.toViewModel(question),
  );
  questionViewModels.forEach((viewModel) => {
    if (!!checkAnswerModels) {
      questionViewModelMapper.addGivenAnswerToViewModel(
        viewModel,
        checkAnswerModels,
      );
    }
    if (!!isAnswerCorrectModels) {
      questionViewModelMapper.addAnswerToViewModel(
        viewModel,
        isAnswerCorrectModels,
      );
    }
  });

  return (
    <FormComponent
      formSubmitHandler={(formData) =>
        formSubmitHandler(formData, setCheckAnswerModels, setAnswers, setErrors)
      }
      questionViewModels={questionViewModels}
    />
  );
}

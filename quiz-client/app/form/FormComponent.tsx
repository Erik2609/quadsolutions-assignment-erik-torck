import Form from "next/form";
import { questionViewModel } from "../viewmodels/questionViewModel";

export interface FormComponentProps {
  formSubmitHandler: (formData: FormData) => void;
  questionViewModels: questionViewModel[];
}
export function FormComponent({
  formSubmitHandler,
  questionViewModels,
}: FormComponentProps) {
  return (
    <Form action={formSubmitHandler}>
      {questionViewModels.map((question) => (
        <div key={question.id.toString()}>
          <fieldset>
            <legend className="questionTitle">
              {decodeURIComponent(question.question)}
            </legend>
            {question.possibleAnswers.map((answer) => (
              <div key={answer}>
                <input
                  type="radio"
                  id={question.id.toString() + answer}
                  name={question.id.toString()}
                  value={answer}
                  disabled={question.givenAnswer !== undefined}
                  defaultChecked={question.givenAnswer === answer}
                  required
                />
                <label htmlFor={question.id.toString() + answer}>
                  {decodeURIComponent(answer)}
                </label>
              </div>
            ))}
          </fieldset>
          {question.correctAnswer &&
            question.givenAnswer === question.correctAnswer && (
              <div className="correct text-center">Correct!</div>
            )}
          {question.correctAnswer &&
            question.givenAnswer !== question.correctAnswer && (
              <div className="incorrect text-center">
                Incorrect! The correct answer is{" "}
                {decodeURIComponent(question.correctAnswer)}
              </div>
            )}
          <br />
        </div>
      ))}

      <button type="submit" className="btn btn-success">
        Submit
      </button>
    </Form>
  );
}

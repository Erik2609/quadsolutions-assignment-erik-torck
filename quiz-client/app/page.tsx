import Form from "next/form";
import Question from "./models/question";

export default async function Home() {
  const data = await fetch("http://localhost:5183/questions");
  const questions: Question[] = await data.json();
  return (
    <Form action="/search">
      {questions.map((question) => (
        <fieldset key={question.id.toString()}>
          <legend>{decodeURIComponent(question.question)}</legend>
          {question.possibleAnswers.map((answer) => (
            <div key={answer}>
              <input
                type="radio"
                id={question.id.toString() + answer}
                name={question.id.toString()}
                value={answer}
              />
              <label htmlFor={question.id.toString() + answer}>
                {decodeURIComponent(answer)}
              </label>
            </div>
          ))}
        </fieldset>
      ))}
      <button type="submit">Submit</button>
    </Form>
  );
}

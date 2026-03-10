import { formDataMapper } from "../mappers/formDataMapper";
import { checkAnswerModel } from "../models/checkAnswerModel";
import { isAnswerCorrectModel } from "../models/isAnswerCorrectModel";

export function formSubmitHandler(
  formData: FormData,
  setCheckAnswerModels: React.Dispatch<
    React.SetStateAction<checkAnswerModel[]>
  >,
  setAnswers: React.Dispatch<React.SetStateAction<isAnswerCorrectModel[]>>,
  setErrors: React.Dispatch<React.SetStateAction<string[]>>,
) {
  {
    const checkAnswerModels =
      formDataMapper.mapFormDataToCheckAnswerModel(formData);
    setCheckAnswerModels(checkAnswerModels);
    fetch("https://quizservereriktorck.azurewebsites.net/CheckAnswers", {
      method: "POST",
      body: JSON.stringify(checkAnswerModels),
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((data) => {
        data
          .json()
          .then((json) => {
            setAnswers(json);
          })
          .catch(() => {
            setErrors((prev) => [...prev, "Failed to parse JSON"]);
          });
      })
      .catch((err) => {
        setErrors((prev) => [...prev, err.message]);
      });
  }
}

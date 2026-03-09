import { FormDataMapper } from "./mappers/FormDataMapper";
import { CheckAnswerModel } from "./models/CheckAnswerModel";
import { IsAnswerCorrectModel } from "./models/IsAnswerCorrectModel";

export function FormSubmitHandler(
  formData: FormData,
  setCheckAnswerModels: React.Dispatch<
    React.SetStateAction<CheckAnswerModel[]>
  >,
  setAnswers: React.Dispatch<React.SetStateAction<IsAnswerCorrectModel[]>>,
  setErrors: React.Dispatch<React.SetStateAction<string[]>>,
) {
  {
    const checkAnswerModels =
      FormDataMapper.mapFormDataToCheckAnswerModel(formData);
    setCheckAnswerModels(checkAnswerModels);
    fetch("http://localhost:5183/CheckAnswers", {
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

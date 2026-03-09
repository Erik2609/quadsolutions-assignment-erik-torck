import { CheckAnswerModel } from "../models/CheckAnswerModel";

export class FormDataMapper {
  static mapFormDataToCheckAnswerModel(formData: FormData): CheckAnswerModel[] {
    const checkAnswerModels: CheckAnswerModel[] = [];
    formData.forEach((answer, key) => {
      if (typeof answer !== "string") {
        throw new Error(
          `Expected answer to be a string, but got ${typeof answer}`,
        );
      }
      const id = parseInt(key);
      checkAnswerModels.push({ id, answer });
    });
    return checkAnswerModels;
  }
}

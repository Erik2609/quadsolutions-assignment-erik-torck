import { checkAnswerModel } from "../models/checkAnswerModel";

export class formDataMapper {
  static mapFormDataToCheckAnswerModel(formData: FormData): checkAnswerModel[] {
    const checkAnswerModels: checkAnswerModel[] = [];
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

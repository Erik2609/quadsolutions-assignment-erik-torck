import { checkAnswerModel } from "../models/checkAnswerModel";
import { isAnswerCorrectModel } from "../models/isAnswerCorrectModel";
import { questionModel } from "../models/questionModel";
import { questionViewModel } from "../viewmodels/questionViewModel";

export class questionViewModelMapper {
  static toViewModel(question: questionModel): questionViewModel {
    return {
      id: question.id,
      question: question.question,
      possibleAnswers: question.possibleAnswers,
      correctAnswer: undefined,
      givenAnswer: undefined,
    };
  }
  static addGivenAnswerToViewModel(
    questionViewModel: questionViewModel,
    givenAnswers: checkAnswerModel[],
  ): void {
    const matchingAnswer = givenAnswers.find(
      (answer) => answer.id === questionViewModel.id,
    );
    if (!matchingAnswer) {
      return;
    }
    questionViewModel.givenAnswer = matchingAnswer.answer;
  }
  static addAnswerToViewModel(
    questionViewModel: questionViewModel,
    IsAnswerCorrectModels: isAnswerCorrectModel[],
  ): void {
    const matchingModel = IsAnswerCorrectModels.find(
      (model) => model.id === questionViewModel.id,
    );
    if (!matchingModel) {
      return;
    }
    questionViewModel.correctAnswer = matchingModel.correctAnswer;
  }
}

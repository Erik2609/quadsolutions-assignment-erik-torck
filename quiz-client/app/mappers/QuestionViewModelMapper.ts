import { CheckAnswerModel } from "../models/CheckAnswerModel";
import { IsAnswerCorrectModel } from "../models/IsAnswerCorrectModel";
import { QuestionModel } from "../models/QuestionModel";
import { QuestionViewModel } from "../viewmodels/QuestionViewModel";

export class QuestionViewModelMapper {
  static toViewModel(question: QuestionModel): QuestionViewModel {
    return {
      id: question.id,
      question: question.question,
      possibleAnswers: question.possibleAnswers,
      correctAnswer: undefined,
      givenAnswer: undefined,
    };
  }
  static addGivenAnswerToViewModel(
    questionViewModel: QuestionViewModel,
    givenAnswers: CheckAnswerModel[],
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
    questionViewModel: QuestionViewModel,
    IsAnswerCorrectModels: IsAnswerCorrectModel[],
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

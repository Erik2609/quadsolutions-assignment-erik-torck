export interface QuestionViewModel {
  id: number;
  question: string;
  possibleAnswers: string[];
  correctAnswer: string | undefined;
  givenAnswer: string | undefined;
}

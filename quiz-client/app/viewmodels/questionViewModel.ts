export interface questionViewModel {
  id: number;
  question: string;
  possibleAnswers: string[];
  correctAnswer: string | undefined;
  givenAnswer: string | undefined;
}

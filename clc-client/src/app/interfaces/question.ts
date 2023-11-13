import { Option } from "./option";

export interface Question {
    questionId: number;
    title: string;
    subText: string;
    answerOptionId: number | null;
    options: Option[];
}
import { Question } from "./question";

export interface Poll {
    pollId: number;
    topic: string;
    questions: Question[];
}
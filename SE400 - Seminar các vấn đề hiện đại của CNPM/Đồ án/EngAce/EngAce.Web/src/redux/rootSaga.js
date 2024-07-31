import { all } from "redux-saga/effects";
import * as ChatbotSaga from "./sagaActions/ChatbotSaga";
import * as QuizSaga from "./sagaActions/QuizSaga";

export default function* rootSaga() {
  yield all([
    ChatbotSaga.followActSendMessage(),
    QuizSaga.followActGetTopics(),
    QuizSaga.followActGetQuizTypes(),
    QuizSaga.followActGenerateQuiz(),
  ]);
}

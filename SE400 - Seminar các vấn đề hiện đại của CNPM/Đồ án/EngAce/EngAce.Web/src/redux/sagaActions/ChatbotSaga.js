import { call, put, select, takeLatest } from "redux-saga/effects";
import * as SagaActionTypes from "../constants";
import { chatbotActions } from "../reducer/ChatbotReducer";
import { AppService } from "../../services/api";

const selectListChat = (state) => state.chatbotSlice.listChat;

function* actSendMessage(action) {
  const { message, onLoading, onFinish } = action;
  let responseMessage;
  try {
    const newMessage = {
      FromUser: true,
      Message: message,
    };
    yield put(chatbotActions.sendMessage({ data: newMessage }));
    const listChat = yield select(selectListChat);
    onLoading();
    const res = yield call(() => AppService.sendChatMessage(listChat, message));
    const { status, data } = res;
    console.log(res);
    if (status === 200) {
      responseMessage = {
        FromUser: false,
        Message: data,
      };
    } else {
      responseMessage = {
        FromUser: false,
        Message: "Oops! Something went wrong",
      };
    }
  } catch (err) {
    console.log(err);
    const { response } = err;
    responseMessage = {
      FromUser: false,
      Message: response.data.error,
    };
  } finally {
    onFinish();
    yield put(chatbotActions.responseMessage({ data: responseMessage }));
  }
}

export function* followActSendMessage() {
  yield takeLatest(SagaActionTypes.SEND_MESSAGE_SAGA, actSendMessage);
}

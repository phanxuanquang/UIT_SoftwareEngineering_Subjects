import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  qaList: [],
  topics: [],
  qTypes: [],
  answer: [],
  submit: false,
};

const quizSlice = createSlice({
  name: "quizzSlice",
  initialState,
  reducers: {
    getTopicsSuccess: (state, action) => {
      state.topics = action.payload.topics;
    },
    getQuizTypesSuccess: (state, action) => {
      state.qTypes = action.payload.qTypes;
    },

    createQuizzesSuccess: (state, action) => {
      state.qaList = action.payload.qaList;
    },

    setAnswerList: (state, action) => {
      const { qIndex, value } = action.payload;
      const newAnswer = [...state.answer];
      newAnswer[qIndex] = value;
      state.answer = newAnswer;
    },
    onSubmit: (state) => {
      state.submit = true;
    },

    resetQuiz: (state) => {
      state.qaList = [];
      state.topics = [];
      state.answer = [];
      state.submit = false;
    },
  },
});

export const quizActions = quizSlice.actions;

export default quizSlice.reducer;

import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  listChat: [],
};

const chatbotSlice = createSlice({
  name: "chatbotSlice",
  initialState,
  reducers: {
    sendMessage: (state, action) => {
      state.listChat = [...state.listChat, action.payload.data];
    },
    responseMessage: (state, action) => {
      state.listChat = [...state.listChat, action.payload.data];
    },

    resetChat: (state) => {
      state.listChat = [];
    },
  },
});

export const chatbotActions = chatbotSlice.actions;

export default chatbotSlice.reducer;

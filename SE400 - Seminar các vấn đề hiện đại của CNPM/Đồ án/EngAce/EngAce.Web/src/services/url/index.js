//export const DOMAIN_NAME = process.env.REACT_APP_PROD_BE_URL;
export const DOMAIN_NAME = "https://engace.azurewebsites.net/api";

export const URL_GET_HEALCHECK = `${DOMAIN_NAME}/Healthcheck`;
export const URL_GET_ENGLISH_LEVEL = `${DOMAIN_NAME}/Quiz/GetEnglishLevels`;
export const URL_GET_DICTIONARY_SEARCH = `${DOMAIN_NAME}/Dictionary/Search`;
export const URL_GET_ESSAY_REVIEW = `${DOMAIN_NAME}/Review/Generate`;
export const URL_GET_CHAT_MESSAGE = `${DOMAIN_NAME}/Chatbot/GenerateAnswer`;
export const URL_GET_SUGGEST_TOPICS = `${DOMAIN_NAME}/Quiz/Suggest3Topics`;
export const URL_GET_QUIZ_TYPES = `${DOMAIN_NAME}/Quiz/GetQuizTypes`;
export const URL_GENERATE_QUIZ = `${DOMAIN_NAME}/Quiz/Generate`;

export const URL_GET_USER_INFO =
  "https://www.googleapis.com/oauth2/v3/userinfo";

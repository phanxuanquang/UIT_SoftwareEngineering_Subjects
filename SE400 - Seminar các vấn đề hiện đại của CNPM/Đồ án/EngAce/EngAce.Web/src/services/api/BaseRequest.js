import axios from "axios";
import { DOMAIN_NAME } from "../url";
import Cookies from "js-cookie";

const baseRequest = axios.create({
  baseURL: DOMAIN_NAME,
  headers: {
    "Content-Type": "application/json",
  },
});

// Add a request interceptor to set the Authentication header dynamically
baseRequest.interceptors.request.use(
  (config) => {
    const token = Cookies.get("token");
    if (token) {
      config.headers["Authentication"] = token;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default baseRequest;

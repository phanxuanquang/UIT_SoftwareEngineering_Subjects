import Cookies from "js-cookie";
import { redirect } from "react-router-dom";

export function checkAuthLoader() {
  const token = Cookies.get("token");
  if (!token) {
    return redirect("/auth");
  }
  return null;
}

export function checkEnglishLevelLoader() {
  const token = Cookies.get("token");
  const level = localStorage.getItem("level");
  if (!token) {
    return redirect("/auth");
  } else if (!level) {
    return redirect("/level");
  }
  return null;
}

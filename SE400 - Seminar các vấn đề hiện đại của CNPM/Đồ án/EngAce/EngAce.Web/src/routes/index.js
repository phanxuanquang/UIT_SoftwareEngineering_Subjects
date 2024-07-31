import { createBrowserRouter } from "react-router-dom";
import Auth from "../pages/Auth";
import Home from "../pages/Home";
import Level from "../pages/Level";
import Test from "../pages/Test";
import MainLayout from "../layout/MainLayout";
import { checkAuthLoader, checkEnglishLevelLoader } from "../utils/auth";
import Dictionary from "../pages/Dictionary";
import Writing from "../pages/Writing";
import ChatPage from "../pages/ChatPage";
import Introduction from "../pages/Introduction";

export const router = createBrowserRouter([
  {
    path: "/",
    loader: checkEnglishLevelLoader,
    children: [
      { index: true, element: <Home /> },
      {
        path: "dictionary",
        element: <MainLayout />,
        children: [{ index: true, element: <Dictionary /> }],
      },
      {
        path: "test",
        element: <MainLayout />,
        children: [{ index: true, element: <Test /> }],
      },
      {
        path: "writing",
        element: <MainLayout />,
        children: [{ index: true, element: <Writing /> }],
      },
      {
        path: "chat",
        element: <MainLayout />,
        children: [{ index: true, element: <ChatPage /> }],
      },
      {
        path: "introduction",
        element: <Introduction />,
      },
    ],
  },
  {
    path: "/auth",
    element: <Auth />,
  },
  {
    path: "/level",
    element: <Level />,
    loader: checkAuthLoader,
  },
]);

import MenuBookIcon from "@mui/icons-material/MenuBook";
import QuizIcon from "@mui/icons-material/Quiz";
import DrawIcon from "@mui/icons-material/Draw";
import QuestionAnswerIcon from "@mui/icons-material/QuestionAnswer";

const navConfig = [
  {
    title: "Từ điển",
    path: "/dictionary",
    icon: <MenuBookIcon />,
  },
  {
    title: "Bài tập",
    path: "/test",
    icon: <QuizIcon />,
  },
  {
    title: "Luyện viết",
    path: "/writing",
    icon: <DrawIcon />,
  },
  {
    title: "Tư vấn",
    path: "/chat",
    icon: <QuestionAnswerIcon />,
  },
];

export default navConfig;

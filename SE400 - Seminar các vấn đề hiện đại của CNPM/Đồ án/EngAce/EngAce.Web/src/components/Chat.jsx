import PropTypes from "prop-types";
import { Avatar, Paper } from "@mui/material";
import { deepOrange } from "@mui/material/colors";
import MarkdownView from "react-showdown";
import ChatLoader from "../common/ChatLoader";

const Chat = ({ isAnswer, content, loading }) => {
  return (
    <Paper
      sx={{
        width: "100%",
        bgcolor: "transparent",
        display: "flex",
        gap: 1,
        justifyContent: isAnswer ? "flex-start" : "flex-end",
      }}
    >
      {isAnswer && (
        <Avatar
          sx={{ bgcolor: deepOrange[500], display: { xs: "none", md: "flex" } }}
        >
          B
        </Avatar>
      )}
      <Paper
        sx={{
          width: "content-fit",
          marginLeft: !isAnswer && "10%",
          marginRight: isAnswer && "10%",
          bgcolor: isAnswer ? "primary.lighter" : "secondary.lighter",
          p: 1.2,
          display: "flex",
          flexDirection: "column",
          gap: 1,
        }}
      >
        {loading ? (
          <ChatLoader />
        ) : (
          <MarkdownView
            className="markdown"
            markdown={content}
            options={{ tables: true, emoji: true }}
          />
        )}
      </Paper>
    </Paper>
  );
};

Chat.propTypes = {
  isAnswer: PropTypes.bool,
  content: PropTypes.string,
  loading: PropTypes.bool,
};

export default Chat;

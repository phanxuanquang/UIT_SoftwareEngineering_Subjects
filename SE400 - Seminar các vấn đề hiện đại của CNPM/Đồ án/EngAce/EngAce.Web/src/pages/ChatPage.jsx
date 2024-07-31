import {
  Box,
  IconButton,
  InputAdornment,
  Paper,
  TextField,
} from "@mui/material";
import { useEffect, useRef, useState } from "react";
import SendIcon from "@mui/icons-material/Send";
import { useDispatch, useSelector } from "react-redux";
import * as SagaActionTypes from "../redux/constants";
import Chat from "../components/Chat";
import Background from "../assets/ChatbotBackground.png"

const ChatPage = () => {
  const dispatch = useDispatch();
  const { listChat } = useSelector((state) => state.chatbotSlice);
  const [loading, setLoading] = useState(false);
  const [newMessage, setNewMessage] = useState("");
  const paperRef = useRef(null);

  useEffect(() => {
    if (paperRef.current) {
      paperRef.current.scrollTop = paperRef.current.scrollHeight;
    }
  }, [listChat, loading]);

  const handleSendMessage = () => {
    if (newMessage.trim() !== "") {
      dispatch({
        type: SagaActionTypes.SEND_MESSAGE_SAGA,
        message: newMessage,
        onLoading: () => setLoading(true),
        onFinish: () => setLoading(false),
      });
      setNewMessage("");
    }
  };

  const handleEnterMesage = (key) => {
    if (key.key === "Enter" && !loading) {
      handleSendMessage();
    }
  };

  return (
    <div
      style={{
        height: "100%",
        width: "100%",
        backgroundImage:
          `linear-gradient(rgba(255, 255, 255, 0.8), rgba(255, 255, 255, 0.8)), url(${Background})`,
        backgroundSize: "40%",
        backgroundPosition: "center",
        backgroundRepeat: "no-repeat",
      }}
    >
      <Paper
        ref={paperRef}
        sx={{
          display: "flex",
          alignItems: "center",
          flexDirection: "column",
          gap: "10px",
          height: "calc(100% - 90px)",
          width: "100%",
          backgroundColor: "transparent",
          marginLeft: "auto",
          marginRight: "auto",
          padding: "10px",
          overflow: "auto",
        }}
      >
        {listChat.map((item, index) => (
          <Chat
            key={index}
            content={item.Message}
            isAnswer={!item.FromUser}
          ></Chat>
        ))}
        {loading && <Chat loading={true} isAnswer={true}></Chat>}
      </Paper>

      <Paper
        elevation={4}
        sx={{
          width: "100%",
          display: "flex",
          gap: 1,
          position: "fixed",
          bottom: 0,
          left: 0,
          padding: "8px 8px 16px 8px",
          justifyContent: "center",
        }}
      >
        <Box
          sx={{
            width: "90%",
            maxWidth: 1000,
            minWidth: 200,
            position: "relative",
          }}
        >
          <TextField
            inputProps={{ style: { fontWeight: 500 }, autoComplete: "off" }}
            variant="outlined"
            placeholder="Aa . . ."
            fullWidth
            color="secondary"
            value={newMessage}
            onChange={(e) => setNewMessage(e.target.value)}
            onKeyDown={(key) => {
              handleEnterMesage(key);
            }}
            InputProps={{
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton
                    aria-label="send button"
                    disabled={loading}
                    onClick={handleSendMessage}
                    edge="end"
                  >
                    <SendIcon />
                  </IconButton>
                </InputAdornment>
              ),
            }}
          />
        </Box>
      </Paper>
    </div>
  );
};

export default ChatPage;

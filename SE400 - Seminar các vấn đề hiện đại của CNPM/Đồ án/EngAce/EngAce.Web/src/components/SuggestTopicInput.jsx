import PropTypes from "prop-types";
import {
  Box,
  Button,
  Chip,
  InputLabel,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import * as SagaActionTypes from "../redux/constants";
import { useState } from "react";
import ChatLoader from "../common/ChatLoader";

export default function SuggestTopicInput({ topic, setTopic, error }) {
  const dispatch = useDispatch();
  const level = localStorage.getItem("level");
  const [loading, setLoading] = useState(false);
  const { topics } = useSelector((state) => state.quizSlice);

  const handleSuggestTopic = () => {
    dispatch({
      type: SagaActionTypes.GET_SUGGEST_TOPICS,
      level: level,
      onLoading: () => setLoading(true),
      onFinish: () => setLoading(false),
    });
  };

  const handleChooseTopic = (topic) => {
    setTopic(topic);
  };

  return (
    <Box sx={{ display: "flex", flexDirection: "column", marginBottom: 1 }}>
      <InputLabel htmlFor="topic">
        <Typography
          id="modal-modal-title"
          variant="h6"
          sx={{ color: "primary.black" }}
        >
          Chủ đề cho bài tập
        </Typography>
      </InputLabel>
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          marginBottom: 1.5,
          gap: 1,
        }}
      >
        <TextField
          id="topic"
          variant="standard"
          placeholder="Ex: Traveling"
          sx={{
            flexGrow: 1,
            height: "2.5rem",
            "& .MuiInputBase-root": {
              height: "100%",
            },
          }}
          value={topic}
          error={!!error}
          onChange={(e) => setTopic(e.target.value)}
          helperText={error}
        />
        <Button
          variant="outlined"
          color="secondary"
          target="_blank"
          sx={{
            height: "2.5rem",
            textTransform: "none",
          }}
          onClick={handleSuggestTopic}
        >
          Gợi ý
        </Button>
      </Box>
      <Stack
        spacing={{ xs: 1, sm: 2 }}
        direction="row"
        useFlexGap
        flexWrap="wrap"
      >
        {!loading &&
          topics.map((topicItem) => (
            <Chip
              key={topicItem}
              label={topicItem}
              variant={topicItem === topic ? "filled" : "outlined"}
              onClick={() => handleChooseTopic(topicItem)}
            />
          ))}

        {loading && <ChatLoader />}
      </Stack>
    </Box>
  );
}

SuggestTopicInput.propTypes = {
  topic: PropTypes.string.isRequired,
  setTopic: PropTypes.func.isRequired,
  error: PropTypes.string.isRequired,
};

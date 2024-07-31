import {
  Box,
  Button,
  FormControl,
  InputLabel,
  TextField,
  Typography,
} from "@mui/material";
import { useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import PropTypes from "prop-types";

export default function WritingForm({ onClosePannel }) {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const [content, setContent] = useState(searchParams.get("content") ?? "");

  const handleSearch = () => {
    if (content) {
      navigate(`?content=${encodeURIComponent(content.trim())}`);
      onClosePannel ? onClosePannel(false) : null;
    }
  };

  return (
    <Box display="flex" flexDirection="column" gap={2} sx={{ width: "100%" }}>
      <Typography variant="h1" textAlign={"center"}>
        LUYỆN VIẾT
      </Typography>
      <Box sx={{ width: "100%" }}>
        <Typography
          variant="body1"
          component={InputLabel}
          required
          htmlFor="search"
          sx={{ color: "#202124", marginBottom: "0.5rem" }}
        >
          Nội dung bài viết của bạn
        </Typography>
        <FormControl fullWidth>
          <TextField
            id="search"
            variant="outlined"
            placeholder="Ex: My name is EngAce. I live in Vietnam. I work in small software company. I like listen to music and watch movie. English very hard for me. I learn English very long ago. My talk not clear, hard for people understand. I feel shy when speak English. I want to improve for job and future."
            value={content}
            onChange={(e) => setContent(e.target.value)}
            multiline
            rows={12}
          />
        </FormControl>
      </Box>
      <Button
        variant="contained"
        sx={{
          width: "fit-content",
          alignSelf: "center",
          color: "white",
          "&:not(:disabled)": {
            background: "linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)",
            transition:
              "transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out",
            "&:hover": {
              background: "linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)",
              opacity: 0.8,
              transform: "scale(1.05)",
              boxShadow: "0 0.2rem 1.2rem rgba(255, 0, 0, 0.2)",
            },
          },
        }}
        size="large"
        disabled={!content.trim()}
        onClick={handleSearch}
      >
        ĐÁNH GIÁ
      </Button>
    </Box>
  );
}

WritingForm.propTypes = {
  onClosePannel: PropTypes.func,
};

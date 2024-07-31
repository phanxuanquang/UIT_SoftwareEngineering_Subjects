import {
  Box,
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Typography,
} from "@mui/material";
import { useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import PropTypes from "prop-types";

export default function DistionarySearchForm({ onClosePannel }) {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const [keyword, setSearch] = useState(searchParams.get("keyword") ?? "");
  const [context, setContext] = useState(searchParams.get("context") ?? "");
  const [mode, setMode] = useState(
    searchParams.get("useEnglishToExplain") === "true" ? true : false
  );

  const handleSearch = () => {
    if (keyword) {
      navigate(
        `?keyword=${encodeURIComponent(
          keyword.trim()
        )}&context=${encodeURIComponent(
          context.trim()
        )}&useEnglishToExplain=${mode}`
      );
      onClosePannel ? onClosePannel(false) : null;
    }
  };

  return (
    <Box display="flex" flexDirection="column" gap={2} sx={{ width: "100%" }}>
      <Typography variant="h2" textAlign={"center"}>
        TỪ ĐIỂN
      </Typography>
      <Box sx={{ width: "100%" }}>
        <Typography
          variant="body1"
          component={InputLabel}
          required
          htmlFor="search"
          color={"primary.black"}
          sx={{ marginBottom: "0.5rem" }}
        >
          Từ hoặc cụm từ cần tra
        </Typography>
        <FormControl fullWidth>
          <TextField
            id="search"
            variant="outlined"
            placeholder="Have a crush on"
            value={keyword}
            onChange={(e) => setSearch(e.target.value)}
          />
        </FormControl>
      </Box>
      <Box sx={{ width: "100%" }}>
        <Typography
          variant="body1"
          component={InputLabel}
          htmlFor="context"
          color={"primary.black"}
          sx={{ marginBottom: "0.5rem" }}
        >
          Ngữ cảnh nếu có
        </Typography>
        <FormControl fullWidth>
          <TextField
            id="context"
            variant="outlined"
            placeholder="I have a crush on you for a long time"
            value={context}
            onChange={(e) => setContext(e.target.value)}
          />
        </FormControl>
      </Box>
      <Box sx={{ width: "100%" }}>
        <Typography
          variant="body1"
          component={InputLabel}
          htmlFor="mode"
          id="mode-label"
          color={"primary.black"}
          sx={{ marginBottom: "0.5rem" }}
        >
          Loại từ điển
        </Typography>
        <FormControl fullWidth>
          <Select
            labelId="mode-label"
            id="mode"
            defaultValue={false}
            value={mode}
            onChange={(e) => setMode(e.target.value)}
          >
            <MenuItem value={true}>Anh - Anh</MenuItem>
            <MenuItem value={false}>Anh - Việt</MenuItem>
          </Select>
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
        disabled={!keyword.trim()}
        onClick={handleSearch}
      >
        TRA CỨU
      </Button>
    </Box>
  );
}

DistionarySearchForm.propTypes = {
  onClosePannel: PropTypes.func,
};

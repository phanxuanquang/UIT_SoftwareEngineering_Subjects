import { Box, IconButton, Typography } from "@mui/material";
import ArrowBackIosIcon from "@mui/icons-material/ArrowBackIos";
import ArrowForwardIosIcon from "@mui/icons-material/ArrowForwardIos";
import PropTypes from "prop-types";

export default function QuestionIndex({ index, length, setIndex }) {
  const handleBack = () => {
    setIndex(index - 1);
  };

  const handleNext = () => {
    setIndex(index + 1);
  };

  const formatIndex = (index) => {
    return index < 9 ? `0${index + 1}` : index + 1;
  };

  return (
    <Box
      sx={{
        width: "100%",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <IconButton disabled={index === 0} onClick={handleBack}>
        <ArrowBackIosIcon />
      </IconButton>
      <Typography sx={{ mx: 2 }}>
        {formatIndex(index)} / {length < 10 ? `0${length}` : length}
      </Typography>
      <IconButton disabled={index === length - 1} onClick={handleNext}>
        <ArrowForwardIosIcon />
      </IconButton>
    </Box>
  );
}

QuestionIndex.propTypes = {
  index: PropTypes.number.isRequired,
  length: PropTypes.number.isRequired,
  setIndex: PropTypes.func.isRequired,
};

import PropTypes from "prop-types";
import { Chip, Typography } from "@mui/material";

export default function MyCustomQuizChip({
  index,
  isDone,
  setIndex,
  isActive,
  submit,
  result,
}) {
  const handleOnClick = () => {
    setIndex(index);
  };
  return (
    <Chip
      variant={!isDone ? "outlined" : "filled"}
      label={
        <Typography
          variant="body2"
          sx={{
            position: "absolute",
            top: "50%",
            left: "50%",
            transform: "translate(-50%, -50%)",
            mb: 1.5,
          }}
        >
          {index + 1}
        </Typography>
      }
      sx={{
        width: 32,
        height: 32,
        position: "relative",
        border: isActive ? "solid 2px black" : "solid 1px grey",
        bgcolor: !submit ? "" : result ? "#AAF27F" : "#FFA48D",
      }}
      onClick={handleOnClick}
    />
  );
}

MyCustomQuizChip.propTypes = {
  index: PropTypes.number.isRequired,
  isDone: PropTypes.bool.isRequired,
  setIndex: PropTypes.func.isRequired,
  isActive: PropTypes.bool.isRequired,
  submit: PropTypes.bool.isRequired,
  result: PropTypes.bool,
};

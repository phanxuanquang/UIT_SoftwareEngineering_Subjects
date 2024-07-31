import { Box, InputLabel, TextField, Typography } from "@mui/material";
import PropTypes from "prop-types";

export default function QuestionsQuantity({ quantity, setQuantity, error }) {
  return (
    <Box
      sx={{ display: "flex", flexDirection: "column", gap: 1, marginBottom: 3 }}
    >
      <InputLabel htmlFor="quantity">
        <Typography
          id="modal-modal-title"
          variant="h6"
          sx={{ color: "primary.black" }}
        >
          Số lượng câu hỏi
        </Typography>
      </InputLabel>
      <TextField
        id="quantity"
        type="number"
        variant="standard"
        value={quantity}
        onChange={(e) => setQuantity(e.target.value)}
        error={!!error}
        helperText={!error ? "Số lượng câu hỏi từ 10 đến 40" : error}
        inputProps={{
          min: 10,
          max: 40,
        }}
        sx={{
          height: "2.5rem",
          "& .MuiInputBase-root": {
            height: "100%",
          },
          "& input[type=number]::-webkit-outer-spin-button, & input[type=number]::-webkit-inner-spin-button":
            {
              WebkitAppearance: "auto",
              margin: 0,
            },
          "& input[type=number]": {
            MozAppearance: "textfield",
          },
        }}
      />
    </Box>
  );
}
QuestionsQuantity.propTypes = {
  quantity: PropTypes.number.isRequired,
  setQuantity: PropTypes.func.isRequired,
  error: PropTypes.string.isRequired,
};

import {
  Divider,
  FormControl,
  FormControlLabel,
  FormLabel,
  Radio,
  RadioGroup,
  Typography,
} from "@mui/material";
import PropTypes from "prop-types";
import { useDispatch } from "react-redux";
import { quizActions } from "../redux/reducer/QuizReducer";

export default function QuestionRadioGroup({
  question,
  answer,
  index: qIndex,
  submit,
}) {
  const dispatch = useDispatch();
  const handleChange = (event) => {
    dispatch(quizActions.setAnswerList({ qIndex, value: event.target.value }));
  };

  return (
    <FormControl disabled={submit} fullWidth sx={{ paddingLeft: "16px" }}>
      <FormLabel>
        <Typography variant="body1" sx={{ color: "black !important" }}>
          <strong>Câu hỏi {qIndex + 1}: </strong> {question.Question}
        </Typography>
      </FormLabel>
      <RadioGroup
        name="radio-buttons-group"
        onChange={handleChange}
        value={answer[qIndex] || ""}
        sx={{ mb: 1.5, mt: 1 }}
      >
        {question.Options.map((option, index) => (
          <FormControlLabel
            key={option}
            value={index}
            control={<Radio />}
            label={
              <Typography
                sx={{
                  color: !submit
                    ? "black"
                    : question.RightOptionIndex === index
                    ? "success.dark"
                    : "grey",
                  fontWeight: !submit
                    ? "normal"
                    : question.RightOptionIndex === index
                    ? "bold"
                    : "normal",
                }}
              >
                {option}
              </Typography>
            }
          />
        ))}
      </RadioGroup>
      <Divider />
      {submit && (
        <Typography
          sx={{
            marginTop: 1.5,
            marginBottom: 1.5,
            color: "info.dark",
          }}
        >
          <strong>Giải thích:</strong> {question.ExplanationInVietnamese}
        </Typography>
      )}
      <Divider />
    </FormControl>
  );
}
QuestionRadioGroup.propTypes = {
  question: PropTypes.object.isRequired,
  answer: PropTypes.array.isRequired,
  index: PropTypes.number.isRequired,
  submit: PropTypes.bool.isRequired,
};

import PropTypes from "prop-types";
import {
  Checkbox,
  FormControl,
  FormControlLabel,
  FormGroup,
  FormHelperText,
  FormLabel,
  Typography,
} from "@mui/material";
import { useEffect } from "react";
import { useSelector } from "react-redux";
import CircleCheckedFilled from '@mui/icons-material/CheckCircle';
import CircleUnchecked from '@mui/icons-material/RadioButtonUnchecked';

export default function QuestionsTypeForm({ types, setTypes, error }) {
  const { qTypes } = useSelector((state) => state.quizSlice);

  const handleCheckboxChange = (key) => {
    const updatedStates = {
      ...types,
      [key]: !types[key],
    };
    setTypes(updatedStates);
  };

  useEffect(() => {
    const initialStates = Object.keys(qTypes).reduce((acc, key) => {
      acc[key] = false;
      return acc;
    }, {});

    setTypes(initialStates);
  }, [qTypes, setTypes]);

  return (
    <FormControl
      component="fieldset"
      variant="standard"
      error={!!error}
      sx={{ paddingLeft: { sx: 0, md: 1 }, marginBottom: 1.5}}
    >
      <FormLabel component="legend">
        <Typography
          id="modal-modal-title"
          variant="h6"
          sx={{ color: "primary.black", marginBottom: 1 }}
        >
          Chọn loại câu hỏi
        </Typography>
      </FormLabel>
      <FormGroup>
        {Object.keys(qTypes).map((key) => (
          <FormControlLabel
            sx={{ marginTop: 0.6 }}
            key={key}
            control={<Checkbox icon={<CircleUnchecked />}
            checkedIcon={<CircleCheckedFilled />} checked={!!types[key]} />}
            onChange={() => handleCheckboxChange(key)}
            label={
              <Typography
                id="modal-modal-title"
                variant="body3"
                sx={{ color: "primary.black" }}
              >
                {qTypes[key]}
              </Typography>
            }
          />
        ))}
      </FormGroup>
      {!!error && <FormHelperText>{error}</FormHelperText>}
    </FormControl>
  );
}

QuestionsTypeForm.propTypes = {
  types: PropTypes.object.isRequired,
  setTypes: PropTypes.func.isRequired,
  error: PropTypes.string.isRequired,
};

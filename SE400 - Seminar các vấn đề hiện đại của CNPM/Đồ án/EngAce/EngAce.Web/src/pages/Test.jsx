import { Box, CircularProgress, Typography } from "@mui/material";
import TestGenerateForm from "../components/TestGenerateForm";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import * as SagaActionTypes from "../redux/constants";
import QuestionAndAnswer from "../components/QuestionAndAnswer";

export default function Test() {
  const dispatch = useDispatch();
  const { qaList } = useSelector((state) => state.quizSlice);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    dispatch({
      type: SagaActionTypes.GET_QUIZ_TYPES,
      onLoading: () => setLoading(true),
      onFinish: () => setLoading(false),
    });
  }, [dispatch]);

  if (qaList.length > 0) {
    return <QuestionAndAnswer />;
  }

  return (
    <Box
      sx={{
        height: "100%",
        width: "100%",
      }}
      display="flex"
      justifyContent="center"
      alignItems="center"
      flexDirection="column"
      gap={2}
    >
      {!loading ? (
        <>
          <Typography variant="h2">BÀI TẬP</Typography>
          <TestGenerateForm />
        </>
      ) : (
        <CircularProgress />
      )}
    </Box>
  );
}

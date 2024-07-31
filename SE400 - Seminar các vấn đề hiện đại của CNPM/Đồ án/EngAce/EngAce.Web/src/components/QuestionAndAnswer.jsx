import { Divider, Grid, Box, Hidden } from "@mui/material";
import { useSelector } from "react-redux";
import { useState, useRef } from "react";
import QuestionRadioGroup from "./QuestionRadioGroup";
import QuestionIndex from "./QuestionIndex";
import QuizzStatus from "./QuizzStatus";

export default function QuestionAndAnswer() {
  const { qaList, answer, submit } = useSelector((state) => state.quizSlice);
  const [index, setIndex] = useState(0);
  const questionRefs = useRef([]);

  const scrollToQuestion = (i) => {
    setIndex(i);
    if (questionRefs.current[i]) {
      questionRefs.current[i].scrollIntoView({ behavior: "smooth" });
      questionRefs.current[i].classList.add("highlight");
      setTimeout(() => {
        questionRefs.current[i]?.classList.remove("highlight");
      }, 2000);
    }
  };

  return (
    <Box
      sx={{
        height: "100%",
        overflow: "auto",
      }}
    >
      <Grid container spacing={2}>
        <Grid
          item
          xs={12}
          md={9}
          sx={{
            display: "flex",
            flexDirection: "column",
            gap: 2,
            mt: 1,
          }}
        >
          <Hidden mdUp>
            <QuestionIndex
              index={index}
              length={qaList.length}
              setIndex={setIndex}
            />
            <Divider />
            <QuestionRadioGroup
              question={qaList[index]}
              answer={answer}
              index={index}
              submit={submit}
            />
          </Hidden>

          <Hidden mdDown>
            {qaList.map((question, i) => (
              <Box key={i} ref={(el) => (questionRefs.current[i] = el)}>
                <QuestionRadioGroup
                  question={question}
                  answer={answer}
                  index={i}
                  submit={submit}
                />
              </Box>
            ))}
          </Hidden>
        </Grid>

        <Grid item xs={12} md={3}>
          <QuizzStatus
            qaList={qaList}
            answer={answer}
            setIndex={scrollToQuestion}
            index={index}
            submit={submit}
          />
        </Grid>
      </Grid>
    </Box>
  );
}

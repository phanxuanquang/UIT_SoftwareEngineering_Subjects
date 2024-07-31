import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Box,
  Grid,
  Hidden,
} from "@mui/material";
import WritingForm from "../components/WritingForm";
import { useSearchParams } from "react-router-dom";
import { AppService } from "../services/api";
import { useEffect, useState } from "react";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import ChatLoader from "../common/ChatLoader";
import { MuiMarkdown, getOverrides } from "mui-markdown";
import AlertCustom from "../common/Alert";

export default function Writing() {
  const [searchParams] = useSearchParams();
  const content = searchParams.get("content");
  const level = localStorage.getItem("level");
  const [review, setReview] = useState({});
  const [loading, setLoading] = useState(false);
  const [expanded, setExpanded] = useState(false);

  const handleCloseAccordion = () => () => {
    setExpanded((pre) => !pre);
  };

  useEffect(() => {
    const fetchEssayReview = async () => {
      try {
        if (content) {
          setLoading(true);
          const response = await AppService.getEssayReview(content, level);
          if (response.status === 200) {
            console.log(response.data);
            setReview(response.data);
          } else {
            AlertCustom({
              type: "error",
              title: response?.data || "Có lỗi xảy ra, vui lòng thử lại",
            });
          }
        }
      } catch (error) {
        AlertCustom({
          type: "error",
          title: error.response?.data || "Có lỗi xảy ra, vui lòng thử lại",
        });
      } finally {
        setLoading(false);
      }
    };

    fetchEssayReview();
  }, [content, level]);

  if (content) {
    return (
      <Box
        sx={{
          height: "100%",
          overflow: "auto",
        }}
      >
        <Grid container columnSpacing={4}>
          <Hidden mdDown>
            <Grid item xs={6}>
              <Box sx={{ position: "sticky", top: 0 }}>
                <WritingForm />
              </Box>
            </Grid>
          </Hidden>
          <Grid item xs={12} md={6}>
            <Hidden mdUp>
              <Accordion
                sx={{
                  padding: 0,
                  position: "sticky",
                  top: 0,
                  zIndex: 100,
                  boxShadow: "1px 1px 1px 1px #ccc",
                }}
                expanded={expanded}
                onChange={handleCloseAccordion()}
              >
                <AccordionSummary
                  expandIcon={<ExpandMoreIcon />}
                  aria-controls="panel1-content"
                  id="panel1-header"
                  sx={{ bgcolor: "#f5f5f5", borderRadius: 0 }}
                >
                  Bài viết của bạn
                </AccordionSummary>
                <AccordionDetails>
                  <WritingForm onClosePannel={setExpanded} />
                </AccordionDetails>
              </Accordion>
            </Hidden>

            {!loading ? (
              <>
                <Accordion
                  sx={{
                    padding: 0,
                    marginBottom: 2,
                  }}
                  defaultExpanded
                >
                  <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel1-content"
                    id="panel1-header"
                    sx={{ bgcolor: "#f5f5f5", borderRadius: 0 }}
                  >
                    Nhận xét
                  </AccordionSummary>
                  <AccordionDetails>
                    <MuiMarkdown
                      overrides={{
                        ...getOverrides({}),
                        div: {
                          props: {
                            style: { lineHeight: 2, textAlign: "justify" },
                          },
                        },
                        h2: {
                          component: "h1",
                          props: {
                            style: { color: "#e28048" },
                          },
                        },
                        ul: {
                          props: {
                            style: { marginLeft: 15 },
                          },
                        },
                        p: {
                          props: {
                            style: { marginBottom: 2 },
                          },
                        },
                      }}
                    >
                      {`<div>${review?.GeneralComment ?? ""}</div>`}
                    </MuiMarkdown>
                  </AccordionDetails>
                </Accordion>
                <MuiMarkdown
                  overrides={{
                    ...getOverrides({}),
                    div: {
                      props: {
                        style: { lineHeight: 2, textAlign: "justify" },
                      },
                    },
                    h2: {
                      component: "h1",
                      props: {
                        style: { color: "#e28048" },
                      },
                    },
                    ul: {
                      props: {
                        style: { marginLeft: 20 },
                      },
                    },
                    p: {
                      props: {
                        style: { marginBottom: 2 },
                      },
                    },
                  }}
                >
                  {`<div>${review?.ImprovedContent ?? ""}</div>`}
                </MuiMarkdown>
              </>
            ) : (
              <Box
                sx={{
                  display: "flex",
                  justifyContent: "center",
                  alignItems: "center",
                  padding: 4,
                }}
              >
                <ChatLoader />
              </Box>
            )}
          </Grid>
        </Grid>
      </Box>
    );
  }

  return (
    <Box
      sx={{
        height: "100%",
        width: "80%",
        margin: "auto",
      }}
      display="flex"
      justifyContent="center"
      alignItems="center"
      flexDirection="column"
    >
      <WritingForm />
    </Box>
  );
}

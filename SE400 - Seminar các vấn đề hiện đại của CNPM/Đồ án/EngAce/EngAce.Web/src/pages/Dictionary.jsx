import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Box,
  Grid,
  Hidden,
} from "@mui/material";
import DistionarySearchForm from "../components/DistionarySearchForm";
import { useSearchParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { AppService } from "../services/api";
import { MuiMarkdown, getOverrides } from "mui-markdown";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import ChatLoader from "../common/ChatLoader";
import AlertCustom from "../common/Alert";

export default function Dictionary() {
  const [searchParams, setSearchParams] = useSearchParams();
  const keyword = searchParams.get("keyword");
  const context = searchParams.get("context");
  const useEnglishToExplain = searchParams.get("useEnglishToExplain");
  const [markdown, setMarkDown] = useState("");
  const [expanded, setExpanded] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleCloseAccordion = () => () => {
    setExpanded((pre) => !pre);
  };

  useEffect(() => {
    if (useEnglishToExplain !== "true" && useEnglishToExplain !== "false") {
      setSearchParams((prev) => ({
        ...Object.fromEntries(prev.entries()),
        useEnglishToExplain: false,
      }));
    }
    const fetchDictionarySearch = async () => {
      try {
        if (keyword) {
          setLoading(true);
          const response = await AppService.getDictionarySearch(
            keyword,
            context,
            useEnglishToExplain
          );
          if (response.status === 200 || response.status === 201) {
            console.log(response.data);
            setMarkDown(response.data);
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

    fetchDictionarySearch();
  }, [keyword, context, useEnglishToExplain, setSearchParams]);

  if (keyword) {
    return (
      <Box
        sx={{
          height: "100%",
          overflow: "auto",
        }}
      >
        <Grid container columnSpacing={4}>
          <Hidden mdDown>
            <Grid item xs={4}>
              <Box sx={{ position: "sticky", top: 0 }}>
                <DistionarySearchForm />
              </Box>
            </Grid>
          </Hidden>
          <Grid item xs={12} md={8}>
            <Hidden mdUp>
              <Accordion
                sx={{
                  padding: 0,
                  position: "sticky",
                  top: 0,
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
                  Tra cứu
                </AccordionSummary>
                <AccordionDetails>
                  <DistionarySearchForm onClosePannel={setExpanded} />
                </AccordionDetails>
              </Accordion>
            </Hidden>

            {!loading ? (
              <MuiMarkdown
                overrides={{
                  ...getOverrides({}),
                  div: {
                    props: {
                      style: { lineHeight: 2 },
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
                {`<div>${markdown}</div>`}
              </MuiMarkdown>
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
      <DistionarySearchForm />
    </Box>
  );
}

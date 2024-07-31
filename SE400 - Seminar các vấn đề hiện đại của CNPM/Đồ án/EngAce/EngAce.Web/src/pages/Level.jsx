import {
  Box,
  Container,
  CssBaseline,
  FormControl,
  MenuItem,
  Select,
  Typography,
  Button,
  TextField,
  CircularProgress,
} from "@mui/material";
import Cookies from "js-cookie";
import { useEffect, useState } from "react";
import { AppService } from "../services/api";
import { useNavigate } from "react-router-dom";
import Logo from "../assets/user.png";
import AlertCustom from "../common/Alert";
export default function Level() {
  const token = Cookies.get("token");
  const initName = localStorage.getItem("name");
  const initLevel = localStorage.getItem("level");

  const [data, setData] = useState({});

  const [level, setLevel] = useState(initLevel ?? "1");
  const [englishLevels, setEnglishLevels] = useState({});
  const [name, setName] = useState(initName ?? "");
  const [userLoading, setUserLoading] = useState(false);
  const [levelLoading, setLevelLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUserInfo = async () => {
      try {
        setUserLoading(true);
        if (token.startsWith("ya29.")) {
          const response = await AppService.getUserInfo();
          if (response.status === 200) {
            console.log(response.data);
            setData(response.data);
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
        setUserLoading(false);
      }
    };

    const fetchEnglishLevels = async () => {
      try {
        setLevelLoading(true);
        const response = await AppService.getEnglishLevel();

        if (response.status === 200) {
          console.log(response.data);
          setEnglishLevels(response.data);
        }
      } catch (error) {
        console.log(error);
      } finally {
        setLevelLoading(false);
      }
    };

    fetchEnglishLevels();
    fetchUserInfo();
  }, [token]);

  const handleChange = (event) => {
    setLevel(event.target.value);
  };

  const handleSubmit = () => {
    if (token.startsWith("ya29.")) {
      const fullName = data?.family_name + " " + data?.given_name;

      localStorage.setItem("name", fullName);
      localStorage.setItem("picture", data?.picture);
      localStorage.setItem("level", level);
    } else {
      localStorage.setItem("name", name);
      localStorage.setItem("level", level);
    }
    navigate("/");
  };

  return (
    <Box
      sx={{
        backgroundImage: `
          linear-gradient(
            200deg,
            #E3FDFFff,
            #DFFBFFff,
            #DBF8FFff,
            #D6F6FFff,
            #D2F3FFff,
            #CEF1FFff,
            #CAEEFFff,
            #C6ECFFff,
            #C2E9FFff,
            #BDE7FFff,
            #B9E4FFff,
            #B5E2FFff,
            #B1DFFFff,
            #ADDDFFff,
            #A9DAFFff,
            #A4D8FFff,
            #A0D5FFff,
            #9CD3FFff
          )
        `,
        backgroundSize: "cover",
        backgroundPosition: "center",
        height: "100vh",
      }}
    >
      <CssBaseline />
      <Container fixed sx={{ height: "100%" }}>
        <Box
          display="flex"
          justifyContent="center"
          alignItems="center"
          height={"100%"}
        >
          {!userLoading && !levelLoading && (
            <Box
              display="flex"
              justifyContent="center"
              alignItems="center"
              flexDirection="column"
              gap={2}
              sx={{
                width: "80%",
                height: "100%",
              }}
            >
              <Box sx={{ textAlign: "center" }}>
                {data?.picture ? (
                  <img
                    src={data?.picture.replace("=s96-c", "")}
                    alt="User's avatar"
                    style={{
                      maxWidth: 300,
                      maxheight: 300,
                      width: "100%",
                      height: "auto",
                      borderRadius: "50%",
                      border: "0.5rem solid #ffa16c",
                    }}
                  />
                ) : (
                  <img
                    src={Logo}
                    alt="Default avatar"
                    style={{
                      maxWidth: 300,
                      maxheight: 300,
                      width: "100%",
                      height: "auto",
                    }}
                  />
                )}
              </Box>
              {token.startsWith("ya29.") ? (
                <Typography variant="h3" sx={{ fontWeight: "normal" }}>
                  Xin chào {data?.family_name} {data?.given_name}
                </Typography>
              ) : (
                <FormControl sx={{ m: 1, minWidth: 120, width: "100%" }}>
                  <Typography variant="body1" sx={{ marginBottom: "0.5rem" }}>
                    Tên của bạn là
                  </Typography>
                  <TextField
                    id="outlined-basic"
                    variant="outlined"
                    required
                    sx={{
                      width: "100%",
                      "& .MuiInputBase-input": {
                        bgcolor: "white",
                        borderRadius: 1,
                      },
                    }}
                    placeholder="Nguyễn Văn A"
                    onChange={(e) => setName(e.target.value)}
                    value={name}
                  />
                </FormControl>
              )}

              {Object.keys(englishLevels).length > 0 && (
                <FormControl sx={{ m: 1, minWidth: 120, width: "100%" }}>
                  <Typography variant="body1" sx={{ marginBottom: "0.5rem" }}>
                    Trình độ tiếng Anh của bạn
                  </Typography>
                  <Select
                    id="level"
                    value={level}
                    onChange={handleChange}
                    sx={{
                      bgcolor: "white",
                    }}
                    MenuProps={{
                      PaperProps: {
                        style: {
                          width: 0,
                        },
                      },
                    }}
                  >
                    {Object.keys(englishLevels).map((key) => (
                      <MenuItem
                        key={key}
                        value={key}
                        sx={{
                          whiteSpace: "normal",
                          wordWrap: "break-word",
                        }}
                      >
                        {
                          <span>
                            <strong>{englishLevels[key].split(":")[0]}:</strong>
                            {englishLevels[key].split(":")[1]}
                          </span>
                        }
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              )}
              <Button
                variant="contained"
                size="large"
                sx={{
                  color: "white",
                  fontSize: "1rem",
                  "&:not(:disabled)": {
                    background:
                      "linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)",
                    transition:
                      "transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out",
                    "&:hover": {
                      background:
                        "linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)",
                      opacity: 0.8,
                      transform: "scale(1.05)",
                      boxShadow: "0 4px 20px rgba(255, 255, 255, 0.5)",
                    },
                  },
                }}
                onClick={() => handleSubmit()}
              >
                XÁC NHẬN
              </Button>
            </Box>
          )}
          {userLoading && levelLoading && <CircularProgress />}
        </Box>
      </Container>
    </Box>
  );
}

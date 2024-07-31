import {
  Box,
  Button,
  Modal,
  TextField,
  Typography,
  Link,
  InputLabel,
  Checkbox,
  FormControlLabel,
} from "@mui/material";
import { useState } from "react";
import LoadingButton from "@mui/lab/LoadingButton";
import DoneIcon from "@mui/icons-material/Check";
import Cookies from "js-cookie";
import { useNavigate } from "react-router-dom";
import { AppService } from "../services/api";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: {
    xs: "95%",
    sm: 400,
    md: 500,
    lg: 500,
    xl: 600,
  },
  bgcolor: "background.paper",
  outline: "none",
  borderRadius: 2,
  boxShadow: "0 0.2rem 1.2rem #ffffff",
  p: 4,
};

export default function GuessLoginButton() {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const [loading, setLoading] = useState(false);
  const [keyValue, setKeyValue] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [agreeTerms, setAgreeTerms] = useState(false);

  const handleOpen = () => setOpen(true);
  const handleClose = () => {
    setOpen(false);
    setErrorMessage("");
  };

  const handleSubmit = async () => {
    if (!agreeTerms) {
      setErrorMessage("Bạn phải đồng ý với điều khoản sử dụng.");
      return;
    }
    setLoading(true);
    setErrorMessage("");
    try {
      const response = await AppService.healCheck(keyValue.trim());
      if (response.status === 200) {
        Cookies.set("token", keyValue.trim());
        handleClose();
        navigate("/level");
      }
    } catch (error) {
      setErrorMessage("Không hợp lệ");
      setKeyValue("");
      console.log(error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Button
        variant="outlined"
        sx={{
          color: "white",
          textTransform: "none",
          fontSize: "1.2rem",
          width: "100%",
          maxWidth: "40rem",
          background: "linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)",
          transition: "transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out",
          "&:hover": {
            background: "linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)",
            opacity: 0.9,
            transform: "scale(1.05)",
            boxShadow: "0 0.2rem 1.2rem #ffffff",
            border: "0px",
          },
          "&:disabled": {
            background: "#e0e0e0",
            color: "#c5c5c5",
          },
        }}
        size="large"
        onClick={handleOpen}
      >
        Tiếp tục với tư cách khách
      </Button>

      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <InputLabel htmlFor="outlined-basic">
            <Typography id="modal-modal-title" variant="h6">
              Nhập Gemini API Key để tiếp tục
            </Typography>
          </InputLabel>
          <Box
            sx={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
              marginBottom: 1,
              marginTop: 1,
              gap: 1,
            }}
          >
            <TextField
              id="standard-basic"
              variant="standard"
              placeholder="AIza . . ."
              sx={{
                flexGrow: 1,
                height: "2.5rem",
                "& .MuiInputBase-root": {
                  height: "100%",
                },
              }}
              onChange={(e) => setKeyValue(e.target.value)}
              value={keyValue}
              error={!!errorMessage}
              helperText={errorMessage}
            />
            <Button
              component={Link}
              variant="outlined"
              color="secondary"
              href="https://aistudio.google.com/app/apikey"
              target="_blank"
              sx={{
                height: "2.5rem",
              }}
            >
              Lấy Key
            </Button>
          </Box>
          <FormControlLabel
            control={
              <Checkbox
                checked={agreeTerms}
                onChange={(e) => setAgreeTerms(e.target.checked)}
              />
            }
            label={
              <>
                Tôi đồng ý với{" "}
                <Link
                  href="https://ai.google.dev/gemini-api/terms"
                  target="_blank"
                  sx={{ fontWeight: "bold" }}
                >
                  Điều khoản sử dụng
                </Link>
                .
              </>
            }
            sx={{ mb: 1 }}
          />

          <Box
            sx={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            <LoadingButton
              loading={loading}
              loadingPosition="end"
              endIcon={<DoneIcon />}
              variant="contained"
              onClick={handleSubmit}
              disabled={!keyValue.trim() || !agreeTerms}
              sx={{
                textTransform: "none",
                color: "white",
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
                    boxShadow: "0 0.2rem 1.2rem rgba(255, 255, 255, 0.2)",
                  },
                },
              }}
            >
              Xác nhận
            </LoadingButton>
          </Box>
        </Box>
      </Modal>
    </>
  );
}

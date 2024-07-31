import PropTypes from "prop-types";
import { useState } from "react";
import {
  Button,
  Tooltip,
  Dialog,
  DialogContent,
  DialogActions,
  Box,
} from "@mui/material";
import { useGoogleLogin } from "@react-oauth/google";
import Cookies from "js-cookie";
import { useNavigate } from "react-router-dom";
import { AppService } from "../services/api";
import AlertCustom from "./Alert";

export default function GoogleLoginButton({ setLoading }) {
  const navigate = useNavigate();
  const [openDialog, setOpenDialog] = useState(false);

  const handleOpenDialog = () => {
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
  };

  const login = useGoogleLogin({
    onSuccess: async (tokenResponse) => {
      try {
        setLoading(true);
        const response = await AppService.healCheck(tokenResponse.access_token);

        console.log({ TokenResponse: tokenResponse });
        console.log({ HealthcheckResult: response });

        if (response.status === 200) {
          const remainingMilliseconds = tokenResponse.expires_in * 1000;
          const expiryDate = new Date(
            new Date().getTime() + remainingMilliseconds
          );
          Cookies.set("token", tokenResponse.access_token, {
            expires: expiryDate,
          });
          navigate("/level");
        } else {
          AlertCustom({
            type: "error",
            title: "Tài khoản Google của bạn hiện chưa được hỗ trợ.",
          });
        }
      } catch (error) {
        AlertCustom({
          type: "error",
          title: "Tài khoản Google của bạn hiện chưa được hỗ trợ.",
        });
      } finally {
        setLoading(false);
      }
    },
    scopes:
      "https://www.googleapis.com/auth/cloud-platform https://www.googleapis.com/auth/generative-language.retriever",
  });

  const handleLogin = () => {
    handleCloseDialog();
    login();
  };

  return (
    <>
      <Tooltip title="Chỉ dành cho sinh viên UIT" placement="top">
        <Button
          disabled
          variant="contained"
          onClick={handleOpenDialog}
          sx={{
            color: "white",
            textTransform: "none",
            fontSize: "1.2rem",
            width: "100%",
            maxWidth: "40rem",
            background: "linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)",
            transition:
              "transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out",
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
        >
          Đăng nhập bằng Google
        </Button>
      </Tooltip>
      <Dialog
        open={openDialog}
        onClose={handleCloseDialog}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <DialogContent
          sx={{
            paddingBottom: "0.5rem",
          }}
        >
          <a>Tính năng này hiện chỉ dùng cho Gmail của sinh viên UIT.</a>
        </DialogContent>
        <DialogActions>
          <Box
            display="flex"
            justifyContent="center"
            width="100%"
            size="large"
            sx={{
              margin: "0 1rem 0.5rem",
            }}
          >
            <Button onClick={handleCloseDialog}>Hủy</Button>
            <Button onClick={handleLogin} variant="contained" autoFocus>
              Tiếp tục
            </Button>
          </Box>
        </DialogActions>
      </Dialog>
    </>
  );
}

GoogleLoginButton.propTypes = {
  setLoading: PropTypes.func.isRequired,
};

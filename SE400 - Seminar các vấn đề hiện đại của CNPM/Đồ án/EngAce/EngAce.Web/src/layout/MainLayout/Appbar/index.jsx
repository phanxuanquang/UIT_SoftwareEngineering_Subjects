import PropTypes from "prop-types";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import navConfig from "../NavConfig";
import { NavLink, useLocation, useNavigate } from "react-router-dom";
import AppBarItem from "./AppbarItem";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import {
  Avatar,
  Divider,
  ListItemIcon,
  Menu,
  MenuItem,
  Tooltip,
} from "@mui/material";
import { useState } from "react";
import { Logout, Settings } from "@mui/icons-material";
import Cookies from "js-cookie";
import { googleLogout } from "@react-oauth/google";
import { chatbotActions } from "../../../redux/reducer/ChatbotReducer";
import { useDispatch } from "react-redux";

const ResponsiveAppBar = ({ onOpenSidebar }) => {
  const { pathname } = useLocation();
  const dispatch = useDispatch();
  const name = localStorage.getItem("name");
  const level = localStorage.getItem("level");
  const picture = localStorage.getItem("picture");
  const navigate = useNavigate();

  const surveyFormUrl = "https://docs.google.com/forms/d/e/1FAIpQLSeKxVDOywFoWoSfDiiSU4QAZGNYn_RJP3_j0TK9ByA1nWV9GA/viewform";
  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };
  const handleLogout = () => {
    setAnchorEl(null);
    Cookies.remove("token");
    localStorage.removeItem("name");
    localStorage.removeItem("level");
    localStorage.removeItem("picture");
    googleLogout();
    dispatch(chatbotActions.resetChat());
    navigate("/auth");
    window.open(surveyFormUrl, "_blank");
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleSetting = () => {
    setAnchorEl(null);
    navigate("/level");
  };

  const match = (path) => {
    const pathFirstPart = path.split("/")[1];
    const pathnameFirstPart = pathname.split("/")[1];
    return pathFirstPart === pathnameFirstPart;
  };

  return (
    <AppBar position="fixed">
      <Container maxWidth="false">
        <Toolbar disableGutters>
          <Typography
            variant="h6"
            noWrap
            component={NavLink}
            to={"/"}
            sx={{
              mr: 2,
              display: { xs: "none", md: "flex" },
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".2rem",
              color: "inherit",
              textDecoration: "none",
              textTransform: "uppercase",
              userSelect: "none",
              flexGrow: 1,
            }}
          >
            ENGACE
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={onOpenSidebar}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
          </Box>
          <Typography
            variant="h6"
            noWrap
            component={NavLink}
            to={"/"}
            sx={{
              mr: 2,
              display: { xs: "flex", md: "none" },
              flexGrow: 1,
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".2rem",
              color: "inherit",
              textDecoration: "none",
              textTransform: "uppercase",
              userSelect: "none",
            }}
          >
            ENGACE
          </Typography>
          <Box sx={{ flexGrow: 0, display: { xs: "none", md: "flex" } }}>
            {navConfig.map((item) => (
              <AppBarItem
                key={item.title}
                item={item}
                active={match(item.path)}
              />
            ))}
          </Box>
          <Box
            sx={{
              flexGrow: 0,
              marginLeft: 0,
              display: { xs: "none", md: "flex" },
            }}
          >
            <Tooltip title="Thiết lập">
              <IconButton
                onClick={handleClick}
                size="large"
                sx={{ ml: 2 }}
                aria-controls={open ? "account-menu" : undefined}
                aria-haspopup="true"
                aria-expanded={open ? "true" : undefined}
              >
                {picture ? (
                  <Avatar alt="Avatar" src={picture}></Avatar>
                ) : (
                  <AccountCircleIcon
                    fontSize="large"
                    sx={{ color: "primary.black" }}
                  />
                )}
              </IconButton>
            </Tooltip>
            <Menu
              anchorEl={anchorEl}
              id="account-menu"
              open={open}
              onClose={handleClose}
              onClick={handleClose}
              PaperProps={{
                elevation: 0,
                sx: {
                  overflow: "visible",
                  filter: "drop-shadow(0px 2px 8px rgba(0,0,0,0.32))",
                  mt: 1.5,
                  "& .MuiAvatar-root": {
                    width: 32,
                    height: 32,
                    ml: -0.5,
                    mr: 1,
                  },
                  "&::before": {
                    content: '""',
                    display: "block",
                    position: "absolute",
                    top: 0,
                    right: 25,
                    width: 10,
                    height: 10,
                    bgcolor: "background.paper",
                    transform: "translateY(-50%) rotate(45deg)",
                    zIndex: 0,
                  },
                },
              }}
              transformOrigin={{ horizontal: "right", vertical: "top" }}
              anchorOrigin={{ horizontal: "right", vertical: "bottom" }}
            >
              <Box
                sx={{
                  width: "100%",
                  display: "flex",
                  flexDirection: "column",
                  padding: "6px 16px 6px 16px",
                  gap: 1,
                }}
              >
                <Typography>{name}</Typography>
                <Typography>Trình độ: {level}</Typography>
              </Box>
              <Divider />
              <MenuItem onClick={handleSetting}>
                <ListItemIcon>
                  <Settings fontSize="small" />
                </ListItemIcon>
                Cài đặt
              </MenuItem>
              <MenuItem
                onClick={handleLogout}
                sx={{
                  color: "error.main",
                  fontWeight: "bold",
                }}
              >
                <ListItemIcon>
                  <Logout
                    fontSize="small"
                    sx={{
                      color: "error.main",
                    }}
                  />
                </ListItemIcon>
                Đăng xuất
              </MenuItem>
            </Menu>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};

ResponsiveAppBar.propTypes = {
  onOpenSidebar: PropTypes.func.isRequired,
};

export default ResponsiveAppBar;

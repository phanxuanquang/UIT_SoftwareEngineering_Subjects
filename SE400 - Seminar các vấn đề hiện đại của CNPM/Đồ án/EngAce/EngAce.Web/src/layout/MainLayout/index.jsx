import { useState } from "react";
import { Outlet } from "react-router-dom";
// material
import { styled } from "@mui/material/styles";
import ResponsiveAppBar from "./Appbar";
import Sidebar from "./Drawer";
import { Container, CssBaseline } from "@mui/material";

const APP_BAR_MOBILE = 56;
const APP_BAR_DESKTOP = 68;

const RootStyle = styled("div")({
  display: "flex",
  height: "100%",
  overflow: "hidden",
});

const MainStyle = styled("div")(({ theme }) => ({
  flexGrow: 1,
  overflow: "auto",
  height: "100%",
  paddingTop: APP_BAR_MOBILE + 8,
  paddingLeft: theme.spacing(0),
  paddingRight: theme.spacing(0),
  [theme.breakpoints.up("sm")]: {
    paddingTop: APP_BAR_DESKTOP + 8,
    paddingLeft: theme.spacing(2),
    paddingRight: theme.spacing(2),
  },
}));

export default function DashboardLayout() {
  const [open, setOpen] = useState(false);

  return (
    <RootStyle>
      <ResponsiveAppBar onOpenSidebar={() => setOpen(true)} />
      <Sidebar isOpenSidebar={open} onCloseSidebar={() => setOpen(false)} />
      <MainStyle>
        <CssBaseline />
        <Container fixed sx={{ height: "100%", bgcolor: "#fcfcfc" }}>
          <Outlet />
        </Container>
      </MainStyle>
    </RootStyle>
  );
}

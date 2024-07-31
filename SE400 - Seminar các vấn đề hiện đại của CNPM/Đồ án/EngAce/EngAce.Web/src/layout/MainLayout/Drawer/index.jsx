import PropTypes from "prop-types";
import { Drawer } from "@mui/material";

import NavSection from "./NavSection";
import navConfig from "../NavConfig";
import { useLocation } from "react-router-dom";
import { useEffect } from "react";
import useResponsive from "../../../hooks/useResponsive";

const DRAWER_WIDTH = 280;

export default function Sidebar({ isOpenSidebar, onCloseSidebar }) {
  const { pathname } = useLocation();

  const isTablet = useResponsive("up", "md");

  useEffect(() => {
    if (isOpenSidebar) {
      onCloseSidebar();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pathname]);

  return (
    !isTablet && (
      <Drawer
        open={isOpenSidebar}
        onClose={onCloseSidebar}
        PaperProps={{
          sx: { width: DRAWER_WIDTH },
        }}
      >
        <NavSection navConfig={navConfig} padding={1} />
      </Drawer>
    )
  );
}

Sidebar.propTypes = {
  isOpenSidebar: PropTypes.bool.isRequired,
  onCloseSidebar: PropTypes.func.isRequired,
};

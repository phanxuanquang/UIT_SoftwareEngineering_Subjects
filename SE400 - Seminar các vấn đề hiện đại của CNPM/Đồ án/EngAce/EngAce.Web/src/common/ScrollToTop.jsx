import { Fab } from "@mui/material";
import KeyboardArrowUpRoundedIcon from "@mui/icons-material/KeyboardArrowUpRounded";
import { useEffect, useState } from "react";

const ScrollToTop = () => {
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    const toggleVisibility = () => {
      if (window.scrollY > 300) {
        setVisible(true);
      } else {
        setVisible(false);
      }
    };

    window.addEventListener("scroll", toggleVisibility);

    return () => {
      window.removeEventListener("scroll", toggleVisibility);
    };
  }, []);

  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };

  return (
    <div
      style={{
        position: "fixed",
        bottom: "2rem",
        right: "2rem",
        opacity: visible ? 1 : 0,
        visibility: visible ? "visible" : "hidden",
        transition:
          "opacity 195ms cubic-bezier(0.4, 0, 0.2, 1) 0ms, visibility 195ms",
      }}
      aria-label="Scroll to top"
    >
      <Fab
        size="small"
        color="default"
        aria-label="Scroll back to top"
        onClick={scrollToTop}
      >
        <KeyboardArrowUpRoundedIcon />
      </Fab>
    </div>
  );
};

export default ScrollToTop;

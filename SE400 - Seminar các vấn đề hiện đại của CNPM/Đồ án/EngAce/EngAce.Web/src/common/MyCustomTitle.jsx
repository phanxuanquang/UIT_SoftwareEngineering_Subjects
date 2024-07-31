import { Typography, styled } from "@mui/material";
import PropTypes from "prop-types";

const CustomTitle = styled(Typography)({
  background: "-webkit-linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)",
  WebkitBackgroundClip: "text",
  WebkitTextFillColor: "transparent",
});

MyCustomTitle.propTypes = {
  children: PropTypes.node.isRequired,
};

export default function MyCustomTitle({ children, ...rest }) {
  return (
    <CustomTitle variant="h1" fontWeight="bold" {...rest}>
      {children}
    </CustomTitle>
  );
}

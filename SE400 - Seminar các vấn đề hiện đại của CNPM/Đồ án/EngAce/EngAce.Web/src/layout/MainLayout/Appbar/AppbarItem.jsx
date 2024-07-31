import PropTypes from "prop-types";
import { NavLink as RouterLink } from "react-router-dom";
import { alpha, useTheme } from "@mui/material/styles";
import Button from "@mui/material/Button";

const AppBarItem = ({ item, active }) => {
  const theme = useTheme();

  const { title, path } = item;

  const activeStyle = {
    color: "primary.black",
    bgcolor: alpha(
      theme.palette.primary.dark,
      theme.palette.action.selectedOpacity
    ),
  };

  return (
    <Button
      component={RouterLink}
      to={path}
      sx={{
        my: 2,
        px: 2,
        color: "white",
        display: "block",
        textTransform: "none",
        ...(active && activeStyle),
      }}
    >
      {title}
    </Button>
  );
};

AppBarItem.propTypes = {
  item: PropTypes.shape({
    title: PropTypes.string.isRequired,
    path: PropTypes.string.isRequired,
  }).isRequired,
  active: PropTypes.bool,
};

export default AppBarItem;

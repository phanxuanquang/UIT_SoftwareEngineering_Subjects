import { Paper, Typography, styled } from "@mui/material";
import PropTypes from "prop-types";
import { useNavigate } from "react-router-dom";

const Item = styled(Paper)(({ theme }) => ({
  padding: theme.spacing(1),
  textAlign: "center",
  color: theme.palette.text.secondary,
  aspectRatio: "1/1",
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
  justifyContent: "center", 
  margin: 3,
}));

const IconWrapper = styled("div")({
  alignSelf: "center", 
  marginBottom: "0.5rem", 
});

MyBentoItem.propTypes = {
  Icon: PropTypes.elementType.isRequired,
  title: PropTypes.string.isRequired,
  route: PropTypes.string.isRequired,
  backgroundColor: PropTypes.string.isRequired,
};

export default function MyBentoItem({ title, route, backgroundColor, Icon }) {
  const navigate = useNavigate();
  const handleNavigation = () => {
    navigate(route);
  };
  return (
    <Item
    sx={{
      bgcolor: { backgroundColor },
      color: "white",
      cursor: "pointer",
      overflow: 'hidden',
      border: "0px",
      transition: "transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out, background-color 0.3s ease-in-out", /* Chỉnh background-color */
      "&:hover": {
        transform: "scale(1.05)",
        boxShadow: "0 0.2rem 1.2rem rgba(255, 255, 255, 0.5)",
        backgroundColor: "rgba(0, 0, 0, 0.5)"
      },
    }}
      onClick={() => handleNavigation()}
      elevation={8}
    >
      <IconWrapper>
        <Icon sx={{ width: "55%", height: "auto", marginBotton: "0px" }} />
      </IconWrapper>
      <Typography variant="h5" sx={{ alignSelf: "center" }}>
        {title}
      </Typography>
    </Item>
  );
}

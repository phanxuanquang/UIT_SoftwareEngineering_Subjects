import Card from "@mui/material/Card";
import CardHeader from "@mui/material/CardHeader";
import CardMedia from "@mui/material/CardMedia";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";

export default function IntroduceCard() {
  return (
    <Card sx={{ maxWidth: 345 }}>
      <CardHeader title="Shrimp and Chorizo Paella" />

      <CardContent>
        <Typography variant="body2" color="text.secondary">
          This impressive paella is a perfect party dish and a fun meal to cook
          together with your guests. Add 1 cup of frozen peas along with the
          mussels, if you like.
        </Typography>
      </CardContent>
      <CardMedia
        component="img"
        height="auto"
        image="https://lh6.googleusercontent.com/kLbn37tH9cNrNUAcYV4Fl5G2lx5Q1aV1vNtfyuiqihD86NcMDqDhgDZir4jy_ZHwB222c6mkbvYQT-ZokMn2hvxnER1wLDCtiXK-UK3LY12rpVlNkRI1nXlkxSs4sBWb4g=w2040"
        alt="Paella dish"
      />
    </Card>
  );
}

function pxToRem(value) {
  return `${value / 16}rem`;
}

function responsiveFontSizes({ xs, sm, md, lg, xl }) {
  return {
    "@media (min-width:375px)": { 
      fontSize: pxToRem(xs),
    },
    "@media (min-width:600px)": { 
      fontSize: pxToRem(sm),
    },
    "@media (min-width:900px)": { 
      fontSize: pxToRem(md),
    },
    "@media (min-width:1200px)": { 
      fontSize: pxToRem(lg),
    },
    "@media (min-width:2560px)": { 
      fontSize: pxToRem(xl),
    },
  };
}

const FONT_PRIMARY = "'Open Sans', sans-serif";

const typography = {
  fontFamily: FONT_PRIMARY,
  fontWeightRegular: 400,
  fontWeightMedium: 500,
  fontWeightBold: 700,
  h1: {
    fontWeight: 700,
    lineHeight: 1.25,
    fontSize: pxToRem(28),
    ...responsiveFontSizes({ xs: 32, sm: 36, md: 48, lg: 56, xl: 64 }),
  },
  h2: {
    fontWeight: 700,
    lineHeight: 1.3,
    fontSize: pxToRem(24),
    ...responsiveFontSizes({ xs: 28, sm: 32, md: 40, lg: 48, xl: 56 }),
  },
  h3: {
    fontWeight: 700,
    lineHeight: 1.35,
    fontSize: pxToRem(20),
    ...responsiveFontSizes({ xs: 24, sm: 28, md: 32, lg: 40, xl: 48 }),
  },
  h4: {
    fontWeight: 700,
    lineHeight: 1.4,
    fontSize: pxToRem(18),
    ...responsiveFontSizes({ xs: 20, sm: 24, md: 28, lg: 32, xl: 40 }),
  },
  h5: {
    fontWeight: 700,
    lineHeight: 1.5,
    fontSize: pxToRem(16),
    ...responsiveFontSizes({ xs: 18, sm: 20, md: 24, lg: 28, xl: 32 }),
  },
  h6: {
    fontWeight: 700,
    lineHeight: 1.6,
    fontSize: pxToRem(14),
    ...responsiveFontSizes({ xs: 16, sm: 18, md: 20, lg: 20, xl: 28 }),
  },
  h7: {
    fontWeight: 700,
    lineHeight: 1.6,
    fontSize: pxToRem(13),
    ...responsiveFontSizes({ xs: 16, sm: 18, md: 20, lg: 20, xl: 28 }),
  },
  subtitle1: {
    fontWeight: 500,
    lineHeight: 1.75,
    fontSize: pxToRem(16),
  },
  subtitle2: {
    fontWeight: 500,
    lineHeight: 1.75,
    fontSize: pxToRem(14),
  },
  body1: {
    lineHeight: 1.75,
    fontSize: pxToRem(14),
    ...responsiveFontSizes({ xs: 16, sm: 18, md: 20, lg: 16, xl: 28 }),
  },
  body2: {
    lineHeight: 1.75,
    fontSize: pxToRem(14),
  },
  body3: {
    lineHeight: 1.6,
    fontSize: pxToRem(13.5),
    ...responsiveFontSizes({ xs: 16, sm: 18, md: 20, lg: 16, xl: 28 }),
  },
  caption: {
    lineHeight: 1.5,
    fontSize: pxToRem(12),
  },
  overline: {
    fontWeight: 700,
    lineHeight: 1.5,
    fontSize: pxToRem(12),
    letterSpacing: 1.1,
    textTransform: "uppercase",
  },
  button: {
    fontWeight: 700,
    lineHeight: 1.75,
    fontSize: pxToRem(14),
    textTransform: "capitalize",
  },
};

export default typography;

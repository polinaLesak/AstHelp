import * as React from "react";
import LocalPhoneOutlinedIcon from "@mui/icons-material/LocalPhoneOutlined";
import EmailOutlinedIcon from "@mui/icons-material/EmailOutlined";
import { AppBar, Stack, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";

export default function Footer() {
  return (
    <AppBar
      sx={{
        top: "auto",
        bottom: 0,
        height: "7%",
      }}
    >
      <Grid
        container
        direction="row"
        spacing={12}
        sx={{
          justifyContent: "center",
          alignItems: "center",
          height: "100%",
        }}
      >
        <Grid>
          <Stack direction="row" gap={2}>
            <LocalPhoneOutlinedIcon />
            <Typography variant="body1">+375 11 1234567</Typography>
          </Stack>
        </Grid>
        <Grid>
          <Stack direction="row" gap={2}>
            <EmailOutlinedIcon />
            <Typography variant="body1">astHelp@aston.ru</Typography>
          </Stack>
        </Grid>
      </Grid>
    </AppBar>
  );
}

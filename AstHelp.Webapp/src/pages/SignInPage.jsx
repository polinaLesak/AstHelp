import React from "react";
import LoginForm from "../features/auth/ui/LoginForm";
import Grid from "@mui/material/Grid2";

const SignInPage = () => {
  return (
    <Grid
      container
      justifyContent="center"
      alignItems="center"
      style={{ minHeight: '80vh' }}
    >
      <Grid size={4} sm={4}>
        <LoginForm />
      </Grid>
    </Grid>
  );
};

export default SignInPage;

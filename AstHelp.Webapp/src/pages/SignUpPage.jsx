import React from "react";
import Grid from "@mui/material/Grid2";
import RegForm from "../features/auth/ui/RegForm";

const SignUpPage = () => {
  return (
    <Grid
      container
      justifyContent="center"
      alignItems="center"
      style={{ minHeight: '80vh' }}
    >
      <Grid size={6}>
        <RegForm />
      </Grid>
    </Grid>
  );
};

export default SignUpPage;
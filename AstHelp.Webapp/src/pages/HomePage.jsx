import React from "react";
// import React, { useEffect } from "react";
// import { useDispatch, useSelector } from "react-redux";
// import { fetchUser } from "../entities/user/api/userApi";
import { Box, Button, Container, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import mainImage from "../assets/main.png";

const HomePage = () => {
  // const dispatch = useDispatch();
  // const user = useSelector((state) => state.user.data);

  // useEffect(() => {
  //   dispatch(fetchUser(1)); // Fetch user with ID 1
  // }, [dispatch]);

  // if (!user) {
  //   return <div>Loading...</div>;
  // }

  return (
    <Container>
      <Grid
        container
        sx={{
          mt: 4,
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Grid size={12} display="flex" justifyContent="center" alignItems="center">
          <Box component="img" src={mainImage} />
        </Grid>
        <Grid size={12} display="flex" justifyContent="center" alignItems="center">
          <Typography variant="body1">
            AstHelp - закажи себе оборудование для работы
          </Typography>
        </Grid>
        <Grid size={12} display="flex" justifyContent="center" alignItems="center">
          <Typography variant="body1">Ваше удобство - наш приоритет</Typography>
        </Grid>
        <Grid size={12} display="flex" justifyContent="center" alignItems="center" sx={{mt:4}}>
          <Button href="/sign_in" variant="contained">
            Войти
          </Button>
        </Grid>
      </Grid>
    </Container>
  );
};

export default HomePage;

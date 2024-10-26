import React from "react";
import { Box, Button, Container, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import mainImage from "../assets/main.png";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "../features/auth/model/loginSlice";
import { Link as RouterLink } from "react-router-dom";

const HomePage = () => {
  const dispatch = useDispatch();
  const { user, isAuthenticated } = useSelector((state) => state.login);

  const handleLogout = () => {
    dispatch(logout());
  };

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
        <Grid
          size={12}
          display="flex"
          justifyContent="center"
          alignItems="center"
        >
          <Box component="img" src={mainImage} />
        </Grid>
        <Grid
          size={12}
          display="flex"
          justifyContent="center"
          alignItems="center"
        >
          <Typography variant="body1">
            AstHelp - закажи себе оборудование для работы
          </Typography>
        </Grid>
        <Grid
          size={12}
          display="flex"
          justifyContent="center"
          alignItems="center"
        >
          <Typography variant="body1">Ваше удобство - наш приоритет</Typography>
        </Grid>
        {isAuthenticated ? (
          <Grid
            size={12}
            display="flex"
            justifyContent="center"
            alignItems="center"
            sx={{ mt: 2 }}
          >
            <Typography variant="body1">
              Добро пожаловать, {user.fullname}
            </Typography>
          </Grid>
        ) : (
          <></>
        )}
        <Grid
          size={12}
          display="flex"
          justifyContent="center"
          alignItems="center"
          sx={{ mt: 4 }}
        >
          {isAuthenticated ? (
            <Button onClick={handleLogout} variant="contained">
              Выйти
            </Button>
          ) : (
            <RouterLink to="/sign_in">
              <Button variant="contained">Войти</Button>
            </RouterLink>
          )}
        </Grid>
      </Grid>
    </Container>
  );
};

export default HomePage;

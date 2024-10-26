import React from 'react';
import { Box, Button, Container, Typography } from '@mui/material';
import Grid from "@mui/material/Grid2";
import notFoundImage from "../assets/notFound.png";
import { useNavigate } from 'react-router-dom';

const NotFoundPage = () => {
  const navigate = useNavigate();

  const handleBack = () => {
    navigate(-1);
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
        <Grid size={12} display="flex" justifyContent="center" alignItems="center">
          <Box component="img" src={notFoundImage} />
        </Grid>
        <Grid size={12} display="flex" justifyContent="center" alignItems="center">
          <Typography variant="caption">Не удалось найти запрашиваемый ресурс</Typography>
        </Grid>
        <Grid size={12} display="flex" justifyContent="center" alignItems="center" sx={{mt:4}}>
          <Button href="/sign_in" variant="contained" onClick={handleBack}>
            Вернуться на шаг назад
          </Button>
        </Grid>
      </Grid>
    </Container>
  );
};

export default NotFoundPage;

import React from "react";
import { Link } from "react-router-dom";
import {
  Button,
  Card,
  CardContent,
  TextField,
  Typography,
} from "@mui/material";
import Grid from "@mui/material/Grid2";
import { useDispatch } from 'react-redux';
import { login } from '../model/loginSlice';
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { loginValidationSchema } from "../../../validation/userValidation";
import { useNavigate } from 'react-router-dom';

const LoginForm = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(loginValidationSchema),
  });

  const onSubmit = (data) => {
    dispatch(login(data)).unwrap()
    .then(() => {
      navigate('/about');
    })
    .catch((err) => {
      console.error('Login failed:', err);
    });
    console.log(data);
  };

  return (
    <Card>
      <CardContent>
        <Grid container>
          <form onSubmit={handleSubmit(onSubmit)}>
            <Grid>
              <Typography align="center">
                Чтобы авторизоваться в системе, необходимо ввести личные данные
              </Typography>
              <TextField
                label="Введите логин"
                variant="standard"
                fullWidth
                sx={{ mb: 3 }}
                {...register("username")}
                error={!!errors.username}
                helperText={errors.username ? errors.username.message : ""}
              />
              <TextField
                label="Введите пароль"
                variant="standard"
                type="password"
                fullWidth
                sx={{ mb: 3 }}
                {...register("password")}
                error={!!errors.password}
                helperText={errors.password ? errors.password.message : ""}
              />
            </Grid>
            <Grid
              size={12}
              display="flex"
              justifyContent="center"
              alignItems="center"
            >
              <Button variant="contained" type="submit">
                Войти в аккаунт
              </Button>
            </Grid>
          </form>
          <Grid
            size={12}
            display="flex"
            justifyContent="center"
            alignItems="center"
          >
            <Typography variant="caption">
              Нет аккаунта?{" "}
              <Link component="button" to="/sign_up">
                Зарегистрироваться
              </Link>
            </Typography>
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
};

export default LoginForm;

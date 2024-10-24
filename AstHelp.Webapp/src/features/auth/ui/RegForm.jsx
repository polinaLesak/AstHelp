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
import { useDispatch } from "react-redux";
import { registration } from "../model/registrationSlice";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { registerValidationSchema } from "../../../validation/userValidation";
import { useNavigate } from "react-router-dom";

const RegForm = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(registerValidationSchema),
  });

  const onSubmit = (data) => {
    dispatch(registration({
      username: data.username,
      email: data.email,
      password: data.password,
      fullname: data.fullname,
      position: data.position
    }))
      .unwrap()
      .then(() => {
        navigate("/sign_in");
      })
      .catch((err) => {
        console.error("Registration failed:", err);
      });
  };

  return (
    <Card>
      <CardContent>
        <Grid container>
          <form onSubmit={handleSubmit(onSubmit)}>
            <Grid size={12}>
              <Typography align="center">
                Чтобы пользоваться системой, необходимо зарегистрироваться
              </Typography>
            </Grid>
            <Grid container spacing={4}>
              <Grid size={6}>
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
                  label="Введите Email"
                  variant="standard"
                  type="email"
                  fullWidth
                  sx={{ mb: 3 }}
                  {...register("email")}
                  error={!!errors.email}
                  helperText={errors.email ? errors.email.message : ""}
                />
                <TextField
                  label="Введите ФИО"
                  variant="standard"
                  fullWidth
                  sx={{ mb: 3 }}
                  {...register("fullname")}
                  error={!!errors.fullname}
                  helperText={errors.fullname ? errors.fullname.message : ""}
                />
              </Grid>
              <Grid size={6}>
                <TextField
                  label="Введите должность"
                  variant="standard"
                  fullWidth
                  sx={{ mb: 3 }}
                  {...register("position")}
                  error={!!errors.position}
                  helperText={errors.position ? errors.position.message : ""}
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
                <TextField
                  label="Подтвердите пароль"
                  variant="standard"
                  type="password"
                  fullWidth
                  sx={{ mb: 3 }}
                  {...register("confirmPassword")}
                  error={!!errors.confirmPassword}
                  helperText={
                    errors.confirmPassword ? errors.confirmPassword.message : ""
                  }
                />
              </Grid>
            </Grid>
            <Grid
              size={12}
              display="flex"
              justifyContent="center"
              alignItems="center"
            >
              <Button variant="contained" type="submit">
                Зарегистрироваться
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
              Уже есть аккаунта?{" "}
              <Link component="button" to="/sign_in">
                Авторизация
              </Link>
            </Typography>
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
};

export default RegForm;

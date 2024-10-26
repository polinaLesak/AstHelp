import { Box, Button, Modal, TextField, Typography } from "@mui/material";
import { useDispatch } from "react-redux";
import { yupResolver } from "@hookform/resolvers/yup";
import { editUserSchema } from "../../../validation/userValidation";
import { useForm } from "react-hook-form";
import { fetchAllUsers, updateUserProfile } from "../../../entities/user/api/userApi";
import { useEffect } from "react";

const UsersEditModal = ({ open, onClose, user }) => {
  const dispatch = useDispatch();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(editUserSchema),
  });
  
  useEffect(() => {
    if (open && user) {
      reset({
        fullname: user.profile.fullname,
        position: user.profile.position,
      });
    }
  }, [open, user, reset]);

  const onSubmit = async (data) => {
    try {
      await dispatch(updateUserProfile({ 
        userId: user.id, 
        fullname: data.fullname, 
        position: data.position
      }))
      dispatch(fetchAllUsers());
      onClose();
    } catch (error) {
      console.error("Ошибка при редактировании пользователя:", error);
    }
  };

  return (
    <Modal open={open} onClose={onClose}>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Box
          sx={{
            position: 'absolute',
            top: '50%',
            left: '50%',
            transform: 'translate(-50%, -50%)',
            width: 400,
            bgcolor: 'background.paper',
            boxShadow: 24,
            p: 4,
            zIndex: 100
          }}
        >
          <Typography variant="h6">Редактировать пользователя</Typography>
          <TextField
            label="ФИО"
            fullWidth
            margin="normal"
            {...register("fullname")}
            error={!!errors.fullname}
            helperText={errors.fullname ? errors.fullname.message : ""}
          />
          <TextField
            label="Должность"
            fullWidth
            margin="normal"
            {...register("position")}
            error={!!errors.position}
            helperText={errors.position ? errors.position.message : ""}
          />
          <Button
            // onClick={handleEdit}
            type="submit"
            color="primary"
            variant="contained"
            fullWidth
          >
            Сохранить
          </Button>
        </Box>
      </form>
    </Modal>
  );
};

export default UsersEditModal;

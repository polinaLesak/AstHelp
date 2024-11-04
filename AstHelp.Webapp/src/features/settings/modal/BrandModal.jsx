import React, { useEffect } from "react";
import { yupResolver } from "@hookform/resolvers/yup";
import { Dialog, DialogActions, DialogContent, DialogTitle, TextField, Button } from "@mui/material";
import { useDispatch } from "react-redux";
import { useForm } from "react-hook-form";
import { brandValidationSchema } from "../../../validation/settingsValidation";
import { createBrand, fetchAllBrands, updateBrand } from "../../../entities/brand/api/brandApi";

const BrandModal = ({ open, onClose, brand }) => {
  const dispatch = useDispatch();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(brandValidationSchema),
  });
  
  useEffect(() => {
    reset({ name: "" })
    if (open && brand) {
      reset({
        name: brand.name
      });
    }
  }, [open, brand, reset]);

  const onSubmit = async (data) => {
    try {
      if(brand) {
        await dispatch(updateBrand({ 
          brandId: brand.id, 
          name: data.name
        }))
      } else {
        await dispatch(createBrand({
          name: data.name
        }))
      }
      await dispatch(fetchAllBrands());
      onClose();
    } catch (error) {
      console.error("Ошибка при операции с брендом:", error);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{brand ? "Редактировать Бренд" : "Добавить Бренд"}</DialogTitle>
      <DialogContent>
        <TextField
          autoFocus
          margin="dense"
          label="Наименование"
          type="text"
          fullWidth
          {...register("name")}
          error={!!errors.name}
          helperText={errors.name ? errors.name.message : ""}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="primary">
          Отмена
        </Button>
        <Button onClick={handleSubmit(onSubmit)} color="primary">
          {brand ? "Сохранить" : "Добавить"}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default BrandModal;

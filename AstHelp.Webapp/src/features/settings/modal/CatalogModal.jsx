import React, { useEffect, useState } from "react";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
  Button,
  MenuItem,
  Select,
  InputLabel,
  FormControl,
  FormHelperText,
  OutlinedInput,
  Box,
  Chip,
} from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { Controller, useForm } from "react-hook-form";
import { catalogValidationSchema } from "../../../validation/settingsValidation";
import {
  createCatalog,
  fetchAllCatalogs,
  updateCatalog,
} from "../../../entities/catalog/api/catalogApi";
import { fetchAllAttributes } from "../../../entities/attribute/api/attributeApi";
import { createSelector } from "reselect";

const selectAttributeState = (state) => state.attribute;

const selectAttribute = createSelector(
  [selectAttributeState],
  (attributeState) => attributeState.attributes || []
);

const CatalogModal = ({ open, onClose, catalog }) => {
  const dispatch = useDispatch();
  const attributes = useSelector(selectAttribute);
  const [selectedAttributeIds, setSelectedAttributeIds] = useState([]);

  const {
    register,
    handleSubmit,
    reset,
    control,
    setValue,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(catalogValidationSchema),
  });

  useEffect(() => {
    dispatch(fetchAllAttributes());
  }, [dispatch]);

  useEffect(() => {
    reset({ name: "", attributeIds: [] });

    if (open) {
      let name = "";
      let attributeIds = [];
      if (catalog) {
        name = catalog.name;
        attributeIds =
          catalog.catalogAttributes.map((attr) => attr.attribute.id) || [];
      }
      setSelectedAttributeIds(attributeIds);
      reset({ name: name, attributeIds: attributeIds });
    }
  }, [open, catalog, reset]);

  const handleAttributeSelection = (event) => {
    const { value } = event.target;
    setSelectedAttributeIds(value);
    setValue("attributeIds", value);
  };

  const onSubmit = async (data) => {
    try {
      const formData = {
        name: data.name,
        attributeIds: selectedAttributeIds,
      };

      if (catalog) {
        await dispatch(updateCatalog({ ...formData, catalogId: catalog.id }));
      } else {
        await dispatch(createCatalog(formData));
      }
      await dispatch(fetchAllCatalogs());
      onClose();
    } catch (error) {
      console.error("Ошибка при операции с каталогом:", error);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>
        {catalog ? "Редактировать каталог" : "Добавить каталог"}
      </DialogTitle>
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
        <FormControl fullWidth margin="dense" error={!!errors.attributeIds}>
          <InputLabel>Атрибуты</InputLabel>
          <Controller
            name="attributeIds"
            control={control}
            defaultValue={[]}
            render={({ field }) => (
              <Select
                {...field}
                multiple
                value={selectedAttributeIds}
                onChange={handleAttributeSelection}
                input={<OutlinedInput label="Атрибуты" />}
                renderValue={(selected) => (
                  <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                    {selected.map((id) => {
                      const attribute = attributes.find(
                        (type) => type.id === id
                      );
                      return (
                        <Chip
                          key={id}
                          label={attribute ? attribute.name : id}
                        />
                      );
                    })}
                  </Box>
                )}
              >
                {attributes.map((type) => (
                  <MenuItem key={type.id} value={type.id}>
                    {type.name}
                  </MenuItem>
                ))}
              </Select>
            )}
          />
          {errors.attributeIds && (
            <FormHelperText>{errors.attributeIds.message}</FormHelperText>
          )}
        </FormControl>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="primary">
          Отмена
        </Button>
        <Button onClick={handleSubmit(onSubmit)} color="primary">
          {catalog ? "Сохранить" : "Добавить"}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CatalogModal;

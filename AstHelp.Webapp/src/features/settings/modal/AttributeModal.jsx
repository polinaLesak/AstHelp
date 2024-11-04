import React, { useEffect, useState } from "react";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
  Button,
  Select,
  MenuItem,
  FormHelperText,
  InputLabel,
  FormControl,
} from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { useForm } from "react-hook-form";
import { attributeValidationSchema } from "../../../validation/settingsValidation";
import {
  createAttribute,
  fetchAllAttributes,
  fetchAllAttributeTypes,
  updateAttribute,
} from "../../../entities/attribute/api/attributeApi";
import { createSelector } from "reselect";

const selectAttributeState = (state) => state.attribute;
const selectAttributeTypes = createSelector(
  [selectAttributeState],
  (attributeState) => attributeState.attributeTypes || []
);

const AttributeModal = ({ open, onClose, attribute }) => {
  const dispatch = useDispatch();
  const attributeTypes = useSelector(selectAttributeTypes);
  const [attributeTypeId, setAttributeTypeId] = useState("");

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(attributeValidationSchema),
  });

  useEffect(() => {
    dispatch(fetchAllAttributeTypes());
  }, [dispatch]);

  useEffect(() => {
    reset({ name: "", attributeTypeId: "" });

    if (open) {
      let name = "", attributeTypeId = "";
      if (attribute) {
        name = attribute.name
        attributeTypeId = attribute.attributeTypeId || ""
      } else if (attributeTypes.length > 0) {
        attributeTypeId = attributeTypes[0].id
      } else {
        attributeTypeId = ""
      }
      setAttributeTypeId(attributeTypeId);
      reset({ name: name, attributeTypeId: attributeTypeId });
    }
  }, [open, attribute, reset, attributeTypes]);

  const handleAttributeTypeChange = (event) => {
    setAttributeTypeId(event.target.value);
  };

  const onSubmit = async (data) => {
    try {
      const formData = {
        name: data.name,
        attributeTypeId: attributeTypeId,
      };

      if (attribute) {
        await dispatch(
          updateAttribute({ ...formData, attributeId: attribute.id })
        );
      } else {
        await dispatch(createAttribute(formData));
      }
      await dispatch(fetchAllAttributes());
      onClose();
    } catch (error) {
      console.error("Ошибка при операции с атрибутами:", error);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>
        {attribute ? "Редактировать атрибут" : "Добавить атрибут"}
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
        <FormControl fullWidth margin="dense" error={!!errors.attributeTypeId}>
          <InputLabel>Тип атрибута</InputLabel>
          <Select
            label="Тип атрибута"
            value={attributeTypeId || ""}
            onChange={handleAttributeTypeChange}
          >
            {attributeTypes.map((type) => (
              <MenuItem key={type.id} value={type.id}>
                {type.name}
              </MenuItem>
            ))}
          </Select>
          {errors.attributeTypeId && (
            <FormHelperText>{errors.attributeTypeId.message}</FormHelperText>
          )}
        </FormControl>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="primary">
          Отмена
        </Button>
        <Button onClick={handleSubmit(onSubmit)} color="primary">
          {attribute ? "Сохранить" : "Добавить"}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default AttributeModal;

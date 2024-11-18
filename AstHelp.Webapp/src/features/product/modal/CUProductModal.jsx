import React, { useEffect, useState } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
  MenuItem,
} from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import {
  createProduct,
  fetchAllProducts,
  updateProduct,
} from "../../../entities/product/api/productApi";
import { fetchAttributesByCatalogId } from "../../../entities/attribute/api/attributeApi";
import { fetchAllCatalogs } from "../../../entities/catalog/api/catalogApi";
import { fetchAllBrands } from "../../../entities/brand/api/brandApi";
import { Controller, useForm } from "react-hook-form";
import { productValidationSchema } from "../../../validation/productValidation";
import { yupResolver } from "@hookform/resolvers/yup";

export default function CUProductModal({ open, onClose, product }) {
  const dispatch = useDispatch();
  const { sortState } = useSelector((state) => state.product);
  const catalogs = useSelector((state) => state.catalog.catalogs);
  const brands = useSelector((state) => state.brand.brands);

  const [formData, setFormData] = useState({
    productId: null,
    name: "",
    quantity: 0,
    catalogId: "",
    brandId: "",
    productAttributes: [],
    imageFile: null,
  });

  const { handleSubmit, control, setValue, reset } = useForm({
    resolver: yupResolver(productValidationSchema),
    defaultValues: formData,
  });

  useEffect(() => {
    dispatch(fetchAllBrands());
    dispatch(fetchAllCatalogs());
  }, [dispatch]);

  useEffect(() => {
    if (product) {
      const productAttributes = product.attributeValues.map((attrValue) => ({
        id: attrValue.attributeId,
        name: attrValue.attribute.name,
        attributeTypeId: attrValue.attribute.attributeTypeId,
        value:
          attrValue.valueString ||
          attrValue.valueInt ||
          attrValue.valueNumeric ||
          "",
      }));

      const newFormData = {
        productId: product.id,
        name: product.name,
        quantity: product.quantity,
        catalogId: product.catalogId,
        brandId: product.brandId,
        productAttributes,
        imageFile: null,
      };
      setFormData(newFormData);
      dispatch(fetchAttributesByCatalogId(product.catalogId)).then(
        (response) => {
          const productAttributes = response.payload.map((attr) => ({
            ...attr,
            value: "",
          }));
          setFormData((prev) => ({ ...prev, productAttributes }));
        }
      );

      reset(newFormData);
    } else {
      const resetData = {
        productId: null,
        name: "",
        quantity: 0,
        catalogId: "",
        brandId: "",
        productAttributes: [],
        imageFile: null,
      };
      setFormData(resetData);
      reset(resetData);
    }
  }, [dispatch, product, reset, open]);

  const handleCatalogChange = async (event) => {
    console.log("asdasdasd");
    const catalogId = event.target.value;
    setFormData({ ...formData, catalogId });
    setValue("catalogId", catalogId);

    await dispatch(fetchAttributesByCatalogId(catalogId)).then((response) => {
      const productAttributes = response.payload.map((attr) => ({
        ...attr,
        value: "",
      }));
      setFormData((prev) => ({ ...prev, productAttributes }));
    });
  };

  const handleAttributeChange = (index, value) => {
    const newAttributes = [...formData.productAttributes];
    newAttributes[index].value = value;
    setFormData({ ...formData, productAttributes: newAttributes });
    setValue(`productAttributes.${index}.value`, value);
  };

  const handleImageChange = (event) => {
    setFormData({ ...formData, imageFile: event.target.files[0] });
  };

  const onSubmit = async (data) => {
    const formDataToSend = new FormData();
    formDataToSend.append("id", data.productId || "");
    formDataToSend.append("name", data.name);
    formDataToSend.append("quantity", data.quantity);
    formDataToSend.append("catalogId", data.catalogId);
    formDataToSend.append("brandId", data.brandId);

    let attributes = [];
    formData.productAttributes.forEach((attr) => {
      attributes.push({
        attributeId: attr.id,
        valueString: attr.attributeTypeId === 1 ? attr.value : null,
        valueInt: attr.attributeTypeId === 2 ? parseInt(attr.value, 10) : null,
        valueNumeric:
          attr.attributeTypeId === 3 ? parseFloat(attr.value) : null,
      });
    });
    formDataToSend.append(`productAttributes`, JSON.stringify(attributes));

    if (formData.imageFile) {
      formDataToSend.append("image", formData.imageFile);
    }

    if (product) {
      await dispatch(updateProduct(formDataToSend));
    } else {
      await dispatch(createProduct(formDataToSend));
    }

    await dispatch(fetchAllProducts(sortState));
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>
        {Boolean(product)
          ? "Редактировать оборудование"
          : "Добавить оборудование"}
      </DialogTitle>
      <DialogContent>
        <Controller
          name="name"
          control={control}
          defaultValue={formData.name}
          render={({ field, fieldState }) => (
            <TextField
              {...field}
              label="Наименование"
              fullWidth
              margin="dense"
              error={!!fieldState.error}
              helperText={fieldState.error ? fieldState.error.message : ""}
            />
          )}
        />
        <Controller
          name="quantity"
          control={control}
          defaultValue={formData.quantity}
          render={({ field, fieldState }) => (
            <TextField
              {...field}
              label="Количество в наличии"
              fullWidth
              type="number"
              margin="dense"
              error={!!fieldState.error}
              helperText={fieldState.error ? fieldState.error.message : ""}
            />
          )}
        />
        <Controller
          name="image"
          control={control}
          render={({ field, fieldState }) => (
            <TextField
              {...field}
              label="Загрузить изображение"
              type="file"
              fullWidth
              margin="dense"
              InputLabelProps={{
                shrink: true,
              }}
              error={!!fieldState.error}
              helperText={fieldState.error ? fieldState.error.message : ""}
              inputProps={{
                accept: "image/*",
              }}
              onChange={handleImageChange}
            />
          )}
        />
        <Controller
          name="brandId"
          control={control}
          defaultValue={formData.brandId}
          render={({ field, fieldState }) => (
            <TextField
              {...field}
              label="Бренд"
              select
              fullWidth
              margin="dense"
              error={!!fieldState.error}
              helperText={fieldState.error ? fieldState.error.message : ""}
              onChange={(e) => {
                field.onChange(e);
                setFormData({ ...formData, brandId: e.target.value });
              }}
            >
              {brands?.map((brand) => (
                <MenuItem key={brand.id} value={brand.id}>
                  {brand.name}
                </MenuItem>
              ))}
            </TextField>
          )}
        />
        <Controller
          name="catalogId"
          control={control}
          defaultValue={formData.catalogId}
          render={({ field, fieldState }) => (
            <TextField
              {...field}
              label="Каталог"
              select
              fullWidth
              margin="dense"
              error={!!fieldState.error}
              helperText={fieldState.error ? fieldState.error.message : ""}
              onChange={(e) => {
                field.onChange(e);
                handleCatalogChange(e);
              }}
            >
              {catalogs?.map((catalog) => (
                <MenuItem key={catalog.id} value={catalog.id}>
                  {catalog.name}
                </MenuItem>
              ))}
            </TextField>
          )}
        />
        {formData.productAttributes.map((attribute, index) => (
          <Controller
            key={attribute.id}
            name={`productAttributes.${index}.value`}
            control={control}
            defaultValue={attribute.value}
            render={({ field, fieldState }) => (
              <TextField
                {...field}
                label={attribute.name}
                fullWidth
                margin="dense"
                error={!!fieldState.error}
                helperText={fieldState.error ? fieldState.error.message : ""}
                onChange={(e) => {
                  field.onChange(e.target.value);
                  handleAttributeChange(index, e.target.value);
                }}
                type={
                  attribute.attributeTypeId === 2
                    ? "number"
                    : attribute.attributeTypeId === 3
                    ? "number"
                    : "text"
                }
              />
            )}
          />
        ))}
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Отмена</Button>
        <Button
          onClick={handleSubmit(onSubmit)}
          variant="contained"
          color="primary"
        >
          {Boolean(product) ? "Сохранить" : "Добавить"}
        </Button>
      </DialogActions>
    </Dialog>
  );
}

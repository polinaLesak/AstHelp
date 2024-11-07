import React, { useEffect, useState } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
  MenuItem
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
  const catalogs = useSelector((state) => state.catalog.catalogs);
  const brands = useSelector((state) => state.brand.brands);

  const [formData, setFormData] = useState({
    productId: null,
    name: '',
    catalogId: '',
    brandId: '',
    productAttributes: [],
  });

  const {
    handleSubmit,
    control,
    setValue,
    reset,
  } = useForm({
    resolver: yupResolver(productValidationSchema),
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
        value: attrValue.valueString || attrValue.valueInt || attrValue.valueNumeric || '',
      }));

      const newFormData = {
        productId: product.id,
        name: product.name,
        catalogId: product.catalogId,
        brandId: product.brandId,
        productAttributes,
      };

      setFormData(newFormData);
      reset(newFormData);
    } else {
      let resetData = {
        productId: null,
        name: '',
        catalogId: '',
        brandId: '',
        productAttributes: [],
      }
      setFormData(resetData);
      reset(resetData);
    }
  }, [product, reset, open]);

  const handleCatalogChange = async (event) => {
    const catalogId = event.target.value;
    setFormData({ ...formData, catalogId });
    setValue('catalogId', catalogId);

    await dispatch(fetchAttributesByCatalogId(catalogId)).then((response) => {
      const productAttributes = response.payload.map((attr) => ({
        ...attr,
        value: '',
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

  const onSubmit = async (data) => {
    const payload = {
      productId: data.productId,
      name: data.name,
      catalogId: data.catalogId,
      brandId: data.brandId,
      productAttributes: formData.productAttributes.map((attr) => ({
        attributeId: attr.id,
        valueString: attr.attributeTypeId === 1 ? attr.value : null,
        valueInt: attr.attributeTypeId === 2 ? parseInt(attr.value, 10) : null,
        valueNumeric: attr.attributeTypeId === 3 ? parseFloat(attr.value) : null,
      })),
    };

    if (product) {
      await dispatch(updateProduct({ id: product.id, ...payload }));
    } else {
      await dispatch(createProduct(payload));
    }

    await dispatch(fetchAllProducts());
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

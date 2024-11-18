import * as yup from "yup";

export const productValidationSchema = yup.object().shape({
  name: yup.string().required("Наименование обязательно"),
  catalogId: yup.string().required("Выбор каталога обязателен"),
  quantity: yup.number().min(1, "Количество не может быть меньше 1"),
  brandId: yup.string().required("Выбор бренда обязателен")
});

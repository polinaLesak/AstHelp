import * as yup from "yup";

export const brandValidationSchema = yup.object().shape({
  name: yup
    .string()
    .min(2, "Бренд должен быть не менее 2 символов")
    .required("Введите наименование бренда"),
});

export const catalogValidationSchema = yup.object().shape({
  name: yup
    .string()
    .min(2, "Наименование категории должно быть не менее 2 символов")
    .required("Введите наименование категории"),
  attributeIds: yup.array().min(1, "Выберите хотя бы один атрибут"),
});

export const attributeValidationSchema = yup.object().shape({
  name: yup
    .string()
    .min(2, "Наименование атрибута должно быть не менее 2 символов")
    .required("Введите наименование атрибута"),
  attributeTypeId: yup.string().required("Выберите тип атрибута"),
});

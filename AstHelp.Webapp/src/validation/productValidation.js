import * as yup from "yup";

export const productValidationSchema = yup.object().shape({
  name: yup.string().required("Наименование обязательно"),
  catalogId: yup.string().required("Выбор каталога обязателен"),
  brandId: yup.string().required("Выбор бренда обязателен"),
  // productAttributes: yup.array().of(
  //   yup.object().shape({
  //     attributeTypeId: yup
  //       .number()
  //       .oneOf([1, 2, 3], "Некорректный тип атрибута")
  //       .required("Тип атрибута обязателен"),
  //     value: yup
  //       .mixed()
  //       .when("attributeTypeId", {
  //         is: 1,
  //         then: yup
  //           .string()
  //           .required("Значение обязательно для текстового поля"),
  //         is: 2,
  //         then: yup
  //           .number()
  //           .typeError("Значение должно быть целым числом")
  //           .integer("Должно быть целым числом")
  //           .required("Значение обязательно для числового поля"),
  //         is: 3,
  //         then: yup
  //           .number()
  //           .typeError("Значение должно быть числом")
  //           .required("Значение обязательно для числового поля"),
  //       }),
  //   })
  // ),
});

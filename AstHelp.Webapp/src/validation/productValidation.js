import * as yup from "yup";

export const productValidationSchema = yup.object().shape({
  name: yup.string().required("Наименование обязательно"),
  catalogId: yup.string().required("Выбор каталога обязателен"),
  brandId: yup.string().required("Выбор бренда обязателен"),
  // attributes: yup.array().of(
  //   yup.object().shape({
  //     type: yup.string().oneOf(["string", "int", "numeric"]).required(),
  //     value: yup.mixed().when("type", {
  //       is: "string",
  //       then: yup.string().required("Значение обязательно для текстового поля"),
  //       otherwise: yup.number()
  //         .typeError("Значение должно быть числом")
  //         .required("Значение обязательно")
  //         .when("type", {
  //           is: "int",
  //           then: yup.number().integer("Должно быть целым числом"),
  //           otherwise: yup.number().required("Значение обязательно для числового поля"),
  //         }),
  //     }),
  //   })
  // ),
});

import { useParams } from "react-router-dom";
import { Box, Button, Table, TableBody, TableCell, TableRow, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { fetchProductById } from "../entities/product/api/productApi";
import config from "../config";
import noImage from "../assets/noImage.svg";
import ProductQuantityModal from "../features/product/modal/ProductQuantityModal";
import { addProductToUserCart, fetchCartProductsCountByUserId } from "../entities/cart/api/cartApi";

export default function ProductDetailsPage() {
  const dispatch = useDispatch();
  const { user } = useSelector((state) => state.login);
  const [open, setOpen] = useState(false);
  const [availableQuantity, setAvailableQuantity] = useState(1);
  const { id } = useParams();
  const product = useSelector((state) => state.product.product);

  useEffect(() => {
    dispatch(fetchProductById(id));
  }, [dispatch, id]);

  if (!product) {
    return <Typography>Продукт не найден.</Typography>;
  }

  const handleSubmitQuantity = async (quantity) => {
    const orderData = {
      userId: user.id,
      productId: product.id,
      quantity: quantity,
    };
    await dispatch(addProductToUserCart(orderData));
    await dispatch(fetchCartProductsCountByUserId(user.id));
  };

  const handleOpenModal = (quantity) => {
    setAvailableQuantity(quantity);
    setOpen(true);
  };
  const handleCloseModal = () => setOpen(false);

  return (
    <Box sx={{ padding: 4 }}>
      <Grid container spacing={4}>
        <Grid size={12} item>
          <Typography variant="h4" gutterBottom>
            {product.name}
          </Typography>
          <Typography variant="subtitle1" sx={{ color: product.quantity < 10 ? "red" : "green" }}>
            В наличии { product.quantity } шт.
          </Typography>
        </Grid>

        <Grid container spacing={2}>
          <Grid item xs={4}>
            <Box
              component="img"
              sx={{ width: "100%", height: "auto", maxHeight: 300 }}
              src={
                product.imageUrl !== ""
                  ? `${config.apiUrl}resources/catalog${product.imageUrl}`
                  : noImage
              }
              alt={product.name}
            />
          </Grid>
          <Grid item xs={8}>
          {[2, 3].some((num) => user.roles.includes(num)) ? (
              <Button
                size="small"
                variant="contained"
                color="primary"
                onClick={() => handleOpenModal(product.quantity)}
              >
                Добавить в заявку
              </Button>
            ) : (
              <></>
            )}
          </Grid>
        </Grid>
      </Grid>

      <Box sx={{ marginTop: 4 }}>
        <Typography variant="h5" gutterBottom>
          Характеристики
        </Typography>
        <Table>
          <TableBody>
            {product.attributeValues?.map((attr, index) => (
              <TableRow key={index}>
                <TableCell>{attr.attribute?.name}</TableCell>
                <TableCell>{attr.valueString || attr.valueInt || attr.valueNumeric}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Box>
      <ProductQuantityModal
        open={open}
        onClose={handleCloseModal}
        onSubmit={handleSubmitQuantity}
        maxQuantity={availableQuantity}
      />
    </Box>
  );
}

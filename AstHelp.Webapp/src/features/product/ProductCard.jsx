import { Box, Button, Card, CardContent, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import noImage from "../../assets/noImage.svg";
import { useDispatch, useSelector } from "react-redux";
import ProductQuantityModal from "./modal/ProductQuantityModal";
import InventoryIcon from "@mui/icons-material/Inventory";
import CalendarTodayIcon from "@mui/icons-material/CalendarToday";
import {
  addProductToUserCart,
  fetchCartProductsCountByUserId,
} from "../../entities/cart/api/cartApi";
import { useState } from "react";
import config from "../../config";

export default function ProductCard({ product, onEdit, onDelete }) {
  const dispatch = useDispatch();
  const { user } = useSelector((state) => state.login);
  const [open, setOpen] = useState(false);

  const handleOpenModal = () => setOpen(true);
  const handleCloseModal = () => setOpen(false);

  const handleSubmitQuantity = async (quantity) => {
    const orderData = {
      userId: user.id,
      productId: product.id,
      quantity: quantity,
    };
    await dispatch(addProductToUserCart(orderData));
    await dispatch(fetchCartProductsCountByUserId(user.id));
  };

  return (
    <Card
      sx={{
        backgroundColor: `#e8e8e8`,
      }}
    >
      <CardContent>
        <Grid container>
          <Grid size={8}>
            <Typography variant="h6" mb={1}>
              {product.catalog?.name} {product.name}
            </Typography>
            <Typography variant="body2">
              Бренд: {product.brand?.name}
            </Typography>
            {product.attributeValues?.map((attr) => (
              <Typography variant="body2" key={attr.id}>
                {attr.attribute?.name}:{" "}
                {attr.valueString || attr.valueInt || attr.valueNumeric}
              </Typography>
            ))}
            <Typography variant="body2" color="text.secondary">
              <InventoryIcon fontSize="small" /> Доступное количество:{" "}
              <b style={{ color: product.quantity < 10 ? "red" : "inherit" }}>
                {product.quantity}
              </b>
            </Typography>
            <Typography variant="body2" color="text.secondary">
              <CalendarTodayIcon fontSize="small" /> Дата создания:{" "}
              {new Date(product.createdAt).toLocaleDateString()}
            </Typography>
          </Grid>
          <Grid
            size={2}
            sx={{
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              gap: 1,
            }}
          >
            {[1].some((num) => user.roles.includes(num)) ? (
              <>
                <Button
                  size="small"
                  variant="contained"
                  color="secondary"
                  onClick={onEdit}
                >
                  Редактировать
                </Button>
                <Button
                  size="small"
                  variant="contained"
                  color="error"
                  onClick={onDelete}
                >
                  Удалить
                </Button>
              </>
            ) : (
              <></>
            )}
            {[3].some((num) => user.roles.includes(num)) ? (
              <Button
                size="small"
                variant="contained"
                color="primary"
                onClick={handleOpenModal}
              >
                Добавить в заявку
              </Button>
            ) : (
              <></>
            )}
          </Grid>
          <Grid size={2}>
            <Box
              component="img"
              sx={{
                width: "100%",
                height: "auto",
                maxHeight: 140,
                objectFit: "contain",
              }}
              src={
                product.imageUrl !== ""
                  ? `${config.apiUrl}resources/catalog${product.imageUrl}`
                  : noImage
              }
            />
          </Grid>
        </Grid>
      </CardContent>
      <ProductQuantityModal
        open={open}
        onClose={handleCloseModal}
        onSubmit={handleSubmitQuantity}
      />
    </Card>
  );
}

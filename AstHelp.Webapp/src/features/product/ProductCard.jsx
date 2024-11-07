import { Box, Button, Card, CardContent, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import noImage from "../../assets/noImage.svg";
import { useSelector } from "react-redux";

export default function ProductCard({ product, onEdit, onDelete }) {
  const { user } = useSelector((state) => state.login);

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
            <Button size="small" variant="contained" color="primary">
              Добавить в заявку
            </Button>
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
              src={product.imageUrl ?? noImage}
            />
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
}

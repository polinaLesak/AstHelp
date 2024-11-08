import React from "react";
import Grid from "@mui/material/Grid2";
import CartTable from "../features/cart/CartTable";

export default function CartPage() {
  return (
    <Grid container>
      <Grid size={12} sx={{ mb: 10 }}>
        <CartTable />
      </Grid>
    </Grid>
  );
}

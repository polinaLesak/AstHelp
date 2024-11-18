import React, { useState } from "react";
import {
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Button,
  TextField,
} from "@mui/material";

export default function ProductQuantityModal({
  open,
  onClose,
  onSubmit,
  maxQuantity,
}) {
  const [quantity, setQuantity] = useState(1);
  const [error, setError] = useState("");

  const handleQuantityChange = (event) => {
    const value = Number(event.target.value);

    if (value < 1 || isNaN(value)) {
      setError("Введите количество больше 0");
    } else if (maxQuantity && value > maxQuantity) {
      setError(`Максимальное количество: ${maxQuantity}`);
    } else {
      setError("");
    }

    setQuantity(value);
  };

  const handleSubmit = () => {
    if (quantity > 0 && (!maxQuantity || quantity <= maxQuantity)) {
      onSubmit(quantity);
      onClose();
    } else {
      setError(`Введите корректное количество (макс. ${maxQuantity})`);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="xs" fullWidth>
      <DialogTitle>Выберите количество</DialogTitle>
      <DialogContent>
        <TextField
          label="Количество"
          type="number"
          value={quantity}
          onChange={handleQuantityChange}
          fullWidth
          sx={{ mt: 2 }}
          error={!!error}
          helperText={error}
          inputProps={{
            min: 1,
            max: maxQuantity,
          }}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Отмена</Button>
        <Button
          onClick={handleSubmit}
          variant="contained"
          color="primary"
          disabled={!!error || quantity < 1}
        >
          Подтвердить
        </Button>
      </DialogActions>
    </Dialog>
  );
}

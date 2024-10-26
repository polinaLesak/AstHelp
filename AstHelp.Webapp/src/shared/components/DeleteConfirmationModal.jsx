import React from "react";
import { Modal, Box, Typography, Button } from "@mui/material";

const DeleteConfirmationModal = ({ open, onClose, onConfirm, message = "Вы уверены, что хотите удалить этот элемент?" }) => (
  <Modal open={open} onClose={onClose}>
    <Box sx={{ p: 4, backgroundColor: "white", mx: "auto", my: "20%", width: 400, borderRadius: 1 }}>
      <Typography variant="h6" color="error">
        Удалить элемент?
      </Typography>
      <Typography variant="body1" sx={{ mt: 2 }}>{message}</Typography>
      <Button onClick={onConfirm} color="error" variant="contained" fullWidth sx={{ mt: 2 }}>
        Удалить
      </Button>
    </Box>
  </Modal>
);

export default DeleteConfirmationModal;
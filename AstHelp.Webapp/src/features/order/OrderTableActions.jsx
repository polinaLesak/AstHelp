import React, { useState } from "react";
import { IconButton, Tooltip } from "@mui/material";
import { Delete } from "@mui/icons-material";
import { useDispatch } from "react-redux";
import DeleteConfirmationModal from "../../shared/components/DeleteConfirmationModal";
import { removeProductFromOrder } from "../../entities/order/api/orderApi";

export default function OrderTableActions({ product, userRoles, isAdmin }) {
  const dispatch = useDispatch();
  const [openDelete, setOpenDelete] = useState(false);

  const handleOpenDelete = () => setOpenDelete(true);
  const handleCloseDelete = () => setOpenDelete(false);

  const handleDeleteProduct = async () => {
    try {
      await dispatch(
        removeProductFromOrder({
          orderId: product.orderId,
          productId: product.productId
        })
      );
      handleCloseDelete();
    } catch (error) {
      console.error("Ошибка при удалении продукта из корзины:", error);
    }
  };

  return (
    <>
      {[1, 2].some((num) => userRoles.includes(num)) && (
        <Tooltip title="Удалить товар из заказа">
          <IconButton onClick={handleOpenDelete} color="error" size="small">
            <Delete />
          </IconButton>
        </Tooltip>
      )}

      <DeleteConfirmationModal
        open={openDelete}
        onClose={handleCloseDelete}
        onConfirm={() => handleDeleteProduct()}
        message="Вы уверены, что хотите удалить данный товар?"
      />
    </>
  );
}

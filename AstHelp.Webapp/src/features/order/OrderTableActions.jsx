import React, { useState } from "react";
import { IconButton, Tooltip } from "@mui/material";
import { Delete } from "@mui/icons-material";
import { useDispatch, useSelector } from "react-redux";
import DeleteConfirmationModal from "../../shared/components/DeleteConfirmationModal";
// import { updateOrderItemStatus } from "../../entities/order/api/orderApi";

export default function OrderTableActions({ product, userRoles, isAdmin }) {
  const dispatch = useDispatch();
  const { user } = useSelector((state) => state.login);
  const [openDelete, setOpenDelete] = useState(false);

  const handleOpenDelete = () => setOpenDelete(true);
  const handleCloseDelete = () => setOpenDelete(false);

  const handleChangeStatus = async (newStatus) => {
    // try {
    //   await dispatch(
    //     updateOrderItemStatus({
    //       orderId: product.orderId,
    //       productId: product.id,
    //       status: newStatus,
    //     })
    //   );
    // } catch (error) {
    //   console.error("Ошибка при обновлении статуса:", error);
    // }
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
        onConfirm={() => handleChangeStatus("Canceled")}
        message="Вы уверены, что хотите отменить данный товар?"
      />
    </>
  );
}

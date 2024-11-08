import { Delete } from "@mui/icons-material";
import { IconButton, Tooltip } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { useState } from "react";
import { deleteProductFromUserCart, fetchCartByUserId, fetchCartProductsCountByUserId } from "../../entities/cart/api/cartApi";
import DeleteConfirmationModal from "../../shared/components/DeleteConfirmationModal";

export default function CartTableActions({product}) {
  const dispatch = useDispatch();
  const { user } = useSelector((state) => state.login);

  const [openDelete, setOpenDelete] = useState(false);

  const handleOpenDelete = () => setOpenDelete(true);
  const handleCloseDelete = () => setOpenDelete(false);
  
  const handleDelete = async () => {
    try {
      console.log(product.id, 'product')
      await dispatch(deleteProductFromUserCart({ userId: user.id, productId: product.id })).unwrap();
      dispatch(fetchCartProductsCountByUserId(user.id));
      dispatch(fetchCartByUserId(user.id));
      handleCloseDelete();
    } catch (error) {
      console.error("Ошибка при удалении:", error);
    }
  };

  return (
    <>
      <Tooltip title="Удалить">
        <IconButton onClick={handleOpenDelete} color="error" size="small">
          <Delete />
        </IconButton>
      </Tooltip>

      <DeleteConfirmationModal
        open={openDelete}
        onClose={handleCloseDelete}
        onConfirm={handleDelete}
        message="Вы уверены, что хотите удалить данное оборудование из заказа?"
      />
    </>
  );
};

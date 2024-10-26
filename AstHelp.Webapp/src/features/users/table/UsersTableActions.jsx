import { Delete, Edit } from "@mui/icons-material";
import { IconButton, Tooltip } from "@mui/material";
import { useDispatch } from "react-redux";
import { deleteUser, fetchAllUsers } from "../../../entities/user/api/userApi";
import DeleteConfirmationModal from "../../../shared/components/DeleteConfirmationModal";
import UsersEditModal from "./UsersEditModal";
import { useState } from "react";

const UsersTableActions = ({user}) => {
  const dispatch = useDispatch();

  const [openEdit, setOpenEdit] = useState(false);
  const [openDelete, setOpenDelete] = useState(false);

  const handleOpenEdit = () => setOpenEdit(true);
  const handleCloseEdit = () => setOpenEdit(false);

  const handleOpenDelete = () => setOpenDelete(true);
  const handleCloseDelete = () => setOpenDelete(false);
  
  const handleDelete = async () => {
    try {
      await dispatch(deleteUser(user.id)).unwrap();
      dispatch(fetchAllUsers());
      handleCloseDelete();
    } catch (error) {
      console.error("Ошибка при удалении:", error);
    }
  };

  return (
    <>
      <Tooltip title="Редактировать">
        <IconButton onClick={handleOpenEdit} color="primary" size="small">
          <Edit />
        </IconButton>
      </Tooltip>
      <Tooltip title="Удалить">
        <IconButton onClick={handleOpenDelete} color="error" size="small">
          <Delete />
        </IconButton>
      </Tooltip>

      <UsersEditModal
        open={openEdit}
        onClose={handleCloseEdit}
        user={user}
      />

      <DeleteConfirmationModal
        open={openDelete}
        onClose={handleCloseDelete}
        onConfirm={handleDelete}
        message="Вы уверены, что хотите удалить этого пользователя?"
      />
    </>
  );
};

export default UsersTableActions;

import { CancelOutlined, CheckCircleOutline } from "@mui/icons-material";
import { IconButton, Tooltip } from "@mui/material";
import { fetchAllUsers, updateUserStatus } from "../../entities/user/api/userApi";
import { useDispatch } from "react-redux";

const ActivationButton = ({ isActive, userId }) => {
  const dispatch = useDispatch();

  const handleActivationToggle = async () => {
    try {
      await dispatch(updateUserStatus(userId)).unwrap();
      dispatch(fetchAllUsers());
    } catch (error) {
      console.error("Ошибка при изменении статуса пользователя:", error);
    }
  };

  return (
    <Tooltip title={isActive ? "Деактивировать аккаунт" : "Активировать аккаунт"}>
      <IconButton
        onClick={handleActivationToggle}
        color={isActive ? "success" : "error"}
        size="small"
      >
        {isActive ? <CheckCircleOutline /> : <CancelOutlined />}
      </IconButton>
    </Tooltip>
  );
};

export default ActivationButton;
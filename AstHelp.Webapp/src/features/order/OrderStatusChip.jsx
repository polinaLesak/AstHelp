import React from "react";
import Chip from "@mui/material/Chip";
import { green, orange, blue, red, grey } from "@mui/material/colors";

const OrderStatusChip = ({ status }) => {
  const getChipProps = (status) => {
    switch (status) {
      case "Pending":
        return { label: "В ожидании", color: orange[500], bgColor: orange[100] };
      case "Processing":
        return { label: "В обработке", color: blue[500], bgColor: blue[100] };
      case "Packaged":
        return { label: "Укомплектован", color: green[500], bgColor: green[100] };
      case "Performed":
        return { label: "Выполнен", color: green[500], bgColor: green[100] };
      case "Canceled":
        return { label: "Отменён", color: red[500], bgColor: red[100] };
      default:
        return { label: "Неизвестно", color: grey[500], bgColor: grey[100] };
    }
  };

  const { label, color, bgColor } = getChipProps(status);

  return (
    <Chip
      label={label}
      style={{
        color: color,
        backgroundColor: bgColor,
        fontWeight: "bold",
      }}
    />
  );
};

export default OrderStatusChip;

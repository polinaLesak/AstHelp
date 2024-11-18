import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import moment from "moment";
import { ruRU } from "@mui/x-data-grid/locales";
import { Box, Button, Typography } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import { fetchAllOrders, fetchOrdersByManagerId, fetchOrdersByUserId } from "../entities/order/api/orderApi";
import OrderStatusChip from "../features/order/OrderStatusChip";

const getChipStatus = (statusId) => {
  switch (statusId) {
    case 0:
      return "Pending";
    case 1:
      return "Processing";
    case 2:
      return "Packaged";
    case 3:
      return "Performed";
    case 4:
      return "Canceled";
    default:
      return "default";
  }
};

export default function OrdersPage({pageType}) {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { user } = useSelector((state) => state.login);
  const orders = useSelector((state) => state.order.orders);

  useEffect(() => {
    if (user) {
      if(pageType === "Мои заказы")
        dispatch(fetchOrdersByUserId(user.id));
      else if(pageType === "Заявки")
        dispatch(fetchOrdersByManagerId(user.id));
      else 
        dispatch(fetchAllOrders());
    }
  }, [dispatch, user, pageType]);

  const handleRowClick = (params) => {
    navigate(`/orders/${params.id}`);
  };

  const columns = [
    { field: "№",
      headerName: "№",
      renderCell: (params) =>
        params.api.getRowIndexRelativeToVisibleRows(params.id) + 1,
      flex: 0.3,
    },
    { field: "customerFullname", headerName: "ФИО заказчика", flex: 1 },
    { field: "customerPosition", headerName: "Должность заказчика", flex: 1 },
    { field: "managerFullname", headerName: "ФИО менеджера", flex: 1 },
    { field: "managerPosition", headerName: "Должность менеджера", flex: 1 },
    { field: "reasonForIssue", headerName: "Причина выдачи", flex: 1 },
    { field: "createdAt", headerName: "Дата создания", flex: 1, 
      renderCell: (params) => (
        moment(params.row.createdAt).format("DD.MM.YYYY HH:mm")
    ), },
    {
      field: "status",
      headerName: "Статус",
      align: "center",
      flex: 1,
      renderCell: (params) => (
        <OrderStatusChip status={getChipStatus(params.row.status)} />
      ),
    },
    {
      field: "actions",
      type: "actions",
      headerName: "",
      cellClassName: "actions",
      flex: 1,
      renderCell: (params) => {
        return (
          <Button
            variant="outlined"
            color="primary"
            size="small"
            onClick={() => handleRowClick(params.row)}
          >
            Подробнее
          </Button>
        )
      },
      align: "center",
    },
  ];

  return (
    <Box p={2}>
      <Typography variant="h4" gutterBottom>
        { pageType }
      </Typography>
      <DataGrid
        rows={orders}
        columns={columns}
        pageSize={5}
        onRowClick={handleRowClick}
        initialState={{
          pagination: { paginationModel: { pageSize: 5 } },
        }}
        pageSizeOptions={[5, 10, 25]}
        localeText={ruRU.components.MuiDataGrid.defaultProps.localeText}
        disableRowSelectionOnClick
      />
    </Box>
  );
}
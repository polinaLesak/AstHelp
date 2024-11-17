import React, { useEffect } from "react";
import Grid from "@mui/material/Grid2";
import { DataGrid } from "@mui/x-data-grid";
import { ruRU } from "@mui/x-data-grid/locales";
import { useDispatch, useSelector } from "react-redux";
import { Box } from "@mui/material";
import { fetchOrderById } from "../../entities/order/api/orderApi";
import OrderTableActions from "./OrderTableActions";
import OrderStatusChip from "./OrderStatusChip";

const columns = (isAdmin) => [
  {
    field: "№",
    headerName: "№",
    renderCell: (params) =>
      params.api.getRowIndexRelativeToVisibleRows(params.id) + 1,
    flex: 0.5,
  },
  {
    field: "catalogName",
    headerName: "Категория",
    valueGetter: (params) => params.row.catalogName,
    flex: 1,
  },
  {
    field: "productName",
    headerName: "Наименование",
    valueGetter: (params) => params.row.productName,
    flex: 1,
  },
  {
    field: "quantity",
    headerName: "Количество",
    valueGetter: (params) => params.row.quantity,
    align: "center",
    flex: 1,
  },
  {
    field: "status",
    headerName: "Статус",
    renderCell: (params) => <OrderStatusChip status={params.row.status} />,
    flex: 1,
    align: "center",
  },
  {
    field: "actions",
    type: "actions",
    headerName: "Действия",
    cellClassName: "actions",
    flex: 1,
    renderCell: (params) => (
      <OrderTableActions product={params.row} isAdmin={isAdmin} />
    ),
    align: "center",
  },
];

export default function OrderDetailsTable({ orderId }) {
  const dispatch = useDispatch();
  const orderDetails = useSelector((state) => state.order.orderDetails);
  const { user } = useSelector((state) => state.login);

  const isAdmin = user?.roles?.includes("Admin");

  useEffect(() => {
    if (orderId) {
      dispatch(fetchOrderById(orderId));
    }
  }, [dispatch, orderId]);

  return (
    <Grid
      container
      justifyContent="center"
      alignItems="top"
      sx={{
        m: 2,
      }}
    >
      <Grid size={12}>
        <Box width="100%" mt={2}>
          <DataGrid
            autoHeight
            rows={orderDetails?.items || []}
            columns={columns(isAdmin)}
            getRowId={(row) => row.id}
            localeText={ruRU.components.MuiDataGrid.defaultProps.localeText}
            disableRowSelectionOnClick
            pageSizeOptions={[5, 10, 25]}
            sx={{
              "&.MuiDataGrid-root--densityCompact .MuiDataGrid-cell": {
                py: 1,
              },
              "&.MuiDataGrid-root--densityStandard .MuiDataGrid-cell": {
                py: "15px",
              },
              "&.MuiDataGrid-root--densityComfortable .MuiDataGrid-cell": {
                py: "22px",
              },
            }}
          />
        </Box>
      </Grid>
    </Grid>
  );
}

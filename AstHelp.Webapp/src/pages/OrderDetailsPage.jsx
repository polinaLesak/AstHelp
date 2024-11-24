import React, { useEffect } from "react";
import Grid from "@mui/material/Grid2";
import { DataGrid } from "@mui/x-data-grid";
import { ruRU } from "@mui/x-data-grid/locales";
import { useDispatch, useSelector } from "react-redux";
import { Box, Button, Chip, TextField, Typography } from "@mui/material";
import {
  fetchOrderById,
  generateOrderAct,
  generateOrderReport,
  updateOrderStatus,
} from "../entities/order/api/orderApi";
import OrderTableActions from "../features/order/OrderTableActions";
import OrderStatusChip from "../features/order/OrderStatusChip";
import { useParams } from "react-router-dom";
import { grey } from "@mui/material/colors";
import download from "downloadjs";

const columns = (userRoles) => [
  {
    field: "№",
    headerName: "№",
    renderCell: (params) => {
      const rowIndex = params.api.getRowIndexRelativeToVisibleRows(params.id);
      return isNaN(rowIndex) ? 1 : rowIndex + 1;
    },
    flex: 0.5,
  },
  {
    field: "catalogName",
    headerName: "Категория",
    valueGetter: (value, row) => row.catalogName,
    flex: 1,
  },
  {
    field: "productName",
    headerName: "Наименование",
    valueGetter: (value, row) => row.productName,
    flex: 1,
  },
  {
    field: "quantity",
    headerName: "Количество",
    valueGetter: (value, row) => row.quantity,
    align: "center",
    flex: 1,
  },
  ...([1, 2].some((num) => userRoles.includes(num))
    ? [
        {
          field: "actions",
          type: "actions",
          headerName: "Действия",
          cellClassName: "actions",
          flex: 1,
          renderCell: (params) => (
            <OrderTableActions product={params.row} userRoles={userRoles} />
          ),
          align: "center",
        },
      ]
    : []),
];

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

export default function OrderDetailsTable() {
  const { orderId } = useParams();
  const dispatch = useDispatch();
  const order = useSelector((state) => state.order.order);
  const { user } = useSelector((state) => state.login);

  useEffect(() => {
    if (orderId) {
      dispatch(fetchOrderById(orderId));
    }
  }, [dispatch, orderId]);

  const handleChangeOrderStatus = async (status) => {
    try {
      await dispatch(
        updateOrderStatus({
          orderId: order.id,
          status,
        })
      );
    } catch (err) {
      console.error("Ошибка при изменении статуса заявки:", err);
    }
  };

  const handleGenerateAct = async (orderId) => {
    try {
      const result = await dispatch(generateOrderAct(orderId));
      if (generateOrderAct.fulfilled.match(result)) {
        const { data, status } = result.payload;
        
        if (status === 200) {
          download(data, `Act_${orderId}.docx`, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }
      }
    } catch (error) {
      console.error("Ошибка при генерации акта:", error);
    }
  };

  const handleGenerateReport = async (orderId) => {
    try {
      const result = await dispatch(generateOrderReport(orderId));
      if (generateOrderReport.fulfilled.match(result)) {
        const { data, status } = result.payload;
        
        if (status === 200) {
          download(data, `Report_${orderId}.xlsx`, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
      }
    } catch (error) {
      console.error("Ошибка при генерации отчёта:", error);
    }
  };

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
        <Box
          width="100%"
          mt={2}
          mb={2}
          display="flex"
          justifyContent="space-between"
        >
          <Box display="flex" alignItems="center">
            <Chip
              label={order?.customerFullname + ` (${order?.customerPosition})`}
              style={{
                color: grey,
                backgroundColor: grey,
                fontWeight: "bold",
              }}
            />
          </Box>
          <Box display="flex" alignItems="center">
            <Typography variant="h6" gutterBottom>
              Статус:
            </Typography>
            <OrderStatusChip status={getChipStatus(order?.status)} />
            {user != null && [1, 2].some((num) => user.roles.includes(num)) 
              && order?.status === 0 ? (
              <>
                <Button
                  variant="contained"
                  color="error"
                  sx={{ ml: 2 }}
                  onClick={() => handleChangeOrderStatus(4)}
                >
                  Отклонить заявку
                </Button>
              </>
            ) : (
              <></>
            )}
            {user != null && [2].some((num) => user.roles.includes(num)) 
              && order?.status === 0 ? (
              <>
                <Button
                  variant="contained"
                  sx={{ ml: 2 }}
                  onClick={() => handleChangeOrderStatus(1)}
                >
                  Одобрить заявку
                </Button>
              </>
            ) : (
              <></>
            )}
            {user != null && [1].some((num) => user.roles.includes(num)) 
              && order?.status === 1 ? (
              <>
                <Button
                  variant="contained"
                  sx={{ ml: 2 }}
                  onClick={() => handleChangeOrderStatus(2)}
                >
                  Заказ собран
                </Button>
              </>
            ) : (
              <></>
            )}
            {user != null && [1].some((num) => user.roles.includes(num)) 
              && order?.status === 2 ? (
              <>
                <Button
                  variant="contained"
                  sx={{ ml: 2 }}
                  onClick={() => handleChangeOrderStatus(3)}
                  disabled={order?.status !== 2}
                >
                  Выдать заказ
                </Button>
              </>
            ) : (
              <></>
            )}
          </Box>
        </Box>
      </Grid>
      <Grid size={12} m={2}>
        <TextField
          label="Причину выдачи"
          variant="outlined"
          fullWidth
          margin="dense"
          rows={2}
          multiline
          value={order?.reasonForIssue}
          disabled
        />
      </Grid>
      <Grid size={12}>
        <DataGrid
          autoHeight
          rows={order?.items || []}
          columns={columns(user?.roles)}
          getRowId={(row) => row.id}
          localeText={ruRU.components.MuiDataGrid.defaultProps.localeText}
          disableRowSelectionOnClick
          initialState={{
            pagination: { paginationModel: { pageSize: 5 } },
          }}
          pageSizeOptions={[5, 10, 25]}
        />
      </Grid>
      {user != null && [1].some((num) => user.roles.includes(num)) ? (
        <>
          <Grid size={12}>
            <Box display="flex" p={2}>
              <Box width="100%">
                <Box display="flex" justifyContent="end">
                  <Button
                    variant="contained"
                    onClick={() => handleGenerateAct(orderId)}
                  >
                    Выгрузить акт
                  </Button>
                </Box>
              </Box>
            </Box>
          </Grid>
        </>
      ) : (
        <></>
      )}
      {user != null && [2].some((num) => user.roles.includes(num)) ? (
        <>
          <Grid size={12}>
            <Box display="flex" p={2}>
              <Box width="100%">
                <Box display="flex" justifyContent="end">
                  <Button
                    variant="contained"
                    onClick={() => handleGenerateReport(orderId)}
                  >
                    Выгрузить отчёт
                  </Button>
                </Box>
              </Box>
            </Box>
          </Grid>
        </>
      ) : (
        <></>
      )}
    </Grid>
  );
}

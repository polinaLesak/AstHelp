import React, { useEffect, useState } from "react";
import Grid from "@mui/material/Grid2";
import { DataGrid } from "@mui/x-data-grid";
import { ruRU } from "@mui/x-data-grid/locales";
import { useDispatch, useSelector } from "react-redux";
import CartTableActions from "./CartTableActions";
import { fetchCartByUserId } from "../../entities/cart/api/cartApi";
import {
  Box,
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@mui/material";
import * as yup from "yup";
import { createOrder } from "../../entities/order/api/orderApi";
import { fetchAllUsers } from "../../entities/user/api/userApi";

const validationSchema = yup.object({
  reasonForIssue: yup.string().required("Причина выдачи обязательна"),
});

const columns = [
  {
    field: "№",
    headerName: "№",
    renderCell: (params) =>
      params.api.getRowIndexRelativeToVisibleRows(params.id) + 1,
    flex: 1,
  },
  {
    field: "catalogName",
    headerName: "Категория",
    valueGetter: (value, row) => row.catalogName,
    flex: 1,
  },
  {
    field: "productName",
    valueGetter: (value, row) => row.productName,
    headerName: "Наименование",
    flex: 1,
  },
  {
    field: "quantity",
    valueGetter: (value, row) => row.quantity,
    headerName: "Количество",
    align: "center",
    flex: 1,
  },
  {
    field: "actions",
    type: "actions",
    headerName: "Действия",
    cellClassName: "actions",
    flex: 1,
    renderCell: (params) => <CartTableActions product={params.row} />,
    align: "center",
  },
];

export default function CartTable() {
  const dispatch = useDispatch();
  const [reasonForIssue, setReasonForIssue] = useState("");
  const [error, setError] = useState(null);
  const cart = useSelector((state) => state.cart.cart);
  const { user } = useSelector((state) => state.login);
  const users = useSelector((state) => state.user.users);
  const [selectedUserId, setSelectedUserId] = useState(null);

  useEffect(() => {
    dispatch(fetchCartByUserId(user?.id ?? 0));
    if ([2].some((num) => user.roles.includes(num))) {
      dispatch(fetchAllUsers());
    }
  }, [dispatch, user]);

  useEffect(() => {
    if ([2].some((num) => user.roles.includes(num)) && users) {
      setSelectedUserId(users[0]?.id || "")
    }
  }, [user.roles, users]);

  const handleSendOrder = async () => {
    try {
      await validationSchema.validate({ reasonForIssue });

      const orderItems = cart?.items?.map((item) => ({
        productId: item.productId,
        quantity: item.quantity,
      }));
      const orderData = {
        customerId: user.id,
        items: orderItems,
        reasonForIssue: reasonForIssue.trim(),
      };
      if ([2].some((num) => user.roles.includes(num)) && users) {
        orderData.customerId = selectedUserId
      }
      await dispatch(createOrder(orderData));
      setReasonForIssue("");
    } catch (err) {
      if (err.name === "ValidationError") {
        setError(err.errors[0]);
      } else {
        console.error("Ошибка при отправке заявки:", err);
      }
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
        <Box display="flex" p={2}>
          <Box width="100%">
            <Box display="flex" justifyContent="end">
              <Button
                variant="contained"
                onClick={handleSendOrder}
                disabled={!cart?.items?.length}
              >
                Отправить заявку
              </Button>
            </Box>
          </Box>
        </Box>
      </Grid>
      {[2].some((num) => user.roles.includes(num)) && (
        <Grid size={12} m={2}>
          <FormControl fullWidth style={{ overflow: "visible" }}>
            <InputLabel id="user-select-label">
              Выберите пользователя
            </InputLabel>
            <Select
              labelId="user-select-label"
              value={selectedUserId || ""}
              onChange={(e) => setSelectedUserId(e.target.value)}
              fullWidth
              MenuProps={{
                PaperProps: {
                  style: {
                    maxHeight: 48 * 4.5,
                    width: "auto",
                  },
                },
              }}
            >
              {users?.map((u) => (
                <MenuItem
                  key={u.id}
                  value={u.id}
                  style={{ whiteSpace: "normal" }}
                >
                  {u.profile?.fullname} ({u.username})
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
      )}
      <Grid size={12} m={2}>
        <TextField
          label="Введите причину выдачи"
          variant="outlined"
          fullWidth
          margin="dense"
          rows={2}
          multiline
          value={reasonForIssue}
          onChange={(e) => setReasonForIssue(e.target.value)}
          error={!!error}
          helperText={error}
        />
      </Grid>
      <Grid size={12} justifyContent="center">
        <DataGrid
          autoHeight
          rows={cart?.items}
          getEstimatedRowHeight={() => 100}
          getRowHeight={() => "auto"}
          columns={columns}
          getRowId={(row) => row.id}
          initialState={{
            pagination: { paginationModel: { pageSize: 5 } },
          }}
          pageSizeOptions={[5, 10, 25]}
          localeText={ruRU.components.MuiDataGrid.defaultProps.localeText}
          disableRowSelectionOnClick
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
      </Grid>
    </Grid>
  );
}

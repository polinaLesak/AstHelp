import React, { useEffect } from "react";
import Grid from "@mui/material/Grid2";
import { DataGrid } from "@mui/x-data-grid";
import { ruRU } from "@mui/x-data-grid/locales";
import { Typography } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { fetchAllUsers } from "../../../entities/user/api/userApi";
import RoleChips from "../RoleChips";
import ActivationButton from "../ActivationButton";
import UsersTableActions from "./UsersTableActions";

const columns = [
  { field: "id", headerName: "#ID", flex: 1 },
  { field: "username", headerName: "Логин", flex: 1 },
  {
    field: "profile.fullname",
    valueGetter: (value, row) => row.profile.fullname,
    headerName: "ФИО",
    flex: 1,
  },
  {
    field: "profile.position",
    valueGetter: (value, row) => row.profile.position,
    headerName: "Должность",
    flex: 1,
  },
  {
    field: "userRoles",
    renderCell: (params) => <RoleChips roles={params.row.userRoles} />,
    valueGetter: (value, row) => row.userRoles.map(role => role.role.name).join(", "),
    sortComparator: (v1, v2) => {
      const roles1 = v1.split(", ");
      const roles2 = v2.split(", ");
      return roles1.length === roles2.length 
        ? v1.localeCompare(v2)
        : roles1.length - roles2.length;
    },
    headerName: "Роль",
    align: "center",
    flex: 1,
  },
  {
    field: "isActive",
    headerName: "Активация аккаунта",
    renderCell: (params) => (
      <ActivationButton isActive={params.row.isActive} userId={params.row.id} />
    ),
    filterable: false,
    align: "center",
    flex: 1,
  },
  {
    field: "actions",
    type: 'actions',
    headerName: "Действия",
    cellClassName: 'actions',
    flex: 1,
    renderCell: (params) => (
      <UsersTableActions
        user={params.row}
      />
    ),
    align: "center",
  },
];

export default function UsersTable() {
  const dispatch = useDispatch();
  const users = useSelector((state) => state.user.users);

  useEffect(() => {
    dispatch(fetchAllUsers())
      .unwrap()
      .catch((error) => console.error("Error fetching users:", error));
  }, [dispatch]);

  return (
    <Grid
      container
      justifyContent="center"
      alignItems="top"
      sx={{
        m: 2,
      }}
    >
      <Grid size={12} justifyContent="center">
        <Typography variant="h5" align="center">
          Пользователи системы AstHelp
        </Typography>
        <DataGrid
          rows={users}
          columns={columns}
          getRowId={(row) => row.id}
          initialState={{
            pagination: { paginationModel: { pageSize: 5 } },
          }}
          pageSizeOptions={[5, 10, 25]}
          localeText={ruRU.components.MuiDataGrid.defaultProps.localeText}
          disableRowSelectionOnClick
        />
      </Grid>
    </Grid>
  );
}

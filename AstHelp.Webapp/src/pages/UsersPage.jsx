import React from 'react';
import Grid from "@mui/material/Grid2";
import UsersTable from '../features/users/table/UsersTable';

const UsersPage = () => {
  return (
    <Grid
      container
    >
      <Grid size={12} sx={{mb:10}}>
        <UsersTable />
      </Grid>
    </Grid>
  );
};

export default UsersPage;
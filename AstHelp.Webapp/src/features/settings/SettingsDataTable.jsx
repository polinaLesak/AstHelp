import React from "react";
import { DataGrid } from "@mui/x-data-grid";
import { ruRU } from "@mui/x-data-grid/locales";

const SettingsDataTable = ({ rows, columns }) => {
  return (
    <DataGrid
      autoHeight
      rows={rows}
      getEstimatedRowHeight={() => 100}
      getRowHeight={() => 'auto'}
      columns={columns}
      getRowId={(row) => row.id}
      initialState={{
        pagination: { paginationModel: { pageSize: 5 } },
      }}
      pageSizeOptions={[5, 10, 25]}
      localeText={ruRU.components.MuiDataGrid.defaultProps.localeText}
      disableRowSelectionOnClick
      sx={{
        '&.MuiDataGrid-root--densityCompact .MuiDataGrid-cell': {
          py: 1,
        },
        '&.MuiDataGrid-root--densityStandard .MuiDataGrid-cell': {
          py: '15px',
        },
        '&.MuiDataGrid-root--densityComfortable .MuiDataGrid-cell': {
          py: '22px',
        },
      }}
    />
  );
};

export default SettingsDataTable;

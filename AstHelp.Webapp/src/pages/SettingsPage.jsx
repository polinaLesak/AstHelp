import React, { useEffect, useState } from "react";
import Grid from "@mui/material/Grid2";
import {
  Box,
  Button,
  Chip,
  IconButton,
  Tooltip,
  Typography,
} from "@mui/material";
import { Add, Delete, Edit } from "@mui/icons-material";
import { useDispatch, useSelector } from "react-redux";
import {
  deleteCatalog,
  fetchAllCatalogs,
} from "../entities/catalog/api/catalogApi";
import { deleteBrand, fetchAllBrands } from "../entities/brand/api/brandApi";
import {
  deleteAttribute,
  fetchAllAttributes,
} from "../entities/attribute/api/attributeApi";
import SettingsDataTable from "../features/settings/SettingsDataTable";
import SettingsTabs from "../features/settings/SettingsTabs";
import AttributeModal from "../features/settings/modal/AttributeModal";
import CatalogModal from "../features/settings/modal/CatalogModal";
import BrandModal from "../features/settings/modal/BrandModal";
import DeleteConfirmationModal from "../shared/components/DeleteConfirmationModal";

const SettingsPage = () => {
  const [selectedTab, setSelectedTab] = useState(0);
  const [isModalOpen, setModalOpen] = useState(false);
  const [isDeleteModalOpen, setDeleteModalOpen] = useState(false);
  const [editData, setEditData] = useState(null);
  const [deleteData, setDeleteData] = useState(null);
  const dispatch = useDispatch();
  const catalogs = useSelector((state) => state.catalog.catalogs);
  const brands = useSelector((state) => state.brand.brands);
  const attributes = useSelector((state) => state.attribute.attributes);

  useEffect(() => {
    if (selectedTab === 0) dispatch(fetchAllCatalogs());
    if (selectedTab === 1) dispatch(fetchAllBrands());
    if (selectedTab === 2) dispatch(fetchAllAttributes());
  }, [selectedTab, dispatch]);

  const handleTabChange = (event, newValue) => setSelectedTab(newValue);

  const handleOpenDelete = (id) => {
    setDeleteData(id);
    setDeleteModalOpen(true);
  };
  const handleCloseDelete = () => setDeleteModalOpen(false);

  const handleAdd = () => {
    setEditData(null);
    setModalOpen(true);
  };

  const handleEdit = (row) => {
    setEditData(row);
    setModalOpen(true);
  };

  const handleDelete = async () => {
    try {
      switch (selectedTab) {
        case 0: {
          await dispatch(deleteCatalog(deleteData)).unwrap();
          dispatch(fetchAllCatalogs());
          break;
        }
        case 1: {
          await dispatch(deleteBrand(deleteData)).unwrap();
          dispatch(fetchAllBrands());
          break;
        }
        case 2: {
          await dispatch(deleteAttribute(deleteData)).unwrap();
          dispatch(fetchAllAttributes());
          break;
        }
        default:
          break;
      }
    } catch (error) {
      console.error("Ошибка при удалении:", error);
    }
    handleCloseDelete(false);
  };

  const renderModal = () => {
    switch (selectedTab) {
      case 0:
        return (
          <CatalogModal
            open={isModalOpen}
            onClose={() => setModalOpen(false)}
            catalog={editData}
          />
        );
      case 1:
        return (
          <BrandModal
            open={isModalOpen}
            onClose={() => setModalOpen(false)}
            brand={editData}
          />
        );
      case 2:
        return (
          <AttributeModal
            open={isModalOpen}
            onClose={() => setModalOpen(false)}
            attribute={editData}
          />
        );
      default:
        return null;
    }
  };

  const renderTableData = () => {
    const data = (() => {
      switch (selectedTab) {
        case 0:
          return catalogs;
        case 1:
          return brands;
        case 2:
          return attributes;
        default:
          return [];
      }
    })();

    const actionColumn = {
      field: "actions",
      type: "actions",
      headerName: "Действия",
      cellClassName: "actions",
      flex: 1,
      renderCell: (params) => (
        <>
          <Tooltip title="Редактировать">
            <IconButton
              onClick={() => handleEdit(params.row)}
              color="primary"
              size="small"
            >
              <Edit />
            </IconButton>
          </Tooltip>
          <Tooltip title="Удалить">
            <IconButton
              onClick={() => handleOpenDelete(params.row.id)}
              color="error"
              size="small"
            >
              <Delete />
            </IconButton>
          </Tooltip>
        </>
      ),
      align: "center",
    };

    const columns = (() => {
      switch (selectedTab) {
        case 0:
          return [
            { field: "id", headerName: "#ID", flex: 1 },
            { field: "name", headerName: "Наименование", flex: 1 },
            {
              field: "catalogAttributes",
              headerName: "Атрибуты",
              flex: 2,
              valueGetter: (value, row) => {
                return row.catalogAttributes
                  ?.map((attribute) => attribute.attribute.name)
                  .join(', ') || "";
              },
              renderCell: (params) => (
                <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                  {params.row.catalogAttributes.length > 0 ? (
                    params.row.catalogAttributes.map(
                      (catalogAttribute, index) => (
                        <Tooltip
                          key={`${catalogAttribute.attribute.id}-${index}`}
                          title={catalogAttribute.attribute.name}
                        >
                          <Chip label={catalogAttribute.attribute.name} />
                        </Tooltip>
                      )
                    )
                  ) : (
                    <Typography variant="body2" color="textSecondary">
                      Нет атрибутов
                    </Typography>
                  )}
                </Box>
              ),
            },
            actionColumn,
          ];
        case 1:
          return [
            { field: "id", headerName: "#ID", flex: 1 },
            { field: "name", headerName: "Наименование", flex: 1 },
            actionColumn,
          ];
        case 2:
          return [
            { field: "id", headerName: "#ID", flex: 1 },
            { field: "name", headerName: "Наименование", flex: 1 },
            {
              field: "attributeType.name",
              headerName: "Тип данных",
              valueGetter: (value, row) => row.attributeType.name,
              flex: 1,
            },
            actionColumn,
          ];
        default:
          return [];
      }
    })();
    return <SettingsDataTable rows={data} columns={columns} />;
  };

  return (
    <Grid container>
      <Grid size={12} sx={{ mb: 10 }}>
        <Box display="flex" p={2}>
          <SettingsTabs
            selectedTab={selectedTab}
            handleTabChange={handleTabChange}
          />
          <Box width="90%">
            <Box display="flex" justifyContent="space-between" mb={2}>
              <Typography variant="h6">
                {selectedTab === 0
                  ? "Каталог"
                  : selectedTab === 1
                  ? "Бренд"
                  : "Атрибуты"}
              </Typography>
              <Button
                variant="contained"
                startIcon={<Add />}
                onClick={handleAdd}
              >
                Добавить
              </Button>
            </Box>
            {renderTableData()}
          </Box>
        </Box>
        {renderModal()}
        <DeleteConfirmationModal
          open={isDeleteModalOpen}
          onClose={handleCloseDelete}
          onConfirm={handleDelete}
          message="Вы уверены, что хотите удалить выбранные данные?"
        />
      </Grid>
    </Grid>
  );
};

export default SettingsPage;

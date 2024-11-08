import React, { useEffect, useState } from "react";
import Grid from "@mui/material/Grid2";
import { Box, Button, TextField } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import ProductCard from "../features/product/ProductCard";
import CUProductModal from "../features/product/modal/CUProductModal";
import {
  deleteProduct,
  fetchAllProducts,
} from "../entities/product/api/productApi";
import DeleteConfirmationModal from "../shared/components/DeleteConfirmationModal";
import { fetchAllCatalogs } from "../entities/catalog/api/catalogApi";

export default function ProductPage() {
  const dispatch = useDispatch();

  const [searchText, setSearchText] = useState("");
  const [selectedCategory, setSelectedCategory] = useState("");

  const catalogs = useSelector((state) => state.catalog.catalogs);
  const { products } = useSelector((state) => state.product);
  const [filteredProducts, setFilteredProducts] = useState([]);
  const [openModal, setOpenModal] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState(null);

  const [isDeleteModalOpen, setDeleteModalOpen] = useState(false);
  const [deleteData, setDeleteData] = useState(null);

  useEffect(() => {
    dispatch(fetchAllProducts());
    dispatch(fetchAllCatalogs());
  }, [dispatch]);

  useEffect(() => {
    const result = products?.filter(
      (p) =>
        p.name.toLowerCase().includes(searchText.toLowerCase()) &&
        (!selectedCategory || p.catalogId === selectedCategory)
    );
    setFilteredProducts(result)    
  }, [products, searchText, selectedCategory]);

  const handleAddClick = () => {
    setSelectedProduct(null);
    setOpenModal(true);
  };

  const handleEditClick = (product) => {
    setSelectedProduct(product);
    setOpenModal(true);
  };

  const handleDeleteClick = (id) => {
    setDeleteData(id);
    setDeleteModalOpen(true);
  };

  const handleCloseDelete = () => setDeleteModalOpen(false);

  const handleDelete = async () => {
    try {
      await dispatch(deleteProduct(deleteData)).unwrap();
      dispatch(fetchAllProducts());
    } catch (error) {
      console.error("Ошибка при удалении:", error);
    }
    handleCloseDelete(false);
  };

  return (
    <Grid container>
      <Grid size={12} sx={{ mb: 10 }}>
        <Box display="flex" p={2}>
          <Box width="100%">
            <Grid container ml={4} mr={4} spacing={2}>
              <Grid size={10}>
                <TextField
                  label="Поиск оборудования"
                  variant="outlined"
                  fullWidth
                  margin="dense"
                  size="small"
                  value={searchText}
                  onChange={(e) => setSearchText(e.target.value)}
                />
              </Grid>
              <Grid size={2} mt={1}>
                <Button variant="contained" onClick={handleAddClick}>
                  Добавить
                </Button>
              </Grid>
              <Grid mb={2}>
                <Button
                  variant={selectedCategory === "" ? "contained" : "outlined"}
                  onClick={() => setSelectedCategory("")}
                >
                  Все категории
                </Button>
                {catalogs?.map((catalog) => (
                  <Button
                    sx={{ ml: 2}}
                    key={catalog.id}
                    variant={
                      selectedCategory === catalog.id ? "contained" : "outlined"
                    }
                    onClick={() => setSelectedCategory(catalog.id)}
                  >
                    {catalog.name}
                  </Button>
                ))}
              </Grid>
            </Grid>
            <Box display="flex" justifyContent="center" mb={2} ml={4} mr={4}>
              <Grid size={12} container spacing={2}>
                {filteredProducts?.map((product) => (
                  <Grid size={12} key={product.id}>
                    <ProductCard
                      product={product}
                      onEdit={() => handleEditClick(product)}
                      onDelete={() => handleDeleteClick(product.id)}
                    />
                  </Grid>
                ))}
              </Grid>
            </Box>
            <CUProductModal
              open={openModal}
              onClose={() => setOpenModal(false)}
              product={selectedProduct}
            />
            <DeleteConfirmationModal
              open={isDeleteModalOpen}
              onClose={handleCloseDelete}
              onConfirm={handleDelete}
              message="Вы уверены, что хотите удалить выбранные данные?"
            />
          </Box>
        </Box>
      </Grid>
    </Grid>
  );
}

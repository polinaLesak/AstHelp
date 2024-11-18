import React, { useEffect, useState } from "react";
import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { setSortState } from "../../entities/product/model/productSlice";
import { fetchAllProducts } from "../../entities/product/api/productApi";

const ProductSortSelect = () => {
  const dispatch = useDispatch();
  const { sortOptions, sortState } = useSelector((state) => state.product);
  const [selectedOption, setSelectedOption] = useState(sortState);

  useEffect(() => {
    dispatch(setSortState(selectedOption));
  }, [dispatch, selectedOption]);

  const handleSortChange = async (event) => {
    setSelectedOption(event.target.value);
    await dispatch(fetchAllProducts(sortState));
  };

  return (
    <FormControl fullWidth margin="normal" size="small">
      <InputLabel id="sort-label">Сортировка</InputLabel>
      <Select
        labelId="sort-label"
        value={selectedOption}
        onChange={handleSortChange}
        label="Сортировка"
        displayEmpty
        renderValue={(selected) =>
          selected ? selected.label : "Выберите сортировку"
        }
        sx={{ height: 36 }}
      >
        {sortOptions.map((option, index) => (
          <MenuItem key={index} value={option}>
            {option.label}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
};

export default ProductSortSelect;

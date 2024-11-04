import React from "react";
import { Box, Tab, Tabs } from "@mui/material";

const SettingsTabs = ({ selectedTab, handleTabChange }) => {
  return (
    <Box width="10%" mr={2}>
      <Tabs
        orientation="vertical"
        variant="scrollable"
        value={selectedTab}
        onChange={handleTabChange}
      >
        <Tab label="Каталог" />
        <Tab label="Бренд" />
        <Tab label="Атрибут" />
      </Tabs>
    </Box>
  );
};

export default SettingsTabs;

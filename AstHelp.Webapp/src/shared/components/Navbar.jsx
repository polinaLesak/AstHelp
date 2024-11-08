import React, { useEffect } from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import logoImg from "../../assets/logo.svg";
import PersonIcon from "@mui/icons-material/Person";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import NotificationsIcon from "@mui/icons-material/Notifications";
import Badge from "@mui/material/Badge";
import { Divider } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "../../features/auth/model/loginSlice";
import { useNavigate } from "react-router-dom";
import { Link as RouterLink } from "react-router-dom";
import { fetchCartProductsCountByUserId } from "../../entities/cart/api/cartApi";

const pages = [
  { name: "Каталог", href: "/catalog", roles: [1, 2, 3] },
  { name: "Мои заказы", href: "/my-orders", roles: [1, 2, 3] },
  { name: "Заявки", href: "/orders", roles: [1, 2] },
  { name: "Пользователи", href: "/users", roles: [1] },
  { name: "Настройки", href: "/settings", roles: [1] },
];

function Navbar() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { user, isAuthenticated } = useSelector((state) => state.login);
  const cartItemCount = useSelector((state) => state.cart.countProducts);
  const [anchorElNav, setAnchorElNav] = React.useState(null);
  const [anchorElUser, setAnchorElUser] = React.useState(null);

  useEffect(() => {
    if (user && isAuthenticated) dispatch(fetchCartProductsCountByUserId(user.id));
  }, [dispatch, isAuthenticated, user]);

  const handleOpenNavMenu = (event) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  const handleLogout = () => {
    dispatch(logout());
    navigate("/");
  };

  return (
    <AppBar sx={{ p: 0, bottom: "auto", top: 0 }}>
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <RouterLink to="/">
            <Box
              component="img"
              src={logoImg}
              sx={{
                display: { xs: "none", md: "flex" },
                height: "50px",
                width: "50px",
                mr: 2,
              }}
            />
          </RouterLink>
          <Typography
            variant="h6"
            noWrap
            component={RouterLink}
            to="/"
            sx={{
              mr: 2,
              display: { xs: "none", md: "flex" },
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".1rem",
              color: "inherit",
              textDecoration: "none",
            }}
          >
            AstHelp
          </Typography>
          <Divider
            orientation="vertical"
            flexItem
            sx={{ color: "white", display: { xs: "none", md: "flex" } }}
          />

          <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "left",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "left",
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{ display: { xs: "block", md: "none" } }}
            >
              {pages
                .filter(
                  (page) =>
                    user != null &&
                    page.roles.some((num) => user.roles.includes(num))
                )
                .map((page) => {
                  return (
                    <MenuItem key={page.name} onClick={handleCloseNavMenu}>
                      <Typography
                        sx={{
                          textAlign: "center",
                          color: "inherit",
                          textDecoration: "none",
                        }}
                        component={RouterLink}
                        to={page.href}
                      >
                        {page.name}
                      </Typography>
                    </MenuItem>
                  );
                })}
            </Menu>
          </Box>
          <RouterLink to="/">
            <Box
              component="img"
              src={logoImg}
              sx={{
                flexGrow: 1,
                display: { xs: "flex", md: "none" },
                height: "50px",
                width: "50px",
                mr: 2,
              }}
            />
          </RouterLink>
          <Typography
            variant="h5"
            noWrap
            component={RouterLink}
            to="/"
            sx={{
              mr: 2,
              display: { xs: "flex", md: "none" },
              flexGrow: 1,
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".1rem",
              color: "inherit",
              textDecoration: "none",
            }}
          >
            AstHelp
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
            {pages
              .filter(
                (page) =>
                  user != null &&
                  page.roles.some((num) => user.roles.includes(num))
              )
              .map((page) => {
                return (
                  <Button
                    key={page.name}
                    component={RouterLink}
                    to={page.href}
                    onClick={handleCloseNavMenu}
                    sx={{ my: 2, color: "white", display: "block" }}
                  >
                    {page.name}
                  </Button>
                );
              })}
          </Box>
          {/* Иконка уведомлений */}
          {/* <IconButton
            component={RouterLink}
            to="/notifications"
            sx={{ color: "inherit", ml: 1 }}
          >
            <Badge badgeContent={notificationCount} color="error">
              <NotificationsIcon />
            </Badge>
          </IconButton> */}
          {isAuthenticated ? (
            <>
              <IconButton
                component={RouterLink}
                to="/cart"
                sx={{ color: "inherit", mr: 3 }}
              >
                <Badge badgeContent={cartItemCount} color="error">
                  <ShoppingCartIcon />
                </Badge>
              </IconButton>
              <Box sx={{ flexGrow: 0 }}>
                <Typography
                  sx={{
                    textAlign: "center",
                    color: "inherit",
                    textDecoration: "none",
                  }}
                  component="a"
                >
                  {user.fullname}
                </Typography>
                <Tooltip title="Открыть настройки">
                  <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                    <PersonIcon fontSize="large" sx={{ color: "#FFF" }} />
                  </IconButton>
                </Tooltip>
                <Menu
                  sx={{ mt: "45px" }}
                  id="menu-appbar"
                  anchorEl={anchorElUser}
                  anchorOrigin={{
                    vertical: "top",
                    horizontal: "right",
                  }}
                  keepMounted
                  transformOrigin={{
                    vertical: "top",
                    horizontal: "right",
                  }}
                  open={Boolean(anchorElUser)}
                  onClose={handleCloseUserMenu}
                >
                  <MenuItem onClick={handleCloseUserMenu}>
                    <Typography
                      sx={{
                        textAlign: "center",
                        color: "inherit",
                        textDecoration: "none",
                      }}
                      component={RouterLink}
                      to="/profile"
                    >
                      Профиль
                    </Typography>
                  </MenuItem>
                  <MenuItem onClick={handleLogout}>
                    <Typography
                      sx={{
                        textAlign: "center",
                        color: "inherit",
                        textDecoration: "none",
                      }}
                      component="a"
                    >
                      Выйти
                    </Typography>
                  </MenuItem>
                </Menu>
              </Box>
            </>
          ) : (
            <></>
          )}
        </Toolbar>
      </Container>
    </AppBar>
  );
}
export default Navbar;

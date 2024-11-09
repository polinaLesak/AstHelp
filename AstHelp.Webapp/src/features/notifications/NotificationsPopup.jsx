import React, { useEffect, useState } from "react";
import {
  IconButton,
  Badge,
  Popover,
  List,
  ListItem,
  ListItemText,
  Divider,
  Button,
  Typography,
  Box,
  Tooltip,
  ListItemIcon,
} from "@mui/material";
import NotificationsIcon from "@mui/icons-material/Notifications";
import MarkChatReadIcon from "@mui/icons-material/MarkChatRead";
import Delete from "@mui/icons-material/Delete";
import { useDispatch, useSelector } from "react-redux";
import {
  fetchAllNotificationsByUserId,
  markNotificationAsReadById,
  deleteNotificationById,
  markAllNotificationsAsReadByUserId,
  deleteAllNotificationsByUserId,
} from "../../entities/notifications/api/notificationsApi";
import { CheckCircle, ErrorOutline, Info, Warning } from "@mui/icons-material";
import { blue, green, red, yellow } from "@mui/material/colors";

const getNotificationIcon = (type) => {
  switch (type) {
    case 1:
      return <CheckCircle sx={{ color: green[500] }} />;
    case 2:
      return <Warning sx={{ color: yellow[700] }} />;
    case 3:
      return <ErrorOutline sx={{ color: red[500] }} />;
    case 4:
      return <Info sx={{ color: blue[500] }} />;
    default:
      return null;
  }
};

const getNotificationStyle = (type) => {
  switch (type) {
    case 1:
      return { backgroundColor: green[50], color: green[800] };
    case 2:
      return { backgroundColor: yellow[100], color: yellow[800] };
    case 3:
      return { backgroundColor: red[50], color: red[800] };
    case 4:
      return { backgroundColor: blue[50], color: blue[800] };
    default:
      return {};
  }
};

export default function NotificationsPopup() {
  const dispatch = useDispatch();
  const { user } = useSelector((state) => state.login);
  const notifications = useSelector(
    (state) => state.notifications.notifications
  );
  const unreadCount = notifications?.filter((n) => !n.isRead).length;

  const [anchorEl, setAnchorEl] = useState(null);

  const handleOpen = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  useEffect(() => {
    if (user) dispatch(fetchAllNotificationsByUserId(user?.id ?? 0));
  }, [dispatch, anchorEl, user]);

  useEffect(() => {
    if (user && anchorEl)
      dispatch(fetchAllNotificationsByUserId(user?.id ?? 0));
  }, [dispatch, anchorEl, user]);

  const open = Boolean(anchorEl);
  const id = open ? "notifications-popover" : undefined;

  const handleMarkAsRead = async (id) => {
    await dispatch(markNotificationAsReadById(id));
  };

  const handleDelete = async (id) => {
    await dispatch(deleteNotificationById(id));
  };

  const handleMarkAllAsRead = async () => {
    await dispatch(markAllNotificationsAsReadByUserId(user?.id ?? 0));
  };

  const handleDeleteAll = async () => {
    await dispatch(deleteAllNotificationsByUserId(user?.id ?? 0));
  };

  return (
    <>
      <IconButton onClick={handleOpen} sx={{ color: "inherit", mr: 3 }}>
        <Badge badgeContent={unreadCount} color="error">
          <NotificationsIcon />
        </Badge>
      </IconButton>
      <Popover
        id={id}
        open={open}
        anchorEl={anchorEl}
        onClose={handleClose}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
        transformOrigin={{ vertical: "top", horizontal: "center" }}
      >
        <Box sx={{ p: 2, minWidth: 300 }}>
          <Typography variant="h6">Уведомления</Typography>
          <Divider sx={{ my: 1 }} />
          <List>
            {notifications?.length === 0 ? (
              <Typography sx={{ p: 2 }}>Нет уведомлений</Typography>
            ) : (
              notifications?.map((notification) => (
                <ListItem
                  key={notification.id}
                  secondaryAction={
                    <>
                      <Tooltip title="Прочитать">
                        <IconButton
                          onClick={() => handleMarkAsRead(notification.id)}
                          color="success"
                          size="small"
                        >
                          <MarkChatReadIcon />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Удалить">
                        <IconButton
                          onClick={() => handleDelete(notification.id)}
                          color="error"
                          size="small"
                        >
                          <Delete />
                        </IconButton>
                      </Tooltip>
                    </>
                  }
                  sx={getNotificationStyle(notification.type)}
                >
                  <ListItemIcon>
                    {getNotificationIcon(notification.type)}{" "}
                  </ListItemIcon>
                  <ListItemText
                    primary={notification.title}
                    secondary={
                      <>
                        <Typography variant="body2">
                          {notification.message}
                        </Typography>
                        <Typography variant="caption" color="textSecondary">
                          {notification.isRead ? "Прочитано" : "Не прочитано"}
                        </Typography>
                      </>
                    }
                  />
                </ListItem>
              ))
            )}
          </List>
          {notifications?.length > 0 && (
            <Box
              sx={{ display: "flex", justifyContent: "space-between", mt: 2 }}
            >
              <Button onClick={handleMarkAllAsRead}>Прочитать все</Button>
              <Button onClick={handleDeleteAll}>Удалить все</Button>
            </Box>
          )}
        </Box>
      </Popover>
    </>
  );
}

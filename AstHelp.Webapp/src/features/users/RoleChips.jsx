import { Chip } from "@mui/material";

const getRoleColor = (roleId) => {
  switch (roleId) {
    case 1:
      return "success";
    case 2:
      return "primary";
    case 3:
      return "secondary";
    default:
      return "default";
  }
};

const RoleChips = ({ roles }) => {
  return (
    <div>
      {roles.map((role) => (
        <Chip
          key={role.roleId}
          label={role.role.name || `Роль ${role.roleId}`}
          color={getRoleColor(role.roleId)}
          variant="outlined"
          sx={{ mr: 0.5 }}
        />
      ))}
    </div>
  );
};

export default RoleChips;
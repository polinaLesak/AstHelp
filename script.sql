CREATE TABLE "Role" (
  "role_id" uuid PRIMARY KEY,
  "role_name" varchar UNIQUE
);

CREATE TABLE "User" (
  "user_id" uuid PRIMARY KEY,
  "login" varchar UNIQUE,
  "password_hash" varchar,
  "role_id" uuid,
  "manager_id" uuid
);

CREATE TABLE "Equipment" (
  "equipment_id" uuid PRIMARY KEY,
  "equipment_name" varchar,
  "type" varchar,
  "serial_number" varchar UNIQUE,
  "purchase_date" date,
  "warranty_expiration" date,
  "stock_quantity" integer,
  "availability_status_id" uuid
);

CREATE TABLE "AvailabilityStatus" (
  "availability_status_id" uuid PRIMARY KEY,
  "status_name" varchar UNIQUE
);

CREATE TABLE "RequestStatus" (
  "status_id" uuid PRIMARY KEY,
  "status_name" varchar UNIQUE
);

CREATE TABLE "Request" (
  "request_id" uuid PRIMARY KEY,
  "request_date" date,
  "status_id" uuid,
  "justification" text,
  "reason" varchar,
  "employee_id" uuid,
  "manager_id" uuid,
  "approval_date" date
);

CREATE TABLE "OrderStatus" (
  "order_status_id" uuid PRIMARY KEY,
  "status_name" varchar UNIQUE
);

CREATE TABLE "Order" (
  "order_id" uuid PRIMARY KEY,
  "sys_admin_id" uuid,
  "request_id" uuid,
  "order_status_id" uuid,
  "creation_date" date,
  "completion_date" date
);

CREATE TABLE "ActType" (
  "act_type_id" uuid PRIMARY KEY,
  "type_name" varchar UNIQUE
);

CREATE TABLE "Act" (
  "act_id" uuid PRIMARY KEY,
  "order_id" uuid,
  "act_date" date,
  "act_type_id" uuid,
  "document" text
);

CREATE TABLE "PackingTaskStatus" (
  "task_status_id" uuid PRIMARY KEY,
  "status_name" varchar UNIQUE
);

CREATE TABLE "PackingTask" (
  "task_id" uuid PRIMARY KEY,
  "task_status_id" uuid,
  "created_at" timestamp DEFAULT (CURRENT_TIMESTAMP),
  "completed_at" timestamp,
  "order_id" uuid
);

CREATE TABLE "TaskEquipment" (
  "task_equipment_id" uuid PRIMARY KEY,
  "quantity" integer,
  "equipment_id" uuid,
  "task_id" uuid
);

CREATE TABLE "InventoryActionType" (
  "action_type_id" uuid PRIMARY KEY,
  "action_name" varchar UNIQUE
);

CREATE TABLE "InventoryLog" (
  "log_id" uuid PRIMARY KEY,
  "equipment_id" uuid,
  "action_type_id" uuid,
  "action_date" timestamp DEFAULT (CURRENT_TIMESTAMP),
  "quantity_changed" integer,
  "user_id" uuid,
  "notes" text
);

COMMENT ON COLUMN "Equipment"."stock_quantity" IS 'Cannot be less than 0';

COMMENT ON COLUMN "TaskEquipment"."quantity" IS 'Cannot be less than 1';

ALTER TABLE "User" ADD FOREIGN KEY ("role_id") REFERENCES "Role" ("role_id");

ALTER TABLE "User" ADD FOREIGN KEY ("manager_id") REFERENCES "User" ("user_id");

ALTER TABLE "Equipment" ADD FOREIGN KEY ("availability_status_id") REFERENCES "AvailabilityStatus" ("availability_status_id");

ALTER TABLE "Request" ADD FOREIGN KEY ("status_id") REFERENCES "RequestStatus" ("status_id");

ALTER TABLE "Request" ADD FOREIGN KEY ("employee_id") REFERENCES "User" ("user_id");

ALTER TABLE "Request" ADD FOREIGN KEY ("manager_id") REFERENCES "User" ("user_id");

ALTER TABLE "Order" ADD FOREIGN KEY ("request_id") REFERENCES "Request" ("request_id");

ALTER TABLE "Order" ADD FOREIGN KEY ("sys_admin_id") REFERENCES "User" ("user_id");

ALTER TABLE "Order" ADD FOREIGN KEY ("order_status_id") REFERENCES "OrderStatus" ("order_status_id");

ALTER TABLE "Act" ADD FOREIGN KEY ("order_id") REFERENCES "Order" ("order_id");

ALTER TABLE "Act" ADD FOREIGN KEY ("act_type_id") REFERENCES "ActType" ("act_type_id");

ALTER TABLE "PackingTask" ADD FOREIGN KEY ("task_status_id") REFERENCES "PackingTaskStatus" ("task_status_id");

ALTER TABLE "PackingTask" ADD FOREIGN KEY ("order_id") REFERENCES "Order" ("order_id");

ALTER TABLE "TaskEquipment" ADD FOREIGN KEY ("equipment_id") REFERENCES "Equipment" ("equipment_id");

ALTER TABLE "TaskEquipment" ADD FOREIGN KEY ("task_id") REFERENCES "PackingTask" ("task_id");

ALTER TABLE "InventoryLog" ADD FOREIGN KEY ("equipment_id") REFERENCES "Equipment" ("equipment_id");

ALTER TABLE "InventoryLog" ADD FOREIGN KEY ("action_type_id") REFERENCES "InventoryActionType" ("action_type_id");

ALTER TABLE "InventoryLog" ADD FOREIGN KEY ("user_id") REFERENCES "User" ("user_id");

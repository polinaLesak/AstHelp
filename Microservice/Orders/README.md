# Add new migration 
Add-Migration InitDb -Project Microservice\Orders\Orders.Microservice.Infrastructure -OutputDir Migrations

# Apply migrations to DB
Update-Database -Project Microservice\Orders\Orders.Microservice.Infrastructure

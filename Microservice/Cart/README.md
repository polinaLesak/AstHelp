# Add new migration 
Add-Migration InitDb -Project Microservice\Cart\Cart.Microservice.Infrastructure -OutputDir Migrations

# Apply migrations to DB
Update-Database -Project Microservice\Cart\Cart.Microservice.Infrastructure

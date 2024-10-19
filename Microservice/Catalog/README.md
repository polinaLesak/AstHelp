# Add new migration 
Add-Migration InitDb -Project Microservice\Catalog\Catalog.Microservice.Infrastructure -OutputDir Migrations

# Apply migrations to DB
Update-Database -Project Microservice\Catalog\Catalog.Microservice.Infrastructure

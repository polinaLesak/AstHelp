# Add new migration 
Add-Migration InitDb -Project Microservice\Identity\Identity.Microservice.Infrastructure -OutputDir Migrations

# Apply migrations to DB
Update-Database -Project Microservice\Identity\Identity.Microservice.Infrastructure

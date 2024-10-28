# Add new migration 
Add-Migration InitDb -Project Microservice\Notification\Notification.Microservice.Infrastructure -OutputDir Migrations

# Apply migrations to DB
Update-Database -Project Microservice\Notification\Notification.Microservice.Infrastructure

# Micro messaging service

**Backend:** .NET Core  
**Frontend:** Angular  
**Database:** PostgreSQL

**Real-time communication:** SignalR  
**API Documentation:** Swagger  
**Logging:** Built-in from Microsoft

## Start project with Docker

1. Open a command prompt (terminal) under the root
2. `docker-compose up`
3. Navigate to [http://localhost:8080](http://localhost:8080)

## Start project in Development Mode

### Requirements

* .NET Core
* NodeJS

### Connect your database

1. Change connection string in *appsettings.json* file

### Start the backend

1. Open a command prompt (terminal) under the root
2. `cd Messenger.API`
3. `dotnet run` or `dotnet watch run`

Or open .sln file and run project.

### Start the frontend

1. Open a command prompt (terminal) under the root
2. `cd Messenger-SPA`
3. `npm install`
4. `npm start` or `ng serve`
5. Navigate to [http://localhost:4200](http://localhost:4200)

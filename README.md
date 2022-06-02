## Description

The JobApi test is a simple WebApi written in .NET 6 and C#10 that can create and return a sample Job/Note object. 

## Pre-Requisites

### 1. Docker Desktop + WSL2

Follow the instructions in the ```Download``` and ```Install``` sections. [Download + Install link](https://docs.docker.com/desktop/windows/wsl/#download)

The Docker documentation also explains how to ensure WSL2 is setup in your computer.

### 2. Visual Studio 2022
If you already have Visual Studio 2022 installed please make sure you're on the latest version by opening the Visual Studio Installer. 

Otherwise, use the [donwload link](https://visualstudio.microsoft.com/vs/). You can use the version of your choice (Community, Professional, Enterprise).

Note: only the ```ASP.NET and web development``` workload is required. You will see the workloads when installing or updating Visual Studio via the Visual Studio Installer.

### 3. dotnet-ef

The WebApi relies on a database, and as such we'll use [Entity Framework migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli).

For running migrations please make sure you have the latest ```dotnet-ef``` tool installed.

Open any command line and run ```dotnet tool install --global dotnet-ef```

If you already have ```dotnet-ef``` installed, you can run ```dotnet tool update --global dotnet-ef```

## Running the Solution

1. Open Docker Desktop

2. Find and open the ```JobAPITechTest.sln``` file.

3. Once Visual Studio is open, right-click on the ```docker-compose``` project and select the ```Set as Startup Project``` option. Visual Studio might automatically start pulling docker images. Let this process run.

4. You can now run the solution by hitting ```CTRL+F5``` or just ```F5```. Your browser should open the Swagger UI. Try creating a matter via the ```POST``` method, and then retrieving it via any ```GET``` method.

![Swagger UI](/Documentation/SwaggerUI.PNG?raw=true)

## Accessing the Database

When opening the JobApi Tech Test solution, Visual Studio will automatically spin up a SQL Server Docker container called ```fergus.techtest.sqlserver```. You can view this container by opening Docker Desktop.

You can access the containerized SQL Server just as you would with any local DB server.

The DB credentials are in the ```docker-compose.override.yml``` file in the root of this repository.

See below example screenshots.

### SQL Server Management Studio (SSMS)

![SQL Server Management Studio Login Screenshot](/Documentation/SQLServerManagementStudio.png?raw=true "SSMS")

### Azure Data Studio

![Azure Data Studio Login Screenshot](/Documentation/AzureDataStudio.png?raw=true "Azure Data Studio")

## Database Migrations

Database migrations migrations are automatically applied when the solution is run.

Refer to the "Running the Solution" section above.

## The API
# JobApiTest

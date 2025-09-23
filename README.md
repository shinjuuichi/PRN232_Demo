# PRN232 - Microservices Docker Tutorial

This project demonstrates a complete microservices architecture using .NET 8 with Docker containerization. The application consists of a Web API, API Gateway with Ocelot, and an MVC frontend, all orchestrated with Docker Compose.

## 🏗️ Architecture Overview

The project follows a microservices pattern with the following components:

- **SQL Server** - Database server (containerized)
- **Slot2API** - Web API service (Port: 8001)
- **ApiGateway** - API Gateway using Ocelot (Port: 8000)
- **WebMVC** - Frontend MVC application (Port: 8002)

## 📋 Prerequisites

### Required Software

1. **Docker Desktop for Windows**

   - Download from: [Docker Desktop Windows Install](https://docs.docker.com/desktop/setup/install/windows-install)
   - Ensure WSL 2 is enabled
   - Make sure Docker Desktop is running

2. **.NET 8 SDK** (for development)

   - Download from: [Microsoft .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)

3. **Visual Studio 2022** or **VS Code** (recommended for development)

## 🚀 Quick Start

### Option 1: Using Docker Compose (Recommended)

```bash
# Clone the repository
git clone https://github.com/shinjuuichi/PRN232_Demo.git
cd PRN232_Demo

# Start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop all services
docker-compose down
```

### Option 2: Manual Docker Commands

```bash
# 1. Create a custom network
docker network create microservices-network

# 2. Start SQL Server
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Shinjuuichi@11" -p 1433:1433 --name sqlserver --hostname sqlserver -v mssql_data:/var/opt/mssql --network microservices-network -d mcr.microsoft.com/mssql/server:2022-latest

# 3. Build and run Web API
docker build -t webapi:latest -f Slot2API/Dockerfile .
docker run -d -p 8001:8080 --name webapi-container --network microservices-network -e ASPNETCORE_ENVIRONMENT=Development -e "ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=DemoOk;User ID=sa;Password=Shinjuuichi@11;TrustServerCertificate=True" webapi:latest

# 4. Build and run API Gateway
docker build -t apigateway:latest -f ApiGateway/Dockerfile .
docker run -d -p 8000:8080 --name apigateway-container --network microservices-network -e ASPNETCORE_ENVIRONMENT=Development apigateway:latest

# 5. Build and run Web MVC
docker build -t webmvc:latest -f WebMVC/Dockerfile .
docker run -d -p 8002:8080 --name webmvc-container --network microservices-network -e ASPNETCORE_ENVIRONMENT=Development -e "ConnectionStrings__BaseUrl=http://apigateway:8080" webmvc:latest
```

## 🔧 Configuration Details

### Docker Compose Configuration

The project uses two main compose files:

- `docker-compose.yml` - Base configuration
- `docker-compose.override.yml` - Development overrides

### Service Ports

| Service     | Internal Port | External Port | URL                   |
| ----------- | ------------- | ------------- | --------------------- |
| SQL Server  | 1433          | 1433          | localhost:1433        |
| Web API     | 8080          | 8001          | http://localhost:8001 |
| API Gateway | 8080          | 8000          | http://localhost:8000 |
| Web MVC     | 8080          | 8002          | http://localhost:8002 |

### Environment Variables

#### SQL Server

- `ACCEPT_EULA=Y` - Accept SQL Server license
- `MSSQL_SA_PASSWORD=Shinjuuichi@11` - SA password

#### Web API (Slot2API)

- `ASPNETCORE_ENVIRONMENT=Development`
- `ConnectionStrings__DefaultConnection` - Database connection

#### API Gateway

- `ASPNETCORE_ENVIRONMENT=Development`
- Ocelot configuration in `ocelot.json`

#### Web MVC

- `ASPNETCORE_ENVIRONMENT=Development`
- `ConnectionStrings__BaseUrl` - API Gateway URL

## 📁 Project Structure

```
PRN232/
├── docker-compose.yml              # Main compose configuration
├── docker-compose.override.yml     # Development overrides
├── PRN232.sln                      # Solution file
├── README.md                       # This file
│
├── Slot2API/                       # Web API Service
│   ├── Dockerfile                  # API container definition
│   ├── Controllers/                # API controllers
│   ├── Models/                     # Data models
│   ├── Services/                   # Business logic
│   └── ...
│
├── ApiGateway/                     # API Gateway Service
│   ├── Dockerfile                  # Gateway container definition
│   ├── ocelot.json                 # Ocelot routing configuration
│   └── ...
│
├── WebMVC/                         # MVC Frontend
│   ├── Dockerfile                  # MVC container definition
│   ├── Controllers/                # MVC controllers
│   ├── Views/                      # Razor views
│   └── ...
│
└── PRN232.ServiceDefaults/         # Shared configurations
    └── ...
```

## 🐳 Docker Commands Reference

### Building Images

```bash
# Build individual services
docker build -t webapi:latest -f Slot2API/Dockerfile .
docker build -t apigateway:latest -f ApiGateway/Dockerfile .
docker build -t webmvc:latest -f WebMVC/Dockerfile .

# Build all services with docker-compose
docker-compose build
```

### Managing Containers

```bash
# Start all services
docker-compose up -d

# Start specific service
docker-compose up -d sqlserver

# View running containers
docker ps

# View all containers (including stopped)
docker ps -a

# Stop all services
docker-compose down

# Stop and remove volumes
docker-compose down -v

# Restart a service
docker-compose restart webapi
```

### Monitoring and Debugging

```bash
# View logs for all services
docker-compose logs -f

# View logs for specific service
docker-compose logs -f webapi

# Execute command in running container
docker exec -it sqlserver bash

# Inspect container
docker inspect webapi

# View container resource usage
docker stats
```

### Database Management

```bash
# Connect to SQL Server container
docker exec -it sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Shinjuuichi@11"

# Backup database
docker exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Shinjuuichi@11" -Q "BACKUP DATABASE [DemoOk] TO DISK = '/var/opt/mssql/backup/DemoOk.bak'"
```

## 🔍 Health Checks

The SQL Server service includes a health check to ensure it's ready before dependent services start:

```yaml
healthcheck:
  test: ['CMD-SHELL', "bash -c '</dev/tcp/127.0.0.1/1433'"]
  interval: 10s
  timeout: 5s
  retries: 12
```

## 🌐 API Gateway Routing

The API Gateway uses Ocelot for routing. Current configuration:

- **Upstream**: `http://localhost:8000/api/users/*`
- **Downstream**: `http://webapi:8080/api/users/*`

To add new routes, modify `ApiGateway/ocelot.json`.

## 🔧 Development Workflow

### For Development

1. **Start infrastructure services:**

   ```bash
   docker-compose up -d sqlserver
   ```

2. **Run services locally for debugging:**

   - Use Visual Studio or VS Code
   - Services will connect to containerized SQL Server

3. **Full containerized development:**
   ```bash
   docker-compose up -d
   ```

### For Production

1. **Build production images:**

   ```bash
   docker-compose -f docker-compose.yml build
   ```

2. **Deploy with production compose:**
   ```bash
   docker-compose -f docker-compose.yml up -d
   ```

## 🚨 Troubleshooting

### Common Issues

1. **Port already in use:**

   ```bash
   # Find process using port
   netstat -ano | findstr :8001
   # Kill process
   taskkill /PID <PID> /F
   ```

2. **SQL Server connection issues:**

   ```bash
   # Check if SQL Server is running
   docker logs sqlserver
   # Verify connection string in appsettings
   ```

3. **Services can't communicate:**

   ```bash
   # Check network connectivity
   docker network ls
   docker network inspect microservices-network
   ```

4. **Container fails to start:**
   ```bash
   # Check container logs
   docker logs <container-name>
   # Check container configuration
   docker inspect <container-name>
   ```

### Clean Reset

```bash
# Stop and remove all containers, networks, and volumes
docker-compose down -v
docker system prune -a
docker volume prune
```

## 📝 Notes

- Default SQL Server password: `Shinjuuichi@11`
- All services run in the `microservices-network` Docker network
- Data persists in the `mssql_data` Docker volume
- Development environment is configured by default

## 🔗 Useful Links

- [Docker Documentation](https://docs.docker.com/)
- [.NET Docker Images](https://hub.docker.com/_/microsoft-dotnet)
- [Ocelot Documentation](https://ocelot.readthedocs.io/)
- [SQL Server Docker](https://hub.docker.com/_/microsoft-mssql-server)

## 📧 GitHub Repository

**Demo Repository**: [https://github.com/shinjuuichi/PRN232_Demo.git](https://github.com/shinjuuichi/PRN232_Demo.git)

---

_This documentation covers the complete Docker setup for the PRN232 microservices project. For development questions or issues, please refer to the troubleshooting section or create an issue in the GitHub repository._

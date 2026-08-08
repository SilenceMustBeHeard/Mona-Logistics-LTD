[![ASP.NET](https://img.shields.io/badge/ASP.NET-10.0-512BD4?logo=.net)](https://dotnet.microsoft.com/)
[![Entity Framework](https://img.shields.io/badge/Entity_Framework-10.0-512BD4?logo=.net)](https://learn.microsoft.com/en-us/ef/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-17-4169E1?logo=postgresql)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Enabled-2496ED?logo=docker)](https://www.docker.com/)
[![Render](https://img.shields.io/badge/Render-Deployed-46E3B7?logo=render)](https://render.com/)
[![Neon](https://img.shields.io/badge/Neon-Database-00E599?logo=neon)](https://neon.tech/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

---

# 🚛 Mona Logistics LTD

**Mona Logistics LTD** is a full-featured logistics management platform built with ASP.NET Core 10. It enables clients to create load requests, carriers to offer transport, and admins to manage the entire supply chain — from cargo to invoicing.

🔗 **Live Demo:** [https://mona-logistics-ltd.onrender.com](https://mona-logistics-ltd.onrender.com)

---

## 📖 Overview

| Feature | Description |
|---------|-------------|
| 🧾 **Load Requests** | Clients submit cargo details, preferred dates, and special conditions |
| 💰 **Offer System** | Carriers send price offers with discounts and validity |
| 📦 **Shipments & Cargo** | Real-time shipment tracking and cargo management |
| 🚚 **Truck & Driver Management** | Manage fleet, availability, licenses, and trips |
| 🧭 **Route Planning** | Define routes, distances, and trip assignments |
| 📬 **Messaging System** | Contact messages, system notifications, and admin responses |
| 👥 **Role-Based Access** | Admin, Manager, and Client roles with granular permissions |
| 🧾 **Invoicing** | Automatic invoice generation per shipment |
| 🔐 **Secure Authentication** | ASP.NET Core Identity with email confirmation and lockout |

---

## 🎯 Workflow

```
Client → Load Request → Carrier Offer → Shipment Creation → Invoicing
                              ↓
                     Admin/Manager Approval
                              ↓
                    Truck & Driver Assignment
                              ↓
                         Trip Execution
```

---

## 🛠️ Technology Stack

| **Backend** | Version | Purpose |
|-------------|---------|---------|
| **ASP.NET Core** | 10.0 | Web framework |
| **Entity Framework Core** | 10.0 | ORM & data access |
| **PostgreSQL** | 17 | Relational database |
| **Npgsql** | 10.0 | PostgreSQL provider for EF Core |
| **ASP.NET Core Identity** | 10.0 | Authentication & authorization |
| **SendGrid** | Latest | Email service |

| **Infrastructure** | Purpose |
|--------------------|---------|
| **Docker** | Containerization |
| **Render** | Cloud hosting & deployment |
| **Neon** | Serverless PostgreSQL database |
| **GitHub Actions** | CI/CD pipeline |
| **Docker Hub** | Container registry |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL 17](https://www.postgresql.org/download/) (or use Docker)
- [Git](https://git-scm.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (recommended)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (v17.14 or later) or VS Code

### Quick Start with Docker (Recommended)

```bash
# Clone the repository
git clone https://github.com/SilenceMustBeHeard/Mona-Logistics-LTD.git
cd Mona-Logistics-LTD

# Create .env file with your credentials
cat > .env << EOF
POSTGRES_DB=MonaLogisticsDB
POSTGRES_USER=postgres
POSTGRES_PASSWORD=your_secure_password
ASPNETCORE_ENVIRONMENT=Production
EOF

# Start the application with Docker Compose
docker compose up --build
```

The application will be available at `http://localhost:8081`.

### Manual Setup (Without Docker)

1. **Configure PostgreSQL Connection**

   Update `appsettings.json` in the `Mona-Logistics-LTD.Web` project:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=MonaLogisticsDB;Username=postgres;Password=your_password"
     }
   }
   ```

2. **Apply Database Migrations**

   ```bash
   dotnet ef database update --project Mona-Logistics-LTD.Data --startup-project Mona-Logistics-LTD.Web
   ```

3. **Configure SendGrid (for Password Reset)**

   ```bash
   dotnet user-secrets set "SendGrid:ApiKey" "YOUR_SENDGRID_API_KEY"
   dotnet user-secrets set "SendGrid:FromEmail" "your-verified-email@example.com"
   ```

4. **Run the Application**

   ```bash
   cd Mona-Logistics-LTD.Web
   dotnet run
   ```

---

## 🐳 Docker Deployment

```bash
# Build the image
docker build -t mona-logistics .

# Run with PostgreSQL (via docker-compose)
docker compose up -d

# Or run standalone
docker run -d -p 8081:8080 \
  -e ConnectionStrings__DefaultConnection="Host=postgres;Port=5432;Database=MonaLogisticsDB;Username=postgres;Password=secret" \
  -e ASPNETCORE_ENVIRONMENT=Production \
  --name mona-logistics mona-logistics
```

### Environment Variables

| Variable | Description | Required |
|----------|-------------|----------|
| `ASPNETCORE_ENVIRONMENT` | Runtime environment (Development/Production) | ✅ |
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string | ✅ |
| `POSTGRES_DB` | Database name (for docker-compose) | ✅ |
| `POSTGRES_USER` | Database user (for docker-compose) | ✅ |
| `POSTGRES_PASSWORD` | Database password (for docker-compose) | ✅ |
| `SendGrid:ApiKey` | SendGrid API key for email | ❌ |
| `SendGrid:FromEmail` | Verified sender email | ❌ |

---

## 🔧 CI/CD Pipeline

The project uses **GitHub Actions** for CI/CD:

- On push to `main` or `deployment-test`:
  1. Builds the Docker image
  2. Pushes it to **Docker Hub**
  3. Render automatically deploys the latest version

**Live URL:** [https://mona-logistics-ltd.onrender.com](https://mona-logistics-ltd.onrender.com)


---

## 🔒 Environment Variables (Production)

```env
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=Host=ep-shy-block-...;Database=neondb;Username=neondb_owner;Password=...;SSL Mode=Require
```

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---


## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

<div align="center">
  Made with ❤️ by the Mona Logistics Team
  <br/>
  <sub>Built with .NET 10.0 | PostgreSQL | Docker | Deployed on Render</sub>
</div>
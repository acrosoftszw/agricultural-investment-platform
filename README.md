# Agricultural Investment Platform - Backend

Production-ready backend for a comprehensive agricultural investment platform built with .NET 8, ASP.NET Core, Entity Framework Core, and PostgreSQL.

## 🏗️ Architecture

**Clean Architecture Pattern:**
- Domain Layer - Core business entities and rules
- Application Layer - Use cases, business logic, and DTOs
- Infrastructure Layer - Database, external services, and implementations
- API Layer - REST endpoints and controllers

## 📦 Technology Stack

- **.NET 8** with **ASP.NET Core Web API**
- **Entity Framework Core 8** for ORM
- **PostgreSQL 15+** for relational database
- **MediatR** for CQRS pattern
- **FluentValidation** for validation
- **Serilog** for structured logging
- **AutoMapper** for object mapping
- **JWT** for authentication
- **Redis** for caching and sessions
- **Hangfire** for background jobs
- **Swagger/OpenAPI** for API documentation
- **Docker** for containerization

## 🔐 Security Features

- Phone-based authentication with SMS OTP
- JWT access tokens with refresh token rotation
- Argon2id password hashing
- Role-Based Access Control (RBAC)
- Permission-based authorization
- Immutable audit logging
- Double-entry accounting ledger
- Device and IP tracking
- Step-up authentication for sensitive operations
- Rate limiting and brute force protection
- Input validation and sanitization
- SQL injection prevention
- CSRF protection

## 📋 Modules Implemented

1. **Authentication & Authorization** - Phone OTP, JWT, session management
2. **KYC Verification** - Document uploads, approval workflow
3. **Farmer Profiles** - Farm details, certifications, history
4. **Investor Profiles** - Investment preferences, eligibility
5. **Farm Listings** - Agricultural opportunities with versioning
6. **Investments** - Investment lifecycle and tracking
7. **Escrow Wallet System** - Secure fund management
8. **Ledger Accounting** - Double-entry financial records
9. **Contracts Management** - E-signature, versioning, audit trail
10. **Secure Messaging** - User communication and support
11. **Marketplace** - Product listings and commerce
12. **E-Learning** - Courses, lessons, and certifications
13. **ROI Tracking** - Investor and farmer ROI calculations
14. **Admin Dashboard** - Operations and compliance
15. **Audit Logs** - Immutable system activity tracking
16. **Notifications** - Real-time and scheduled alerts
17. **Background Jobs** - Async processing and scheduled tasks

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- PostgreSQL 15+
- Docker & Docker Compose
- Redis (for caching)

### Development Setup

1. Clone the repository:
```bash
git clone https://github.com/acrosoftszw/agricultural-investment-platform.git
cd agricultural-investment-platform
```

2. Configure environment:
```bash
cp .env.example .env
# Edit .env with your settings
```

3. Run with Docker Compose:
```bash
docker-compose up -d
```

4. Apply migrations:
```bash
dotnet ef database update --project src/Infrastructure --startup-project src/API
```

5. Access the API:
- Swagger UI: http://localhost:5000/swagger
- API: http://localhost:5000/api

## 📁 Project Structure

```
src/
├── Domain/                 # Core business entities
│   ├── Common/
│   ├── Entities/
│   ├── ValueObjects/
│   └── Events/
├── Application/            # Business logic & use cases
│   ├── Common/
│   ├── DTOs/
│   ├── Commands/
│   ├── Queries/
│   ├── Handlers/
│   ├── Services/
│   └── Validators/
├── Infrastructure/         # External services & persistence
│   ├── Persistence/
│   ├── Repositories/
│   ├── Services/
│   ├── Authentication/
│   ├── Migrations/
│   └── DependencyInjection.cs
├── API/                    # REST endpoints
│   ├── Controllers/
│   ├── Middleware/
│   ├── Filters/
│   └── Program.cs
└── Tests/
    ├── Unit/
    └── Integration/
```

## 🔄 API Endpoints

All endpoints follow RESTful conventions and are documented in Swagger.

### Authentication
- `POST /api/v1/auth/register` - Register with phone number
- `POST /api/v1/auth/verify-otp` - Verify phone with OTP
- `POST /api/v1/auth/login` - Login
- `POST /api/v1/auth/refresh` - Refresh token
- `POST /api/v1/auth/logout` - Logout

### Farmer
- `GET /api/v1/farmers/profile` - Get farmer profile
- `PUT /api/v1/farmers/profile` - Update farmer profile
- `POST /api/v1/farmers/listings` - Create farm listing
- `GET /api/v1/farmers/listings` - Get farmer's listings

### Investor
- `GET /api/v1/investors/profile` - Get investor profile
- `POST /api/v1/investments` - Create investment
- `GET /api/v1/investments` - Get investments
- `GET /api/v1/investments/{id}/roi` - Get ROI

### Admin
- `GET /api/v1/admin/users` - List users
- `GET /api/v1/admin/kyc-queue` - KYC approval queue
- `POST /api/v1/admin/kyc/{id}/approve` - Approve KYC
- `GET /api/v1/admin/audit-logs` - Audit logs

See `docs/API.md` for complete endpoint documentation.

## 🧪 Testing

Run unit tests:
```bash
dotnet test tests/UnitTests
```

Run integration tests:
```bash
dotnet test tests/IntegrationTests
```

Run with coverage:
```bash
dotnet test /p:CollectCoverage=true
```

## 📊 Database

The platform uses PostgreSQL with Entity Framework Core. Key tables:

- `Users` - Platform users
- `KycVerifications` - KYC documents and status
- `FarmListings` - Agricultural investment opportunities
- `Investments` - Investment records
- `LedgerEntries` - Double-entry accounting
- `EscrowWallets` - Fund management
- `Contracts` - Investment agreements
- `AuditLogs` - Immutable activity logs

## 🔐 Security Considerations

### Production Deployment
1. Enable HTTPS with TLS 1.3
2. Set strong secrets in AWS Secrets Manager
3. Enable database encryption at rest
4. Configure WAF rules
5. Enable MFA for all admin accounts
6. Set up DDoS protection
7. Enable audit logging to immutable storage
8. Configure daily backups

### Environment Variables
Never commit secrets. Use:
- AWS Secrets Manager
- Azure Key Vault
- HashiCorp Vault

## 📝 Logging

Structured logging via Serilog:
- Application logs to console and file
- Audit logs to dedicated database table
- Financial transactions logged with full context
- Admin actions logged with before/after values

## 🚢 Deployment

### Docker Deployment
```bash
docker build -t agriplatform:latest .
docker run -d -p 5000:80 agriplatform:latest
```

### Kubernetes
See `k8s/` directory for Kubernetes manifests.

### AWS ECS
See `infra/ecs/` for ECS task definitions.

## 📞 Support

For issues and questions:
1. Check existing issues on GitHub
2. Create a new issue with detailed information
3. Follow up on your GitHub issue

## 📄 License

Proprietary - All rights reserved

## 🤝 Contributing

Internal contributions only. Follow guidelines in `CONTRIBUTING.md`.

---

**Last Updated:** 2026-05-28
**Version:** 1.0.0-alpha
**Status:** Production-Ready

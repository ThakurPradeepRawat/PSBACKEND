# 🪔 Prashad.com — Backend API

> **ASP.NET Core 8 Web API** | Temple Prasad Delivery Platform  
> Sacred blessings from India's holiest temples, delivered to your doorstep.

---

## 📋 Table of Contents

- [Project Overview](#-project-overview)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Database Schemas](#-database-schemas)
- [Getting Started](#-getting-started)
- [Environment Configuration](#-environment-configuration)
- [Running the Application](#-running-the-application)
- [API Endpoints](#-api-endpoints)
- [Authentication & Authorization](#-authentication--authorization)
- [Payment Integration (Razorpay)](#-payment-integration-razorpay)
- [Notification Services](#-notification-services)
- [Database Migrations](#-database-migrations)
- [Testing](#-testing)
- [Deployment](#-deployment)
- [Contributing](#-contributing)

---

## 🕌 Project Overview

Prashad.com is a full-stack e-commerce platform enabling devotees across India to order authentic prasad from 50+ sacred temples including **Tirupati Balaji, Kashi Vishwanath, Vaishno Devi, Siddhivinayak,** and **Jagannath Temple**.

This repository contains the **ASP.NET Core 8 Web API** backend responsible for:

- JWT-based authentication with refresh token rotation
- Temple & prasad catalog management
- Cart, order, and coupon processing
- Razorpay payment gateway integration
- Real-time shipment tracking
- Email (SendGrid) & SMS (Twilio) notifications
- Admin dashboard, audit logging & system configuration

---

## 🛠 Tech Stack

| Layer | Technology | Version |
|---|---|---|
| Framework | ASP.NET Core Web API | 8.0 |
| ORM | Entity Framework Core | 8.x |
| Database | SQL Server | 2022 |
| Authentication | JWT Bearer Tokens | — |
| Payment Gateway | Razorpay .NET SDK | Latest |
| Email | SendGrid | v3 |
| SMS | Twilio | Latest |
| File Storage | Azure Blob Storage | Latest |
| Logging | Serilog | Latest |
| API Docs | Swagger / Swashbuckle | Latest |
| Testing | xUnit + Moq | Latest |
| Containerization | Docker | — |

---

## 📁 Project Structure

```
Prashad.API/
│
├── Controllers/                    # API Controllers (one per domain)
│   ├── AuthController.cs           # Login, register, refresh token
│   ├── TempleController.cs         # Temple CRUD & listings
│   ├── PrasadController.cs         # Prasad product catalog
│   ├── CartController.cs           # Cart session management
│   ├── OrderController.cs          # Order placement & status
│   ├── PaymentController.cs        # Razorpay integration & webhooks
│   ├── DeliveryController.cs       # Shipment tracking
│   ├── NotificationController.cs   # Notification management
│   └── AdminController.cs          # Admin dashboard & audit
│
├── Services/                       # Business Logic Layer
│   ├── Interfaces/
│   │   ├── IAuthService.cs
│   │   ├── ITempleService.cs
│   │   ├── IPrasadService.cs
│   │   ├── ICartService.cs
│   │   ├── IOrderService.cs
│   │   ├── IPaymentService.cs
│   │   ├── IDeliveryService.cs
│   │   └── INotificationService.cs
│   ├── AuthService.cs
│   ├── TempleService.cs
│   ├── PrasadService.cs
│   ├── CartService.cs
│   ├── OrderService.cs
│   ├── PaymentService.cs
│   ├── DeliveryService.cs
│   └── NotificationService.cs
│
├── Repositories/                   # Data Access Layer (Repository Pattern)
│   ├── Interfaces/
│   │   ├── IUserRepository.cs
│   │   ├── ITempleRepository.cs
│   │   ├── IPrasadRepository.cs
│   │   ├── IOrderRepository.cs
│   │   └── IGenericRepository.cs
│   ├── UserRepository.cs
│   ├── TempleRepository.cs
│   ├── PrasadRepository.cs
│   ├── OrderRepository.cs
│   └── GenericRepository.cs
│
├── Entities/                       # EF Core Database Entities (8 schemas)
│   ├── Auth/
│   │   ├── User.cs
│   │   ├── UserRole.cs
│   │   ├── RefreshToken.cs
│   │   └── UserAddress.cs
│   ├── Temple/
│   │   ├── Temple.cs
│   │   ├── TempleImage.cs
│   │   ├── TempleTiming.cs
│   │   └── TempleReview.cs
│   ├── Catalog/
│   │   ├── PrasadCategory.cs
│   │   ├── Prasad.cs
│   │   ├── PrasadImage.cs
│   │   ├── PrasadInventory.cs
│   │   └── Wishlist.cs
│   ├── Orders/
│   │   ├── CartSession.cs
│   │   ├── CartItem.cs
│   │   ├── Order.cs
│   │   ├── OrderItem.cs
│   │   └── Coupon.cs
│   ├── Payment/
│   │   ├── Payment.cs
│   │   ├── PaymentAttempt.cs
│   │   └── Refund.cs
│   ├── Delivery/
│   │   ├── Shipment.cs
│   │   ├── ShipmentEvent.cs
│   │   └── DeliveryPartner.cs
│   ├── Notification/
│   │   ├── NotificationTemplate.cs
│   │   └── NotificationLog.cs
│   └── Admin/
│       ├── AdminUser.cs
│       ├── AuditLog.cs
│       └── SystemConfig.cs
│
├── DTOs/                           # Data Transfer Objects
│   ├── Auth/
│   │   ├── LoginRequestDto.cs
│   │   ├── LoginResponseDto.cs
│   │   ├── RegisterRequestDto.cs
│   │   └── RefreshTokenRequestDto.cs
│   ├── Temple/
│   │   ├── TempleDto.cs
│   │   ├── TempleListDto.cs
│   │   └── TempleReviewDto.cs
│   ├── Catalog/
│   │   ├── PrasadDto.cs
│   │   ├── PrasadListDto.cs
│   │   └── PrasadInventoryDto.cs
│   ├── Orders/
│   │   ├── CartDto.cs
│   │   ├── PlaceOrderRequestDto.cs
│   │   ├── OrderDto.cs
│   │   └── OrderItemDto.cs
│   ├── Payment/
│   │   ├── CreatePaymentDto.cs
│   │   ├── PaymentVerifyDto.cs
│   │   └── RefundRequestDto.cs
│   └── Delivery/
│       ├── ShipmentDto.cs
│       └── TrackingDto.cs
│
├── Data/                           # EF Core DbContext & Configurations
│   ├── PrashadDbContext.cs
│   ├── Configurations/             # Fluent API configurations per entity
│   │   ├── UserConfiguration.cs
│   │   ├── TempleConfiguration.cs
│   │   ├── PrasadConfiguration.cs
│   │   ├── OrderConfiguration.cs
│   │   └── ...
│   └── Migrations/                 # EF Core Migrations
│
├── Middleware/                     # Custom Middleware
│   ├── ExceptionHandlingMiddleware.cs
│   ├── RequestLoggingMiddleware.cs
│   └── RateLimitingMiddleware.cs
│
├── Helpers/                        # Utility classes
│   ├── JwtHelper.cs
│   ├── PasswordHasher.cs
│   ├── OrderNumberGenerator.cs
│   ├── RazorpaySignatureVerifier.cs
│   └── PaginationHelper.cs
│
├── Constants/                      # App-wide constants
│   ├── Roles.cs
│   ├── OrderStatus.cs
│   ├── PaymentStatus.cs
│   └── NotificationKeys.cs
│
├── appsettings.json                # Base configuration
├── appsettings.Development.json    # Dev overrides
├── appsettings.Production.json     # Prod overrides
├── Program.cs                      # App entry point & DI setup
└── Prashad.API.csproj

Prashad.Tests/                      # xUnit Test Project
├── Controllers/
├── Services/
├── Repositories/
└── Prashad.Tests.csproj
```

---

## 🗄 Database Schemas

The SQL Server database is organized into **8 logical schemas** with **24 tables**:

```
┌─────────────────────────────────────────────────────────────┐
│  Schema         │ Tables │ Purpose                          │
├─────────────────┼────────┼──────────────────────────────────┤
│  auth           │   4    │ Users, Roles, JWT tokens         │
│  temple         │   4    │ Temple master data & reviews     │
│  catalog        │   5    │ Prasad products & inventory      │
│  orders         │   5    │ Cart, orders & coupons           │
│  payment        │   3    │ Razorpay transactions & refunds  │
│  delivery       │   3    │ Shipments & tracking events      │
│  notification   │   2    │ Email/SMS templates & logs       │
│  admin          │   3    │ Admin users, audit & config      │
└─────────────────┴────────┴──────────────────────────────────┘
```

### Order State Machine

```
Pending → Confirmed → Collected → Packed → Dispatched → Delivered
                                                       → Cancelled (from any state)
```

---

## 🚀 Getting Started

### Prerequisites

Ensure the following are installed on your machine:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server 2022](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express / LocalDB)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) _(optional)_
- [Git](https://git-scm.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with C# extension

### Clone the Repository

```bash
git clone https://github.com/your-org/prashad-backend.git
cd prashad-backend
```

### Restore NuGet Packages

```bash
dotnet restore
```

---

## ⚙ Environment Configuration

### `appsettings.Development.json`

Create this file at `Prashad.API/appsettings.Development.json` and fill in your values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PrashadDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },

  "Jwt": {
    "SecretKey": "YOUR_SUPER_SECRET_KEY_MIN_32_CHARS_LONG",
    "Issuer": "prashad.com",
    "Audience": "prashad.com",
    "AccessTokenExpiryMinutes": 15,
    "RefreshTokenExpiryDays": 7
  },

  "Razorpay": {
    "KeyId": "rzp_test_XXXXXXXXXXXXXXXX",
    "KeySecret": "YOUR_RAZORPAY_KEY_SECRET",
    "WebhookSecret": "YOUR_RAZORPAY_WEBHOOK_SECRET",
    "Currency": "INR"
  },

  "SendGrid": {
    "ApiKey": "SG.XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
    "FromEmail": "noreply@prashad.com",
    "FromName": "Prashad.com"
  },

  "Twilio": {
    "AccountSid": "ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
    "AuthToken": "YOUR_TWILIO_AUTH_TOKEN",
    "FromNumber": "+1XXXXXXXXXX"
  },

  "AzureBlobStorage": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net",
    "ContainerName": "prashad-media"
  },

  "Serilog": {
    "MinimumLevel": "Debug",
    "WriteTo": [
      { "Name": "Console" },
      { "Name": "File", "Args": { "path": "logs/prashad-.txt", "rollingInterval": "Day" } }
    ]
  }
}
```

> ⚠️ **Never commit `appsettings.Development.json` or any file with real secrets to version control.**  
> It is already listed in `.gitignore`.

### Required NuGet Packages

```xml
<!-- Prashad.API.csproj -->
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.*" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.*" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.*" />
<PackageReference Include="Razorpay" Version="1.*" />
<PackageReference Include="SendGrid" Version="9.*" />
<PackageReference Include="Twilio" Version="6.*" />
<PackageReference Include="Azure.Storage.Blobs" Version="12.*" />
<PackageReference Include="Serilog.AspNetCore" Version="8.*" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.*" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.*" />
<PackageReference Include="FluentValidation.AspNetCore" Version="11.*" />
<PackageReference Include="AspNetCoreRateLimit" Version="4.*" />
```

Install all at once:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Razorpay
dotnet add package SendGrid
dotnet add package Twilio
dotnet add package Azure.Storage.Blobs
dotnet add package Serilog.AspNetCore
dotnet add package Swashbuckle.AspNetCore
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package FluentValidation.AspNetCore
dotnet add package AspNetCoreRateLimit
```

---

## ▶ Running the Application

### Step 1 — Apply Database Migrations

```bash
cd Prashad.API

# Create the database and apply all migrations
dotnet ef database update

# (Optional) Add a new migration after entity changes
dotnet ef migrations add MigrationName

# (Optional) Rollback to a specific migration
dotnet ef database update MigrationName
```

### Step 2 — Run the API

```bash
dotnet run --project Prashad.API
```

The API will start at:

```
http://localhost:5000
https://localhost:5001
```

### Step 3 — Open Swagger UI

Navigate to:

```
https://localhost:5001/swagger
```

You will see all available endpoints grouped by controller with request/response schemas.

---

## 📡 API Endpoints

### 🔐 Auth — `/api/auth`

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `POST` | `/api/auth/register` | ❌ | Register new customer |
| `POST` | `/api/auth/login` | ❌ | Login & get JWT tokens |
| `POST` | `/api/auth/refresh` | ❌ | Refresh access token |
| `POST` | `/api/auth/logout` | ✅ | Revoke refresh token |
| `POST` | `/api/auth/send-otp` | ❌ | Send OTP for phone verification |
| `POST` | `/api/auth/verify-otp` | ❌ | Verify OTP |
| `GET`  | `/api/auth/me` | ✅ | Get current user profile |
| `PUT`  | `/api/auth/me` | ✅ | Update profile |

---

### 🛕 Temple — `/api/temples`

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `GET` | `/api/temples` | ❌ | List temples (filter, sort, paginate) |
| `GET` | `/api/temples/{id}` | ❌ | Get temple detail |
| `GET` | `/api/temples/{id}/prasad` | ❌ | Get all prasad for a temple |
| `GET` | `/api/temples/{id}/reviews` | ❌ | Get temple reviews |
| `POST` | `/api/temples/{id}/reviews` | ✅ Customer | Submit review |
| `POST` | `/api/temples` | ✅ Admin | Create temple |
| `PUT` | `/api/temples/{id}` | ✅ Admin/TempleManager | Update temple |
| `DELETE` | `/api/temples/{id}` | ✅ Admin | Soft delete temple |
| `POST` | `/api/temples/{id}/images` | ✅ Admin/TempleManager | Upload temple image |

**Query Parameters for `GET /api/temples`:**

```
?state=AndhraPradesh
&deity=Vishnu
&rating=4
&search=tirupati
&page=1
&pageSize=12
&sortBy=rating      # rating | name | reviews
&sortDir=desc
```

---

### 🪔 Prasad Catalog — `/api/prasad`

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `GET` | `/api/prasad` | ❌ | List prasad (filter, paginate) |
| `GET` | `/api/prasad/{id}` | ❌ | Get prasad detail |
| `GET` | `/api/prasad/categories` | ❌ | List all categories |
| `GET` | `/api/prasad/bestsellers` | ❌ | Get bestseller prasad |
| `POST` | `/api/prasad` | ✅ Admin | Create prasad listing |
| `PUT` | `/api/prasad/{id}` | ✅ Admin/TempleManager | Update prasad |
| `PATCH` | `/api/prasad/{id}/inventory` | ✅ Admin | Update stock quantity |
| `DELETE` | `/api/prasad/{id}` | ✅ Admin | Soft delete prasad |
| `POST` | `/api/prasad/{id}/images` | ✅ Admin | Upload prasad image |

---

### 🛒 Cart — `/api/cart`

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `GET` | `/api/cart` | ✅/Session | Get current cart |
| `POST` | `/api/cart/items` | ✅/Session | Add item to cart |
| `PUT` | `/api/cart/items/{id}` | ✅/Session | Update item quantity |
| `DELETE` | `/api/cart/items/{id}` | ✅/Session | Remove item from cart |
| `DELETE` | `/api/cart` | ✅/Session | Clear cart |
| `POST` | `/api/cart/merge` | ✅ | Merge guest cart on login |
| `POST` | `/api/cart/apply-coupon` | ✅/Session | Apply coupon code |
| `DELETE` | `/api/cart/coupon` | ✅/Session | Remove coupon |

---

### 📦 Orders — `/api/orders`

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `POST` | `/api/orders` | ✅ Customer | Place new order |
| `GET` | `/api/orders` | ✅ Customer | List user's orders |
| `GET` | `/api/orders/{id}` | ✅ Customer | Get order detail |
| `POST` | `/api/orders/{id}/cancel` | ✅ Customer | Request cancellation |
| `GET` | `/api/orders/admin` | ✅ Admin | List all orders |
| `PATCH` | `/api/orders/{id}/status` | ✅ Admin | Update order status |

**Place Order Request Body:**

```json
{
  "addressId": 1,
  "deliveryType": "Standard",
  "specialInstructions": "Please pack carefully",
  "couponCode": "DIWALI100"
}
```

---

### 💳 Payment — `/api/payments`

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `POST` | `/api/payments/create` | ✅ Customer | Create Razorpay order |
| `POST` | `/api/payments/verify` | ✅ Customer | Verify payment signature |
| `POST` | `/api/payments/webhook` | ❌ (HMAC) | Razorpay webhook handler |
| `GET` | `/api/payments/{orderId}` | ✅ Customer | Get payment status |
| `POST` | `/api/payments/{id}/refund` | ✅ Admin | Initiate refund |
| `GET` | `/api/payments/refunds` | ✅ Admin | List all refunds |

**Create Payment Response:**

```json
{
  "razorpayOrderId": "order_XXXXXXXXXXXXXXXX",
  "amount": 161000,
  "currency": "INR",
  "keyId": "rzp_test_XXXXXXXXXXXXXXXX"
}
```

---

### 🚚 Delivery — `/api/delivery`

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `GET` | `/api/delivery/track/{orderNumber}` | ✅ Customer | Track shipment by order number |
| `GET` | `/api/delivery/{shipmentId}/events` | ✅ Customer | Get all tracking events |
| `POST` | `/api/delivery` | ✅ Admin | Create shipment record |
| `PATCH` | `/api/delivery/{id}/status` | ✅ Admin | Update shipment status |
| `POST` | `/api/delivery/{id}/events` | ✅ Admin | Add tracking event |
| `GET` | `/api/delivery/partners` | ✅ Admin | List delivery partners |

---

### 🔔 Notifications — `/api/notifications`

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `GET` | `/api/notifications/templates` | ✅ Admin | List all templates |
| `PUT` | `/api/notifications/templates/{id}` | ✅ Admin | Update template |
| `GET` | `/api/notifications/logs` | ✅ Admin | View notification logs |
| `POST` | `/api/notifications/resend/{logId}` | ✅ Admin | Resend failed notification |

---

### 🛠 Admin — `/api/admin`

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `GET` | `/api/admin/dashboard` | ✅ Admin | Dashboard stats & KPIs |
| `GET` | `/api/admin/users` | ✅ Admin | List all users |
| `PATCH` | `/api/admin/users/{id}/status` | ✅ Admin | Activate / deactivate user |
| `GET` | `/api/admin/audit-logs` | ✅ SuperAdmin | View audit logs |
| `GET` | `/api/admin/configs` | ✅ SuperAdmin | List system configs |
| `PUT` | `/api/admin/configs/{key}` | ✅ SuperAdmin | Update config value |
| `GET` | `/api/admin/coupons` | ✅ Admin | List coupons |
| `POST` | `/api/admin/coupons` | ✅ Admin | Create coupon |
| `PATCH` | `/api/admin/coupons/{id}` | ✅ Admin | Update coupon |

---

### 📍 User Addresses — `/api/addresses`

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `GET` | `/api/addresses` | ✅ Customer | List saved addresses |
| `POST` | `/api/addresses` | ✅ Customer | Add new address |
| `PUT` | `/api/addresses/{id}` | ✅ Customer | Update address |
| `DELETE` | `/api/addresses/{id}` | ✅ Customer | Delete address |
| `PATCH` | `/api/addresses/{id}/default` | ✅ Customer | Set as default |

---

## 🔐 Authentication & Authorization

### JWT Flow

```
1. POST /api/auth/login
   → Returns: { accessToken, refreshToken, expiresIn }

2. Include in all protected requests:
   Authorization: Bearer <accessToken>

3. When accessToken expires (15 min):
   POST /api/auth/refresh { refreshToken }
   → Returns new { accessToken, refreshToken }

4. POST /api/auth/logout { refreshToken }
   → Revokes refresh token in DB
```

### Roles

| Role | Description | Permissions |
|------|-------------|-------------|
| `Customer` | Registered devotee | Browse, order, track, review |
| `TempleManager` | Temple admin | Manage own temple & prasad |
| `Admin` | Platform admin | Full access except SuperAdmin ops |
| `SuperAdmin` | Root admin | System configs, audit logs, all ops |

### Protecting Endpoints in Controllers

```csharp
// Any authenticated user
[Authorize]

// Specific role
[Authorize(Roles = "Admin")]

// Multiple roles
[Authorize(Roles = "Admin,SuperAdmin")]

// Policy-based
[Authorize(Policy = "TempleManagerOrAdmin")]
```

---

## 💳 Payment Integration (Razorpay)

### Payment Flow

```
Angular                    API                        Razorpay
  │                         │                             │
  │── POST /payments/create ──►│                          │
  │                         │── Create Razorpay Order ──►│
  │                         │◄─ razorpayOrderId ─────────│
  │◄─ { razorpayOrderId } ──│                             │
  │                         │                             │
  │── Open Razorpay Widget ──────────────────────────────►│
  │◄─ { paymentId, signature } ──────────────────────────│
  │                         │                             │
  │── POST /payments/verify ──►│                          │
  │                         │── Verify HMAC-SHA256 ────►  │
  │                         │── Update DB (Captured) ──►  │
  │◄─ { success: true } ────│                             │
```

### Webhook Setup

Configure your Razorpay Dashboard webhook URL:

```
https://your-domain.com/api/payments/webhook
```

Events to subscribe:
- `payment.captured`
- `payment.failed`
- `refund.processed`

---

## 📧 Notification Services

### Email Templates (SendGrid)

| Template Key | Trigger Event |
|---|---|
| `ORDER_PLACED` | Order successfully placed |
| `PAYMENT_SUCCESS` | Payment captured by Razorpay |
| `PAYMENT_FAILED` | Payment attempt failed |
| `ORDER_CONFIRMED` | Order confirmed by temple |
| `PRASAD_COLLECTED` | Prasad collected from temple |
| `ORDER_DISPATCHED` | Shipment dispatched with AWB |
| `OUT_FOR_DELIVERY` | Shipment out for delivery |
| `ORDER_DELIVERED` | Order successfully delivered |
| `ORDER_CANCELLED` | Order cancelled |
| `REFUND_INITIATED` | Refund initiated |

### SMS (Twilio)

SMS notifications are sent for the same events with shorter template text, targeting the customer's registered mobile number.

---

## 🗃 Database Migrations

```bash
# Add a new migration
dotnet ef migrations add AddTempleVerificationFlag \
  --project Prashad.API \
  --startup-project Prashad.API

# Apply migrations to DB
dotnet ef database update \
  --project Prashad.API

# Generate SQL script for production
dotnet ef migrations script \
  --idempotent \
  --output migrations.sql \
  --project Prashad.API

# Rollback last migration (code only, not DB)
dotnet ef migrations remove \
  --project Prashad.API

# List all migrations
dotnet ef migrations list \
  --project Prashad.API
```

---

## 🧪 Testing

```bash
# Run all tests
dotnet test

# Run with coverage report
dotnet test --collect:"XPlat Code Coverage"

# Run specific test class
dotnet test --filter "FullyQualifiedName~OrderServiceTests"

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"
```

### Test Structure

```
Prashad.Tests/
├── Controllers/
│   ├── AuthControllerTests.cs
│   ├── TempleControllerTests.cs
│   ├── OrderControllerTests.cs
│   └── PaymentControllerTests.cs
├── Services/
│   ├── AuthServiceTests.cs
│   ├── OrderServiceTests.cs
│   ├── PaymentServiceTests.cs
│   └── NotificationServiceTests.cs
└── Repositories/
    ├── OrderRepositoryTests.cs
    └── PrasadRepositoryTests.cs
```

---

## 🚀 Deployment

### Docker

```bash
# Build Docker image
docker build -t prashad-api .

# Run container
docker run -d \
  -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="YOUR_PROD_CONNECTION_STRING" \
  -e Jwt__SecretKey="YOUR_PROD_SECRET" \
  -e Razorpay__KeyId="rzp_live_XXXXX" \
  -e Razorpay__KeySecret="YOUR_PROD_KEY_SECRET" \
  --name prashad-api \
  prashad-api
```

### `Dockerfile`

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Prashad.API/Prashad.API.csproj", "Prashad.API/"]
RUN dotnet restore "Prashad.API/Prashad.API.csproj"
COPY . .
WORKDIR "/src/Prashad.API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Prashad.API.dll"]
```

### Azure App Service Deployment

```bash
# Login to Azure
az login

# Create resource group
az group create --name prashad-rg --location "Central India"

# Create App Service plan
az appservice plan create \
  --name prashad-plan \
  --resource-group prashad-rg \
  --sku B2 \
  --is-linux

# Create Web App
az webapp create \
  --resource-group prashad-rg \
  --plan prashad-plan \
  --name prashad-api \
  --runtime "DOTNETCORE:8.0"

# Deploy
dotnet publish -c Release -o ./publish
az webapp deploy \
  --resource-group prashad-rg \
  --name prashad-api \
  --src-path ./publish \
  --type zip
```

---

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch: `git checkout -b feature/add-puja-booking`
3. Follow the coding conventions (see `.editorconfig`)
4. Write unit tests for new business logic
5. Ensure all tests pass: `dotnet test`
6. Commit your changes: `git commit -m "feat: add puja booking module"`
7. Push to the branch: `git push origin feature/add-puja-booking`
8. Open a Pull Request with a clear description

### Commit Message Convention

```
feat:     New feature
fix:      Bug fix
docs:     Documentation update
refactor: Code refactoring
test:     Adding or updating tests
chore:    Build or tooling changes
```

---



---

<div align="center">
  <strong>🪔 Prashad.com — Divine Blessings, Delivered 🪔</strong><br>
</div>

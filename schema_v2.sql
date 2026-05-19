-- ==========================================
-- Prashad.com Database Schema Creation Script
-- ==========================================

-- 1. Create Schemas
-- ------------------------------------------
CREATE SCHEMA [auth];
GO
CREATE SCHEMA [temple];
GO
CREATE SCHEMA [catalog];
GO
CREATE SCHEMA [orders];
GO
CREATE SCHEMA [payment];
GO
CREATE SCHEMA [delivery];
GO
CREATE SCHEMA [notification];
GO
CREATE SCHEMA [admin];
GO

-- ==========================================
-- SCHEMA: auth
-- ==========================================

CREATE TABLE [auth].[Users] (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(60) NOT NULL,
    LastName NVARCHAR(60) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Phone VARCHAR(15) UNIQUE,
    PasswordHash NVARCHAR(512) NOT NULL,
    ProfileImageUrl NVARCHAR(500) NULL,
    IsEmailVerified BIT DEFAULT 0,
    IsPhoneVerified BIT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    OtpCode VARCHAR(6) NULL,
    OtpExpiry DATETIME2 NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
    LastLoginAt DATETIME2 NULL
);
GO
CREATE INDEX IX_Users_Email ON [auth].[Users](Email);
CREATE INDEX IX_Users_Phone ON [auth].[Users](Phone);
GO

CREATE TABLE [auth].[UserRoles] (
    UserRoleId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES [auth].[Users](UserId),
    Role VARCHAR(30) NOT NULL,
    AssignedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE TABLE [auth].[RefreshTokens] (
    TokenId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES [auth].[Users](UserId),
    Token NVARCHAR(512) NOT NULL UNIQUE,
    ExpiresAt DATETIME2 NOT NULL,
    IsRevoked BIT DEFAULT 0,
    DeviceInfo NVARCHAR(200) NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE TABLE [auth].[UserAddresses] (
    AddressId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES [auth].[Users](UserId),
    AddressType VARCHAR(20) DEFAULT 'Home',
    RecipientName NVARCHAR(100) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Line1 NVARCHAR(200) NOT NULL,
    Line2 NVARCHAR(200) NULL,
    City NVARCHAR(80) NOT NULL,
    State NVARCHAR(80) NOT NULL,
    PinCode VARCHAR(10) NOT NULL,
    Country VARCHAR(60) DEFAULT 'India',
    IsDefault BIT DEFAULT 0,
    Latitude DECIMAL(10,8) NULL,
    Longitude DECIMAL(11,8) NULL
);
GO


-- ==========================================
-- SCHEMA: temple
-- ==========================================

CREATE TABLE [temple].[Temples] (
    TempleId INT IDENTITY(1,1) PRIMARY KEY,
    Slug VARCHAR(120) NOT NULL UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    Deity NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    City NVARCHAR(80) NOT NULL,
    State NVARCHAR(80) NOT NULL,
    PinCode VARCHAR(10) NULL,
    Latitude DECIMAL(10,8) NOT NULL,
    Longitude DECIMAL(11,8) NOT NULL,
    CoverImageUrl NVARCHAR(500) NULL,
    AverageRating DECIMAL(3,2) DEFAULT 0.00,
    TotalReviews INT DEFAULT 0,
    IsVerified BIT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    Tags NVARCHAR(500) NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO
CREATE INDEX IX_Temples_State ON [temple].[Temples](State);
CREATE INDEX IX_Temples_IsActive ON [temple].[Temples](IsActive);
CREATE INDEX IX_Temples_Deity ON [temple].[Temples](Deity);
GO

CREATE TABLE [temple].[TempleImages] (
    ImageId INT IDENTITY(1,1) PRIMARY KEY,
    TempleId INT NOT NULL FOREIGN KEY REFERENCES [temple].[Temples](TempleId),
    ImageUrl NVARCHAR(500) NOT NULL,
    AltText NVARCHAR(200) NULL,
    SortOrder INT DEFAULT 0,
    IsPrimary BIT DEFAULT 0
);
GO

CREATE TABLE [temple].[TempleTimings] (
    TimingId INT IDENTITY(1,1) PRIMARY KEY,
    TempleId INT NOT NULL FOREIGN KEY REFERENCES [temple].[Temples](TempleId),
    DayOfWeek TINYINT NOT NULL,
    SessionName VARCHAR(50) NOT NULL,
    OpenTime TIME NOT NULL,
    CloseTime TIME NOT NULL
);
GO

CREATE TABLE [temple].[TempleReviews] (
    ReviewId INT IDENTITY(1,1) PRIMARY KEY,
    TempleId INT NOT NULL FOREIGN KEY REFERENCES [temple].[Temples](TempleId),
    UserId INT NOT NULL FOREIGN KEY REFERENCES [auth].[Users](UserId),
    Rating TINYINT NOT NULL CHECK (Rating BETWEEN 1 AND 5),
    ReviewText NVARCHAR(1000) NULL,
    IsVerifiedPurchase BIT DEFAULT 0,
    IsApproved BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UNIQUE (TempleId, UserId)
);
GO


-- ==========================================
-- SCHEMA: catalog
-- ==========================================

CREATE TABLE [catalog].[PrasadCategories] (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    ParentCategoryId INT NULL FOREIGN KEY REFERENCES [catalog].[PrasadCategories](CategoryId),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Slug VARCHAR(120) NOT NULL UNIQUE,
    IconUrl NVARCHAR(500) NULL,
    SortOrder INT DEFAULT 0,
    IsActive BIT DEFAULT 1
);
GO

CREATE TABLE [catalog].[Prasad] (
    PrasadId INT IDENTITY(1,1) PRIMARY KEY,
    TempleId INT NOT NULL FOREIGN KEY REFERENCES [temple].[Temples](TempleId),
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES [catalog].[PrasadCategories](CategoryId),
    Slug VARCHAR(160) NOT NULL UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    ShortDesc NVARCHAR(500) NULL,
    Description NVARCHAR(MAX) NULL,
    Price DECIMAL(10,2) NOT NULL CHECK (Price > 0),
    OriginalPrice DECIMAL(10,2) NULL CHECK (OriginalPrice > 0),
    WeightGrams INT NULL,
    ShelfLifeDays INT NULL,
    Ingredients NVARCHAR(MAX) NULL,
    IsGITagged BIT DEFAULT 0,
    IsBestseller BIT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    AverageRating DECIMAL(3,2) DEFAULT 0.00,
    TotalReviews INT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO
CREATE INDEX IX_Prasad_TempleId ON [catalog].[Prasad](TempleId);
CREATE INDEX IX_Prasad_IsActive ON [catalog].[Prasad](IsActive);
CREATE INDEX IX_Prasad_Price ON [catalog].[Prasad](Price);
GO

CREATE TABLE [catalog].[PrasadImages] (
    ImageId INT IDENTITY(1,1) PRIMARY KEY,
    PrasadId INT NOT NULL FOREIGN KEY REFERENCES [catalog].[Prasad](PrasadId),
    ImageUrl NVARCHAR(500) NOT NULL,
    AltText NVARCHAR(200) NULL,
    SortOrder INT DEFAULT 0,
    IsPrimary BIT DEFAULT 0
);
GO

CREATE TABLE [catalog].[PrasadInventory] (
    InventoryId INT IDENTITY(1,1) PRIMARY KEY,
    PrasadId INT NOT NULL UNIQUE FOREIGN KEY REFERENCES [catalog].[Prasad](PrasadId),
    StockQuantity INT DEFAULT 0 CHECK (StockQuantity >= 0),
    ReservedQuantity INT DEFAULT 0,
    ReorderLevel INT DEFAULT 10,
    LastRestockedAt DATETIME2 NULL,
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE TABLE [catalog].[Wishlists] (
    WishlistId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES [auth].[Users](UserId),
    PrasadId INT NOT NULL FOREIGN KEY REFERENCES [catalog].[Prasad](PrasadId),
    AddedAt DATETIME2 DEFAULT GETUTCDATE(),
    UNIQUE (UserId, PrasadId)
);
GO


-- ==========================================
-- SCHEMA: orders
-- ==========================================

CREATE TABLE [orders].[CartSessions] (
    CartId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NULL FOREIGN KEY REFERENCES [auth].[Users](UserId),
    SessionToken VARCHAR(100) UNIQUE,
    ExpiresAt DATETIME2 NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE TABLE [orders].[CartItems] (
    CartItemId INT IDENTITY(1,1) PRIMARY KEY,
    CartId INT NOT NULL FOREIGN KEY REFERENCES [orders].[CartSessions](CartId),
    PrasadId INT NOT NULL FOREIGN KEY REFERENCES [catalog].[Prasad](PrasadId),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(10,2) NOT NULL,
    AddedAt DATETIME2 DEFAULT GETUTCDATE(),
    UNIQUE (CartId, PrasadId)
);
GO

CREATE TABLE [orders].[Coupons] (
    CouponId INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(30) NOT NULL UNIQUE,
    DiscountType VARCHAR(20) NOT NULL,
    DiscountValue DECIMAL(10,2) NOT NULL,
    MinOrderAmount DECIMAL(10,2) DEFAULT 0,
    MaxDiscountCap DECIMAL(10,2) NULL,
    MaxUsageCount INT NULL,
    UsedCount INT DEFAULT 0,
    ValidFrom DATETIME2 NOT NULL,
    ValidUntil DATETIME2 NOT NULL,
    IsActive BIT DEFAULT 1
);
GO

CREATE TABLE [orders].[Orders] (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    OrderNumber VARCHAR(30) NOT NULL UNIQUE,
    UserId INT NOT NULL FOREIGN KEY REFERENCES [auth].[Users](UserId),
    AddressId INT NOT NULL FOREIGN KEY REFERENCES [auth].[UserAddresses](AddressId),
    CouponId INT NULL FOREIGN KEY REFERENCES [orders].[Coupons](CouponId),
    Status VARCHAR(30) NOT NULL DEFAULT 'Pending',
    SubTotal DECIMAL(10,2) NOT NULL,
    DeliveryCharges DECIMAL(10,2) DEFAULT 0,
    DiscountAmount DECIMAL(10,2) DEFAULT 0,
    TaxAmount DECIMAL(10,2) DEFAULT 0,
    TotalAmount DECIMAL(10,2) NOT NULL,
    DeliveryType VARCHAR(20) DEFAULT 'Standard',
    SpecialInstructions NVARCHAR(500) NULL,
    PlacedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO
CREATE INDEX IX_Orders_UserId ON [orders].[Orders](UserId);
CREATE INDEX IX_Orders_Status ON [orders].[Orders](Status);
CREATE INDEX IX_Orders_PlacedAt ON [orders].[Orders](PlacedAt);
GO

CREATE TABLE [orders].[OrderItems] (
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL FOREIGN KEY REFERENCES [orders].[Orders](OrderId),
    PrasadId INT NOT NULL FOREIGN KEY REFERENCES [catalog].[Prasad](PrasadId),
    PrasadName NVARCHAR(200) NOT NULL,
    TempleName NVARCHAR(200) NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    TotalPrice DECIMAL(10,2) NOT NULL,
    ReviewId INT NULL
);
GO


-- ==========================================
-- SCHEMA: payment
-- ==========================================

CREATE TABLE [payment].[Payments] (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL UNIQUE FOREIGN KEY REFERENCES [orders].[Orders](OrderId),
    RazorpayOrderId VARCHAR(100) UNIQUE,
    RazorpayPaymentId VARCHAR(100) NULL,
    RazorpaySignature VARCHAR(300) NULL,
    Amount DECIMAL(10,2) NOT NULL,
    Currency VARCHAR(3) DEFAULT 'INR',
    Method VARCHAR(30) NULL,
    Status VARCHAR(30) DEFAULT 'Pending',
    PaidAt DATETIME2 NULL,
    FailureReason NVARCHAR(500) NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE TABLE [payment].[PaymentAttempts] (
    AttemptId INT IDENTITY(1,1) PRIMARY KEY,
    PaymentId INT NOT NULL FOREIGN KEY REFERENCES [payment].[Payments](PaymentId),
    AttemptNumber INT NOT NULL,
    Gateway VARCHAR(30) DEFAULT 'Razorpay',
    Status VARCHAR(30) NOT NULL,
    ErrorCode VARCHAR(50) NULL,
    ErrorDescription NVARCHAR(500) NULL,
    AttemptedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE TABLE [payment].[Refunds] (
    RefundId INT IDENTITY(1,1) PRIMARY KEY,
    PaymentId INT NOT NULL FOREIGN KEY REFERENCES [payment].[Payments](PaymentId),
    RazorpayRefundId VARCHAR(100) UNIQUE,
    Amount DECIMAL(10,2) NOT NULL,
    Reason NVARCHAR(300) NULL,
    Status VARCHAR(20) DEFAULT 'Pending',
    ProcessedAt DATETIME2 NULL,
    InitiatedBy INT FOREIGN KEY REFERENCES [auth].[Users](UserId),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO


-- ==========================================
-- SCHEMA: delivery
-- ==========================================

CREATE TABLE [delivery].[DeliveryPartners] (
    PartnerId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Code VARCHAR(20) NOT NULL UNIQUE,
    TrackingUrlPattern NVARCHAR(300) NULL,
    SupportPhone VARCHAR(15) NULL,
    IsActive BIT DEFAULT 1
);
GO

CREATE TABLE [delivery].[Shipments] (
    ShipmentId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL UNIQUE FOREIGN KEY REFERENCES [orders].[Orders](OrderId),
    PartnerId INT NULL FOREIGN KEY REFERENCES [delivery].[DeliveryPartners](PartnerId),
    AWBNumber VARCHAR(60) UNIQUE,
    Status VARCHAR(40) DEFAULT 'PendingPickup',
    EstimatedDelivery DATE NULL,
    ActualDelivery DATETIME2 NULL,
    DeliveryAgentName NVARCHAR(100) NULL,
    DeliveryAgentPhone VARCHAR(15) NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE TABLE [delivery].[ShipmentEvents] (
    EventId INT IDENTITY(1,1) PRIMARY KEY,
    ShipmentId INT NOT NULL FOREIGN KEY REFERENCES [delivery].[Shipments](ShipmentId),
    EventType VARCHAR(50) NOT NULL,
    Description NVARCHAR(300) NOT NULL,
    Location NVARCHAR(200) NULL,
    Latitude DECIMAL(10,8) NULL,
    Longitude DECIMAL(11,8) NULL,
    OccurredAt DATETIME2 NOT NULL
);
GO
CREATE INDEX IX_ShipmentEvents_ShipmentId ON [delivery].[ShipmentEvents](ShipmentId);
CREATE INDEX IX_ShipmentEvents_OccurredAt ON [delivery].[ShipmentEvents](OccurredAt);
GO


-- ==========================================
-- SCHEMA: notification
-- ==========================================

CREATE TABLE [notification].[NotificationTemplates] (
    TemplateId INT IDENTITY(1,1) PRIMARY KEY,
    TemplateKey VARCHAR(80) NOT NULL UNIQUE,
    Channel VARCHAR(10) NOT NULL,
    Subject NVARCHAR(200) NULL,
    Body NVARCHAR(MAX) NOT NULL,
    IsActive BIT DEFAULT 1
);
GO

CREATE TABLE [notification].[NotificationLogs] (
    LogId BIGINT IDENTITY(1,1) PRIMARY KEY,
    TemplateId INT NULL FOREIGN KEY REFERENCES [notification].[NotificationTemplates](TemplateId),
    UserId INT NULL FOREIGN KEY REFERENCES [auth].[Users](UserId),
    Channel VARCHAR(10) NOT NULL,
    Recipient NVARCHAR(255) NOT NULL,
    Subject NVARCHAR(200) NULL,
    Body NVARCHAR(MAX) NOT NULL,
    Status VARCHAR(20) DEFAULT 'Queued',
    ErrorMessage NVARCHAR(500) NULL,
    SentAt DATETIME2 NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO


-- ==========================================
-- SCHEMA: admin
-- ==========================================

CREATE TABLE [admin].[AdminUsers] (
    AdminId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL UNIQUE FOREIGN KEY REFERENCES [auth].[Users](UserId),
    AdminRole VARCHAR(30) NOT NULL,
    Permissions NVARCHAR(MAX) NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE TABLE [admin].[AuditLogs] (
    AuditId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ActorUserId INT NULL FOREIGN KEY REFERENCES [auth].[Users](UserId),
    Action VARCHAR(80) NOT NULL,
    EntityType VARCHAR(60) NOT NULL,
    EntityId NVARCHAR(50) NULL,
    OldValues NVARCHAR(MAX) NULL,
    NewValues NVARCHAR(MAX) NULL,
    IpAddress VARCHAR(45) NULL,
    UserAgent NVARCHAR(300) NULL,
    OccurredAt DATETIME2 DEFAULT GETUTCDATE()
);
GO
CREATE INDEX IX_AuditLogs_EntityType ON [admin].[AuditLogs](EntityType);
CREATE INDEX IX_AuditLogs_ActorUserId ON [admin].[AuditLogs](ActorUserId);
GO

CREATE TABLE [admin].[SystemConfigs] (
    ConfigId INT IDENTITY(1,1) PRIMARY KEY,
    ConfigKey VARCHAR(100) NOT NULL UNIQUE,
    ConfigValue NVARCHAR(MAX) NOT NULL,
    Description NVARCHAR(300) NULL,
    IsEncrypted BIT DEFAULT 0,
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedBy INT NULL FOREIGN KEY REFERENCES [auth].[Users](UserId)
);
GO

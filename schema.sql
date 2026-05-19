-- Database Schema for Prashad Application
-- This script contains all table creation statements including Users, Temples, and new tables.

-- 1. Users Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
BEGIN
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(255) NOT NULL UNIQUE,
        MobileNumber NVARCHAR(20) NOT NULL,
        PasswordHash NVARCHAR(255) NOT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 2. Temples Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Temples' AND xtype='U')
BEGIN
    CREATE TABLE Temples (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL,
        Location NVARCHAR(255) NOT NULL,
        Description NVARCHAR(MAX) NOT NULL,
        ImageUrl NVARCHAR(500) NOT NULL,
        Rating DECIMAL(3,2) NOT NULL DEFAULT 0.0
    );
END
GO

-- 3. Prasad (Products) Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Prasad' AND xtype='U')
BEGIN
    CREATE TABLE Prasad (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TempleId INT NOT NULL,
        Name NVARCHAR(255) NOT NULL,
        Description NVARCHAR(MAX) NOT NULL,
        Price DECIMAL(18,2) NOT NULL,
        ImageUrl NVARCHAR(500) NOT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (TempleId) REFERENCES Temples(Id) ON DELETE CASCADE
    );
END
GO

-- 4. CartItems Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CartItems' AND xtype='U')
BEGIN
    CREATE TABLE CartItems (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        PrasadId INT NOT NULL,
        Quantity INT NOT NULL DEFAULT 1,
        AddedAt DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
        FOREIGN KEY (PrasadId) REFERENCES Prasad(Id) ON DELETE CASCADE
    );
END
GO

-- 5. Orders Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Orders' AND xtype='U')
BEGIN
    CREATE TABLE Orders (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        OrderNumber NVARCHAR(50) NOT NULL UNIQUE,
        UserId INT NOT NULL,
        TotalAmount DECIMAL(18,2) NOT NULL,
        Status NVARCHAR(50) NOT NULL DEFAULT 'Placed', -- e.g. Placed, Shipped, Delivered
        ShippingName NVARCHAR(255) NOT NULL,
        ShippingAddress NVARCHAR(MAX) NOT NULL,
        EstimatedDeliveryStart DATETIME NULL,
        EstimatedDeliveryEnd DATETIME NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
    );
END
GO

-- 6. OrderItems Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OrderItems' AND xtype='U')
BEGIN
    CREATE TABLE OrderItems (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL,
        PrasadId INT NOT NULL,
        Quantity INT NOT NULL,
        Price DECIMAL(18,2) NOT NULL, -- Price at the time of purchase
        FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
        FOREIGN KEY (PrasadId) REFERENCES Prasad(Id)
    );
END
GO

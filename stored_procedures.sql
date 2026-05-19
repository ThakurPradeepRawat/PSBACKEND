-- ==========================================
-- Stored Procedures for Prashad.com
-- ==========================================

-- ------------------------------------------
-- AUTH SCHEMA
-- ------------------------------------------

-- Create User
CREATE OR ALTER PROCEDURE [auth].[sp_CreateUser]
    @FirstName NVARCHAR(60),
    @LastName NVARCHAR(60),
    @Email NVARCHAR(255),
    @Phone VARCHAR(15) = NULL,
    @PasswordHash NVARCHAR(512),
    @ProfileImageUrl NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [auth].[Users] (FirstName, LastName, Email, Phone, PasswordHash, ProfileImageUrl)
    VALUES (@FirstName, @LastName, @Email, @Phone, @PasswordHash, @ProfileImageUrl);
    
    SELECT SCOPE_IDENTITY() AS UserId;
END
GO

-- Get User By Email
CREATE OR ALTER PROCEDURE [auth].[sp_GetUserByEmail]
    @Email NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM [auth].[Users] WHERE Email = @Email AND IsActive = 1;
END
GO

-- Get User By Id
CREATE OR ALTER PROCEDURE [auth].[sp_GetUserById]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM [auth].[Users] WHERE UserId = @UserId;
END
GO

-- ------------------------------------------
-- TEMPLE SCHEMA
-- ------------------------------------------

-- Get All Temples
CREATE OR ALTER PROCEDURE [temple].[sp_GetAllTemples]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        TempleId, Slug, Name, Deity, Description, City, State, PinCode, 
        Latitude, Longitude, CoverImageUrl, AverageRating, TotalReviews, 
        IsVerified, IsActive, Tags, CreatedAt 
    FROM [temple].[Temples] 
    WHERE IsActive = 1;
END
GO

-- Get Temple By Id
CREATE OR ALTER PROCEDURE [temple].[sp_GetTempleById]
    @TempleId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        TempleId, Slug, Name, Deity, Description, City, State, PinCode, 
        Latitude, Longitude, CoverImageUrl, AverageRating, TotalReviews, 
        IsVerified, IsActive, Tags, CreatedAt 
    FROM [temple].[Temples] 
    WHERE TempleId = @TempleId AND IsActive = 1;
END
GO

-- Create Temple
CREATE OR ALTER PROCEDURE [temple].[sp_CreateTemple]
    @Slug VARCHAR(120),
    @Name NVARCHAR(200),
    @Deity NVARCHAR(100),
    @Description NVARCHAR(MAX) = NULL,
    @City NVARCHAR(80),
    @State NVARCHAR(80),
    @PinCode VARCHAR(10) = NULL,
    @Latitude DECIMAL(10,8),
    @Longitude DECIMAL(11,8),
    @CoverImageUrl NVARCHAR(500) = NULL,
    @Tags NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [temple].[Temples] 
        (Slug, Name, Deity, Description, City, State, PinCode, Latitude, Longitude, CoverImageUrl, Tags)
    VALUES 
        (@Slug, @Name, @Deity, @Description, @City, @State, @PinCode, @Latitude, @Longitude, @CoverImageUrl, @Tags);
    
    SELECT SCOPE_IDENTITY() AS TempleId;
END
GO

-- ------------------------------------------
-- CATALOG SCHEMA
-- ------------------------------------------

-- Get Prasad By Temple Id
CREATE OR ALTER PROCEDURE [catalog].[sp_GetPrasadByTempleId]
    @TempleId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        PrasadId, TempleId, CategoryId, Slug, Name, ShortDesc, Description, 
        Price, OriginalPrice, WeightGrams, ShelfLifeDays, Ingredients, 
        IsGITagged, IsBestseller, IsActive, AverageRating, TotalReviews, CreatedAt
    FROM [catalog].[Prasad]
    WHERE TempleId = @TempleId AND IsActive = 1;
END
GO

-- Get Prasad By Id
CREATE OR ALTER PROCEDURE [catalog].[sp_GetPrasadById]
    @PrasadId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        PrasadId, TempleId, CategoryId, Slug, Name, ShortDesc, Description, 
        Price, OriginalPrice, WeightGrams, ShelfLifeDays, Ingredients, 
        IsGITagged, IsBestseller, IsActive, AverageRating, TotalReviews, CreatedAt
    FROM [catalog].[Prasad]
    WHERE PrasadId = @PrasadId AND IsActive = 1;
END
GO

-- Create Prasad
CREATE OR ALTER PROCEDURE [catalog].[sp_CreatePrasad]
    @TempleId INT,
    @CategoryId INT,
    @Slug VARCHAR(160),
    @Name NVARCHAR(200),
    @ShortDesc NVARCHAR(500) = NULL,
    @Description NVARCHAR(MAX) = NULL,
    @Price DECIMAL(10,2),
    @OriginalPrice DECIMAL(10,2) = NULL,
    @WeightGrams INT = NULL,
    @ShelfLifeDays INT = NULL,
    @Ingredients NVARCHAR(MAX) = NULL,
    @IsGITagged BIT = 0,
    @IsBestseller BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [catalog].[Prasad]
        (TempleId, CategoryId, Slug, Name, ShortDesc, Description, Price, OriginalPrice, 
         WeightGrams, ShelfLifeDays, Ingredients, IsGITagged, IsBestseller)
    VALUES
        (@TempleId, @CategoryId, @Slug, @Name, @ShortDesc, @Description, @Price, @OriginalPrice, 
         @WeightGrams, @ShelfLifeDays, @Ingredients, @IsGITagged, @IsBestseller);
         
    SELECT SCOPE_IDENTITY() AS PrasadId;
END
GO

-- ------------------------------------------
-- ORDERS SCHEMA
-- ------------------------------------------

-- Create Order
CREATE OR ALTER PROCEDURE [orders].[sp_CreateOrder]
    @OrderNumber VARCHAR(30),
    @UserId INT,
    @AddressId INT,
    @CouponId INT = NULL,
    @SubTotal DECIMAL(10,2),
    @DeliveryCharges DECIMAL(10,2) = 0,
    @DiscountAmount DECIMAL(10,2) = 0,
    @TaxAmount DECIMAL(10,2) = 0,
    @TotalAmount DECIMAL(10,2),
    @DeliveryType VARCHAR(20) = 'Standard',
    @SpecialInstructions NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [orders].[Orders] 
        (OrderNumber, UserId, AddressId, CouponId, SubTotal, DeliveryCharges, 
         DiscountAmount, TaxAmount, TotalAmount, DeliveryType, SpecialInstructions)
    VALUES 
        (@OrderNumber, @UserId, @AddressId, @CouponId, @SubTotal, @DeliveryCharges, 
         @DiscountAmount, @TaxAmount, @TotalAmount, @DeliveryType, @SpecialInstructions);
         
    SELECT SCOPE_IDENTITY() AS OrderId;
END
GO

-- Get Order By Id
CREATE OR ALTER PROCEDURE [orders].[sp_GetOrderById]
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM [orders].[Orders] WHERE OrderId = @OrderId;
END
GO

-- Create Order Item
CREATE OR ALTER PROCEDURE [orders].[sp_CreateOrderItem]
    @OrderId INT,
    @PrasadId INT,
    @PrasadName NVARCHAR(200),
    @TempleName NVARCHAR(200),
    @Quantity INT,
    @UnitPrice DECIMAL(10,2),
    @TotalPrice DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [orders].[OrderItems]
        (OrderId, PrasadId, PrasadName, TempleName, Quantity, UnitPrice, TotalPrice)
    VALUES
        (@OrderId, @PrasadId, @PrasadName, @TempleName, @Quantity, @UnitPrice, @TotalPrice);
        
    SELECT SCOPE_IDENTITY() AS OrderItemId;
END
GO

-- Get Order Items By Order Id
CREATE OR ALTER PROCEDURE [orders].[sp_GetOrderItems]
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM [orders].[OrderItems] WHERE OrderId = @OrderId;
END
GO

USE Prashad;
GO

-- Clear existing data if needed (optional)
DELETE FROM catalog.Prasad;
DELETE FROM catalog.PrasadCategories;
DELETE FROM temple.Temples;

-- 1. Insert Temples
INSERT INTO temple.Temples (Slug, Name, Deity, Description, City, State, PinCode, Latitude, Longitude, CoverImageUrl, AverageRating, TotalReviews, IsVerified, IsActive, Tags)
VALUES
('tirupati-balaji', 'Tirupati Balaji Temple', 'Lord Venkateswara', 'Tirupati Balaji (Sri Venkateswara Temple) is one of the most visited pilgrimage sites in the world, located on the seventh peak of Tirumala Hills in Andhra Pradesh. The temple is dedicated to Lord Venkateswara, a manifestation of Vishnu.', 'Tirupati', 'Andhra Pradesh', '517504', 13.6833, 79.3475, 'https://images.unsplash.com/photo-1590396009890-e435948f219f?auto=format&fit=crop&w=800&q=80', 4.9, 1500, 1, 1, 'Balaji, Vishnu, Tirumala'),

('vaishno-devi', 'Mata Vaishno Devi', 'Mata Vaishno Devi', 'The Vaishno Devi Temple is a prominent Hindu temple dedicated to the Hindu Goddess, located at the Trikuta Mountains within the Indian union territory of Jammu and Kashmir.', 'Katra', 'Jammu & Kashmir', '182320', 33.0298, 74.9482, 'https://images.unsplash.com/photo-1621255536306-056e4c703b4f?auto=format&fit=crop&w=800&q=80', 4.8, 2200, 1, 1, 'Mata, Durga, Katra'),

('kashi-vishwanath', 'Kashi Vishwanath Temple', 'Lord Shiva', 'Kashi Vishwanath Temple is one of the most famous Hindu temples dedicated to Lord Shiva. It is located in Varanasi, Uttar Pradesh, India.', 'Varanasi', 'Uttar Pradesh', '221001', 25.3109, 83.0107, 'https://images.unsplash.com/photo-1620616140404-58a36c6bb110?auto=format&fit=crop&w=800&q=80', 4.9, 3100, 1, 1, 'Shiva, Jyotirlinga, Varanasi');

-- 2. Insert Categories
INSERT INTO catalog.PrasadCategories (Name, Slug, SortOrder, IsActive)
VALUES
('Laddu', 'laddu', 1, 1),
('Dry Fruits', 'dry-fruits', 2, 1),
('Vibhuti & Kumkum', 'vibhuti-kumkum', 3, 1);

-- Get IDs
DECLARE @TirupatiId INT = (SELECT TempleId FROM temple.Temples WHERE Slug = 'tirupati-balaji');
DECLARE @VaishnoDeviId INT = (SELECT TempleId FROM temple.Temples WHERE Slug = 'vaishno-devi');
DECLARE @KashiId INT = (SELECT TempleId FROM temple.Temples WHERE Slug = 'kashi-vishwanath');

DECLARE @LadduCatId INT = (SELECT CategoryId FROM catalog.PrasadCategories WHERE Slug = 'laddu');
DECLARE @DryFruitsCatId INT = (SELECT CategoryId FROM catalog.PrasadCategories WHERE Slug = 'dry-fruits');
DECLARE @VibhutiCatId INT = (SELECT CategoryId FROM catalog.PrasadCategories WHERE Slug = 'vibhuti-kumkum');

-- 3. Insert Prasad Items
INSERT INTO catalog.Prasad (TempleId, CategoryId, Slug, Name, ShortDesc, Description, Price, OriginalPrice, WeightGrams, ShelfLifeDays, Ingredients, IsGITagged, IsBestseller, IsActive, AverageRating, TotalReviews)
VALUES
(@TirupatiId, @LadduCatId, 'tirupati-laddu-small', 'Tirupati Laddu Prasad (Small)', '150g • 1 laddu • Directly from temple TTD', 'The world-famous Tirupati Laddu (Venkateswara Laddu) — an authentic Geographical Indication protected prasad.', 750, 850, 150, 7, 'Pure ghee, besan, sugar, cashews, cardamom', 1, 1, 1, 4.8, 1200),

(@TirupatiId, @LadduCatId, 'tirupati-laddu-large', 'Tirupati Laddu Prasad (Large Pack)', '300g • 2 laddus • Premium gift box', 'Premium pack of two delicious and authentic Tirupati laddus.', 1200, 1400, 300, 7, 'Pure ghee, besan, sugar, cashews, cardamom', 1, 0, 1, 4.9, 450),

(@TirupatiId, @DryFruitsCatId, 'tirupati-combo', 'Tirupati Special Prasad Combo', 'Laddu + Dry Fruits + Sacred Kumkum', 'A divine combo containing laddu, assorted dry fruits, and sacred Kumkum.', 1499, 1800, 500, 15, 'Besan, ghee, cashews, almonds, raisins, kumkum', 0, 1, 1, 4.7, 300),

(@VaishnoDeviId, @DryFruitsCatId, 'vaishno-devi-dry-fruits', 'Mata Vaishno Devi Premium Prasad', 'Assorted Dry Fruits + Coins', 'Sacred offerings from the holy shrine of Mata Vaishno Devi.', 899, 1000, 250, 30, 'Walnuts, Almonds, Mishri (Sugar Candy)', 0, 1, 1, 4.9, 900),

(@KashiId, @VibhutiCatId, 'kashi-bhasma', 'Kashi Vishwanath Bhasma & Rudraksha', 'Sacred Bhasma with 108 bead Rudraksha mala', 'Holy Bhasma (vibhuti) straight from the Jyotirlinga in Kashi, paired with a sacred Rudraksha.', 1100, 1300, 100, 365, 'Sacred Ash (Vibhuti)', 0, 1, 1, 4.9, 1800);
GO

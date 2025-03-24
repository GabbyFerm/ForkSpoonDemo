CREATE TABLE Users (
    UserId INT NOT NULL IDENTITY PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(MAX) NOT NULL,
    Password NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Recipes (
    RecipeId INT NOT NULL IDENTITY PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    Ingredients NVARCHAR(MAX) NOT NULL,
    Steps NVARCHAR(MAX) NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    ImageUrl NVARCHAR(MAX) NOT NULL,
    CreatedBy INT NOT NULL,
    CONSTRAINT FK_Recipes_Users FOREIGN KEY (CreatedBy) REFERENCES Users(UserId) ON DELETE CASCADE
);

CREATE TABLE Favorites (
    FavoriteId INT NOT NULL IDENTITY PRIMARY KEY,
    UserId INT NOT NULL,
    RecipeId INT NOT NULL,
    DateFavorited DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Favorites_Users FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE NO ACTION,
    CONSTRAINT FK_Favorites_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes(RecipeId) ON DELETE NO ACTION
);

SELECT * FROM INFORMATION_SCHEMA.TABLES;

--//add dummy data
INSERT INTO Users (Username, Email, Password) VALUES ('testuser', 'test@example.com', 'password123');
INSERT INTO Recipes (Title, Ingredients, Steps, Category, ImageUrl, CreatedBy) VALUES ('Test Recipe', 'Test Ingredients', 'Test Steps', 'Test Category', 'test.jpg', 1);
INSERT INTO Favorites (UserId, RecipeId) VALUES (1, 1);
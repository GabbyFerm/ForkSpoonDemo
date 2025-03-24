-- Create the Users table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(MAX) NOT NULL,
    Password NVARCHAR(MAX) NOT NULL
);

-- Create the Recipes table
CREATE TABLE Recipes (
    RecipeId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    Ingredients NVARCHAR(MAX) NOT NULL,
    Steps NVARCHAR(MAX) NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    ImageUrl NVARCHAR(MAX) NOT NULL,
    CreatedBy INT NOT NULL,
    CONSTRAINT FK_Recipes_Users FOREIGN KEY (CreatedBy) REFERENCES Users(UserId) ON DELETE CASCADE
);

DROP TABLE IF EXISTS Favorites;
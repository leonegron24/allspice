CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'User Name',
    email VARCHAR(255) UNIQUE COMMENT 'User Email',
    picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

-- Ingredients
CREATE TABLE ingredients (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    name VARCHAR(255) NOT NULL,
    quantity VARCHAR(255) NOT NULL,
    recipe_id INT NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipes (id) ON DELETE CASCADE
)

DROP TABLE IF EXISTS favorites;

DELETE FROM favorites;

INSERT INTO
    favorites (recipe_id, account_id)
values (@RecipeId, @AccountId);

SELECT favorites.*, recipes.*
FROM
    favorites
    INNER JOIN recipes ON recipes.id = favorites.recipe_id
    INNER JOIN acounts ON favorites.acount_id = accounts.id
WHERE
    favorites.id = LAST_INSERT_ID();

-- SELECT INGREDIENTS
SELECT * FROM favorites

SELECT ingredients.*, recipes.*
FROM ingredients
    INNER JOIN recipes ON recipes.id = ingredients.recipe_id
WHERE
    ingredients.id = 1;

-- Create Ingredients
INSERT INTO
    ingredients (name, quantity, recipe_id)
values ('Onion', 2, 6);

-- Get Ingredients for Recipe
Select ingredients.*, recipes.*
From ingredients
    INNER JOIN recipes ON recipes.id = ingredients.recipe_id
WHERE
    recipes.id = 1

-- Get Ingredient By Id
Select * FROM ingredients WHERE ingredients.id = 2;

-- Recipe
CREATE TABLE recipes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    title VARCHAR(255) NOT NULL,
    instructions VARCHAR(5000),
    img VARCHAR(1000) NOT NULL,
    category ENUM(
        'breakfast',
        'lunch',
        'dinner',
        'snack',
        'dessert'
    ) NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    Foreign Key (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

-- CREATE RECIPE
INSERT INTO
    recipes (
        title,
        instructions,
        img,
        category,
        creator_id
    )
values (
        'Salsa',
        'mix the veges together',
        'https://images.unsplash.com/photo-1713374989663-e5b165462fef?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8c2Fsc2F8ZW58MHx8MHx8fDA%3D',
        'snack',
        '685df65b38949a162240dacd'
    );

-- SELECT RECIPES
SELECT * FROM recipes

SELECT recipes.*, accounts.*
FROM recipes
    INNER JOIN accounts ON accounts.id = recipes.creator_id
WHERE
    recipes.id = LAST_INSERT_ID();

-- UPDATE RECIPE
UPDATE recipes SET recipe = WHERE recipes.id = 5

-- DELETE RECIPE
DELETE FROM recipes WHERE id = 5 LIMIT 1;

-- Favorite
CREATE TABLE favorites (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    recipe_id INT NOT NULL,
    account_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE
)

-- Get Account Favorite Recipes
SELECT favorites.*, recipes.*
FROM favorites
    RIGHT JOIN recipes ON favorites.recipe_id = recipes.id
WHERE
    favorites.account_id = '685df65b38949a162240dacd'

SELECT favorites.*, accounts.*
FROM favorites
    INNER JOIN accounts ON favorites.account_id = accounts.id
WHERE
    accounts.id = '685df65b38949a162240dacd'

DELETE FROM favorites WHERE id = 2 LIMIT 1;

SELECT * FROM favorites WHERE id = 2;

SELECT *
FROM favorites
WHERE
    favorites.account_id = '685df65b38949a162240dacd';

SELECT * FROM accounts;

SELECT recipes.*, COUNT(favorites.id) AS favoriteCount, accounts.*
FROM
    recipes
    INNER JOIN accounts ON accounts.id = recipes.creator_id
    LEFT JOIN favorites ON favorites.recipe_id = recipes.id
WHERE
    recipes.id = 169

SELECT * FROM favorites WHERE recipe_id = 168

Select * From ingredients WHERE ingredients.recipe_id = 23

SELECT *
FROM recipes
    LEFT JOIN ingredients ON ingredients.recipe_id = recipes.id
WHERE
    recipes.id = 19
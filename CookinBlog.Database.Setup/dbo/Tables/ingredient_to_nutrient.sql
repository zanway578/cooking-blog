CREATE TABLE [dbo].[ingredient_to_nutrient] (
    [ID]                  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IngredientID]        UNIQUEIDENTIFIER NOT NULL,
    [NutrientID]          UNIQUEIDENTIFIER NOT NULL,
    [NutrientAmount]      REAL             NOT NULL,
    [NutrientMeasurement] VARCHAR (12)     NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([IngredientID]) REFERENCES [dbo].[ingredient] ([ID]),
    FOREIGN KEY ([NutrientID]) REFERENCES [dbo].[nutrient] ([ID]),
    UNIQUE NONCLUSTERED ([IngredientID] ASC, [NutrientID] ASC, [NutrientMeasurement] ASC)
);


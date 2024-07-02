CREATE TABLE [dbo].[recipe_group_ingredient] (
    [ID]                           UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IngredientID]                 UNIQUEIDENTIFIER NOT NULL,
    [MeasurementID]                UNIQUEIDENTIFIER NOT NULL,
    [MeasurementAmount]            INT              NOT NULL,
    [IngredientNumber]             INT              NOT NULL,
    [MeasurementAmountNumerator]   INT              NULL,
    [MeasurementAmountDenominator] INT              NULL,
    [RecipeGroupID]                UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([IngredientID]) REFERENCES [dbo].[ingredient] ([ID]),
    FOREIGN KEY ([MeasurementID]) REFERENCES [dbo].[measurement] ([ID]),
    FOREIGN KEY ([RecipeGroupID]) REFERENCES [dbo].[recipe_group] ([ID]),
    UNIQUE NONCLUSTERED ([RecipeGroupID] ASC, [IngredientID] ASC),
    UNIQUE NONCLUSTERED ([RecipeGroupID] ASC, [IngredientNumber] ASC)
);


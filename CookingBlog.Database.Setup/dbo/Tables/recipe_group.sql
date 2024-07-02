CREATE TABLE [dbo].[recipe_group] (
    [ID]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (128)   NOT NULL,
    [GroupNumber] INT              NOT NULL,
    [RecipeID]    UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([RecipeID]) REFERENCES [dbo].[recipe] ([ID]),
    UNIQUE NONCLUSTERED ([RecipeID] ASC, [GroupNumber] ASC)
);


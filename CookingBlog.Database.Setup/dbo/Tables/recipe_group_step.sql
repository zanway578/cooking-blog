CREATE TABLE [dbo].[recipe_group_step] (
    [ID]            UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [StepNumber]    INT              NOT NULL,
    [StepText]      NVARCHAR (MAX)   NULL,
    [RecipeGroupID] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([RecipeGroupID]) REFERENCES [dbo].[recipe_group] ([ID]),
    UNIQUE NONCLUSTERED ([RecipeGroupID] ASC, [StepNumber] ASC)
);


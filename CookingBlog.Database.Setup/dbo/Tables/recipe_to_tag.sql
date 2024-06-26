﻿CREATE TABLE [dbo].[recipe_to_tag] (
    [ID]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [RecipeID] UNIQUEIDENTIFIER NOT NULL,
    [TagID]    UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    UNIQUE NONCLUSTERED ([RecipeID] ASC, [TagID] ASC)
);


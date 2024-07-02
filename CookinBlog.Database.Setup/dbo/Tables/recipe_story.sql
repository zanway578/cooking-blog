CREATE TABLE [dbo].[recipe_story] (
    [ID]              UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [RecipeID]        UNIQUEIDENTIFIER NOT NULL,
    [ParagraphNumber] INT              NOT NULL,
    [StoryText]       NVARCHAR (MAX)   NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([RecipeID]) REFERENCES [dbo].[recipe] ([ID]),
    UNIQUE NONCLUSTERED ([RecipeID] ASC, [ParagraphNumber] ASC)
);


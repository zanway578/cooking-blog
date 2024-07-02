CREATE TABLE [dbo].[recipe_note] (
    [ID]         UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [RecipeID]   UNIQUEIDENTIFIER NOT NULL,
    [NoteNumber] INT              NOT NULL,
    [NoteText]   NVARCHAR (2048)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([RecipeID]) REFERENCES [dbo].[recipe] ([ID]),
    UNIQUE NONCLUSTERED ([RecipeID] ASC, [NoteNumber] ASC)
);


CREATE TABLE [dbo].[recipe_to_media] (
    [ID]              UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Data]            VARBINARY (MAX)  NOT NULL,
    [RecipeID]        UNIQUEIDENTIFIER NOT NULL,
    [MediaNumber]     INT              NOT NULL,
    [FileExtensionID] UNIQUEIDENTIFIER NOT NULL,
    [MediaCategory]   INT              NOT NULL,
    [FileName]        NVARCHAR (128)   NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([FileExtensionID]) REFERENCES [dbo].[file_extension] ([ID]),
    FOREIGN KEY ([RecipeID]) REFERENCES [dbo].[recipe] ([ID]),
    UNIQUE NONCLUSTERED ([FileName] ASC),
    UNIQUE NONCLUSTERED ([RecipeID] ASC, [MediaNumber] ASC)
);


CREATE TABLE [dbo].[recipe_media_to_sized_media] (
    [ID]              UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [RecipeMediaID]   UNIQUEIDENTIFIER NOT NULL,
    [MediaSizeTypeID] UNIQUEIDENTIFIER NOT NULL,
    [Data]            VARBINARY (MAX)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([MediaSizeTypeID]) REFERENCES [dbo].[media_size_type] ([ID]),
    FOREIGN KEY ([RecipeMediaID]) REFERENCES [dbo].[recipe_to_media] ([ID]),
    UNIQUE NONCLUSTERED ([RecipeMediaID] ASC, [MediaSizeTypeID] ASC)
);


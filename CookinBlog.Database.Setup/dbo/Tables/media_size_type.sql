CREATE TABLE [dbo].[media_size_type] (
    [ID]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name] NVARCHAR (256)   NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


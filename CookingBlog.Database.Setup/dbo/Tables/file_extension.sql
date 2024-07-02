CREATE TABLE [dbo].[file_extension] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]        NVARCHAR (32)    NOT NULL,
    [Extension]   NVARCHAR (32)    NOT NULL,
    [MediaTypeID] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([MediaTypeID]) REFERENCES [dbo].[media_type] ([ID]),
    UNIQUE NONCLUSTERED ([Extension] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);


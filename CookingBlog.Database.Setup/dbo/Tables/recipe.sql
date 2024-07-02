CREATE TABLE [dbo].[recipe] (
    [ID]               UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]             NVARCHAR (256)   NOT NULL,
    [JsonData]         NVARCHAR (MAX)   NULL,
    [IsPublished]      BIT              DEFAULT ((0)) NOT NULL,
    [Description]      NVARCHAR (1024)  NOT NULL,
    [PrepTime]         INT              NOT NULL,
    [CookTime]         INT              NOT NULL,
    [CreatedById]      UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedTime] DATETIME2 (7)    NULL,
    [CreatedTime]      DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [NumberServings]   INT              DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    UNIQUE NONCLUSTERED ([Name] ASC)
);


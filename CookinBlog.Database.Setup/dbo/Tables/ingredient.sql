CREATE TABLE [dbo].[ingredient] (
    [ID]                        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]                      NVARCHAR (256)   NOT NULL,
    [NumberGramsInACup]         INT              NULL,
    [CaloriesInOneHundredGrams] INT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);


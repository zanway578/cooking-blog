CREATE TABLE [dbo].[measurement] (
    [ID]              UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]            NVARCHAR (64)    NOT NULL,
    [UnitName]        NVARCHAR (32)    NOT NULL,
    [IsVolumetric]    BIT              DEFAULT ((0)) NOT NULL,
    [ConversionRatio] REAL             NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC),
    UNIQUE NONCLUSTERED ([UnitName] ASC)
);


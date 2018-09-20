CREATE TABLE [dbo].[tblItems] (
    [ID]             INT             IDENTITY (1, 1) NOT NULL,
    [Image1]         VARBINARY (MAX) NULL,
    [Image2]         VARBINARY (MAX) NULL,
    [Name]           NVARCHAR (MAX)  NOT NULL,
    [Manufacturer]   NVARCHAR (MAX)  NULL,
    [Size]           NVARCHAR (MAX)  NULL,
    [Source]         NVARCHAR (MAX)  NULL,
    [Value]          FLOAT (53)      NOT NULL,
    [Qauntity]       INT             NOT NULL,
    [ValueSource]    NVARCHAR (MAX)  NULL,
    [StoredLocation] NVARCHAR (MAX)  NULL,
    [Comments]       NVARCHAR (MAX)  NULL,
    [Descritption]   NVARCHAR (MAX)  NULL,
    [tags]           NVARCHAR (MAX)  NOT NULL,
    CONSTRAINT [PK_dbo.tblItems] PRIMARY KEY CLUSTERED ([ID] ASC)
);


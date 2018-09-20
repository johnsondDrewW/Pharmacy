CREATE TABLE [dbo].[tblCategories] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX) NULL,
    [ParentCategory] NVARCHAR (MAX) NULL,
    [Description]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.tblCategories] PRIMARY KEY CLUSTERED ([ID] ASC)
);


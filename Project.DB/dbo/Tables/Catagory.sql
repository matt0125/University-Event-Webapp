CREATE TABLE [dbo].[Catagory] (
    [c_id]    INT          IDENTITY (1, 1) NOT NULL,
    [name]    VARCHAR (50) NOT NULL,
    [entered] DATETIME     CONSTRAINT [DF_Catagory_entered] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Catagory] PRIMARY KEY CLUSTERED ([c_id] ASC)
);


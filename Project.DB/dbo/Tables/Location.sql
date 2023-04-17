CREATE TABLE [dbo].[Location] (
    [location_id] INT             IDENTITY (1000, 1) NOT NULL,
    [name]        VARCHAR (50)    NOT NULL,
    [lattitude]   DECIMAL (18, 6) NOT NULL,
    [longitude]   DECIMAL (18, 6) NOT NULL,
    [description] VARCHAR (500)   NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([location_id] ASC)
);




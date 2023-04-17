CREATE TABLE [dbo].[RSO] (
    [rso_id]      INT           IDENTITY (1, 1) NOT NULL,
    [name]        CHAR (30)     NOT NULL,
    [uni_id]      INT           NOT NULL,
    [status]      TINYINT       CONSTRAINT [DF_RSO_status] DEFAULT ((0)) NOT NULL,
    [created_by]  INT           NOT NULL,
    [description] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_RSO] PRIMARY KEY CLUSTERED ([rso_id] ASC),
    CONSTRAINT [FK_RSO_University] FOREIGN KEY ([uni_id]) REFERENCES [dbo].[University] ([uni_id]),
    CONSTRAINT [FK_RSO_User] FOREIGN KEY ([created_by]) REFERENCES [dbo].[User] ([user_id])
);




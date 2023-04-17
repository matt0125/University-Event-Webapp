CREATE TABLE [dbo].[University] (
    [uni_id]       INT            IDENTITY (1, 1) NOT NULL,
    [location_id]  INT            NOT NULL,
    [name]         VARCHAR (100)  NOT NULL,
    [description]  VARCHAR (5000) NOT NULL,
    [num_students] INT            NOT NULL,
    [created_by]   INT            NOT NULL,
    CONSTRAINT [PK_University] PRIMARY KEY CLUSTERED ([uni_id] ASC),
    CONSTRAINT [FK_University_Location] FOREIGN KEY ([location_id]) REFERENCES [dbo].[Location] ([location_id]),
    CONSTRAINT [SuperAdmin] FOREIGN KEY ([created_by]) REFERENCES [dbo].[User] ([user_id])
);


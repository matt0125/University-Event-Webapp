CREATE TABLE [dbo].[User] (
    [AspNetUser_id] NVARCHAR (450) NOT NULL,
    [user_id]       INT            IDENTITY (1000, 1) NOT NULL,
    [uni_id]        INT            NOT NULL,
    [first_name]    VARCHAR (50)   NOT NULL,
    [last_name]     VARCHAR (50)   NOT NULL,
    [isAdmin]       BIT            CONSTRAINT [DF_User_isAdmin] DEFAULT ((0)) NOT NULL,
    [isSuperAdmin]  BIT            CONSTRAINT [DF_User_isSuperAdmin] DEFAULT ((0)) NOT NULL,
    [isStudent]     BIT            CONSTRAINT [DF_User_isStudent] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([user_id] ASC),
    CONSTRAINT [FK_User_AspNetUsers] FOREIGN KEY ([AspNetUser_id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_User_University1] FOREIGN KEY ([uni_id]) REFERENCES [dbo].[University] ([uni_id])
);




GO
CREATE NONCLUSTERED INDEX [IX_User]
    ON [dbo].[User]([uni_id] ASC);


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER tr_increment_num_students
ON [dbo].[User]
AFTER INSERT
AS
BEGIN
    UPDATE [dbo].[University]
    SET num_students = num_students + 1
    WHERE uni_id IN (SELECT uni_id FROM inserted)
END
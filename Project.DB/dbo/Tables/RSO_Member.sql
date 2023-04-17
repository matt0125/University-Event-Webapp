CREATE TABLE [dbo].[RSO_Member] (
    [member_id] INT     IDENTITY (1, 1) NOT NULL,
    [rso_id]    INT     NOT NULL,
    [user_id]   INT     NOT NULL,
    [is_admin]  BIT     CONSTRAINT [DF_RSO_Member_is_admin] DEFAULT ((0)) NOT NULL,
    [status]    TINYINT CONSTRAINT [DF_RSO_Member_status] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RSO_Member] PRIMARY KEY CLUSTERED ([member_id] ASC),
    CONSTRAINT [FK_RSO_Member_RSO] FOREIGN KEY ([rso_id]) REFERENCES [dbo].[RSO] ([rso_id]),
    CONSTRAINT [FK_RSO_Member_User] FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([user_id])
);




GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[trg_UpdateRSOStatus]
ON [dbo].[RSO_Member]
AFTER UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT rso_id
        FROM inserted
        WHERE status = 1
       
    )
    BEGIN
        UPDATE RSO
        SET status = 1
        WHERE rso_id = (
            SELECT rso_id
            FROM RSO_Member
			WHERE status = 1
			AND rso_id = (select rso_id from inserted)
            GROUP BY rso_id
            HAVING COUNT(*) >= 5
        );
    END
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRigger [dbo].[trg_OnDeleteRSOStatus]
ON [dbo].[RSO_Member]
AFTER DELETE
AS
BEGIN
    
    BEGIN
        UPDATE RSO
        SET status = 4
        WHERE rso_id = (
            SELECT rso_id
            FROM RSO_Member
			WHERE status = 1
			AND rso_id = (select rso_id from DELETED )
            GROUP BY rso_id
            HAVING COUNT(*) < 5
        );
    END
END
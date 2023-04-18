CREATE TABLE [dbo].[Event_Picture] (
    [p_id]    INT             IDENTITY (1, 1) NOT NULL,
    [e_id]    INT             NOT NULL,
    [picture] VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_Event_Picture] PRIMARY KEY CLUSTERED ([p_id] ASC),
    CONSTRAINT [FK_Event_Picture_Event] FOREIGN KEY ([e_id]) REFERENCES [dbo].[Event] ([e_id]) ON DELETE CASCADE
);




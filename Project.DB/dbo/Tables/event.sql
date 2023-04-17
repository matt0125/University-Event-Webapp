CREATE TABLE [dbo].[Event] (
    [e_id]        INT            IDENTITY (1, 1) NOT NULL,
    [location_id] INT            NOT NULL,
    [rso_id]      INT            NULL,
    [uni_id]      INT            NOT NULL,
    [name]        CHAR (30)      NOT NULL,
    [c_id]        INT            NOT NULL,
    [visibility]  VARCHAR (50)   NOT NULL,
    [description] VARCHAR (5000) NOT NULL,
    [start_time]  DATETIME       NOT NULL,
    [end_time]    DATETIME       NOT NULL,
    [status]      TINYINT        CONSTRAINT [DF_event_status] DEFAULT ((0)) NOT NULL,
    [phone]       VARCHAR (50)   NOT NULL,
    [email]       VARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_event_1] PRIMARY KEY CLUSTERED ([e_id] ASC),
    CONSTRAINT [FK_event_Catagory] FOREIGN KEY ([c_id]) REFERENCES [dbo].[Catagory] ([c_id]),
    CONSTRAINT [FK_event_event] FOREIGN KEY ([e_id]) REFERENCES [dbo].[Event] ([e_id]),
    CONSTRAINT [FK_event_Location] FOREIGN KEY ([location_id]) REFERENCES [dbo].[Location] ([location_id]),
    CONSTRAINT [FK_event_RSO1] FOREIGN KEY ([rso_id]) REFERENCES [dbo].[RSO] ([rso_id]),
    CONSTRAINT [FK_event_University] FOREIGN KEY ([uni_id]) REFERENCES [dbo].[University] ([uni_id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Event_Uni]
    ON [dbo].[Event]([uni_id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Event_Name]
    ON [dbo].[Event]([name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Event_Catagory]
    ON [dbo].[Event]([c_id] ASC);


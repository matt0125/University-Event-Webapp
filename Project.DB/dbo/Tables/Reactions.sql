CREATE TABLE [dbo].[Reactions] (
    [reaction_id] INT           IDENTITY (1, 1) NOT NULL,
    [e_id]        INT           NOT NULL,
    [user_id]     INT           NOT NULL,
    [comment]     VARCHAR (500) NULL,
    [rating]      REAL          NULL,
    [save]        BIT           CONSTRAINT [DF_Reactions_save] DEFAULT ((0)) NOT NULL,
    [rsvp]        BIT           CONSTRAINT [DF_Reactions_rsvp] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Reactions] PRIMARY KEY CLUSTERED ([reaction_id] ASC),
    CONSTRAINT [FK_Reactions_event] FOREIGN KEY ([e_id]) REFERENCES [dbo].[event] ([e_id]),
    CONSTRAINT [FK_Reactions_User] FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([user_id])
);


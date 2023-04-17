CREATE TABLE [dbo].[Uni_Picture] (
    [p_id]    INT             IDENTITY (1, 1) NOT NULL,
    [uni_id]  INT             NOT NULL,
    [picture] VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_Uni_Picture] PRIMARY KEY CLUSTERED ([p_id] ASC),
    CONSTRAINT [FK_Uni_Picture_University] FOREIGN KEY ([uni_id]) REFERENCES [dbo].[University] ([uni_id])
);


CREATE TABLE [dbo].[Mail] (
    [MailID]      INT            NOT NULL,
    [SenderMail]  NVARCHAR (50)  NOT NULL,
    [Recivermail] NVARCHAR (50)  NOT NULL,
    [Subject]     NVARCHAR (20)  NULL,
    [Body]        NVARCHAR (MAX) NULL,
    [SendEmailTO] NVARCHAR (30)  NULL,
    PRIMARY KEY CLUSTERED ([MailID] ASC)
);
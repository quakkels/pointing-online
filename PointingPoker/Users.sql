CREATE TABLE [dbo].[Users]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Email] VARCHAR(256) NOT NULL, 
    [PasswordHash] VARCHAR(512) NULL
)

GO

CREATE INDEX [IX_Users_Column] ON [dbo].[Users] (Username)

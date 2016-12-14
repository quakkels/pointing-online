CREATE TABLE [dbo].[Cards]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Description] NVARCHAR(512) NOT NULL, 
    [CreatedBy] INT NOT NULL FOREIGN KEY (CreatedBy) REFERENCES Users(Id), 
    [ClosedBy] INT NULL FOREIGN KEY (ClosedBy) REFERENCES Users(Id), 
    [TeamId] INT NOT NULL FOREIGN KEY (TeamId) REFERENCES Teams(Id), 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE()
)

GO


CREATE INDEX [IX_Cards_DateCreated] ON [dbo].[Cards] ([DateCreated])

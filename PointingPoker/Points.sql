CREATE TABLE [dbo].[Points]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[PointedBy] uniqueidentifier NOT NULL FOREIGN KEY (PointedBy) REFERENCES Users(Id), 
    [CardId] INT NOT NULL FOREIGN KEY (CardId) REFERENCES Cards(Id),
	[Points] INT NOT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE() 
)

GO

CREATE INDEX [IX_Points_DateCreated] ON [dbo].[Points] ([DateCreated])

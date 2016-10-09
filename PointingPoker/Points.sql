CREATE TABLE [dbo].[Points]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[PointedBy] uniqueidentifier NOT NULL FOREIGN KEY (PointedBy) REFERENCES Users(Id), 
    [CardId] uniqueidentifier NOT NULL FOREIGN KEY (CardId) REFERENCES Cards(Id),
	[Points] smallint NOT NULL 
)

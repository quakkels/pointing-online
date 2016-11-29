﻿CREATE TABLE [dbo].[Teams]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(256) NOT NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
)

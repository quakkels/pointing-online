CREATE TABLE [dbo].[TeamMembers]
(
    [TeamId] INT NOT NULL FOREIGN KEY (TeamId) REFERENCES Teams(Id), 
    [UserId] INT NOT NULL FOREIGN KEY (UserId) REFERENCES Users(Id), 
    PRIMARY KEY ([TeamId], [UserId])
)

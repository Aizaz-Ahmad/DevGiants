CREATE TABLE [dbo].[admin] (
    [admin_id] INT          IDENTITY (1, 1) NOT NULL,
    [username] VARCHAR (50) NOT NULL,
    [password] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([admin_id] ASC)
);

--RUN THE BELOW CODE IN SEPERATE QUERY--

SET IDENTITY_INSERT [dbo].[admin] ON
INSERT INTO [dbo].[admin] ([admin_id], [username], [password]) VALUES (1, N'aizazahmad', N'aizaz')
SET IDENTITY_INSERT [dbo].[admin] OFF

SET IDENTITY_INSERT [dbo].[Announcements] ON
INSERT INTO [dbo].[Announcements] ([Id], [Title], [CarouselContent], [ImagePath], [IsActive]) VALUES (1, N'Christmas', N'Happy Christmas', N'\Content\Images\Christmas.png', 1)
INSERT INTO [dbo].[Announcements] ([Id], [Title], [CarouselContent], [ImagePath], [IsActive]) VALUES (2, N'New Year', N'Happy New Year', N'\Content\Images\New Year.jpg', 1)
INSERT INTO [dbo].[Announcements] ([Id], [Title], [CarouselContent], [ImagePath], [IsActive]) VALUES (3, N'Computer Vision', N'Computer Cision', N'\Content\Images\computer-vision-ai-enterprise.jpg', 1)
SET IDENTITY_INSERT [dbo].[Announcements] OFF

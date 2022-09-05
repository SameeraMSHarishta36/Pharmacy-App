GO
SET IDENTITY_INSERT [dbo].[Roles] ON
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'User')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [PhoneNumber], [EmailConfirmed], [CreatedDate]) VALUES (1, N'Admin', N'admin@gmail.com', N'$2a$11$NulP7XYlUOjMELsrj/me0uO/1OIQiHnMl.DVUk7LgB5SqjyWSas5K', N'9876543210', 0, CAST(N'2021-12-21T11:03:11.457' AS DateTime))
GO
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [PhoneNumber], [EmailConfirmed], [CreatedDate]) VALUES (2, N'User', N'user@gmail.com', N'$2a$11$oNn03spA.XrRD8shVW9Z2.72X6ljCU/S6fjOZOTybNVFLtEr6Kb5y', N'9876543210', 0, CAST(N'2021-12-21T11:05:19.160' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 1)
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (2, 2)
GO

SET IDENTITY_INSERT [dbo].[Categories] ON
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, 'Capsules')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, 'Topical medicines')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, 'Inhalers')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemTypes] ON
GO
INSERT [dbo].[ItemTypes] ([Id], [Name]) VALUES (1, 'Liquid')
GO
INSERT [dbo].[ItemTypes] ([Id], [Name]) VALUES (2, 'Tablet')
GO
SET IDENTITY_INSERT [dbo].[ItemTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Items] ON
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId], CreatedDate) VALUES (1, 'Metformin', ' combination of candelila wax, Cellulose acetate,hypromelose', CAST(299.00 AS Decimal(18, 2)), '/images/Metformin.webp', 1, 1, GETDATE())
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId], CreatedDate) VALUES (2, 'Losartan', 'Combination of lactose monohydrate,colloidal sillica', CAST(399.00 AS Decimal(18, 2)), '/images/Metformin.webp', 1, 1, GETDATE())
GO

SET IDENTITY_INSERT [dbo].[Items] OFF
GO
CREATE VIEW dbo.RSOEvents
AS
SELECT        dbo.[User].user_id, dbo.RSO_Member.is_admin, dbo.RSO.name AS rso_name, dbo.Event.e_id, dbo.Event.location_id, dbo.Event.name, dbo.Event.c_id, dbo.Event.visibility, dbo.Event.description, dbo.Event.start_time, 
                         dbo.Event.end_time, dbo.Event.status, dbo.Event.phone, dbo.Event.email, dbo.RSO.rso_id
FROM            dbo.RSO_Member INNER JOIN
                         dbo.RSO ON dbo.RSO_Member.rso_id = dbo.RSO.rso_id INNER JOIN
                         dbo.[User] ON dbo.RSO_Member.user_id = dbo.[User].user_id INNER JOIN
                         dbo.AspNetUsers ON dbo.[User].AspNetUser_id = dbo.AspNetUsers.Id INNER JOIN
                         dbo.Event ON dbo.RSO.rso_id = dbo.Event.rso_id
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RSOEvents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'50
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RSOEvents';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RSO_Member"
            Begin Extent = 
               Top = 182
               Left = 560
               Bottom = 342
               Right = 730
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RSO"
            Begin Extent = 
               Top = 81
               Left = 882
               Bottom = 256
               Right = 1052
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 209
               Left = 306
               Bottom = 339
               Right = 476
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 39
               Left = 28
               Bottom = 367
               Right = 252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Event"
            Begin Extent = 
               Top = 12
               Left = 578
               Bottom = 142
               Right = 748
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 13', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RSOEvents';




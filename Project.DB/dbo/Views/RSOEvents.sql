﻿CREATE VIEW dbo.RSOEvents
AS
SELECT        dbo.[User].user_id, dbo.RSO_Member.is_admin, dbo.RSO.name AS Expr2, dbo.RSO.rso_id AS Expr3, dbo.event.*
FROM            dbo.RSO_Member INNER JOIN
                         dbo.RSO ON dbo.RSO_Member.rso_id = dbo.RSO.rso_id INNER JOIN
                         dbo.[User] ON dbo.RSO_Member.user_id = dbo.[User].user_id INNER JOIN
                         dbo.AspNetUsers ON dbo.[User].AspNetUser_id = dbo.AspNetUsers.Id INNER JOIN
                         dbo.event ON dbo.RSO.rso_id = dbo.event.rso_id
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RSOEvents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'0
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
         Begin Table = "User"
            Begin Extent = 
               Top = 50
               Left = 319
               Bottom = 263
               Right = 489
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RSO"
            Begin Extent = 
               Top = 44
               Left = 818
               Bottom = 219
               Right = 988
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RSO_Member"
            Begin Extent = 
               Top = 39
               Left = 567
               Bottom = 199
               Right = 737
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "event"
            Begin Extent = 
               Top = 40
               Left = 1060
               Bottom = 281
               Right = 1230
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
         Or = 135', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RSOEvents';

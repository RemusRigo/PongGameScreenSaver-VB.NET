'--------------------------------------------------------------------------------------------------
' PongGameSS: AppSettings.vb: Class to Save/Load settings to the Registry
'    © 2026 Remus Rigo
'       v1.0.20261112
'--------------------------------------------------------------------------------------------------

Imports Microsoft.Win32

Public Class AppSettings

   Private Const regAppPath As String = "Software\Remus RIGO\PongGameSS.VB-NET"

   Public Property OneGamePerMonitor As Boolean = False

   '-----------------------------------------------------------------------------------------------
   ' Save
   Public Sub Save()
      Try
         Using key As RegistryKey = Registry.CurrentUser.CreateSubKey(regAppPath)
            If key Is Nothing Then Return
            key.SetValue(NameOf(OneGamePerMonitor), OneGamePerMonitor, RegistryValueKind.DWord)
         End Using
      Catch ex As Exception
         ' Ignore registry errors (e.g. access denied)
      End Try
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Load: missing, corrupt or out-of-range values fall back to / are clamped to valid values
   Public Sub Load()
      Try
         Using key As RegistryKey = Registry.CurrentUser.OpenSubKey(regAppPath, False)
            If key Is Nothing Then Return ' Nothing saved yet, keep defaults
            OneGamePerMonitor = ReadInt(key, NameOf(OneGamePerMonitor), OneGamePerMonitor, 0, 1) = 1
         End Using
      Catch ex As Exception
         ' Ignore registry errors, keep defaults
      End Try
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Delete
   Public Sub Delete()
      Try
         Registry.CurrentUser.DeleteSubKeyTree(regAppPath, False)
      Catch ex As Exception
      End Try
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' ReadInt: reads one DWord value, keeping the default if it is missing or not a number
   Private Shared Function ReadInt(key As RegistryKey, name As String, def As Integer, min As Integer, max As Integer) As Integer
      Try
         Dim v As Integer = Convert.ToInt32(key.GetValue(name, def))
         Return Math.Min(max, Math.Max(min, v))
      Catch
         Return def
      End Try
   End Function

End Class

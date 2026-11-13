'--------------------------------------------------------------------------------------------------
' PongGameSS: Main.vb: Entry point for the application
'    © 2026 Remus Rigo
'       v1.0.2026111
'--------------------------------------------------------------------------------------------------

Imports PongGameSS.API

Module Main

   <STAThread>
   Sub Main(args As String())
#If Not NETFRAMEWORK Then
      Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
      Application.EnableVisualStyles()
      Application.SetCompatibleTextRenderingDefault(False)

      cfg.Load()

      Dim mode As String = If(args.Length > 0, args(0).Trim().ToLowerInvariant(), "/s")

      Select Case If(mode.Length >= 2, mode.Substring(0, 2), mode)
         Case "/a" ' legacy password change
         Case "/c"
            ShowConfig(args)
         Case "/p"
            Dim h As IntPtr = ParseHandleArg(args)
            If h <> IntPtr.Zero Then RunPreview(h)
         Case Else ' "/s"
            RunScreenSaver()
      End Select
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' RunScreenSaver: one form (one game) per monitor; when any form closes (key / mouse),
   ' close all of them and exit
   Private Sub RunScreenSaver()
      Dim forms As New List(Of frmSS)
      Dim closing As Boolean = False

      For Each scr As Screen In Screen.AllScreens
         Dim f As New frmSS() With {.ScreenBounds = scr.Bounds}
         AddHandler f.FormClosed,
            Sub(sender, e)
               If closing Then Return
               closing = True
               For Each other As frmSS In forms
                  If other IsNot sender AndAlso Not other.IsDisposed Then other.Close()
               Next
               Application.ExitThread()
            End Sub
         forms.Add(f)
         f.Show()
      Next

      Application.Run()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' ShowConfig: Windows passes the Screen Saver Settings window as "/c:1234";
   ' use it as the owner so the dialog stays on top of it. ShowDialog only disables windows of
   ' this process, so the owner (another process) is disabled by hand while the dialog is open.
   Private Sub ShowConfig(args As String())
      Dim owner As IntPtr = If(args.Length > 0, ParseHandleArg(args), IntPtr.Zero)

      Using frm As New frmCfg()
         If owner <> IntPtr.Zero AndAlso IsWindow(owner) Then
            Dim nw As New NativeWindow()
            nw.AssignHandle(owner)

            ' Re-enable the owner before the dialog is destroyed, otherwise Windows activates
            ' some other application instead of returning focus to Screen Saver Settings
            AddHandler frm.FormClosing, Sub() EnableWindow(owner, True)

            EnableWindow(owner, False)
            Try
               frm.StartPosition = FormStartPosition.CenterParent
               frm.ShowDialog(nw)
            Finally
               EnableWindow(owner, True)   ' safety net, e.g. if ShowDialog throws
               nw.ReleaseHandle()
            End Try
         Else
            frm.ShowDialog()
         End If
      End Using
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' ParseHandleArg: Windows passes a window handle as "/p 1234", "/p:1234", "/c:1234", ...
   Private Function ParseHandleArg(args As String()) As IntPtr
      Dim s As String = Nothing
      Dim first As String = args(0).Trim()
      If first.Length > 2 Then
         s = first.Substring(2).TrimStart(":"c)
      ElseIf args.Length > 1 Then
         s = args(1)
      End If

      Dim v As Long
      If s IsNot Nothing AndAlso Long.TryParse(s.Trim(), v) Then Return New IntPtr(v)
      Return IntPtr.Zero
   End Function

   '-----------------------------------------------------------------------------------------------
   ' RunPreview: draw inside the small monitor in Screen Saver Settings
   Private Sub RunPreview(parentHandle As IntPtr)
      Application.Run(New frmSS() With {.PreviewHandle = parentHandle})
   End Sub

End Module

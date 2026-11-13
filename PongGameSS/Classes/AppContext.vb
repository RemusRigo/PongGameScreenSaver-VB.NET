'-----------------------------------------------------------------------------------------------
' AppContext: keeps the app alive while the screensaver forms are open.
'    OneGamePerMonitor = True : one form (one game) per monitor
'    OneGamePerMonitor = False: one form covering the whole virtual screen, the game is played in the
'                               horizontal strip that every monitor shows. If the monitors can't share
'                               a usable strip (stacked, L-shape, gaps), fall back to one game per monitor.
' When any form closes (key / mouse), close all of them and exit.

Public Class AppContext

   Inherits ApplicationContext

   ' The shared strip must be at least this part of the smallest monitor's height
   Private Const MinStripRatio As Double = 0.5

   Private ReadOnly _forms As New List(Of frmSS)
   Private _closing As Boolean = False

   Public Sub New()
      Dim strip As Rectangle = Rectangle.Empty

      If cfg.OneGamePerMonitor OrElse Not TryGetSharedStrip(strip) Then
         For Each scr As Screen In Screen.AllScreens
            AddForm(scr.Bounds, Rectangle.Empty)
         Next
      Else
         Dim vs As Rectangle = SystemInformation.VirtualScreen
         ' the strip is in screen coords; the form starts at vs.Location, so convert to client coords
         strip.Offset(-vs.Left, -vs.Top)
         AddForm(vs, strip)
      End If
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' TryGetSharedStrip: the horizontal strip (screen coords) visible on every monitor.
   ' Fails if the monitors don't form one continuous row, or the strip is too short.
   Private Shared Function TryGetSharedStrip(ByRef strip As Rectangle) As Boolean
      Dim screens As Screen() = Screen.AllScreens
      If screens.Length = 1 Then
         strip = screens(0).Bounds
         Return True
      End If

      ' Monitors sorted left to right must touch or overlap: no horizontal gaps
      Dim sorted = screens.Select(Function(s) s.Bounds).OrderBy(Function(b) b.Left).ToList()
      Dim reach As Integer = sorted(0).Right
      For i As Integer = 1 To sorted.Count - 1
         If sorted(i).Left > reach Then Return False
         reach = Math.Max(reach, sorted(i).Right)
      Next

      ' Vertical overlap of all monitors
      Dim top As Integer = sorted.Max(Function(b) b.Top)
      Dim bottom As Integer = sorted.Min(Function(b) b.Bottom)
      Dim minHeight As Integer = sorted.Min(Function(b) b.Height)
      If bottom - top < minHeight * MinStripRatio Then Return False

      ' Every monitor contains [top, bottom) and there are no horizontal gaps, so the whole strip
      ' is visible. Stacked monitors have no vertical overlap and were rejected above.
      Dim left As Integer = sorted(0).Left
      strip = New Rectangle(left, top, reach - left, bottom - top)
      Return True
   End Function

   '-----------------------------------------------------------------------------------------------
   ' AddForm
   Private Sub AddForm(bounds As Rectangle, gameArea As Rectangle)
      Dim f As New frmSS() With {.ScreenBounds = bounds, .GameArea = gameArea}
      AddHandler f.FormClosed, AddressOf OnFormClosed
      _forms.Add(f)
      f.Show()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' OnFormClosed
   Private Sub OnFormClosed(sender As Object, e As FormClosedEventArgs)
      If _closing Then Return
      _closing = True
      For Each f As frmSS In _forms
         If f IsNot sender AndAlso Not f.IsDisposed Then f.Close()
      Next
      ExitThread()
   End Sub
End Class

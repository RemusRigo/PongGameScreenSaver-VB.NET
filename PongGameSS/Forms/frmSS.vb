'--------------------------------------------------------------------------------------------------
' PongGameSS: frmSS.vb
'    © 2026 Remus Rigo
'       v1.0.20261112
'--------------------------------------------------------------------------------------------------

Imports PongGameSS.API

Public Class frmSS

   Public PreviewHandle As IntPtr = IntPtr.Zero   ' set by Main for /p
   Public ScreenBounds As Rectangle = Rectangle.Empty   ' set by AppContext for /s: the area this form covers (screen coords)
   Public GameArea As Rectangle = Rectangle.Empty       ' where the game is played (client coords); empty = whole client area
   Private _isPreview As Boolean = False
   Private _scale As Single = 1.0F                ' < 1 in preview so the game fits the small window

   Private mouseStart As Point? = Nothing         ' Nothing until the first MouseMove

   ' Full-screen sizes and speeds (pixels, pixels per tick). ApplyScale copies them, scaled,
   ' into the fields below: unchanged for full screen, shrunk for the preview.
   Private Const BasePaddleWidth As Integer = 15
   Private Const BasePaddleHeight As Integer = 100
   Private Const BasePaddleMargin As Integer = 40
   Private Const BasePaddleSpeed As Integer = 6
   Private Const BaseBallSize As Integer = 15
   Private Const BaseBallSpeed As Integer = 7
   Private Const BaseMaxBallSpeedX As Integer = 14
   Private Const BaseMaxBounceSpeedY As Integer = 9
   Private Const BaseCenterLineWidth As Integer = 4
   Private Const BaseScoreFontSize As Single = 48.0F
   Private Const BaseScoreTop As Integer = 20

   ' Paddle settings
   Private leftPaddle As Rectangle
   Private rightPaddle As Rectangle

   Private PaddleWidth As Integer
   Private PaddleHeight As Integer
   Private PaddleMargin As Integer
   Private PaddleSpeed As Integer

   ' Ball settings
   Private ball As Rectangle
   Private ballSpeedX As Integer
   Private ballSpeedY As Integer

   Private BallSize As Integer
   Private BallSpeed As Integer
   Private MaxBallSpeedX As Integer
   Private MaxBounceSpeedY As Integer

   ' Score
   Private leftScore As Integer
   Private rightScore As Integer
   Private ScoreFontSize As Single
   Private ScoreTop As Integer

   Private rnd As New Random()

   ' Center line
   Private CenterLineWidth As Integer
   Private seams As New List(Of Integer)   ' X (client coords) where one monitor ends and the next begins

   '-----------------------------------------------------------------------------------------------
   ' ApplyScale: size everything for this window (_scale = 1 full screen, ~0.1 in preview).
   ' Never below 1 pixel, so nothing disappears and everything keeps moving.
   Private Sub ApplyScale()
      Dim sc = Function(v As Integer) Math.Max(1, CInt(Math.Round(v * _scale)))

      PaddleWidth = sc(BasePaddleWidth)
      PaddleHeight = sc(BasePaddleHeight)
      PaddleMargin = sc(BasePaddleMargin)
      PaddleSpeed = sc(BasePaddleSpeed)
      BallSize = Math.Max(2, sc(BaseBallSize))
      BallSpeed = sc(BaseBallSpeed)
      MaxBallSpeedX = Math.Max(BallSpeed, sc(BaseMaxBallSpeedX))
      MaxBounceSpeedY = sc(BaseMaxBounceSpeedY)
      CenterLineWidth = sc(BaseCenterLineWidth)
      ScoreFontSize = Math.Max(6.0F, BaseScoreFontSize * _scale)
      ScoreTop = sc(BaseScoreTop)
   End Sub

   Private Sub InitSS()
      'Place both paddles vertically centered, one on each side
      Dim y As Integer = GameArea.Top + (GameArea.Height - PaddleHeight) \ 2

      leftPaddle = New Rectangle(GameArea.Left + PaddleMargin, y, PaddleWidth, PaddleHeight)
      rightPaddle = New Rectangle(GameArea.Right - PaddleMargin - PaddleWidth, y, PaddleWidth, PaddleHeight)

      ResetBall()
   End Sub

   Private Sub ResetBall()
      ' Ball in the center of the screen
      ball = New Rectangle(GameArea.Left + (GameArea.Width - BallSize) \ 2, GameArea.Top + (GameArea.Height - BallSize) \ 2, BallSize, BallSize)

      ' Random direction: left or right, up or down
      ballSpeedX = If(rnd.Next(2) = 0, -BallSpeed, BallSpeed)
      ballSpeedY = If(rnd.Next(2) = 0, -BallSpeed, BallSpeed)
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' MovePaddle: Move the paddle toward the ball, but never faster than PaddleSpeed
   ' use ByRef so both paddles can be moved with the same code
   Private Sub MovePaddle(ByRef paddle As Rectangle)
      ' Compare the center of the paddle with the center of the ball
      Dim paddleCenter As Integer = paddle.Y + PaddleHeight \ 2
      Dim ballCenter As Integer = ball.Y + BallSize \ 2

      ' Move toward the ball, but never faster than PaddleSpeed
      If ballCenter < paddleCenter - PaddleSpeed Then
         paddle.Y -= PaddleSpeed
      ElseIf ballCenter > paddleCenter + PaddleSpeed Then
         paddle.Y += PaddleSpeed
      End If

      ' Keep the paddle inside the screen
      If paddle.Y < GameArea.Top Then paddle.Y = GameArea.Top
      If paddle.Bottom > GameArea.Bottom Then paddle.Y = GameArea.Bottom - PaddleHeight
   End Sub

   Private Sub BounceOffPaddle(paddle As Rectangle, direction As Integer)
      ' Where did the ball hit? -1 = top edge, 0 = center, +1 = bottom edge
      Dim paddleCenter As Integer = paddle.Y + PaddleHeight \ 2
      Dim ballCenter As Integer = ball.Y + BallSize \ 2
      Dim offset As Double = (ballCenter - paddleCenter) / (PaddleHeight / 2.0 + BallSize / 2.0)
      offset = Math.Max(-1.0, Math.Min(1.0, offset))

      ' The farther from the center, the steeper the angle
      ballSpeedY = CInt(offset * MaxBounceSpeedY)

      ' Never let the ball fly in a flat horizontal line: the paddles follow the ball's center,
      ' so they would keep hitting it dead center and it would never go up or down again.
      ' Keep a minimum vertical speed (1/4 of the max, at least 1 pixel per tick)
      Dim minSpeedY As Integer = Math.Max(1, MaxBounceSpeedY \ 4)
      If Math.Abs(ballSpeedY) < minSpeedY Then
         If ballSpeedY = 0 Then
            ballSpeedY = If(rnd.Next(2) = 0, -minSpeedY, minSpeedY)   ' dead center: random up/down
         Else
            ballSpeedY = Math.Sign(ballSpeedY) * minSpeedY            ' keep the direction it had
         End If
      End If

      ' Speed up a little on every hit, up to a maximum
      Dim newSpeed As Integer = Math.Min(Math.Abs(ballSpeedX) + 1, MaxBallSpeedX)
      ballSpeedX = direction * newSpeed
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' tmr: Tick
   Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
      ' Move the ball
      ball.X += ballSpeedX
      ball.Y += ballSpeedY

      ' Bounce off the top and bottom edges
      If ball.Top <= GameArea.Top Then
         ball.Y = GameArea.Top
         ballSpeedY = -ballSpeedY
      ElseIf ball.Bottom >= GameArea.Bottom Then
         ball.Y = GameArea.Bottom - BallSize
         ballSpeedY = -ballSpeedY
      End If

      ' Move both paddles toward the ball
      MovePaddle(leftPaddle)
      MovePaddle(rightPaddle)

      ' Bounce off the paddles
      If ballSpeedX < 0 AndAlso ball.IntersectsWith(leftPaddle) Then
         ball.X = leftPaddle.Right
         'ballSpeedX = -ballSpeedX
         BounceOffPaddle(leftPaddle, 1)
      ElseIf ballSpeedX > 0 AndAlso ball.IntersectsWith(rightPaddle) Then
         ball.X = rightPaddle.Left - BallSize
         'ballSpeedX = -ballSpeedX
         BounceOffPaddle(rightPaddle, -1)
      End If

      ' The ball got past a paddle: start again from the center
      ' If ball.Right < 0 OrElse ball.Left > Me.ClientSize.Width Then ResetBall()

      If ball.Right < GameArea.Left Then
         ' Missed by the left paddle: the right side scores
         rightScore += 1
         ResetBall()
      ElseIf ball.Left > GameArea.Right Then
         ' Missed by the right paddle: the left side scores
         leftScore += 1
         ResetBall()
      End If

      Me.Invalidate()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: Paint
   Private Sub frmSS_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
      e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

      Dim g As Graphics = e.Graphics

      ' Dashed center line
      ' If it falls on a seam between two monitors, each monitor would show only part of it,
      ' so draw it twice: once fully on each side of the seam
      Using pen As New Pen(Color.Gray, CenterLineWidth)
         pen.DashStyle = Drawing2D.DashStyle.Dash
         Dim cx As Integer = GameArea.Left + GameArea.Width \ 2
         Dim seam As Integer = seams.Find(Function(s) Math.Abs(s - cx) <= CenterLineWidth)
         If seam <> 0 Then
            Dim half As Integer = CenterLineWidth \ 2
            g.DrawLine(pen, seam - half, GameArea.Top, seam - half, GameArea.Bottom)   ' covers seam-4 .. seam-1
            g.DrawLine(pen, seam + half, GameArea.Top, seam + half, GameArea.Bottom)   ' covers seam   .. seam+3
         Else
            g.DrawLine(pen, cx, GameArea.Top, cx, GameArea.Bottom)
         End If
      End Using

      ' Paddles
      g.FillRectangle(Brushes.White, leftPaddle)
      g.FillRectangle(Brushes.White, rightPaddle)

      ' Ball
      g.FillRectangle(Brushes.White, ball)

      ' Scores, centered in each half of the screen
      Using f As New Font("Consolas", ScoreFontSize, FontStyle.Bold), sf As New StringFormat With {.Alignment = StringAlignment.Center}
         g.DrawString(leftScore.ToString(), f, Brushes.White, GameArea.Left + GameArea.Width \ 4, GameArea.Top + ScoreTop, sf)
         g.DrawString(rightScore.ToString(), f, Brushes.White, GameArea.Left + GameArea.Width * 3 \ 4, GameArea.Top + ScoreTop, sf)
      End Using

   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: Load
   Private Sub frmSS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      ' Common settings. ShowInTaskbar recreates the window handle,
      ' so it must be set BEFORE SetParent in preview mode.
      With Me
         .AutoScaleMode = AutoScaleMode.None
         .FormBorderStyle = FormBorderStyle.None
         .StartPosition = FormStartPosition.Manual
         .ShowInTaskbar = False
         .BackColor = Color.Black
         .DoubleBuffered = True
         .KeyPreview = True
      End With

      If PreviewHandle <> IntPtr.Zero Then
         ' Preview: become a child of the little monitor in Screen Saver Settings
         _isPreview = True

         Dim rc As RECT
         GetClientRect(PreviewHandle, rc)
         SetParent(Me.Handle, PreviewHandle)

         Dim style As Integer = GetWindowLong(Me.Handle, GWL_STYLE)
         SetWindowLong(Me.Handle, GWL_STYLE, (style And Not WS_POPUP) Or WS_CHILD)

         Me.Bounds = New Rectangle(0, 0, rc.Right - rc.Left, rc.Bottom - rc.Top)

         ' Keep the same proportions as full screen
         _scale = CSng(Me.ClientSize.Height / Screen.PrimaryScreen.Bounds.Height)
      Else
         ' Full screen: cover the area assigned by AppContext (one monitor, or the whole
         ' virtual screen when OneGamePerMonitor is False); primary monitor if none
         ' set bounds after StartPosition, so it doesn't get overridden
         Me.WindowState = FormWindowState.Normal
         Me.Bounds = If(ScreenBounds.IsEmpty, Screen.PrimaryScreen.Bounds, ScreenBounds)

#If Not DEBUG Then
         Me.TopMost = True
#End If
         ' Hide/Show are counted: one Hide here, one Show in FormClosed
         Cursor.Hide()
      End If

      ' Play in the whole client area unless AppContext gave a smaller area (spanning mode)
      If GameArea.IsEmpty Then GameArea = New Rectangle(Point.Empty, Me.ClientSize)

      ' Monitor seams inside this form (only when it spans more than one monitor)
      If Not _isPreview Then
         For Each scr As Screen In Screen.AllScreens
            Dim x As Integer = scr.Bounds.Left - Me.Bounds.Left
            If x > 0 AndAlso x < Me.ClientSize.Width Then seams.Add(x)
         Next
      End If

      tmr.Interval = 16 ' approximately 60 FPS
      ApplyScale()
      InitSS()
      tmr.Start()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: FormClosed
   Private Sub frmSS_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
      tmr.Stop()
      If Not _isPreview Then Cursor.Show()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: DpiChanged
   ' Each form is created on the primary monitor and then moved to its own monitor (or spans all
   ' monitors). With mixed scaling (e.g. 100% + 150%) Windows sends DpiChanged and WinForms would
   ' rescale/move the form. Cancel that and keep covering exactly this form's area.
   Private Sub frmSS_DpiChanged(sender As Object, e As DpiChangedEventArgs) Handles MyBase.DpiChanged
      If _isPreview Then Return
      e.Cancel = True
      If Not ScreenBounds.IsEmpty Then Me.Bounds = ScreenBounds
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: KeyDown
   Private Sub frmSS_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
      If _isPreview Then Return
      Me.Close()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: MouseDown
   Private Sub frmSS_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
      If _isPreview Then Return
      Me.Close()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: MouseMove
   Private Sub frmSS_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
      If _isPreview Then Return

      ' remember the first position
      If Not mouseStart.HasValue Then
         mouseStart = e.Location
         Return
      End If

      ' exit if the mouse moved more than 4 pixels
      Dim start As Point = mouseStart.Value
      If Math.Abs(e.X - start.X) > 4 OrElse Math.Abs(e.Y - start.Y) > 4 Then
         Me.Close()
      End If
   End Sub

End Class

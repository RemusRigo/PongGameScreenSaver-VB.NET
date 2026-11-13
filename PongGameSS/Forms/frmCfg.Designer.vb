<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCfg
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()>
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.
   'Do not modify it using the code editor.
   <System.Diagnostics.DebuggerStepThrough()>
   Private Sub InitializeComponent()
      btnOk = New Button()
      btnCancel = New Button()
      grpBoxMultiMonitor = New GroupBox()
      rbtnAllMon = New RadioButton()
      rbtnPerMon = New RadioButton()
      grpBoxScore = New GroupBox()
      txtBoxPlayer2 = New TextBox()
      lblPlayer2 = New Label()
      txtBoxPlayer1 = New TextBox()
      lblPlayer1 = New Label()
      chkBoxShowPlayers = New CheckBox()
      chkBoxShowScores = New CheckBox()
      chkBoxShowCenterLine = New CheckBox()
      grpBoxMultiMonitor.SuspendLayout()
      grpBoxScore.SuspendLayout()
      SuspendLayout()
      ' 
      ' btnOk
      ' 
      btnOk.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
      btnOk.DialogResult = DialogResult.OK
      btnOk.Location = New Point(306, 125)
      btnOk.Name = "btnOk"
      btnOk.Size = New Size(35, 23)
      btnOk.TabIndex = 9
      btnOk.Text = "&Ok"
      btnOk.UseVisualStyleBackColor = True
      ' 
      ' btnCancel
      ' 
      btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
      btnCancel.DialogResult = DialogResult.Cancel
      btnCancel.Location = New Point(347, 125)
      btnCancel.Name = "btnCancel"
      btnCancel.Size = New Size(60, 23)
      btnCancel.TabIndex = 10
      btnCancel.Text = "&Cancel"
      btnCancel.UseVisualStyleBackColor = True
      ' 
      ' grpBoxMultiMonitor
      ' 
      grpBoxMultiMonitor.Controls.Add(rbtnAllMon)
      grpBoxMultiMonitor.Controls.Add(rbtnPerMon)
      grpBoxMultiMonitor.Location = New Point(270, 2)
      grpBoxMultiMonitor.Name = "grpBoxMultiMonitor"
      grpBoxMultiMonitor.Size = New Size(129, 72)
      grpBoxMultiMonitor.TabIndex = 12
      grpBoxMultiMonitor.TabStop = False
      grpBoxMultiMonitor.Text = "Multi Monitor"
      ' 
      ' rbtnAllMon
      ' 
      rbtnAllMon.AutoSize = True
      rbtnAllMon.Location = New Point(6, 47)
      rbtnAllMon.Name = "rbtnAllMon"
      rbtnAllMon.Size = New Size(90, 19)
      rbtnAllMon.TabIndex = 15
      rbtnAllMon.TabStop = True
      rbtnAllMon.Text = "All monitors"
      rbtnAllMon.UseVisualStyleBackColor = True
      ' 
      ' rbtnPerMon
      ' 
      rbtnPerMon.AutoSize = True
      rbtnPerMon.Location = New Point(7, 22)
      rbtnPerMon.Name = "rbtnPerMon"
      rbtnPerMon.Size = New Size(113, 19)
      rbtnPerMon.TabIndex = 14
      rbtnPerMon.TabStop = True
      rbtnPerMon.Text = "One per monitor"
      rbtnPerMon.UseVisualStyleBackColor = True
      ' 
      ' grpBoxScore
      ' 
      grpBoxScore.Controls.Add(chkBoxShowCenterLine)
      grpBoxScore.Controls.Add(txtBoxPlayer2)
      grpBoxScore.Controls.Add(lblPlayer2)
      grpBoxScore.Controls.Add(txtBoxPlayer1)
      grpBoxScore.Controls.Add(lblPlayer1)
      grpBoxScore.Controls.Add(chkBoxShowPlayers)
      grpBoxScore.Controls.Add(chkBoxShowScores)
      grpBoxScore.Location = New Point(2, 2)
      grpBoxScore.Name = "grpBoxScore"
      grpBoxScore.Size = New Size(261, 129)
      grpBoxScore.TabIndex = 13
      grpBoxScore.TabStop = False
      grpBoxScore.Text = "Scores"
      ' 
      ' txtBoxPlayer2
      ' 
      txtBoxPlayer2.Location = New Point(111, 95)
      txtBoxPlayer2.Name = "txtBoxPlayer2"
      txtBoxPlayer2.Size = New Size(144, 23)
      txtBoxPlayer2.TabIndex = 5
      ' 
      ' lblPlayer2
      ' 
      lblPlayer2.AutoSize = True
      lblPlayer2.Location = New Point(10, 98)
      lblPlayer2.Name = "lblPlayer2"
      lblPlayer2.Size = New Size(51, 15)
      lblPlayer2.TabIndex = 4
      lblPlayer2.Text = "Player 2:"
      ' 
      ' txtBoxPlayer1
      ' 
      txtBoxPlayer1.Location = New Point(111, 66)
      txtBoxPlayer1.Name = "txtBoxPlayer1"
      txtBoxPlayer1.Size = New Size(144, 23)
      txtBoxPlayer1.TabIndex = 3
      ' 
      ' lblPlayer1
      ' 
      lblPlayer1.AutoSize = True
      lblPlayer1.Location = New Point(10, 69)
      lblPlayer1.Name = "lblPlayer1"
      lblPlayer1.Size = New Size(51, 15)
      lblPlayer1.TabIndex = 2
      lblPlayer1.Text = "Player 1:"
      ' 
      ' chkBoxShowPlayers
      ' 
      chkBoxShowPlayers.AutoSize = True
      chkBoxShowPlayers.Location = New Point(10, 47)
      chkBoxShowPlayers.Name = "chkBoxShowPlayers"
      chkBoxShowPlayers.Size = New Size(95, 19)
      chkBoxShowPlayers.TabIndex = 1
      chkBoxShowPlayers.Text = "Show players"
      chkBoxShowPlayers.UseVisualStyleBackColor = True
      ' 
      ' chkBoxShowScores
      ' 
      chkBoxShowScores.AutoSize = True
      chkBoxShowScores.Location = New Point(10, 22)
      chkBoxShowScores.Name = "chkBoxShowScores"
      chkBoxShowScores.Size = New Size(91, 19)
      chkBoxShowScores.TabIndex = 0
      chkBoxShowScores.Text = "Show scores"
      chkBoxShowScores.UseVisualStyleBackColor = True
      ' 
      ' chkBoxShowCenterLine
      ' 
      chkBoxShowCenterLine.AutoSize = True
      chkBoxShowCenterLine.Location = New Point(111, 22)
      chkBoxShowCenterLine.Name = "chkBoxShowCenterLine"
      chkBoxShowCenterLine.Size = New Size(113, 19)
      chkBoxShowCenterLine.TabIndex = 6
      chkBoxShowCenterLine.Text = "Show center line"
      chkBoxShowCenterLine.UseVisualStyleBackColor = True
      ' 
      ' frmCfg
      ' 
      AcceptButton = btnOk
      AutoScaleDimensions = New SizeF(7F, 15F)
      AutoScaleMode = AutoScaleMode.Font
      CancelButton = btnCancel
      ClientSize = New Size(409, 151)
      Controls.Add(grpBoxScore)
      Controls.Add(grpBoxMultiMonitor)
      Controls.Add(btnCancel)
      Controls.Add(btnOk)
      FormBorderStyle = FormBorderStyle.FixedDialog
      MaximizeBox = False
      MinimizeBox = False
      Name = "frmCfg"
      Text = "Settings"
      grpBoxMultiMonitor.ResumeLayout(False)
      grpBoxMultiMonitor.PerformLayout()
      grpBoxScore.ResumeLayout(False)
      grpBoxScore.PerformLayout()
      ResumeLayout(False)
   End Sub
   Friend WithEvents btnOk As Button
   Friend WithEvents btnCancel As Button
   Friend WithEvents grpBoxMultiMonitor As GroupBox
   Friend WithEvents rbtnAllMon As RadioButton
   Friend WithEvents rbtnPerMon As RadioButton
   Friend WithEvents grpBoxScore As GroupBox
   Friend WithEvents chkBoxShowPlayers As CheckBox
   Friend WithEvents chkBoxShowScores As CheckBox
   Friend WithEvents lblPlayer1 As Label
   Friend WithEvents lblPlayer2 As Label
   Friend WithEvents txtBoxPlayer1 As TextBox
   Friend WithEvents txtBoxPlayer2 As TextBox
   Friend WithEvents chkBoxShowCenterLine As CheckBox
End Class

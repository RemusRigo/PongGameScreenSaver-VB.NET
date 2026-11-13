<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCfg
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()> _
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
   <System.Diagnostics.DebuggerStepThrough()> _
   Private Sub InitializeComponent()
      btnOk = New Button()
      btnCancel = New Button()
      grpBoxMultiMonitor = New GroupBox()
      rbtnAllMon = New RadioButton()
      rbtnPerMon = New RadioButton()
      grpBoxMultiMonitor.SuspendLayout()
      SuspendLayout()
      ' 
      ' btnOk
      ' 
      btnOk.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
      btnOk.DialogResult = DialogResult.OK
      btnOk.Location = New Point(249, 110)
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
      btnCancel.Location = New Point(290, 110)
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
      grpBoxMultiMonitor.Location = New Point(5, 8)
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
      ' frmCfg
      ' 
      AcceptButton = btnOk
      AutoScaleDimensions = New SizeF(7F, 15F)
      AutoScaleMode = AutoScaleMode.Font
      CancelButton = btnCancel
      ClientSize = New Size(352, 136)
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
      ResumeLayout(False)
   End Sub
   Friend WithEvents btnOk As Button
   Friend WithEvents btnCancel As Button
   Friend WithEvents grpBoxMultiMonitor As GroupBox
   Friend WithEvents rbtnAllMon As RadioButton
   Friend WithEvents rbtnPerMon As RadioButton
End Class

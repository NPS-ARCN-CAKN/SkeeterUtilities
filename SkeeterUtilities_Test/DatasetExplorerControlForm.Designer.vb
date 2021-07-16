<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatasetExplorerControlForm
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
        Me.SourceDataTableExplorerControl = New SkeeterUtilities.DataTableExplorerControl()
        Me.SuspendLayout()
        '
        'SourceDataTableExplorerControl
        '
        Me.SourceDataTableExplorerControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SourceDataTableExplorerControl.Location = New System.Drawing.Point(0, 0)
        Me.SourceDataTableExplorerControl.Name = "SourceDataTableExplorerControl"
        Me.SourceDataTableExplorerControl.Size = New System.Drawing.Size(640, 360)
        Me.SourceDataTableExplorerControl.TabIndex = 0
        '
        'DatasetExplorerControlForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SourceDataTableExplorerControl)
        Me.Name = "DatasetExplorerControlForm"
        Me.Text = "DatasetExplorerControlForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SourceDataTableExplorerControl As SkeeterUtilities.DataTableExplorerControl
End Class

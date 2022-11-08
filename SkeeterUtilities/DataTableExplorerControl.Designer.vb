<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataTableExplorerControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataTableExplorerControl))
        Me.MainDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.MetadataDockPanel = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.MetadataGridControl = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.MetadataToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ExportMetadataToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.DataTableGridControl = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.MainDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MetadataDockPanel.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.MetadataGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MetadataToolStrip.SuspendLayout()
        CType(Me.DataTableGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainDockManager
        '
        Me.MainDockManager.Form = Me
        Me.MainDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.MetadataDockPanel})
        Me.MainDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane", "DevExpress.XtraBars.TabFormControl", "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl", "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"})
        '
        'MetadataDockPanel
        '
        Me.MetadataDockPanel.Controls.Add(Me.DockPanel1_Container)
        Me.MetadataDockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.MetadataDockPanel.ID = New System.Guid("e4e549fd-8178-4d3b-a906-719fd6038fcd")
        Me.MetadataDockPanel.Location = New System.Drawing.Point(0, 0)
        Me.MetadataDockPanel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MetadataDockPanel.Name = "MetadataDockPanel"
        Me.MetadataDockPanel.OriginalSize = New System.Drawing.Size(480, 200)
        Me.MetadataDockPanel.Size = New System.Drawing.Size(640, 713)
        Me.MetadataDockPanel.Text = "Metadata"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.MetadataGridControl)
        Me.DockPanel1_Container.Controls.Add(Me.MetadataToolStrip)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(630, 677)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'MetadataGridControl
        '
        Me.MetadataGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetadataGridControl.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MetadataGridControl.Location = New System.Drawing.Point(0, 27)
        Me.MetadataGridControl.MainView = Me.GridView2
        Me.MetadataGridControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MetadataGridControl.Name = "MetadataGridControl"
        Me.MetadataGridControl.Size = New System.Drawing.Size(630, 650)
        Me.MetadataGridControl.TabIndex = 0
        Me.MetadataGridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.MetadataGridControl
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsView.ShowFooter = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'MetadataToolStrip
        '
        Me.MetadataToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MetadataToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportMetadataToolStripButton})
        Me.MetadataToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.MetadataToolStrip.Name = "MetadataToolStrip"
        Me.MetadataToolStrip.Size = New System.Drawing.Size(630, 27)
        Me.MetadataToolStrip.TabIndex = 1
        Me.MetadataToolStrip.Text = "Metadata"
        '
        'ExportMetadataToolStripButton
        '
        Me.ExportMetadataToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ExportMetadataToolStripButton.Image = CType(resources.GetObject("ExportMetadataToolStripButton.Image"), System.Drawing.Image)
        Me.ExportMetadataToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ExportMetadataToolStripButton.Name = "ExportMetadataToolStripButton"
        Me.ExportMetadataToolStripButton.Size = New System.Drawing.Size(133, 24)
        Me.ExportMetadataToolStripButton.Text = "Export metadata..."
        '
        'DataTableGridControl
        '
        Me.DataTableGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataTableGridControl.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataTableGridControl.Location = New System.Drawing.Point(640, 0)
        Me.DataTableGridControl.MainView = Me.GridView1
        Me.DataTableGridControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataTableGridControl.Name = "DataTableGridControl"
        Me.DataTableGridControl.Size = New System.Drawing.Size(473, 713)
        Me.DataTableGridControl.TabIndex = 0
        Me.DataTableGridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.DataTableGridControl
        Me.GridView1.Name = "GridView1"
        '
        'DataTableExplorerControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DataTableGridControl)
        Me.Controls.Add(Me.MetadataDockPanel)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "DataTableExplorerControl"
        Me.Size = New System.Drawing.Size(1113, 713)
        CType(Me.MainDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MetadataDockPanel.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.MetadataGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MetadataToolStrip.ResumeLayout(False)
        Me.MetadataToolStrip.PerformLayout()
        CType(Me.DataTableGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MainDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DataTableGridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents MetadataDockPanel As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents MetadataGridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents MetadataToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ExportMetadataToolStripButton As System.Windows.Forms.ToolStripButton
End Class

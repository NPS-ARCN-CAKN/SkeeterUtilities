<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataTableFromCSVButton = New System.Windows.Forms.Button()
        Me.GetFileButton = New System.Windows.Forms.Button()
        Me.GetDirectoryContentsButton = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GetMetadataFromDataTableButton = New System.Windows.Forms.Button()
        Me.DataTableFromSQLServerButton = New System.Windows.Forms.Button()
        Me.TransposeADataTableButton = New System.Windows.Forms.Button()
        Me.PDFTableToDataTableButton = New System.Windows.Forms.Button()
        Me.ShowDataTableExplorerControlButton = New System.Windows.Forms.Button()
        Me.DataColumnIsNumericButton = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GetDatasetFromExcelButton = New System.Windows.Forms.Button()
        Me.GetMetadataDatasetFromExcelButton = New System.Windows.Forms.Button()
        Me.DescribeADatasetButton = New System.Windows.Forms.Button()
        Me.GetMetadataDatasetFromDataTableButton = New System.Windows.Forms.Button()
        Me.DataTableToCSVButton = New System.Windows.Forms.Button()
        Me.ShowDataTableInAFormButton = New System.Windows.Forms.Button()
        Me.DataTableFromExcelButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OutputTabControl = New System.Windows.Forms.TabControl()
        Me.InputTabPage = New System.Windows.Forms.TabPage()
        Me.InputTextBox = New System.Windows.Forms.TextBox()
        Me.TextTabPage = New System.Windows.Forms.TabPage()
        Me.OutputTextBox = New System.Windows.Forms.TextBox()
        Me.DataGridViewTabPage = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.OutputDataGridView = New System.Windows.Forms.DataGridView()
        Me.OutputGridControl = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PropertyGridTabPage = New System.Windows.Forms.TabPage()
        Me.OutputPropertyGrid = New System.Windows.Forms.PropertyGrid()
        Me.DevExpressMapTabPage = New System.Windows.Forms.TabPage()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.OutputTabControl.SuspendLayout()
        Me.InputTabPage.SuspendLayout()
        Me.TextTabPage.SuspendLayout()
        Me.DataGridViewTabPage.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.OutputDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OutputGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PropertyGridTabPage.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataTableFromCSVButton
        '
        Me.DataTableFromCSVButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DataTableFromCSVButton.Location = New System.Drawing.Point(17, 292)
        Me.DataTableFromCSVButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataTableFromCSVButton.Name = "DataTableFromCSVButton"
        Me.DataTableFromCSVButton.Size = New System.Drawing.Size(265, 34)
        Me.DataTableFromCSVButton.TabIndex = 0
        Me.DataTableFromCSVButton.Text = "DataTable from CSV..."
        Me.DataTableFromCSVButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DataTableFromCSVButton.UseVisualStyleBackColor = True
        '
        'GetFileButton
        '
        Me.GetFileButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GetFileButton.Location = New System.Drawing.Point(17, 57)
        Me.GetFileButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GetFileButton.Name = "GetFileButton"
        Me.GetFileButton.Size = New System.Drawing.Size(265, 28)
        Me.GetFileButton.TabIndex = 1
        Me.GetFileButton.Text = "Get file..."
        Me.GetFileButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GetFileButton.UseVisualStyleBackColor = True
        '
        'GetDirectoryContentsButton
        '
        Me.GetDirectoryContentsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GetDirectoryContentsButton.Location = New System.Drawing.Point(17, 90)
        Me.GetDirectoryContentsButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GetDirectoryContentsButton.Name = "GetDirectoryContentsButton"
        Me.GetDirectoryContentsButton.Size = New System.Drawing.Size(333, 28)
        Me.GetDirectoryContentsButton.TabIndex = 2
        Me.GetDirectoryContentsButton.Text = "Get a list of subdirectories and files in a directory:"
        Me.GetDirectoryContentsButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GetDirectoryContentsButton.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GetMetadataFromDataTableButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataTableFromSQLServerButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TransposeADataTableButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PDFTableToDataTableButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ShowDataTableExplorerControlButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataColumnIsNumericButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GetDatasetFromExcelButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GetMetadataDatasetFromExcelButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DescribeADatasetButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GetMetadataDatasetFromDataTableButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataTableToCSVButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ShowDataTableInAFormButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataTableFromExcelButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataTableFromCSVButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GetFileButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GetDirectoryContentsButton)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.OutputTabControl)
        Me.SplitContainer1.Size = New System.Drawing.Size(1579, 937)
        Me.SplitContainer1.SplitterDistance = 353
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 4
        '
        'GetMetadataFromDataTableButton
        '
        Me.GetMetadataFromDataTableButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GetMetadataFromDataTableButton.Location = New System.Drawing.Point(17, 506)
        Me.GetMetadataFromDataTableButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GetMetadataFromDataTableButton.Name = "GetMetadataFromDataTableButton"
        Me.GetMetadataFromDataTableButton.Size = New System.Drawing.Size(265, 49)
        Me.GetMetadataFromDataTableButton.TabIndex = 19
        Me.GetMetadataFromDataTableButton.Text = "Get Metadata from DataTable..."
        Me.GetMetadataFromDataTableButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GetMetadataFromDataTableButton.UseVisualStyleBackColor = True
        '
        'DataTableFromSQLServerButton
        '
        Me.DataTableFromSQLServerButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DataTableFromSQLServerButton.Location = New System.Drawing.Point(17, 730)
        Me.DataTableFromSQLServerButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataTableFromSQLServerButton.Name = "DataTableFromSQLServerButton"
        Me.DataTableFromSQLServerButton.Size = New System.Drawing.Size(265, 34)
        Me.DataTableFromSQLServerButton.TabIndex = 18
        Me.DataTableFromSQLServerButton.Text = "DataTable from SQL Server"
        Me.DataTableFromSQLServerButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DataTableFromSQLServerButton.UseVisualStyleBackColor = True
        '
        'TransposeADataTableButton
        '
        Me.TransposeADataTableButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TransposeADataTableButton.Location = New System.Drawing.Point(17, 690)
        Me.TransposeADataTableButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TransposeADataTableButton.Name = "TransposeADataTableButton"
        Me.TransposeADataTableButton.Size = New System.Drawing.Size(265, 34)
        Me.TransposeADataTableButton.TabIndex = 17
        Me.TransposeADataTableButton.Text = "Transpose a DataTable"
        Me.TransposeADataTableButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TransposeADataTableButton.UseVisualStyleBackColor = True
        '
        'PDFTableToDataTableButton
        '
        Me.PDFTableToDataTableButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PDFTableToDataTableButton.Location = New System.Drawing.Point(17, 651)
        Me.PDFTableToDataTableButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PDFTableToDataTableButton.Name = "PDFTableToDataTableButton"
        Me.PDFTableToDataTableButton.Size = New System.Drawing.Size(265, 34)
        Me.PDFTableToDataTableButton.TabIndex = 16
        Me.PDFTableToDataTableButton.Text = "PDF table to DataTable"
        Me.PDFTableToDataTableButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PDFTableToDataTableButton.UseVisualStyleBackColor = True
        '
        'ShowDataTableExplorerControlButton
        '
        Me.ShowDataTableExplorerControlButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ShowDataTableExplorerControlButton.Location = New System.Drawing.Point(17, 612)
        Me.ShowDataTableExplorerControlButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ShowDataTableExplorerControlButton.Name = "ShowDataTableExplorerControlButton"
        Me.ShowDataTableExplorerControlButton.Size = New System.Drawing.Size(265, 34)
        Me.ShowDataTableExplorerControlButton.TabIndex = 15
        Me.ShowDataTableExplorerControlButton.Text = "ShowDatasetExplorerControl..."
        Me.ShowDataTableExplorerControlButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ShowDataTableExplorerControlButton.UseVisualStyleBackColor = True
        '
        'DataColumnIsNumericButton
        '
        Me.DataColumnIsNumericButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DataColumnIsNumericButton.Location = New System.Drawing.Point(17, 572)
        Me.DataColumnIsNumericButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataColumnIsNumericButton.Name = "DataColumnIsNumericButton"
        Me.DataColumnIsNumericButton.Size = New System.Drawing.Size(265, 34)
        Me.DataColumnIsNumericButton.TabIndex = 14
        Me.DataColumnIsNumericButton.Text = "DataColumn IsNumeric..."
        Me.DataColumnIsNumericButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DataColumnIsNumericButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 134)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 24)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Dataset"
        '
        'GetDatasetFromExcelButton
        '
        Me.GetDatasetFromExcelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GetDatasetFromExcelButton.Location = New System.Drawing.Point(17, 160)
        Me.GetDatasetFromExcelButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GetDatasetFromExcelButton.Name = "GetDatasetFromExcelButton"
        Me.GetDatasetFromExcelButton.Size = New System.Drawing.Size(265, 34)
        Me.GetDatasetFromExcelButton.TabIndex = 12
        Me.GetDatasetFromExcelButton.Text = "Get Dataset from Excel..."
        Me.GetDatasetFromExcelButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GetDatasetFromExcelButton.UseVisualStyleBackColor = True
        '
        'GetMetadataDatasetFromExcelButton
        '
        Me.GetMetadataDatasetFromExcelButton.Location = New System.Drawing.Point(17, 769)
        Me.GetMetadataDatasetFromExcelButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GetMetadataDatasetFromExcelButton.Name = "GetMetadataDatasetFromExcelButton"
        Me.GetMetadataDatasetFromExcelButton.Size = New System.Drawing.Size(265, 34)
        Me.GetMetadataDatasetFromExcelButton.TabIndex = 11
        Me.GetMetadataDatasetFromExcelButton.Text = "Get metadata Dataset from Excel..."
        Me.GetMetadataDatasetFromExcelButton.UseVisualStyleBackColor = True
        '
        'DescribeADatasetButton
        '
        Me.DescribeADatasetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DescribeADatasetButton.Location = New System.Drawing.Point(17, 199)
        Me.DescribeADatasetButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DescribeADatasetButton.Name = "DescribeADatasetButton"
        Me.DescribeADatasetButton.Size = New System.Drawing.Size(265, 50)
        Me.DescribeADatasetButton.TabIndex = 10
        Me.DescribeADatasetButton.Text = "Describe a Dataset, then show it in a form..."
        Me.DescribeADatasetButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DescribeADatasetButton.UseVisualStyleBackColor = True
        '
        'GetMetadataDatasetFromDataTableButton
        '
        Me.GetMetadataDatasetFromDataTableButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GetMetadataDatasetFromDataTableButton.Location = New System.Drawing.Point(17, 452)
        Me.GetMetadataDatasetFromDataTableButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GetMetadataDatasetFromDataTableButton.Name = "GetMetadataDatasetFromDataTableButton"
        Me.GetMetadataDatasetFromDataTableButton.Size = New System.Drawing.Size(265, 49)
        Me.GetMetadataDatasetFromDataTableButton.TabIndex = 9
        Me.GetMetadataDatasetFromDataTableButton.Text = "Get MetadataDataset from DataTable..."
        Me.GetMetadataDatasetFromDataTableButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GetMetadataDatasetFromDataTableButton.UseVisualStyleBackColor = True
        '
        'DataTableToCSVButton
        '
        Me.DataTableToCSVButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DataTableToCSVButton.Location = New System.Drawing.Point(17, 331)
        Me.DataTableToCSVButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataTableToCSVButton.Name = "DataTableToCSVButton"
        Me.DataTableToCSVButton.Size = New System.Drawing.Size(265, 34)
        Me.DataTableToCSVButton.TabIndex = 8
        Me.DataTableToCSVButton.Text = "DataTable to CSV..."
        Me.DataTableToCSVButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DataTableToCSVButton.UseVisualStyleBackColor = True
        '
        'ShowDataTableInAFormButton
        '
        Me.ShowDataTableInAFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ShowDataTableInAFormButton.Location = New System.Drawing.Point(17, 412)
        Me.ShowDataTableInAFormButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ShowDataTableInAFormButton.Name = "ShowDataTableInAFormButton"
        Me.ShowDataTableInAFormButton.Size = New System.Drawing.Size(265, 34)
        Me.ShowDataTableInAFormButton.TabIndex = 7
        Me.ShowDataTableInAFormButton.Text = "Show a DataTable in a Form..."
        Me.ShowDataTableInAFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ShowDataTableInAFormButton.UseVisualStyleBackColor = True
        '
        'DataTableFromExcelButton
        '
        Me.DataTableFromExcelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DataTableFromExcelButton.Location = New System.Drawing.Point(17, 373)
        Me.DataTableFromExcelButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataTableFromExcelButton.Name = "DataTableFromExcelButton"
        Me.DataTableFromExcelButton.Size = New System.Drawing.Size(265, 34)
        Me.DataTableFromExcelButton.TabIndex = 6
        Me.DataTableFromExcelButton.Text = "DataTable from Excel..."
        Me.DataTableFromExcelButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DataTableFromExcelButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 266)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 24)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "DataTable"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 31)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(246, 24)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Directory and file utilities"
        '
        'OutputTabControl
        '
        Me.OutputTabControl.Controls.Add(Me.InputTabPage)
        Me.OutputTabControl.Controls.Add(Me.TextTabPage)
        Me.OutputTabControl.Controls.Add(Me.DataGridViewTabPage)
        Me.OutputTabControl.Controls.Add(Me.PropertyGridTabPage)
        Me.OutputTabControl.Controls.Add(Me.DevExpressMapTabPage)
        Me.OutputTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputTabControl.Location = New System.Drawing.Point(0, 0)
        Me.OutputTabControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OutputTabControl.Name = "OutputTabControl"
        Me.OutputTabControl.SelectedIndex = 0
        Me.OutputTabControl.Size = New System.Drawing.Size(1221, 937)
        Me.OutputTabControl.TabIndex = 0
        '
        'InputTabPage
        '
        Me.InputTabPage.Controls.Add(Me.InputTextBox)
        Me.InputTabPage.Location = New System.Drawing.Point(4, 25)
        Me.InputTabPage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.InputTabPage.Name = "InputTabPage"
        Me.InputTabPage.Size = New System.Drawing.Size(1213, 908)
        Me.InputTabPage.TabIndex = 3
        Me.InputTabPage.Text = "Input"
        Me.InputTabPage.UseVisualStyleBackColor = True
        '
        'InputTextBox
        '
        Me.InputTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputTextBox.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InputTextBox.Location = New System.Drawing.Point(0, 0)
        Me.InputTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.InputTextBox.Multiline = True
        Me.InputTextBox.Name = "InputTextBox"
        Me.InputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.InputTextBox.Size = New System.Drawing.Size(1213, 908)
        Me.InputTextBox.TabIndex = 1
        Me.InputTextBox.WordWrap = False
        '
        'TextTabPage
        '
        Me.TextTabPage.Controls.Add(Me.OutputTextBox)
        Me.TextTabPage.Location = New System.Drawing.Point(4, 25)
        Me.TextTabPage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextTabPage.Name = "TextTabPage"
        Me.TextTabPage.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextTabPage.Size = New System.Drawing.Size(1213, 908)
        Me.TextTabPage.TabIndex = 0
        Me.TextTabPage.Text = "Output"
        Me.TextTabPage.UseVisualStyleBackColor = True
        '
        'OutputTextBox
        '
        Me.OutputTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputTextBox.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OutputTextBox.Location = New System.Drawing.Point(4, 4)
        Me.OutputTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OutputTextBox.Multiline = True
        Me.OutputTextBox.Name = "OutputTextBox"
        Me.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.OutputTextBox.Size = New System.Drawing.Size(1205, 900)
        Me.OutputTextBox.TabIndex = 0
        Me.OutputTextBox.WordWrap = False
        '
        'DataGridViewTabPage
        '
        Me.DataGridViewTabPage.Controls.Add(Me.SplitContainer2)
        Me.DataGridViewTabPage.Location = New System.Drawing.Point(4, 25)
        Me.DataGridViewTabPage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridViewTabPage.Name = "DataGridViewTabPage"
        Me.DataGridViewTabPage.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridViewTabPage.Size = New System.Drawing.Size(1213, 908)
        Me.DataGridViewTabPage.TabIndex = 2
        Me.DataGridViewTabPage.Text = "Data table"
        Me.DataGridViewTabPage.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(4, 4)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.OutputDataGridView)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.OutputGridControl)
        Me.SplitContainer2.Size = New System.Drawing.Size(1205, 900)
        Me.SplitContainer2.SplitterDistance = 370
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 2
        '
        'OutputDataGridView
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.OutputDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.OutputDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.OutputDataGridView.DefaultCellStyle = DataGridViewCellStyle2
        Me.OutputDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.OutputDataGridView.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OutputDataGridView.Name = "OutputDataGridView"
        Me.OutputDataGridView.RowHeadersWidth = 51
        Me.OutputDataGridView.Size = New System.Drawing.Size(1205, 370)
        Me.OutputDataGridView.TabIndex = 0
        '
        'OutputGridControl
        '
        Me.OutputGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputGridControl.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OutputGridControl.Location = New System.Drawing.Point(0, 0)
        Me.OutputGridControl.MainView = Me.GridView1
        Me.OutputGridControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OutputGridControl.Name = "OutputGridControl"
        Me.OutputGridControl.Size = New System.Drawing.Size(1205, 525)
        Me.OutputGridControl.TabIndex = 1
        Me.OutputGridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.OutputGridControl
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        '
        'PropertyGridTabPage
        '
        Me.PropertyGridTabPage.Controls.Add(Me.OutputPropertyGrid)
        Me.PropertyGridTabPage.Location = New System.Drawing.Point(4, 25)
        Me.PropertyGridTabPage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PropertyGridTabPage.Name = "PropertyGridTabPage"
        Me.PropertyGridTabPage.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PropertyGridTabPage.Size = New System.Drawing.Size(1213, 908)
        Me.PropertyGridTabPage.TabIndex = 1
        Me.PropertyGridTabPage.Text = "Properties"
        Me.PropertyGridTabPage.UseVisualStyleBackColor = True
        '
        'OutputPropertyGrid
        '
        Me.OutputPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputPropertyGrid.Location = New System.Drawing.Point(4, 4)
        Me.OutputPropertyGrid.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OutputPropertyGrid.Name = "OutputPropertyGrid"
        Me.OutputPropertyGrid.Size = New System.Drawing.Size(1205, 900)
        Me.OutputPropertyGrid.TabIndex = 0
        '
        'DevExpressMapTabPage
        '
        Me.DevExpressMapTabPage.Location = New System.Drawing.Point(4, 25)
        Me.DevExpressMapTabPage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DevExpressMapTabPage.Name = "DevExpressMapTabPage"
        Me.DevExpressMapTabPage.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DevExpressMapTabPage.Size = New System.Drawing.Size(1213, 908)
        Me.DevExpressMapTabPage.TabIndex = 4
        Me.DevExpressMapTabPage.Text = "DevExpress Map"
        Me.DevExpressMapTabPage.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1579, 937)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Form1"
        Me.Text = "Skeeter Utilities Tester"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.OutputTabControl.ResumeLayout(False)
        Me.InputTabPage.ResumeLayout(False)
        Me.InputTabPage.PerformLayout()
        Me.TextTabPage.ResumeLayout(False)
        Me.TextTabPage.PerformLayout()
        Me.DataGridViewTabPage.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.OutputDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OutputGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PropertyGridTabPage.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataTableFromCSVButton As Button
    Friend WithEvents GetFileButton As Button
    Friend WithEvents GetDirectoryContentsButton As Button
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents OutputTabControl As TabControl
    Friend WithEvents TextTabPage As TabPage
    Friend WithEvents OutputTextBox As TextBox
    Friend WithEvents PropertyGridTabPage As TabPage
    Friend WithEvents OutputPropertyGrid As PropertyGrid
    Friend WithEvents DataGridViewTabPage As TabPage
    Friend WithEvents OutputDataGridView As DataGridView
    Friend WithEvents DataTableFromExcelButton As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ShowDataTableInAFormButton As Button
    Friend WithEvents DataTableToCSVButton As Button
    Friend WithEvents GetMetadataDatasetFromDataTableButton As Button
    Friend WithEvents DescribeADatasetButton As Button
    Friend WithEvents OutputGridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GetMetadataDatasetFromExcelButton As Button
    Friend WithEvents GetDatasetFromExcelButton As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents DataColumnIsNumericButton As Button
    Friend WithEvents ShowDataTableExplorerControlButton As Button
    Friend WithEvents PDFTableToDataTableButton As Button
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents InputTabPage As TabPage
    Friend WithEvents InputTextBox As TextBox
    Friend WithEvents TransposeADataTableButton As Button
    Friend WithEvents DataTableFromSQLServerButton As Button
    Friend WithEvents DevExpressMapTabPage As TabPage
    Friend WithEvents GetMetadataFromDataTableButton As Button
End Class

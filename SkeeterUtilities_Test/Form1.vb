Imports System.IO
Imports SkeeterUtilities.DirectoryAndFile.DirectoryAndFileUtilities
Imports SkeeterUtilities.DataFileToDataTableConverters.DataFileToDataTableConverters



Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.OutputTextBox.WordWrap = False
        Me.OutputDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells

        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub DataTableFromCSVButton_Click(sender As Object, e As EventArgs) Handles DataTableFromCSVButton.Click
        'Clear the tools
        ClearTools()

        Try
            Dim CSVFile As FileInfo = GetFile("Data files|*.csv", "Select a CSV file.", "C:\Temp")
            Dim DT As DataTable = GetDataTableFromCSV(New FileInfo(CSVFile.FullName), True, Format.Delimited)

            'DataTable
            Me.OutputDataGridView.DataSource = DT

            'Metadata to GridControl
            Me.OutputGridControl.DataSource = GetMetadataFromDataTable(DT)

            'Text
            Me.OutputTextBox.Text = DataTableToCSV(DT, "|")

            'Object
            Me.OutputPropertyGrid.SelectedObject = DT

            Me.OutputTabControl.SelectedTab = DataGridViewTabPage
        Catch ex As Exception
            ClearTools()
            Me.OutputTextBox.Text = ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")"
            Me.OutputTabControl.SelectedTab = TextTabPage
        End Try
    End Sub

    Private Sub DataTableToCSVButton_Click(sender As Object, e As EventArgs) Handles DataTableToCSVButton.Click
        'Clear the tools
        ClearTools()

        Try
            Dim CSVFile As FileInfo = GetFile("Data files|*.csv", "Select a CSV file.", "C:\Temp")
            Dim DT As DataTable = GetDataTableFromCSV(New FileInfo(CSVFile.FullName), True, Format.Delimited)

            'DataTable
            Me.OutputDataGridView.DataSource = DT

            'Text
            Me.OutputTextBox.Text = DataTableToCSV(DT, "|")

            'Object
            Me.OutputPropertyGrid.SelectedObject = DT

            Me.OutputTabControl.SelectedTab = TextTabPage
        Catch ex As Exception
            ClearTools()
            Me.OutputTextBox.Text = ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")"
            Me.OutputTabControl.SelectedTab = TextTabPage
        End Try
    End Sub

    Private Sub GetFileButton_Click(sender As Object, e As EventArgs) Handles GetFileButton.Click
        'Clear the tools
        ClearTools()

        Try
            Dim Result As FileInfo = GetFile("Any file|*.*", "Select a file", "C:\temp")
            Me.OutputTextBox.Text = Result.FullName
            Me.OutputTabControl.SelectedTab = TextTabPage
        Catch ex As Exception
            ClearTools()
            Me.OutputTextBox.Text = ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")"
            Me.OutputTabControl.SelectedTab = TextTabPage
        End Try
    End Sub

    Private Sub GetDirectoryContentsButton_Click(sender As Object, e As EventArgs) Handles GetDirectoryContentsButton.Click
        'Clear the tools
        ClearTools()

        Try
            Dim MyFileInfo As FileInfo = GetFile("*|*", "Select a directory", "C:\Temp")
            If Not MyFileInfo Is Nothing Then
                Dim DirInfo As New DirectoryInfo(MyFileInfo.DirectoryName)

                'Dim DirInfo As New DirectoryInfo(Me.GetAListOfSubdirectoriesAndFilesInADirectoryTextBox.Text)

                'Text
                Dim Result As String = GetContentsOfDirectory(DirInfo, "|")
                Me.OutputTextBox.Text = Result

                'DataTable
                Dim DT As DataTable = GetContentsOfDirectoryAsDataTable(DirInfo)
                Me.OutputDataGridView.DataSource = DT
                Me.OutputTabControl.SelectedTab = Me.DataGridViewTabPage

                'Object
                Me.OutputPropertyGrid.SelectedObject = DT
            Else
                MsgBox("There was a problem getting the directory from the file you selected. Canceled.")
            End If


        Catch ex As Exception
            ClearTools()
            Me.OutputTextBox.Text = ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")"
            Me.OutputTabControl.SelectedTab = TextTabPage
        End Try
    End Sub





    Private Sub DataTableFromExcelButton_Click(sender As Object, e As EventArgs) Handles DataTableFromExcelButton.Click
        'Clear the tools
        ClearTools()

        Dim InputFileInfo As FileInfo = GetFile("Excel files|*.xls;*.xlsx", "Select an Excel workbook.", "C:\Temp")
        Dim CS As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & InputFileInfo.FullName & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1"";" ';IMEX=1 means all columns imported as text

        Try
            Dim DS As DataSet = GetDatasetFromExcelWorkbook(CS)

            'Text
            Me.OutputTextBox.Text = "See DataGridView tab page for contents of first DataTable in the Dataset's Tables collection." & vbNewLine & "Contents of Dataset: " & DS.DataSetName & vbNewLine
            For Each DT As DataTable In DS.Tables
                Me.OutputTextBox.AppendText(vbTab & "Table: " & DT.TableName & " Rows: " & DT.Rows.Count & vbNewLine)
            Next

            'Object
            Me.OutputPropertyGrid.SelectedObject = DS

            'DataTable
            If DS.Tables.Count > 0 Then
                OutputDataGridView.DataSource = DS.Tables(0)
            End If

            Me.OutputTabControl.SelectedTab = TextTabPage

        Catch ex As Exception
            ClearTools()
            Me.OutputTextBox.Text = ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")"
            Me.OutputTabControl.SelectedTab = TextTabPage
        End Try
    End Sub

    Private Sub ShowDataTableInAFormButton_Click(sender As Object, e As EventArgs) Handles ShowDataTableInAFormButton.Click
        'Clear the tools
        ClearTools()

        Try
            Dim CSVFile As FileInfo = GetFile("Data files|*.csv", "Select a CSV file.", "C:\Temp")
            Dim DT As DataTable = GetDataTableFromCSV(New FileInfo(CSVFile.FullName), True, Format.Delimited)

            ShowDataTableInForm(DT)
        Catch ex As Exception
            ClearTools()
            Me.OutputTextBox.Text = ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")"
            Me.OutputTabControl.SelectedTab = TextTabPage
        End Try
    End Sub


    Private Sub OutputDataGridView_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles OutputDataGridView.DataError
        'Clear the tools
        ClearTools()
        Me.OutputTextBox.Text = "Data Error. " & e.Exception.Message
    End Sub

    Private Sub ClearTools()
        Me.OutputTextBox.Text = ""
        Me.OutputPropertyGrid.SelectedObject = Nothing
        Me.OutputDataGridView.DataSource = Nothing
    End Sub

    Private Sub GetMetadataDatasetFromDataTableButton_Click(sender As Object, e As EventArgs) Handles GetMetadataDatasetFromDataTableButton.Click
        'Clear the tools
        ClearTools()

        Try
            Dim CSVFile As FileInfo = GetFile("Data files|*.csv", "Select a CSV file.", "C:\Temp")
            Dim DT As DataTable = GetDataTableFromCSV(New FileInfo(CSVFile.FullName), True, Format.Delimited)
            If Not DT Is Nothing Then
                Dim MetadataDataset As DataSet = GetMetadataDatasetFromDataTable(DT, CSVFile.Name, "TEST")


                'DataTable
                Dim OutputDataTable As DataTable = MetadataDataset.Tables(0)
                Me.OutputDataGridView.DataSource = OutputDataTable
                Me.OutputGridControl.DataSource = MetadataDataset.Tables(0)
                Me.OutputGridControl.MainView.PopulateColumns()

                'Text
                Me.OutputTextBox.Text = DataTableToCSV(OutputDataTable, ",")

                'Object
                Me.OutputPropertyGrid.SelectedObject = OutputDataTable

                Me.OutputTabControl.SelectedTab = DataGridViewTabPage

                'Show the data
                ShowDataSetInForm(MetadataDataset)
            Else
                MsgBox("Data table is nothing (GetMetadataDatasetFromDataTableButton_Click).")
            End If

        Catch ex As Exception
            ClearTools()
            Me.OutputTextBox.Text = ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")"
            Me.OutputTabControl.SelectedTab = TextTabPage
        End Try
    End Sub

    Private Sub DescribeADatasetButton_Click(sender As Object, e As EventArgs) Handles DescribeADatasetButton.Click
        ClearTools()

        Dim ExcelFileInfo As FileInfo = GetFile("Excel files|*.xls;*.xlsx", "Selecte an Excel file", "C:\Temp") ' 
        Dim ExcelFile As String = ExcelFileInfo.FullName
        Dim CS As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ExcelFile & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1"";" ';IMEX=1 means all columns imported as text
        Me.OutputTextBox.Text = CS & vbNewLine
        Dim DS As DataSet = GetDatasetFromExcelWorkbook(CS)
        Dim DatasetDescription As String = GetDatasetDescription(DS, True)
        Me.OutputTextBox.AppendText(DatasetDescription)
        Me.OutputTabControl.SelectedTab = TextTabPage
    End Sub

    Private Sub GetMetadataDatasetFromExcelButton_Click(sender As Object, e As EventArgs) Handles GetMetadataDatasetFromExcelButton.Click
        ClearTools()

        Dim ExcelFileInfo As FileInfo = GetFile("Excel files|*.xls;*.xlsx", "Selecte an Excel file", "C:\Temp") ' 
        Dim ExcelFile As String = ExcelFileInfo.FullName
        Dim ExcelConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ExcelFile & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1"";" ';IMEX=1 means all columns imported as text
        Dim DS As DataSet = GetDatasetFromExcelWorkbook(ExcelConnectionString)

        ' Me.OutputTextBox.Text = ExcelConnectionString & vbNewLine
        'Dim DS As DataSet = GetDatasetFromExcelWorkbook(ExcelConnectionString)
        Debug.Print(GetDatasetDescription(DS, False))
    End Sub

    Private Sub GetDatasetFromExcelButton_Click(sender As Object, e As EventArgs) Handles GetDatasetFromExcelButton.Click
        'Clear the tools
        ClearTools()

        'Get an Excel file to work with
        Dim ExcelFileInfo As FileInfo = GetFile("Excel files|*.xls;*.xlsx", "Selecte an Excel file", "C:\Temp")
        Dim ExcelFile As String = ExcelFileInfo.FullName
        Dim CS As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ExcelFile & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1"";" ';IMEX=1 means all columns imported as text
        Me.OutputTextBox.Text = CS & vbNewLine

        'Build the Excel Dataset
        Dim DS As DataSet = GetDatasetFromExcelWorkbook(CS, True, True)

        'Output some stuff
        Dim DatasetDescription As String = GetDatasetDescription(DS, False)
        Me.OutputTextBox.AppendText(DatasetDescription)
        Me.OutputTabControl.SelectedTab = TextTabPage

        For Each T As DataTable In DS.Tables
            Me.OutputTextBox.AppendText("------------------------------------------------------" & vbNewLine & T.TableName & vbNewLine & DataTableToCSV(T, vbTab) & vbNewLine & vbNewLine)

        Next

        ShowDataSetInForm(DS)
    End Sub

    Private Sub DataColumnIsNumericButton_Click(sender As Object, e As EventArgs) Handles DataColumnIsNumericButton.Click
        'Clear the tools
        ClearTools()

        Try
            'Make a DataTable
            Dim DT As New DataTable("DataColumn IsNumeric")
            DT.Columns.Add("Name", GetType(String))
            DT.Columns.Add("Age", GetType(Integer))
            DT.Columns.Add("Height", GetType(Double))
            DT.Columns.Add("IsMale", GetType(Boolean))

            'Add two rows
            Dim EdRow As DataRow = DT.NewRow
            With EdRow
                .Item("Name") = "Ed"
                .Item("Age") = 9
                .Item("Height") = 122.34
                .Item("IsMale") = True
            End With
            DT.Rows.Add(EdRow)

            Dim PRow As DataRow = DT.NewRow
            With PRow
                .Item("Name") = "Penelope"
                .Item("Age") = 88
                .Item("Height") = 112.59
                .Item("IsMale") = False
            End With
            DT.Rows.Add(PRow)

            'Output
            Me.OutputDataGridView.DataSource = DT
            Dim MetaDS As DataSet = GetMetadataDatasetFromDataTable(DT)
            Me.OutputGridControl.DataSource = MetaDS.Tables(0)

            'Output DT as text
            Me.OutputTextBox.AppendText(SkeeterUtilities.DataFileToDataTableConverters.DataFileToDataTableConverters.DataTableToCSV(DT, "|") & vbNewLine & vbNewLine)

            'Show whether the datacolumn is numeri
            For Each Row As DataRow In DT.Rows
                For Each Col As DataColumn In DT.Columns
                    Me.OutputTextBox.AppendText(Col.ColumnName & vbTab & Col.DataType.ToString & vbTab & ColumnDataTypeIsNumeric(Col) & vbNewLine)
                Next
            Next
        Catch ex As Exception
            ClearTools()
            Me.OutputTextBox.Text = ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")"
            Me.OutputTabControl.SelectedTab = TextTabPage
        End Try
    End Sub

    Private Sub ShowDataTableExplorerControlButton_Click(sender As Object, e As EventArgs) Handles ShowDataTableExplorerControlButton.Click
        'Clear the tools
        ClearTools()

        'Make a DataTable
        Dim ReturnDataTable As New DataTable("A DataTable")

        'Add some columns
        With ReturnDataTable.Columns
            .Add("Name", GetType(String))
            .Add("Age", GetType(Integer))
            .Add("Birthday", GetType(Date))
        End With

        'Create two data rows and add them to the table
        Dim SkeeterDataRow As DataRow = ReturnDataTable.NewRow
        With SkeeterDataRow
            .Item("Name") = "Skeeter"
            .Item("Age") = 26
            .Item("Birthday") = "2010-05-23"
        End With
        ReturnDataTable.Rows.Add(SkeeterDataRow)

        Dim PjoleneDataRow As DataRow = ReturnDataTable.NewRow
        With SkeeterDataRow
            .Item("Name") = "Pjolene"
            .Item("Age") = 27
            .Item("Birthday") = "2011-04-29"
        End With
        ReturnDataTable.Rows.Add(PjoleneDataRow)
        Debug.Print(ReturnDataTable.Rows.Count)

        Dim DECForm As New DatasetExplorerControlForm(ReturnDataTable)
        'DECForm.Show()
    End Sub

    Private Sub PDFTableToDataTableButton_Click(sender As Object, e As EventArgs) Handles PDFTableToDataTableButton.Click
        ClearTools()

        Dim PDFTableText As String = "PAR/STRAT low medium high TOTAL 
N 60 35 10 105 
Totarea 795.31 452.35 138.43 1386.09 
n 17 27 10 54 
Areasur 217.74 354.72 138.43 710.89 
#seen 37 246 205 488 
Density 0.1699 0.6935 1.4809 0.6024"
        'Me.InputTextBox.Text = PDFTableText
        Me.OutputTextBox.Text = "Converting the following PDF table text to DataTable. See the output data grid for conversion results" & vbNewLine & vbNewLine
        Me.OutputTextBox.AppendText(Me.InputTextBox.Text & vbNewLine)


        Dim PDFDT As DataTable = GetDataTableFromPDFTableText(Me.InputTextBox.Text, " ")
        'Dim TransposedDT As DataTable = GetTransposedDataTable(PDFDT)
        Me.OutputDataGridView.DataSource = PDFDT
        Me.OutputTabControl.SelectedTab = DataGridViewTabPage
    End Sub

    Private Sub TransposeADataTableButton_Click(sender As Object, e As EventArgs) Handles TransposeADataTableButton.Click
        ClearTools()
        Me.InputTextBox.Text = "PAR/STRAT,low,medium,high,TOTAL 
N,60,35,10,105 
Totarea,795.31,452.35,138.43,1386.09 
n,17,27,10,54 
Areasur,217.74,354.72,138.43,710.89 
#seen,37,246,205,488 
Density,0.1699,0.6935,1.4809,0.6024"
        Dim DT As DataTable = GetDataTableFromCSVString(Me.InputTextBox.Text)
        Me.OutputTextBox.Text = "The unformatted CSV DataTable looks like this:" & vbNewLine & vbNewLine
        Me.OutputTextBox.AppendText(DataTableToCSV(DT))
        Me.OutputDataGridView.DataSource = DT
        Me.OutputGridControl.DataSource = GetTransposedDataTable(DT)
        Me.OutputTabControl.SelectedTab = DataGridViewTabPage
    End Sub

    Private Sub DataTableFromSQLServerButton_Click(sender As Object, e As EventArgs) Handles DataTableFromSQLServerButton.Click

        ClearTools()
        Try
            Me.InputTextBox.Text = "Server=inpyugamsvm01\nuna_dev;Database=Moose;Trusted_Connection=True;"
            Dim CS As String = InputTextBox.Text
            Dim Sql As String = "SELECT Top 10 SurveyName FROM GSPE_Surveys"
            Dim DT As DataTable = GetDataTableFromSQLServerDatabase(CS, Sql)

            Me.OutputTextBox.Text = "ConnectionString = " & CS & vbNewLine & "SQl = " & Sql & vbNewLine & vbNewLine
            Me.OutputTextBox.AppendText(DataTableToCSV(DT))
            Me.OutputDataGridView.DataSource = DT
            Me.OutputTabControl.SelectedTab = DataGridViewTabPage

        Catch ex As Exception
            ClearTools()
        Me.OutputTextBox.Text = ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")"
        Me.OutputTabControl.SelectedTab = TextTabPage
        End Try
    End Sub

    Private Sub GetMetadataFromDataTableButton_Click(sender As Object, e As EventArgs) Handles GetMetadataFromDataTableButton.Click
        'Clear the tools
        ClearTools()

        Try

            'Transform a CSV into a DataTable
            Dim CSVFileInfo As FileInfo = GetFile("Data files|*.csv", "Select a CSV file.", "C:\Temp")
            'Dim CSVFileInfo As New FileInfo("C:\Temp\zSomeSheepData.csv")
            'Dim DT As DataTable = GetDataTableFromCSV(New FileInfo(CSVFileInfo.FullName), True, Format.Delimited)
            Dim DT As DataTable = GetDataTableFromCSV(CSVFileInfo)

            'Get Metadata from the CSV DataTable
            Dim MetadataDataTable As DataTable = GetMetadataFromDataTable(DT, ",", CSVFileInfo.Name, "Worksheet name would go here")
            ''Dim MetadataDataset As DataSet = GetMetadataDatasetFromDataTable(DT, CSVFile.Name, "TEST")


            ''DataTable

            Me.OutputDataGridView.DataSource = DT
            Me.OutputGridControl.DataSource = MetadataDataTable
            Me.OutputGridControl.MainView.PopulateColumns()

            '    'Text
            Me.OutputTextBox.Text = DataTableToCSV(MetadataDataTable, ",")

            '    'Object
            Me.OutputPropertyGrid.SelectedObject = MetadataDataTable

            Me.OutputTabControl.SelectedTab = DataGridViewTabPage


        Catch ex As Exception
            ClearTools()
            Me.OutputTextBox.Text = ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")"
            Me.OutputTabControl.SelectedTab = TextTabPage
        End Try
    End Sub
End Class

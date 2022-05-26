Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.IO.Compression
Imports System.Windows.Forms
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Namespace DirectoryAndFile
    Public Class DirectoryAndFileUtilities

        ''' <summary>
        ''' Opens and OpenFileDialog to allow the user to select a file to open.
        ''' </summary>
        ''' <param name="Filter">File filter. Example: "Data files|*.csv;*.txt;*.tab;*.xls;*.xlsx;*.dbf". String</param>
        ''' <param name="Title">OpenFileDialog title. String.</param>
        ''' <returns></returns>
        Public Shared Function GetFile(Filter As String, Title As String, InitialDirectory As String) As FileInfo
            If My.Computer.FileSystem.DirectoryExists(InitialDirectory) = False Then
                InitialDirectory = "C:\"
            End If
            Dim OFD As New OpenFileDialog()
            Try
                With OFD
                    .CheckFileExists = True
                    .CheckPathExists = True
                    .InitialDirectory = InitialDirectory
                    .Filter = Filter
                    .RestoreDirectory = False
                    .Title = Title
                End With
                If OFD.ShowDialog = DialogResult.OK Then
                    Dim MyFileInfo As New FileInfo(OFD.FileName)
                    Return MyFileInfo
                Else
                    Return New FileInfo("")
                End If
            Catch ex As Exception
                MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
                Return Nothing
            End Try

        End Function



        ''' <summary>
        ''' Returns a list of subdirectories and files in DirectoryInfo.Fullname
        ''' </summary>
        ''' <param name="DirectoryInfo">DirectoryInfo pointing to the directory to interrogate.</param>
        ''' <returns>Pipe (|) separated values list of subdirectories and files. String.</returns>
        Public Shared Function GetContentsOfDirectory(DirectoryInfo As System.IO.DirectoryInfo, Separator As String) As String
            Dim ReturnString As String = "Type" & Separator & "Name" & Separator & "FullPath" & vbNewLine
            If My.Computer.FileSystem.DirectoryExists(DirectoryInfo.FullName) = True Then
                Try
                    'Loop through the directories and add them
                    For Each MyDirectory As String In My.Computer.FileSystem.GetDirectories(DirectoryInfo.FullName)
                        Dim MyDirectoryInfo As New DirectoryInfo(MyDirectory)
                        ReturnString = ReturnString & "Directory|" & ReturnString & MyDirectoryInfo.Name & Separator & MyDirectoryInfo.FullName & vbNewLine
                    Next

                    'Loop through the files and add them
                    For Each MyFilename As String In My.Computer.FileSystem.GetFiles(DirectoryInfo.FullName)
                        Dim MyFileInfo As New FileInfo(MyFilename)
                        ReturnString = ReturnString & "File" & Separator & MyFileInfo.Name & Separator & MyFileInfo.FullName & vbNewLine
                    Next
                Catch ex As Exception
                    MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
                End Try
            End If
            Return ReturnString
        End Function



        ''' <summary>
        ''' Returns the contents of the submitted directory as a DataTable with three columns (Type, Name, FullPath).
        ''' </summary>
        ''' <param name="DirectoryInfo">Directory to interrogate. DirectoryInfo.</param>
        ''' <returns>DataTable</returns>
        Public Shared Function GetContentsOfDirectoryAsDataTable(DirectoryInfo As System.IO.DirectoryInfo) As DataTable
            Dim DT As New DataTable(DirectoryInfo.Name)
            With DT
                .Columns.Add("Type", GetType(String))
                .Columns.Add("Name", GetType(String))
                .Columns.Add("FullPath", GetType(String))
            End With

            If My.Computer.FileSystem.DirectoryExists(DirectoryInfo.FullName) = True Then
                Try
                    'Loop through the directories and add them
                    For Each MyDirectory As String In My.Computer.FileSystem.GetDirectories(DirectoryInfo.FullName)
                        Dim MyDirectoryInfo As New DirectoryInfo(MyDirectory)
                        Dim NewRow As DataRow = DT.NewRow
                        With NewRow
                            .Item("Type") = "Directory"
                            .Item("Name") = MyDirectoryInfo.Name
                            .Item("FullPath") = MyDirectoryInfo.FullName
                        End With
                        DT.Rows.Add(NewRow)
                    Next

                    'Loop through the files and add them
                    For Each MyFilename As String In My.Computer.FileSystem.GetFiles(DirectoryInfo.FullName)
                        Dim MyFileInfo As New FileInfo(MyFilename)
                        Dim NewRow As DataRow = DT.NewRow
                        With NewRow
                            .Item("Type") = "File"
                            .Item("Name") = MyFileInfo.Name
                            .Item("FullPath") = MyFileInfo.FullName
                        End With
                        DT.Rows.Add(NewRow)
                    Next
                Catch ex As Exception
                    MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
                End Try
            End If
            Return DT
        End Function

    End Class

End Namespace


Namespace DataFileToDataTableConverters


    Public Class DataFileToDataTableConverters

        ''' <summary>
        ''' Returns a DataTable of information about SourceDataTable.
        ''' </summary>
        ''' <param name="SourceDataTable">Source data table. DataTable.</param>
        ''' <param name="UniqueValuesSeparator">One thing that is returned for each column of SourceDataTable is a list of unique values. 
        ''' These unique values will be separated by UniqueValuesSeparator.</param>
        ''' <returns></returns>
        Public Shared Function GetMetadataFromDataTable(SourceDataTable As DataTable, Optional UniqueValuesSeparator As String = ",") As DataTable

            'Build a data table to hold metadata
            Dim MetadataDataTable As DataTable = GetMetadataDataTable()
            MetadataDataTable.TableName = SourceDataTable.TableName & " Metadata"

            'Build a metadata unique values data table
            Dim MetadataUniqueValuesDataTable As DataTable = GetMetadataUniqueValuesDataTable()

            'Loop through the columns in the data table and build up metadata
            For Each SourceColumn As DataColumn In SourceDataTable.Columns
                Dim NewMetadataRow As DataRow = MetadataDataTable.NewRow


                Dim SourceColumnIsNumeric As Boolean = True
                Dim SourceColumnIsBit As Boolean = True
                Dim SourceColumnIsInteger As Boolean = False
                Dim SourceColumnIsDate As Boolean = True
                Dim SourceColumnIsBlank As Boolean = False

                'Loop through each row in the column
                Dim RowIndex As Integer = 0
                Dim BlankCount As Integer = 0
                Dim NullCount As Integer = 0
                Dim NumericValuesCount As Integer = 0
                Dim BitValuesCount As Integer = 0
                Dim TrueValuesCount As Integer = 0
                Dim FalseValuesCount As Integer = 0
                Dim DatesCount As Integer = 0
                Dim MaxLength As Integer = 0
                Dim RowCount As Integer = SourceDataTable.Rows.Count
                Dim Numericity As Decimal = 0
                Dim Bitness As Decimal = 0
                Dim Dateness As Decimal = 0

                'Loop through the rows
                For Each SourceRow As DataRow In SourceDataTable.Rows

                    'Make sure we have a row
                    If Not SourceRow Is Nothing Then

                        'Make sure the SourceRow's cell is not nothing
                        If Not SourceRow.Item(SourceColumn.ColumnName) Is Nothing Then

                            'Make sure the cell is not null
                            If IsDBNull(SourceRow.Item(SourceColumn.ColumnName)) Then

                                'The cell is null
                                NullCount = NullCount + 1

                            ElseIf SourceRow.Item(SourceColumn.ColumnName).ToString.Length = 0 Then

                                'The cell is blank
                                BlankCount = BlankCount + 1

                            Else
                                'Cell is not null and not blank, process it

                                'Get the cell's value
                                Dim CellValue As String = SourceRow.Item(SourceColumn.ColumnName).ToString.Trim

                                'Max length
                                If CellValue.Length > MaxLength Then MaxLength = CellValue.Length

                                'IsNumeric
                                If IsNumeric(CellValue) Then
                                    NumericValuesCount = NumericValuesCount + 1
                                End If
                            End If



                            '    

                            '    

                            '    ''Get the bitness
                            '    'If CellValue.ToString.ToLower = "true" Or CellValue.ToString.ToLower = "t" Or CellValue.ToString.Trim = "1" Or CellValue.ToString.ToLower = "yes" Or CellValue.ToString.ToLower = "y" Then
                            '    '    TrueValuesCount = TrueValuesCount + 1
                            '    'ElseIf CellValue.ToString.ToLower = "false" Or CellValue.ToString.ToLower = "f" Or CellValue.ToString.Trim = "0" Or CellValue.ToString.ToLower = "no" Or CellValue.ToString.ToLower = "n" Then
                            '    '    FalseValuesCount = FalseValuesCount + 1
                            '    'End If

                            '    ''IsBit
                            '    'If CellValue.ToString.Trim <> "1" And CellValue.ToString.Trim <> "0" Then
                            '    '    SourceColumnIsBit = False
                            '    'End If

                            '    ''IsDate
                            '    'If IsDate(CellValue) = True Then DatesCount = DatesCount + 1 Else SourceColumnIsDate = False

                            '    ''If any cell values are not numeric then the column is not numeric
                            '    'If CellValue.ToString.Trim <> "" Then
                            '    '    If IsNumeric(CellValue) = False Then SourceColumnIsNumeric = False
                            '    'Else
                            '    '    'Blank, increment the blank counter
                            '    '    BlankCount = BlankCount + 1
                            '    'End If
                            'Else
                            '    BlankCount = BlankCount + 1
                            'End If
                        End If
                    End If
                    RowIndex = RowIndex + 1
                Next

                'Bitness
                If FalseValuesCount > 0 Then SourceColumnIsBit = False

                'Dateness
                If RowCount - BlankCount > 0 Then
                    Dateness = DatesCount / (RowCount - BlankCount)
                End If


                'Now get all the distinct values
                Dim ColumnNames() As String = {SourceColumn.ColumnName}
                Dim UniqueValuesDataTable As DataTable = SourceDataTable.DefaultView.ToTable(True, ColumnNames)
                Dim UniqueValues As String = ""
                'Dim CSVSeparator As String = ","

                'Build a separated string of unique values
                For Each Row As DataRow In UniqueValuesDataTable.Rows
                    If Not Row.Item(0) Is Nothing Then
                        If Not IsDBNull(Row.Item(0)) Then
                            'Add the item to the list of unique items
                            Dim RowValue As String = Row.Item(0)
                            UniqueValues = UniqueValues & RowValue & UniqueValuesSeparator
                        End If
                    End If
                Next

                'We can only guess at column types if there is data in the column. If the entire column is blank then reset everything.
                If BlankCount = RowCount Then
                    SourceColumnIsNumeric = False
                    SourceColumnIsBit = False
                    SourceColumnIsDate = False
                    SourceColumnIsBlank = True
                End If

                'Get at numericity
                If RowCount - BlankCount > 0 Then Numericity = NumericValuesCount / (RowCount - BlankCount) Else Numericity = 0
                With NewMetadataRow
                    '.Item("Filename") = Filename
                    ' .Item("Worksheet") = Worksheet
                    .Item("TableName") = SourceColumn.Table.TableName
                    .Item("ColumnName") = SourceColumn.ColumnName
                    .Item("Caption") = SourceColumn.Caption
                    .Item("DataType") = SourceColumn.DataType.ToString.Replace("System.", "")
                    .Item("AllowDBNull") = SourceColumn.AllowDBNull
                    .Item("AutoIncrement") = SourceColumn.AutoIncrement
                    .Item("DefaultValue") = SourceColumn.DefaultValue
                    .Item("Expression") = SourceColumn.Expression
                    .Item("IsUnique") = SourceColumn.Unique
                    .Item("MaxLength") = MaxLength
                    .Item("IsNumeric") = SourceColumnIsNumeric
                    .Item("IsBit") = SourceColumnIsBit
                    .Item("Bitness") = Bitness
                    .Item("Blanks") = BlankCount
                    .Item("IsBlank") = SourceColumnIsBlank
                    .Item("UniqueValues") = UniqueValues
                    .Item("NumericValuesCount") = NumericValuesCount
                    .Item("Numericity") = Numericity
                    .Item("BitValuesCount") = TrueValuesCount + FalseValuesCount
                    .Item("TrueValuesCount") = TrueValuesCount
                    .Item("FalseValuesCount") = FalseValuesCount
                    .Item("RowCount") = RowCount
                    .Item("DatesCount") = DatesCount
                    .Item("Dateness") = Dateness
                    .Item("IsDate") = SourceColumnIsDate
                    .Item("Max") = SourceDataTable.Compute("Max([" & SourceColumn.ColumnName & "])", "")
                    .Item("Min") = SourceDataTable.Compute("Min([" & SourceColumn.ColumnName & "])", "")
                End With
                MetadataDataTable.Rows.Add(NewMetadataRow)

                'add the column's unique values to the unique values datatable
                For Each Row As DataRow In UniqueValuesDataTable.Rows
                    If Not Row.Item(0) Is Nothing Then
                        If Not IsDBNull(Row.Item(0)) Then
                            'Make a new row for the unique values data table
                            Dim NewUniqueValuesRow As DataRow = MetadataUniqueValuesDataTable.NewRow
                            NewUniqueValuesRow.Item("ColumnName") = SourceColumn.ColumnName
                            NewUniqueValuesRow.Item("UniqueValue") = Row.Item(0)
                            MetadataUniqueValuesDataTable.Rows.Add(NewUniqueValuesRow)
                        End If
                    End If
                Next
            Next
            Return MetadataDataTable
        End Function

        '''' <summary>
        '''' Converts an Excel workbook and all its worksheets to a Dataset.
        '''' </summary>
        '''' <param name="ExcelConnectionString">Excel ConnectionString. String.</param>
        '''' <returns>Dataset.</returns>
        'Public Shared Function GetDatasetFromExcelWorkbook(ExcelConnectionString As String) As DataSet

        '    'Dataset to return
        '    Dim ExcelDataset As New DataSet

        '    Try

        '        'Name the Dataset for the input filename
        '        Dim CS As New OleDbConnectionStringBuilder(ExcelConnectionString)
        '        Dim DataSourceFileInfo As New FileInfo(CS.DataSource)
        '        If DataSourceFileInfo.Name.Trim.Length > 0 Then
        '            ExcelDataset.DataSetName = DataSourceFileInfo.Name
        '        End If

        '        'Get the workbook's worksheets into a DataTable and add them to the Dataset
        '        Dim WorksheetsDataTable As DataTable = GetExcelWorksheets(ExcelConnectionString)
        '        WorksheetsDataTable.TableName = "Worksheets"
        '        WorksheetsDataTable.Prefix = "Worksheets"
        '        'ExcelDataset.Tables.Add(WorksheetsDataTable)

        '        'Loop through the worksheets, convert them to DataTables and add them to the Dataset
        '        For Each WorksheetRow As DataRow In WorksheetsDataTable.Rows

        '            'Create a DataTable for the worksheet
        '            Dim WorksheetName As String = WorksheetRow.Item("TABLE_NAME")
        '            Dim WorksheetDataTable As New DataTable(WorksheetName)
        '            WorksheetDataTable.TableName = WorksheetName
        '            WorksheetDataTable.Prefix = "Dataset"

        '            'Query the worksheet's data into WorksheetDataTable 
        '            Dim Sql As String = "SELECT * FROM [" & WorksheetName & "]"
        '            Dim MyConnection As New OleDbConnection(ExcelConnectionString)
        '            MyConnection.Open()
        '            Dim MyCommand As New OleDbCommand(Sql, MyConnection)
        '            Dim MyDataAdapter As New OleDbDataAdapter(MyCommand)
        '            MyDataAdapter.Fill(WorksheetDataTable)
        '            ExcelDataset.Tables.Add(WorksheetDataTable)

        '            'Create and add a MetadataDataTable for WorksheetDataTable
        '            'Build a data table to hold metadata
        '            Dim MetadataDataTable As DataTable = GetMetadataDataTable()
        '            With MetadataDataTable
        '                .TableName = WorksheetName & " Metadata"
        '                .Prefix = "Metadata"
        '            End With
        '            ' ExcelDataset.Tables.Add(MetadataDataTable)

        '            'Build a metadata unique values data table
        '            Dim MetadataUniqueValuesDataTable As DataTable = GetMetadataUniqueValuesDataTable()
        '            MetadataUniqueValuesDataTable.TableName = WorksheetName & " Unique Values"
        '            MetadataUniqueValuesDataTable.Prefix = "UniqueValues"
        '            'ExcelDataset.Tables.Add(MetadataUniqueValuesDataTable)


        '            ' Define the relationship between the MetadataDataTable and the MetadataUniqueValuesDataTable on TableName and ColumnName
        '            'Dim ParentColumns() As DataColumn
        '            'Dim ChildColumns() As DataColumn
        '            'ParentColumns = New DataColumn() {MetadataDataTable.Columns("TableName"), MetadataDataTable.Columns("ColumnName")}
        '            'ChildColumns = New DataColumn() {MetadataUniqueValuesDataTable.Columns("TableName"), MetadataUniqueValuesDataTable.Columns("ColumnName")}
        '            'Dim ColumnsUniqueValuesDataRelation As New DataRelation(MetadataUniqueValuesDataTable.TableName, ParentColumns, ChildColumns)
        '            'ExcelDataset.Relations.Add(ColumnsUniqueValuesDataRelation)


        '            'For Each WorksheetColumn As DataColumn In WorksheetDataTable.Columns
        '            '    Dim NewMetadataRow As DataRow = MetadataDataTable.NewRow


        '            '    Dim WorksheetColumnIsNumeric As Boolean = True
        '            '    Dim WorksheetColumnIsBit As Boolean = True
        '            '    Dim WorksheetColumnIsInteger As Boolean = False
        '            '    Dim WorksheetColumnIsDate As Boolean = True
        '            '    Dim WorksheetColumnIsBlank As Boolean = False

        '            '    'Loop through each row in the column
        '            '    Dim RowIndex As Integer = 0
        '            '    Dim BlankCount As Integer = 0
        '            '    Dim NumericValuesCount As Integer = 0
        '            '    Dim BitValuesCount As Integer = 0
        '            '    Dim TrueValuesCount As Integer = 0
        '            '    Dim FalseValuesCount As Integer = 0
        '            '    Dim DatesCount As Integer = 0
        '            '    Dim MaxValue As Double
        '            '    Dim MinValue As Double
        '            '    Dim MaxLength As Integer = 0
        '            '    Dim RowCount As Integer = WorksheetDataTable.Rows.Count
        '            '    Dim Numericity As Decimal = 0
        '            '    Dim Bitness As Decimal = 0
        '            '    Dim Dateness As Decimal = 0

        '            '    For Each SourceRow As DataRow In WorksheetDataTable.Rows

        '            '        Dim CellValue As String = ""
        '            '        If Not SourceRow Is Nothing Then
        '            '            If Not IsDBNull(SourceRow.Item(WorksheetColumn.ColumnName)) Then


        '            '                'Get the cell's value
        '            '                CellValue = SourceRow.Item(WorksheetColumn.ColumnName)

        '            '                If RowIndex = 0 And IsNumeric(CellValue) Then
        '            '                    MinValue = CDbl(CellValue)
        '            '                    MaxValue = CDbl(CellValue)
        '            '                End If

        '            '                'Max length
        '            '                If CellValue.ToString.Trim.Length > MaxLength Then MaxLength = CellValue.ToString.Trim.Length

        '            '                'IsNumeric
        '            '                If IsNumeric(CellValue) Then
        '            '                    NumericValuesCount = NumericValuesCount + 1

        '            '                    'Max
        '            '                    'If CellValue > MaxValue Then MaxValue = CellValue
        '            '                    'MaxValue = WorksheetDataTable.Compute("MAX(" & WorksheetColumn.ColumnName & ")", "")

        '            '                    ''Min 
        '            '                    'If CellValue < MinValue Then MinValue = CellValue

        '            '                    'IsInteger
        '            '                    If Integer.TryParse(CellValue, vbNull) = True Then WorksheetColumnIsInteger = True
        '            '                    Dim IsInteger As Boolean = Integer.TryParse(CellValue, vbNull)

        '            '                End If

        '            '                'Get the bitness
        '            '                If CellValue.ToString.ToLower = "true" Or CellValue.ToString.ToLower = "t" Or CellValue.ToString.Trim = "1" Or CellValue.ToString.ToLower = "yes" Or CellValue.ToString.ToLower = "y" Then
        '            '                    TrueValuesCount = TrueValuesCount + 1
        '            '                ElseIf CellValue.ToString.ToLower = "false" Or CellValue.ToString.ToLower = "f" Or CellValue.ToString.Trim = "0" Or CellValue.ToString.ToLower = "no" Or CellValue.ToString.ToLower = "n" Then
        '            '                    FalseValuesCount = FalseValuesCount + 1
        '            '                End If

        '            '                'IsBit
        '            '                If CellValue.ToString.Trim <> "1" And CellValue.ToString.Trim <> "0" Then
        '            '                    WorksheetColumnIsBit = False
        '            '                End If

        '            '                'IsDate
        '            '                If IsDate(CellValue) = True Then DatesCount = DatesCount + 1 Else WorksheetColumnIsDate = False

        '            '                'If any cell values are not numeric then the column is not numeric
        '            '                If CellValue.ToString.Trim <> "" Then
        '            '                    If IsNumeric(CellValue) = False Then WorksheetColumnIsNumeric = False
        '            '                Else
        '            '                    'Blank, increment the blank counter
        '            '                    BlankCount = BlankCount + 1
        '            '                End If
        '            '            Else
        '            '                BlankCount = BlankCount + 1
        '            '            End If
        '            '        End If
        '            '        RowIndex = RowIndex + 1
        '            '    Next

        '            '    'Bitness
        '            '    If FalseValuesCount > 0 Then WorksheetColumnIsBit = False

        '            '    'Dateness
        '            '    If RowCount - BlankCount > 0 Then
        '            '        Dateness = DatesCount / (RowCount - BlankCount)
        '            '    End If


        '            '    'Now get all the distinct values
        '            '    Dim ColumnNames() As String = {WorksheetColumn.ColumnName}
        '            '    Dim UniqueValuesDataTable As DataTable = WorksheetDataTable.DefaultView.ToTable(True, ColumnNames)

        '            '    Dim UniqueValues As String = ""
        '            '    Dim CSVSeparator As String = ","

        '            '    For Each Row As DataRow In UniqueValuesDataTable.Rows

        '            '        If Not Row.Item(0) Is Nothing Then
        '            '            If Not IsDBNull(Row.Item(0)) Then
        '            '                'Add the item to the list of unique items
        '            '                Dim RowValue As String = Row.Item(0)
        '            '                UniqueValues = UniqueValues & RowValue & CSVSeparator
        '            '            End If
        '            '        End If
        '            '    Next

        '            '    'We can only guess at column types if there is data in the column. If the entire column is blank then reset everything.
        '            '    If BlankCount = RowCount Then
        '            '        WorksheetColumnIsNumeric = False
        '            '        WorksheetColumnIsBit = False
        '            '        WorksheetColumnIsDate = False
        '            '        WorksheetColumnIsBlank = True
        '            '    End If

        '            '    'Get at numericity
        '            '    If RowCount - BlankCount > 0 Then Numericity = NumericValuesCount / (RowCount - BlankCount) Else Numericity = 0
        '            '    With NewMetadataRow
        '            '        .Item("Filename") = DataSourceFileInfo.Name
        '            '        .Item("Worksheet") = WorksheetName
        '            '        .Item("TableName") = WorksheetName
        '            '        .Item("ColumnName") = WorksheetColumn.ColumnName
        '            '        .Item("Caption") = WorksheetColumn.Caption
        '            '        .Item("DataType") = WorksheetColumn.DataType.ToString.Replace("System.", "")
        '            '        .Item("AllowDBNull") = WorksheetColumn.AllowDBNull
        '            '        .Item("AutoIncrement") = WorksheetColumn.AutoIncrement
        '            '        .Item("DefaultValue") = WorksheetColumn.DefaultValue
        '            '        .Item("Expression") = WorksheetColumn.Expression
        '            '        .Item("IsUnique") = WorksheetColumn.Unique
        '            '        .Item("MaxLength") = MaxLength
        '            '        .Item("IsNumeric") = WorksheetColumnIsNumeric
        '            '        .Item("IsBit") = WorksheetColumnIsBit
        '            '        .Item("Bitness") = Bitness
        '            '        .Item("Blanks") = BlankCount
        '            '        .Item("IsBlank") = WorksheetColumnIsBlank
        '            '        .Item("UniqueValues") = UniqueValues
        '            '        .Item("NumericValuesCount") = NumericValuesCount
        '            '        .Item("Numericity") = Numericity
        '            '        .Item("BitValuesCount") = TrueValuesCount + FalseValuesCount
        '            '        .Item("TrueValuesCount") = TrueValuesCount
        '            '        .Item("FalseValuesCount") = FalseValuesCount
        '            '        .Item("RowCount") = RowCount
        '            '        .Item("DatesCount") = DatesCount
        '            '        .Item("Dateness") = Dateness
        '            '        .Item("IsDate") = WorksheetColumnIsDate
        '            '        .Item("Max") = WorksheetDataTable.Compute("Max([" & WorksheetColumn.ColumnName & "])", "")
        '            '        .Item("Min") = WorksheetDataTable.Compute("Min([" & WorksheetColumn.ColumnName & "])", "")
        '            '    End With
        '            '    MetadataDataTable.Rows.Add(NewMetadataRow)

        '            '    'add the column's unique values to the unique values datatable
        '            '    For Each Row As DataRow In UniqueValuesDataTable.Rows
        '            '        If Not Row.Item(0) Is Nothing Then
        '            '            If Not IsDBNull(Row.Item(0)) Then
        '            '                'Make a new row for the unique values data table
        '            '                Dim NewUniqueValuesRow As DataRow = MetadataUniqueValuesDataTable.NewRow
        '            '                With NewUniqueValuesRow
        '            '                    .Item("TableName") = WorksheetName
        '            '                    .Item("ColumnName") = WorksheetColumn.ColumnName
        '            '                    .Item("UniqueValue") = Row.Item(0)
        '            '                End With
        '            '                MetadataUniqueValuesDataTable.Rows.Add(NewUniqueValuesRow)
        '            '            End If
        '            '        End If
        '            '    Next


        '            'Next

        '            'Dim MDS As DataSet = GetMetadataDatasetFromDataTable(WorksheetDataTable)

        '            MyConnection.Close()
        '        Next
        '    Catch ex As Exception
        '        MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
        '    End Try
        '    Return ExcelDataset
        'End Function




        ''' <summary>
        ''' Converts an Excel workbook and all its worksheets to a Dataset.
        ''' </summary>
        ''' <param name="ExcelConnectionString">Excel ConnectionString. String.</param>
        ''' <returns>Dataset.</returns>
        Public Shared Function GetDatasetFromExcelWorkbook(ExcelConnectionString As String, Optional WithWorksheetsDataTable As Boolean = False, Optional WithMetadata As Boolean = False) As DataSet

            'Dataset to return
            Dim ExcelDataset As New DataSet

            Try

                'Name the Dataset for the input filename
                Dim CS As New OleDbConnectionStringBuilder(ExcelConnectionString)
                Dim DataSourceFileInfo As New FileInfo(CS.DataSource)
                If DataSourceFileInfo.Name.Trim.Length > 0 Then
                    ExcelDataset.DataSetName = DataSourceFileInfo.Name
                End If

                'Get the workbook's worksheets into a DataTable and add them to the Dataset
                Dim WorksheetsDataTable As DataTable = GetExcelWorksheets(ExcelConnectionString)
                WorksheetsDataTable.TableName = "Worksheets"
                WorksheetsDataTable.Prefix = "Worksheets"

                'Add to the Dataset a DataTable of worksheet names, if requested.
                If WithWorksheetsDataTable = True Then
                    ExcelDataset.Tables.Add(WorksheetsDataTable)
                End If

                'Loop through the worksheets, convert them to DataTables and add them to the Dataset
                For Each WorksheetRow As DataRow In WorksheetsDataTable.Rows

                    'Create a DataTable for the worksheet
                    Dim WorksheetName As String = WorksheetRow.Item("TABLE_NAME")
                    Dim WorksheetDataTable As New DataTable(WorksheetName)
                    WorksheetDataTable.TableName = WorksheetName
                    WorksheetDataTable.Prefix = "Dataset"

                    'Query the worksheet's data into WorksheetDataTable 
                    Dim Sql As String = "SELECT * FROM [" & WorksheetName & "]"
                    Dim MyConnection As New OleDbConnection(ExcelConnectionString)
                    MyConnection.Open()
                    Dim MyCommand As New OleDbCommand(Sql, MyConnection)
                    Dim MyDataAdapter As New OleDbDataAdapter(MyCommand)
                    MyDataAdapter.Fill(WorksheetDataTable)
                    ExcelDataset.Tables.Add(WorksheetDataTable)

                    'Add in a DataTable of metadata for WorksheetsDataTable if requested
                    If WithMetadata = True Then
                        Dim MDDT As DataTable = GetMetadataFromDataTable(WorksheetDataTable)
                        ExcelDataset.Tables.Add(MDDT)
                    End If

                    MyConnection.Close()
                Next
            Catch ex As Exception
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try
            Return ExcelDataset
        End Function



        ''' <summary>
        ''' Returns a DataTable of worksheets in the submitted Excel workbook
        ''' </summary>
        ''' <param name="ExcelConnectionString"></param>
        ''' <returns>DataTable</returns>
        Public Shared Function GetExcelWorksheets(ExcelConnectionString As String) As DataTable
            Dim ExcelWorksheetsDataTable As New DataTable
            Try
                Dim MyConnection As New OleDbConnection(ExcelConnectionString)
                MyConnection.Open()
                ExcelWorksheetsDataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                MyConnection.Close()
            Catch ex As Exception
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try
            Return ExcelWorksheetsDataTable
        End Function






        Public Shared Function GetDatasetDescription(DS As DataSet, IncludeColumnDescriptionss As Boolean) As String
            Dim R As String = "" 'Return string
            Try
                'IF we have a Dataset
                If Not DS Is Nothing Then
                    R = "Dataset description: " & DS.DataSetName & vbNewLine & vbNewLine
                    R = R & vbTab & "Table name" & vbTab & "Rows" & vbNewLine

                    'Tables
                    For Each DT As DataTable In DS.Tables
                        R = R & vbTab & DT.TableName & vbTab & "(" & DT.Rows.Count & " rows)" & vbNewLine

                        'Columns
                        If IncludeColumnDescriptionss = True Then
                            R = R & vbTab & vbTab & "Column" & vbTab & "DataType" & vbNewLine
                            For Each Col As DataColumn In DT.Columns
                                R = R & vbTab & vbTab & Col.ColumnName & vbTab & Col.DataType.ToString.Replace("System.", "") & vbNewLine
                            Next
                        End If
                        R = R & vbNewLine
                    Next
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try
            Return R
        End Function



        ''' <summary>
        ''' Format is used in ConnectionStrings to determine if the data is delimited or fixed width
        ''' </summary>
        Enum Format
            Delimited = 0
            Fixed = 1
        End Enum


        ''' <summary>
        ''' Converts a DataTable to a delimiter separated values text block.
        ''' </summary>
        ''' <param name="DT">DataTable to convert. DataTable</param>
        ''' <param name="Delimiter">Values separator. String.</param>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Shared Function DataTableToCSV(DT As DataTable, Optional Delimiter As String = "|") As String
            Dim CSV As String = "" 'Return string

            Try

                'Make sure the delimiter is clean
                Delimiter = Delimiter.Trim

                'If we have a DataTable
                If Not DT Is Nothing Then

                    'Output the column names as a header
                    For Each Column As DataColumn In DT.Columns
                        CSV = CSV & Column.ColumnName & Delimiter
                    Next

                    'Trim the trailing delimiter
                    If CSV.Length > 0 Then
                        CSV = CSV.Substring(0, CSV.Trim.Length - Delimiter.Length) & vbNewLine
                    End If

                    'Output the contents of each DataRow
                    If DT.Rows.Count > 0 Then
                        For Each Row As DataRow In DT.Rows
                            For Each Column As DataColumn In DT.Columns
                                CSV = CSV & Row.Item(Column.ColumnName) & Delimiter
                            Next
                            CSV = CSV.Substring(0, CSV.Trim.Length - 1) & vbNewLine
                        Next
                    End If
                Else
                    CSV = "Submitted DataTable is nothing."
                End If
            Catch ex As Exception
                CSV = ex.Message
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try
            Return CSV
        End Function

        ''' <summary>
        ''' Converts a tab delimited text file to a DataTable
        ''' </summary>
        ''' <param name="TDVFileInfo">Tab delimited text file. FileInfo.</param>
        ''' <returns>DataTable</returns>
        Public Shared Function GetDataTableFromTabDelimitedTextFile(TDVFileInfo As FileInfo) As DataTable
            Dim TDVDataTable As New DataTable(TDVFileInfo.Name)
            Try
                Dim MyTextFileParser As New FileIO.TextFieldParser(TDVFileInfo.FullName)
                MyTextFileParser.Delimiters = New String() {vbTab}
                TDVDataTable.Columns.AddRange(Array.ConvertAll(MyTextFileParser.ReadFields, Function(s) New DataColumn With {.Caption = s, .ColumnName = s}))
                Do While Not MyTextFileParser.EndOfData
                    TDVDataTable.Rows.Add(MyTextFileParser.ReadFields)
                Loop
            Catch ex As Exception
                MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End Try
            Return TDVDataTable
        End Function

        ''' <summary>
        ''' Converts an XML file into a Dataset
        ''' </summary>
        ''' <param name="XMLFileInfo">XML file</param>
        ''' <returns>Dataset</returns>
        Public Shared Function GetDatasetFromXMLFile(XMLFileInfo As FileInfo) As DataSet
            Dim XMLDataset As New DataSet(XMLFileInfo.Name)
            Try
                XMLDataset.ReadXml(XMLFileInfo.FullName)
            Catch ex As Exception
                MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End Try
            Return XMLDataset
        End Function

        Public Shared Function GetDatasetFromTextFile(CSVFileInfo As FileInfo, Headers As Boolean, Format As Format) As DataSet
            Dim CSVDataset As New DataSet(CSVFileInfo.Name)
            'Dim FMT As String = ""
            'Select Case Format
            '    Case 0
            '        FMT = "Delimited"
            '    Case 1
            '        FMT = "Fixed"
            'End Select
            Try
                CSVDataset.Tables.Add(GetDataTableFromCSV(CSVFileInfo, Headers, Format))
            Catch ex As Exception
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try
            Return CSVDataset
        End Function


        ''' <summary>
        ''' Converts the submitted CSV text file into a DataTable using Microsoft.Jet.OLEDB.4.0. NOTE: Any text in mixed text/numeric columns will be converted to blanks 
        ''' by the OLEDB engine.
        ''' </summary>
        ''' <param name="CSVFileInfo">Input CSV FileInfo</param>
        ''' <param name="Headers">Whether the file has headers or not</param>
        ''' <param name="Format"></param>
        ''' <returns>DataTable</returns>
        Public Shared Function GetDataTableFromCSV(CSVFileInfo As FileInfo, Headers As Boolean, Format As Format) As DataTable
            Dim MyDataTable As New DataTable(CSVFileInfo.Name) 'this datatable will hold the imported data
            Dim FMT As String = ""
            Select Case Format
                Case 0
                    FMT = "Delimited"
                Case 1
                    FMT = "Fixed"
            End Select
            Try
                Dim CSVConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CSVFileInfo.DirectoryName & ";Extended Properties=""text;HDR=" & Headers & ";FMT=" & FMT & """;"
                Using MyOleDBDataAdapter As New OleDbDataAdapter("SELECT * FROM [" & CSVFileInfo.Name & "]", CSVConnectionString)
                    MyOleDBDataAdapter.Fill(MyDataTable)
                End Using
                MyDataTable.TableName = CSVFileInfo.Name
            Catch ex As Exception
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try
            Return MyDataTable
        End Function

        ''' <summary>
        ''' Converts the submitted CSV text file to a DataTable using a .NET custom function. This overload may give different results from the OLEDB method.
        ''' </summary>
        ''' <param name="CSVFileInfo"></param>
        ''' <returns></returns>
        Public Shared Function GetDataTableFromCSV(CSVFileInfo As FileInfo) As DataTable
            Dim ReturnDataTable As New DataTable
            Dim CSVStringDataTable = New DataTable
            Try
                Dim CSVStreamReader As New StreamReader(CSVFileInfo.FullName)
                Dim CSV As String = CSVStreamReader.ReadToEnd
                CSVStreamReader.Close()
                CSVStreamReader.Dispose()
                Dim CSVLines As String() = CSV.Split(ControlChars.Lf)
                Dim ColumnsArray As String() = CSVLines(0).Split(","c)

                'Add columns to the data table based on the first row of the csv file
                For Each ColumnName As String In ColumnsArray
                    CSVStringDataTable.Columns.Add(New DataColumn(ColumnName))
                Next

                'Add the data rows
                Dim RowIndex As Integer = 0
                For Each CSVLine As String In CSVLines

                    'Don't add any blank rows to the data table
                    If CSVLine.Trim <> "" Then

                        'Avoid duplicating the first row containing the column names
                        If RowIndex > 0 Then

                            Dim NewRow As DataRow = CSVStringDataTable.NewRow
                            Dim CSVFinalLine As String = CSVLine.Replace(Convert.ToString(ControlChars.Cr), "")
                            NewRow.ItemArray = CSVFinalLine.Split(","c)
                            CSVStringDataTable.Rows.Add(NewRow)
                        End If

                    End If
                    RowIndex = RowIndex + 1
                Next

                'Loop through each column in the CSV string data table
                For Each Col As DataColumn In CSVStringDataTable.Columns
                    Dim NullCounter As Integer = 0
                    'Dim BlankCounter As Integer = 0

                    Dim DateCounter As Integer = 0
                    Dim IntegerCounter As Integer = 0
                    Dim BooleanCounter As Integer = 0
                    Dim BitCounter As Integer = 0
                    'Dim DecimalCounter As Integer = 0
                    Dim DoubleCounter As Integer = 0
                    'Dim SingleCounter As Integer = 0
                    Dim TextCounter As Integer = 0

                    'Loop through the columns
                    If Not Col Is Nothing Then


                        'And then loop through each row
                        'Dim i As Integer = 0
                        For Each Row As DataRow In CSVStringDataTable.Rows
                            If Not Row Is Nothing Then

                                'Determine if the row is null
                                If Not IsDBNull(Row.Item(Col.ColumnName)) Then

                                    'Get the cell value
                                    Dim CellValue As String = Row.Item(Col.ColumnName).ToString.Trim

                                    'Determine if the cell is blank
                                    If CellValue.Length > 0 Then

                                        'Determine what data type CellValue is
                                        If ValueIsDate(CellValue) = True Then
                                            'It's a Date. Increment the DateCounter
                                            DateCounter = DateCounter + 1

                                        ElseIf IsNumeric(CellValue) = True Then
                                            'It's a number

                                            'Determine if the value is an integer.
                                            If ValueIsInteger(CellValue) Then
                                                IntegerCounter = IntegerCounter + 1

                                                'Determine if the value is Bit (boolean).
                                                If CellValue = 0 Or CellValue = 1 Then BitCounter = BitCounter + 1
                                            Else
                                                'It must be a Double or similar (Decimal,Single, etc.). Treat as Double.
                                                DoubleCounter = DoubleCounter + 1
                                            End If
                                        Else

                                            'It's a String. See if it's boolean.
                                            If CellValue.ToLower = "true" Or CellValue.ToLower = "false" Or CellValue.ToLower = "t" Or CellValue.ToLower = "f" Or CellValue.ToLower = "y" Or CellValue.ToLower = "n" Or CellValue.ToLower = "yes" Or CellValue.ToLower = "no" Then
                                                'Value is boolean text.
                                                BooleanCounter = BooleanCounter + 1
                                            Else
                                                'Value is non-boolean text
                                                TextCounter = TextCounter + 1
                                            End If

                                        End If
                                    Else

                                        'Cell is Blank
                                        NullCounter = NullCounter + 1

                                    End If
                                Else
                                    'Cell is DBNull
                                    NullCounter = NullCounter + 1

                                End If

                            End If
                        Next
                    End If

                    Debug.Print("-" & Col.ColumnName & " Rows " & CSVStringDataTable.Rows.Count & " nulls " & NullCounter & " texts " & TextCounter & " dbl " & DoubleCounter & " bln " & BooleanCounter & " int " & IntegerCounter)

                    'Add Columns to ReturnDataTable based on what kind of rows we have, as determined above
                    If TextCounter > 0 And CSVStringDataTable.Rows.Count - NullCounter = TextCounter Then

                        'The row has at least one text entry, make the added DataColumn text
                        ReturnDataTable.Columns.Add(Col.ColumnName, GetType(String))

                    ElseIf DateCounter > 0 And CSVStringDataTable.Rows.Count - NullCounter = DateCounter Then

                        'All the rows are Dates. 
                        ReturnDataTable.Columns.Add(Col.ColumnName, GetType(Date))

                    ElseIf IntegerCounter > 0 And CSVStringDataTable.Rows.Count - NullCounter = IntegerCounter And BitCounter <> IntegerCounter Then

                        'All the rows are integers, but not completely Bit
                        ReturnDataTable.Columns.Add(Col.ColumnName, GetType(Integer))

                    ElseIf IntegerCounter > 0 And DoubleCounter = 0 And CSVStringDataTable.Rows.Count - NullCounter = IntegerCounter And BitCounter = IntegerCounter Then

                        'All the rows are Integers and Bit
                        ReturnDataTable.Columns.Add(Col.ColumnName, GetType(Integer))

                    ElseIf DoubleCounter > 0 And CSVStringDataTable.Rows.Count - NullCounter - integercounter = DoubleCounter Then

                        'All the rows are Double
                        ReturnDataTable.Columns.Add(Col.ColumnName, GetType(Double))

                    ElseIf BooleanCounter > 0 And CSVStringDataTable.Rows.Count - NullCounter = BooleanCounter Then

                        'All the rows were boolean.
                        ReturnDataTable.Columns.Add(Col.ColumnName, GetType(Boolean))

                    Else

                        'All other possibilities eliminated, data type will be String.
                        ReturnDataTable.Columns.Add(Col.ColumnName, GetType(String))
                    End If

                Next



                'Debug.Print("UNFIXED string booleans -----------------------------------")
                'Debug.Print(DataTableToCSV(CSVStringDataTable, ","))
                'Debug.Print("UNFIXED string booleans -----------------------------------")

                'Fix all the text booleans
                'Convert all the text booleans to True/False
                For Each Col As DataColumn In CSVStringDataTable.Columns
                    For Each CSVRow As DataRow In CSVStringDataTable.Rows
                        If CSVRow.Item(Col.ColumnName).ToString.Trim.ToLower = "true" Or CSVRow.Item(Col.ColumnName).ToString.Trim.ToLower = "t" Or CSVRow.Item(Col.ColumnName).ToString.Trim.ToLower = "y" Or CSVRow.Item(Col.ColumnName).ToString.Trim.ToLower = "yes" Then
                            CSVRow.Item(Col.ColumnName) = True
                        ElseIf CSVRow.Item(Col.ColumnName).ToString.Trim.ToLower = "false" Or CSVRow.Item(Col.ColumnName).ToString.Trim.ToLower = "f" Or CSVRow.Item(Col.ColumnName).ToString.Trim.ToLower = "n" Or CSVRow.Item(Col.ColumnName).ToString.Trim.ToLower = "no" Then
                            CSVRow.Item(Col.ColumnName) = False
                        End If
                    Next
                Next

                'Debug.Print("FIXED string booleans -----------------------------------")
                'Debug.Print(DataTableToCSV(CSVStringDataTable))
                'Debug.Print("FIXED string booleans -----------------------------------")

                For Each C As DataColumn In ReturnDataTable.Columns
                    Debug.Print(C.ColumnName & " " & C.DataType.ToString)
                Next

                'Now load the data from the all String DataTable into ReturnDataTable with the corrected data types
                Dim i As Integer = 0
                For Each Row As DataRow In CSVStringDataTable.Rows
                    Dim NewRow As DataRow = ReturnDataTable.NewRow
                    For Each Col As DataColumn In CSVStringDataTable.Columns
                        If IsDBNull(Row.Item(Col.ColumnName)) = False Then
                            If Row.Item(Col.ColumnName).ToString.Trim.Length > 0 Then
                                NewRow.Item(Col.ColumnName) = Row.Item(Col.ColumnName)
                            End If
                        End If
                    Next
                    i = i + 1
                    ReturnDataTable.Rows.Add(NewRow)
                Next

            Catch ex As Exception
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try

            Return ReturnDataTable ' ReturnDataTable
        End Function



        '''' <summary>
        '''' Returns true if Value is a text boolean (True/False,T/F,Y/N), otherwise False. This function returns false for Bit values 0/1.
        '''' </summary>
        '''' <param name="Value">Value to evaluate. String.</param>
        '''' <returns></returns>
        'Public Shared Function ValueIsTextBoolean(Value As String) As Boolean
        '    If Not Value Is Nothing Then
        '        If Value.ToLower = "true" Or Value.ToLower = "false" Or Value.ToLower = "yes" Or Value.ToLower = "no" Or Value.ToLower = "y" Or Value.ToLower = "n" Or Value.ToLower = "t" Or Value.ToLower = "f" Then
        '            Return True
        '        Else
        '            Return False
        '        End If
        '    Else
        '        Return False
        '    End If
        'End Function

        '''' <summary>
        '''' Returns a Bit for the equivalent text boolean value (True/False,T/F,Y/N), otherwise returns Nothing
        '''' </summary>
        '''' <param name="Value">Value to evaluate. String.</param>
        '''' <returns></returns>
        'Public Shared Function GetBFromTextBoolean(Value As String) As Integer
        '    Dim ReturnValue As Integer = Nothing
        '    If Not Value Is Nothing Then
        '        If ValueIsTextBoolean(Value) = True Then
        '            If Value.ToLower = "true" Or Value.ToLower = "yes" Or Value.ToLower = "y" Or Value.ToLower = "t" Then
        '                ReturnValue = 1
        '            ElseIf Value.ToLower = "false" Or Value.ToLower = "no" Or Value.ToLower = "n" Or Value.ToLower = "f" Then
        '                ReturnValue = 0
        '            End If
        '        End If
        '    End If
        '    Return ReturnValue
        'End Function

        ''' <summary>
        ''' Returns true if String Value is a Date, otherwise False.
        ''' </summary>
        ''' <param name="Value">Value to evaluate. String.</param>
        ''' <returns></returns>
        Public Shared Function ValueIsDate(Value As String) As Boolean
            If Not Value Is Nothing Then
                If IsNumeric(Value) = False And IsDate(Value) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Returns true if String Value is Bit (0/1), otherwise False.
        ''' </summary>
        ''' <param name="Value">Value to evaluate. String.</param>
        ''' <returns></returns>
        Public Shared Function ValueIsBit(Value As String) As Boolean
            If Not Value Is Nothing Then
                'Test for integer, and then for integer variant of boolean (1 or 0)
                If IsNumeric(Value) Then
                    If Value = 0 Or Value = 1 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Returns true if String Value is an Integer, otherwise False.
        ''' </summary>
        ''' <param name="Value">Value to evaluate. String.</param>
        ''' <returns></returns>
        Public Shared Function ValueIsInteger(Value As String) As Boolean
            If Not Value Is Nothing Then
                'Test for integer, and then for integer variant of boolean (1 or 0)
                If Int16.TryParse(Value, New Integer) = True Or Int32.TryParse(Value, New Integer) = True Or Int64.TryParse(Value, New Integer) = True Or UInt16.TryParse(Value, New Integer) = True Or UInt32.TryParse(Value, New Integer) = True Or UInt64.TryParse(Value, New Integer) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function



        ''' <summary>
        ''' Returns true if String Value is a Double, otherwise False.
        ''' </summary>
        ''' <param name="Value">Value to evaluate. String.</param>
        ''' <returns></returns>
        Public Shared Function ValueIsDouble(Value As String) As Boolean
            If Not Value Is Nothing Then
                'Test for Double
                If Double.TryParse(Value, New Double) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Converts a comma separated values string into a DataTable.
        ''' </summary>
        ''' <param name="CSV">Input comma separated values string. String.</param>
        ''' <returns>Output DataTable</returns>
        Public Shared Function GetDataTableFromCSVString(CSV As String) As System.Data.DataTable

            Dim CSVDataTable As New DataTable() 'The output DataTable
            Try

                'Build a String array and read the lines into it
                Dim CSVLines As String() = CSV.Split(ControlChars.Lf)

                'Assume the first line contains column headers. Load them into the DataTable as DataColumns
                Dim ColumnNames As String() = CSVLines(0).Split(","c)
                For Each ColumnName As String In ColumnNames
                    CSVDataTable.Columns.Add(New DataColumn(ColumnName.Trim))
                Next

                'Make data rows
                Dim NewRow As DataRow
                Dim CleanedCSVLine As String = ""

                'We need to skip the first line which is assumed to be headers so we need a line counter
                Dim Counter As Integer = 0

                'Loop through the CSV lines
                For Each CSVLine As String In CSVLines
                    If Counter <> 0 Then
                        'Make a new DataRow
                        NewRow = CSVDataTable.NewRow()

                        'Replace any carriage returns with blanks
                        CleanedCSVLine = CSVLine.Replace(Convert.ToString(ControlChars.Cr), "")

                        'Split the cleaned line by commas and load them into NewRow DataRow
                        NewRow.ItemArray = CleanedCSVLine.Split(","c)

                        'Add the new row to the output DataTable
                        CSVDataTable.Rows.Add(NewRow)
                    End If

                    'Increment the counter
                    Counter = Counter + 1
                Next




            Catch ex As Exception
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try
            Return CSVDataTable
        End Function

        ''' <summary>
        ''' Converts a DBF file into a Dataset
        ''' </summary>
        ''' <param name="DBFFileInfo">DBF File to convert</param>
        ''' <returns>Dataset</returns>
        Public Shared Function GetDatasetFromDBF(DBFFileInfo As FileInfo) As DataSet
            Dim DBFDataset As New DataSet(DBFFileInfo.Name)
            Try
                DBFDataset.Tables.Add(GetDataTableFromDBF(DBFFileInfo))
            Catch ex As Exception
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try
            Return DBFDataset
        End Function

        ''' <summary>
        ''' Converts a DBF file into a DataTable
        ''' </summary>
        ''' <param name="DBFFileInfo">DBF FileInfo to convert</param>
        ''' <returns>DataTable</returns>
        Public Shared Function GetDataTableFromDBF(DBFFileInfo As FileInfo) As DataTable
            Dim DBFDataTable As New DataTable(DBFFileInfo.Name)
            'dbf file
            Try
                'we'll need the dbf filename and path separately so isolate them here
                Dim DBFDirectory As String = DBFFileInfo.DirectoryName 'the dbf directory

                'NOTE: the dbf query won't work on long filenames so copy the dbf to a temporary file named zzzzzzzz.dbf and work with that
                Dim TemporaryDBFFilename As String = "zzzzzzzz.dbf"
                Dim TemporaryDBFFileFullName As String = DBFDirectory & "\" & TemporaryDBFFilename
                My.Computer.FileSystem.CopyFile(DBFFileInfo.FullName, TemporaryDBFFileFullName, True)

                'connect to the temporary dbf and open the contents into a datareader
                Dim MyConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBFDirectory & ";Extended Properties=dBASE IV;User ID=Admin;Password=;"
                Dim MyConnection As New OleDbConnection(MyConnectionString)
                MyConnection.Open()
                Dim Sql As String = "SELECT * FROM [" & TemporaryDBFFilename.Replace(".dbf", "") & "]"
                Dim MyCommand As New OleDbCommand(Sql, MyConnection)
                Dim MyDataReader As OleDbDataReader = MyCommand.ExecuteReader()

                'load the data from the datareader into the datatable
                DBFDataTable.Load(MyDataReader)

                'close the connection
                MyConnection.Close()

                'delete the temporary dbf file
                My.Computer.FileSystem.DeleteFile(TemporaryDBFFileFullName)
            Catch ex As Exception
                MsgBox("Could not import waypoints from DBF file. " & ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End Try
            Return DBFDataTable
        End Function





        Public Shared Function GetMetadataDatasetFromDataTable(SourceDataTable As DataTable, Optional Filename As String = "", Optional Worksheet As String = "", Optional Separator As String = "|") As DataSet
            'Return Dataset
            Dim MetadataDataset As New DataSet(SourceDataTable.TableName)

            'Build a data table to hold metadata
            Dim MetadataDataTable As DataTable = GetMetadataDataTable()
            'MetadataDataTable.TableName = New FileInfo(Filename).Name

            'Build a metadata unique values data table
            Dim MetadataUniqueValuesDataTable As DataTable = GetMetadataUniqueValuesDataTable()

            'Add the two data tables to the Dataset
            MetadataDataset.Tables.Add(MetadataDataTable)
            MetadataDataset.Tables.Add(MetadataUniqueValuesDataTable)

            ' Define the relationship between the tables.
            Dim ColumnsUniqueValuesDataRelation As New DataRelation("Unique values", MetadataDataTable.Columns("ColumnName"), MetadataUniqueValuesDataTable.Columns("ColumnName"))
            MetadataDataset.Relations.Add(ColumnsUniqueValuesDataRelation)


            For Each SourceColumn As DataColumn In SourceDataTable.Columns
                Dim NewMetadataRow As DataRow = MetadataDataTable.NewRow


                Dim SourceColumnIsNumeric As Boolean = True
                Dim SourceColumnIsBit As Boolean = True
                Dim SourceColumnIsInteger As Boolean = False
                Dim SourceColumnIsDate As Boolean = True
                Dim SourceColumnIsBlank As Boolean = False

                'Loop through each row in the column
                Dim RowIndex As Integer = 0
                Dim BlankCount As Integer = 0
                Dim NumericValuesCount As Integer = 0
                Dim BitValuesCount As Integer = 0
                Dim TrueValuesCount As Integer = 0
                Dim FalseValuesCount As Integer = 0
                Dim DatesCount As Integer = 0
                Dim MaxLength As Integer = 0
                Dim RowCount As Integer = SourceDataTable.Rows.Count
                Dim Numericity As Decimal = 0
                Dim Bitness As Decimal = 0
                Dim Dateness As Decimal = 0

                For Each SourceRow As DataRow In SourceDataTable.Rows

                    Dim CellValue As String = ""
                    If Not SourceRow Is Nothing Then
                        If Not IsDBNull(SourceRow.Item(SourceColumn.ColumnName)) Then

                            'Get the cell's value
                            CellValue = SourceRow.Item(SourceColumn.ColumnName)

                            'Max length
                            If CellValue.ToString.Trim.Length > MaxLength Then MaxLength = CellValue.ToString.Trim.Length

                            'IsNumeric
                            If IsNumeric(CellValue) Then
                                NumericValuesCount = NumericValuesCount + 1

                                'IsInteger
                                If Integer.TryParse(CellValue, vbNull) = True Then SourceColumnIsInteger = True
                                Dim IsInteger As Boolean = Integer.TryParse(CellValue, vbNull)

                            End If

                            'Get the bitness
                            If CellValue.ToString.ToLower = "true" Or CellValue.ToString.ToLower = "t" Or CellValue.ToString.Trim = "1" Or CellValue.ToString.ToLower = "yes" Or CellValue.ToString.ToLower = "y" Then
                                TrueValuesCount = TrueValuesCount + 1
                            ElseIf CellValue.ToString.ToLower = "false" Or CellValue.ToString.ToLower = "f" Or CellValue.ToString.Trim = "0" Or CellValue.ToString.ToLower = "no" Or CellValue.ToString.ToLower = "n" Then
                                FalseValuesCount = FalseValuesCount + 1
                            End If

                            'IsBit
                            If CellValue.ToString.Trim <> "1" And CellValue.ToString.Trim <> "0" Then
                                SourceColumnIsBit = False
                            End If

                            'IsDate
                            If IsDate(CellValue) = True Then DatesCount = DatesCount + 1 Else SourceColumnIsDate = False

                            'If any cell values are not numeric then the column is not numeric
                            If CellValue.ToString.Trim <> "" Then
                                If IsNumeric(CellValue) = False Then SourceColumnIsNumeric = False
                            Else
                                'Blank, increment the blank counter
                                BlankCount = BlankCount + 1
                            End If
                        Else
                            BlankCount = BlankCount + 1
                        End If
                    End If
                    RowIndex = RowIndex + 1
                Next

                'Bitness
                If FalseValuesCount > 0 Then SourceColumnIsBit = False

                'Dateness
                If RowCount - BlankCount > 0 Then
                    Dateness = DatesCount / (RowCount - BlankCount)
                End If


                'Now get all the distinct values
                Dim ColumnNames() As String = {SourceColumn.ColumnName}
                Dim UniqueValuesDataTable As DataTable = SourceDataTable.DefaultView.ToTable(True, ColumnNames)
                Dim UniqueValues As String = ""
                'Dim CSVSeparator As String = ","

                For Each Row As DataRow In UniqueValuesDataTable.Rows
                    If Not Row.Item(0) Is Nothing Then
                        If Not IsDBNull(Row.Item(0)) Then
                            'Add the item to the list of unique items
                            Dim RowValue As String = Row.Item(0)
                            UniqueValues = UniqueValues & RowValue & Separator
                        End If
                    End If
                Next

                'We can only guess at column types if there is data in the column. If the entire column is blank then reset everything.
                If BlankCount = RowCount Then
                    SourceColumnIsNumeric = False
                    SourceColumnIsBit = False
                    SourceColumnIsDate = False
                    SourceColumnIsBlank = True
                End If

                'Get at numericity
                If RowCount - BlankCount > 0 Then Numericity = NumericValuesCount / (RowCount - BlankCount) Else Numericity = 0
                With NewMetadataRow
                    .Item("Filename") = Filename
                    .Item("Worksheet") = Worksheet
                    .Item("TableName") = SourceColumn.Table.TableName
                    .Item("ColumnName") = SourceColumn.ColumnName
                    .Item("Caption") = SourceColumn.Caption
                    .Item("DataType") = SourceColumn.DataType.ToString.Replace("System.", "")
                    .Item("AllowDBNull") = SourceColumn.AllowDBNull
                    .Item("AutoIncrement") = SourceColumn.AutoIncrement
                    .Item("DefaultValue") = SourceColumn.DefaultValue
                    .Item("Expression") = SourceColumn.Expression
                    .Item("IsUnique") = SourceColumn.Unique
                    .Item("MaxLength") = MaxLength
                    .Item("IsNumeric") = SourceColumnIsNumeric
                    .Item("IsBit") = SourceColumnIsBit
                    .Item("Bitness") = Bitness
                    .Item("Blanks") = BlankCount
                    .Item("IsBlank") = SourceColumnIsBlank
                    .Item("UniqueValues") = UniqueValues
                    .Item("NumericValuesCount") = NumericValuesCount
                    .Item("Numericity") = Numericity
                    .Item("BitValuesCount") = TrueValuesCount + FalseValuesCount
                    .Item("TrueValuesCount") = TrueValuesCount
                    .Item("FalseValuesCount") = FalseValuesCount
                    .Item("RowCount") = RowCount
                    .Item("DatesCount") = DatesCount
                    .Item("Dateness") = Dateness
                    .Item("IsDate") = SourceColumnIsDate
                    .Item("Max") = SourceDataTable.Compute("Max([" & SourceColumn.ColumnName & "])", "")
                    .Item("Min") = SourceDataTable.Compute("Min([" & SourceColumn.ColumnName & "])", "")
                End With
                MetadataDataTable.Rows.Add(NewMetadataRow)

                'add the column's unique values to the unique values datatable
                For Each Row As DataRow In UniqueValuesDataTable.Rows
                    If Not Row.Item(0) Is Nothing Then
                        If Not IsDBNull(Row.Item(0)) Then
                            'Make a new row for the unique values data table
                            Dim NewUniqueValuesRow As DataRow = MetadataUniqueValuesDataTable.NewRow
                            NewUniqueValuesRow.Item("ColumnName") = SourceColumn.ColumnName
                            NewUniqueValuesRow.Item("UniqueValue") = Row.Item(0)
                            MetadataUniqueValuesDataTable.Rows.Add(NewUniqueValuesRow)
                        End If
                    End If
                Next


            Next
            Return MetadataDataset
        End Function

        ''' <summary>
        ''' Returns a new, empty, MetadataDataTable. Not to be confused with GetMetadataFromDataTable.
        ''' </summary>
        ''' <returns>MetadataDataTable. DataTable.</returns>
        Public Shared Function GetMetadataDataTable() As DataTable
            Dim DT As New DataTable("Metadata")
            With DT

                .Columns.Add("ColumnName", GetType(String))
                .Columns.Add("Caption", GetType(String))
                .Columns.Add("DataType", GetType(String))
                .Columns.Add("AllowDBNull", GetType(Boolean))
                .Columns.Add("AutoIncrement", GetType(Boolean))
                .Columns.Add("DefaultValue", GetType(String))
                .Columns.Add("Expression", GetType(String))
                .Columns.Add("IsUnique", GetType(Boolean))
                .Columns.Add("MaxLength", GetType(Integer))
                .Columns.Add("Description", GetType(String))
                .Columns.Add("UnitsOfMeasure", GetType(String))
                .Columns.Add("RowCount", GetType(Integer))
                .Columns.Add("IsBlank", GetType(Boolean))
                .Columns.Add("IsNumeric", GetType(Boolean))
                .Columns.Add("IsBit", GetType(Boolean))
                .Columns.Add("IsDate", GetType(Boolean))
                .Columns.Add("Blanks", GetType(Integer))
                .Columns.Add("NullValues", GetType(Integer))
                .Columns.Add("NumericValuesCount", GetType(Integer))
                .Columns.Add("Numericity", GetType(Decimal)) ', "NumericValuesCount / RowCount")
                .Columns.Add("Max", GetType(String))
                .Columns.Add("Min", GetType(String))
                .Columns.Add("TrueValuesCount", GetType(Integer))
                .Columns.Add("FalseValuesCount", GetType(Integer))
                .Columns.Add("BitValuesCount", GetType(Integer))
                .Columns.Add("Bitness", GetType(Decimal)) ', "BitValuesCount / RowCount")
                .Columns.Add("DatesCount", GetType(Integer))
                .Columns.Add("Dateness", GetType(Decimal)) ', "DatesCount / RowCount")
                .Columns.Add("UniqueValues", GetType(String))
                .Columns.Add("Filename", GetType(String))
                .Columns.Add("Worksheet", GetType(String))
                .Columns.Add("TableName", GetType(String))
            End With
            Return DT
        End Function

        ''' <summary>
        ''' Returns an empty MetadataUniqueValuesDataTable.
        ''' </summary>
        ''' <returns>DataTable.</returns>
        Public Shared Function GetMetadataUniqueValuesDataTable() As DataTable
            Dim DT As New DataTable("Unique values")
            With DT
                .Columns.Add("ColumnName", GetType(String))
                .Columns.Add("UniqueValue", GetType(String))
                .Columns.Add("TableName", GetType(String))
            End With
            Return DT
        End Function

        'Public Shared Function RemoveEmptyDataTableRows(InputDataTable As DataTable) As DataTable
        '    Dim RowIndex As Integer = 0
        '    For Each Row As DataRow In InputDataTable.Rows
        '        Dim EmptyCellsCount As Integer = 0
        '        For Each Col As DataColumn In InputDataTable.Columns
        '            If IsDBNull(Row.Item(Col.ColumnName)) = True Or Row.Item(Col.ColumnName).ToString.Trim = "" Then
        '                EmptyCellsCount = EmptyCellsCount + 1
        '            End If
        '        Next
        '        If EmptyCellsCount = InputDataTable.Columns.Count Then
        '            InputDataTable.Rows.Remove(Row)
        '        End If
        '        RowIndex = RowIndex + 1
        '    Next
        '    Return InputDataTable
        'End Function


        Public Shared Function GetMappingsDataTable() As DataTable
            Dim MappingsDataTable As New DataTable("Mappings")

            'build a column names column
            Dim DestinationColumnName As New DataColumn
            With DestinationColumnName
                .DataType = System.Type.GetType("System.String")
                .Caption = "Destination column"
                .ColumnName = "DestinationColumnName"
            End With

            'build a source column name column
            Dim SourceColumnName As New DataColumn
            With SourceColumnName
                .DataType = System.Type.GetType("System.String")
                .Caption = "Source column"
                .ColumnName = "SourceColumnName"
            End With

            'build a default value column
            Dim DefaultValueColumn As New DataColumn
            With DefaultValueColumn
                .DataType = System.Type.GetType("System.String")
                .Caption = "Default value"
                .ColumnName = "DefaultValueColumn"
            End With

            'build a boolean 'quoted' column
            Dim QuotedColumn As New DataColumn
            With QuotedColumn
                .DataType = System.Type.GetType("System.String")
                .Caption = "Quoted"
                .ColumnName = "QuotedColumn"
                .DefaultValue = False
            End With

            'add the columns to the datatable
            MappingsDataTable.Columns.Add(DestinationColumnName)
            MappingsDataTable.Columns.Add(SourceColumnName)
            MappingsDataTable.Columns.Add(DefaultValueColumn)
            MappingsDataTable.Columns.Add(QuotedColumn)
            Return MappingsDataTable
        End Function

        ''' <summary>
        ''' Queries the database specified by ConnectionString using the supplied Sql and returns a DataTable.
        ''' </summary>
        ''' <param name="ConnectionString">ConnectionString to the desired database. String.</param>
        ''' <param name="Sql">SQL query to submit to the database. String.</param>
        ''' <returns>DataTable.</returns>
        Public Shared Function GetDataTableFromSQLServerDatabase(ConnectionString As String, Sql As String) As DataTable
            Dim MyDataTable As New DataTable
            Try
                'make a SqlConnection using the supplied ConnectionString 
                Dim MySqlConnection As New SqlConnection(ConnectionString)
                Using MySqlConnection
                    'make a query using the supplied Sql
                    Dim MySqlCommand As SqlCommand = New SqlCommand(Sql, MySqlConnection)

                    'open the connection
                    MySqlConnection.Open()
                    Using MySqlDataAdapter As New SqlDataAdapter(Sql, MySqlConnection)
                        MySqlDataAdapter.Fill(MyDataTable)
                    End Using
                    'create a DataReader and execute the SqlCommand
                    'Dim MyDataReader As SqlDataReader = MySqlCommand.ExecuteReader()

                    'load the reader into the datatable
                    'MyDataTable.Load(MyDataReader)

                    'clean up
                    'MyDataReader.Close()
                End Using

            Catch ex As Exception
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try
            Return MyDataTable
        End Function

        ''' <summary>
        ''' Accepts a ConnectionString and returns a DataTable of table names
        ''' </summary>
        ''' <param name="ConnectionString">ConnectionString</param>
        ''' <returns>DataTable</returns>
        Public Shared Function GetDatabaseTables(ConnectionString As String) As DataTable
            Dim MyDataTable As New DataTable
            Try
                Dim MyConnection As New SqlConnection(ConnectionString)
                Using MyConnection
                    MyConnection.Open()
                    Dim schemaTable As DataTable = MyConnection.GetSchema("Tables")
                    Return schemaTable
                End Using
            Catch ex As Exception
                MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End Try

            Return MyDataTable
        End Function



        ''' <summary>
        ''' Get a CREATE TABLE SQL query string based on the submitted DataView
        ''' </summary>
        ''' <param name="DataView"></param>
        ''' <param name="NewTableName"></param>
        ''' <returns>String</returns>
        Public Shared Function GetCreateTableQuery(DataView As DataView, NewTableName As String) As String
            Dim Sql As String = ""
            MsgBox("Not functional yet. (GetCreateTableQuery())")
            'Try
            '    Sql = Sql & "--Best guess at columns and datatypes from the metadata available in the source dataset.  Examine and modify as needed" & vbNewLine
            '    Sql = Sql & "CREATE TABLE " & NewTableName & "(" & vbNewLine
            '    Dim CurrentDataTable As DataTable = DataView.ToTable
            '    For Each Col As DataColumn In CurrentDataTable.Columns
            '        Dim DataType As String = Col.DataType.ToString.Replace("System.", "")
            '        Dim SqlDataType As String = ""

            '        Select Case DataType
            '            Case "Boolean"
            '                SqlDataType = "Bit"
            '            Case "Byte"
            '                SqlDataType = "Binary"
            '            Case "Char"
            '                SqlDataType = "Char(" & Col.MaxLength & ")"
            '            Case "Date"
            '                SqlDataType = "Datetime"
            '            Case "Decimal"
            '                SqlDataType = "Decimal" & "(" & Col.MaxLength & ",2)"
            '            Case "Double"
            '                SqlDataType = "Float"
            '            Case "Int16"
            '                SqlDataType = "Int"
            '            Case "Int32"
            '                SqlDataType = "Int"
            '            Case "Int64"
            '                SqlDataType = "Int"
            '            Case "Int"
            '                SqlDataType = "Int"
            '            Case "Integer"
            '                SqlDataType = "Int"
            '            Case "UInteger"
            '                SqlDataType = "Int"
            '            Case "UInt16"
            '                SqlDataType = "Int"
            '            Case "UInt32"
            '                SqlDataType = "Int"
            '            Case "UInt64"
            '                SqlDataType = "Int"
            '            Case "Long"
            '                SqlDataType = "Int"
            '            Case "Object"
            '                SqlDataType = "Object()"
            '            Case "SByte"
            '                SqlDataType = "Binary"
            '            Case "Short"
            '                SqlDataType = "Int"
            '            Case "Single"
            '                SqlDataType = "Float"
            '            Case "String"
            '                SqlDataType = "Varchar(50)"
            '            Case "UInteger"
            '                SqlDataType = "Int"
            '            Case "ULong"
            '                SqlDataType = "Int"
            '            Case "User-Defined"
            '                SqlDataType = "User-Defined"
            '            Case "UShort"
            '                SqlDataType = "Int"
            '            Case Else
            '                SqlDataType = "Varchar(50)"
            '        End Select
            '        Debug.Print(Col.ColumnName & " " & DataType & " " & SqlDataType)
            '        Sql = Sql & "[" & Col.ColumnName & "] " & SqlDataType & "," & vbNewLine
            '    Next
            '    Sql = Sql.Substring(0, Sql.Trim.Length - 1) & ");"
            '    Return Sql
            'Catch ex As Exception
            '    Return Sql
            '    MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
            'End Try
            Return Sql
        End Function

        ''' <summary>
        ''' Returns true if the DataColumn is a numeric type (Decimal,Double,Int16,Int32,Int64,Single,UInt16,UInt32,UInt64)
        ''' </summary>
        ''' <param name="DataColumn">DataColumn</param>
        ''' <returns>Boolean</returns>
        Public Shared Function ColumnDataTypeIsNumeric(DataColumn As DataColumn) As Boolean
            Dim IsNumeric As Boolean = False
            Try
                Dim ColumnDataType As String = DataColumn.DataType.ToString.Trim.Replace("System.", "")
                Dim DataType As String = DataColumn.DataType.ToString.Trim.Replace("System.", "")

                Dim CSV As String = "Decimal,Double,Int16,Int32,Int64,Single,UInt16,UInt32,UInt64"
                For Each DataType In CSV.Split(",")
                    If ColumnDataType = DataType Then
                        IsNumeric = True
                    End If
                Next
            Catch ex As Exception
                MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End Try

            Return IsNumeric
        End Function

        ''' <summary>
        ''' Shows the submitted DataTable in a Form.
        ''' </summary>
        ''' <param name="DT">DataTable to show. DataTable</param>
        Public Shared Sub ShowDataTableInForm(DT As DataTable)
            Dim MyForm As New Form()
            MyForm.Text = DT.TableName


            'property grid
            Dim MyPropertyGrid As New PropertyGrid()
            With MyPropertyGrid
                .SelectedObject = DT
                .Dock = DockStyle.Fill
            End With

            'data grid viiew
            Dim MyDGV As New DataGridView
            With MyDGV
                .DataSource = DT
                .Dock = DockStyle.Fill
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            End With

            'add a split container
            Dim MySplitContainer As New SplitContainer
            With MySplitContainer
                .Dock = DockStyle.Fill
                .Orientation = Orientation.Vertical
                .Panel1.Controls.Add(MyDGV)
                .Panel2.Controls.Add(MyPropertyGrid)
            End With
            MyForm.Controls.Add(MySplitContainer)


            MyForm.Show()
        End Sub


        Public Shared Sub ShowDataSetInForm(DS As DataSet)
            Dim MyForm As New Form()
            Try
                If Not DS Is Nothing Then
                    MyForm.Text = DS.DataSetName

                    'Tabset
                    Dim MyTabControl As New TabControl
                    MyTabControl.Dock = DockStyle.Fill
                    MyForm.Controls.Add(MyTabControl)

                    'Loop through the Dataset's DataTables and add them to the tabcontrol's tabs
                    For Each DT As DataTable In DS.Tables
                        Dim MyTabPage As New TabPage(DT.TableName.Replace("$", ""))

                        Dim MyGridControl As New GridControl
                        Dim MyGridView As New GridView

                        With MyGridControl
                            .DataSource = DT
                            .ForceInitialize()
                            .Dock = DockStyle.Fill
                            .ViewCollection.Add(MyGridView)
                            .MainView = MyGridView
                            .BindingContext = New BindingContext()
                            MyGridView.PopulateColumns()
                            .UseEmbeddedNavigator = True

                        End With

                        With MyGridView
                            .OptionsView.ColumnAutoWidth = False
                            .BestFitColumns()
                            .OptionsSelection.MultiSelect = True
                            .OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect
                        End With

                        MyTabPage.Controls.Add(MyGridControl)
                        MyTabControl.TabPages.Add(MyTabPage)
                    Next
                End If
            Catch ex As Exception
                Dim MyTextBox As New TextBox
                With MyTextBox
                    .Multiline = True
                    .Dock = DockStyle.Fill
                    .Text = ex.Message
                End With
                MyForm.Controls.Clear()
                MyForm.Controls.Add(MyTextBox)
                MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End Try
            MyForm.WindowState = FormWindowState.Maximized
            MyForm.Show()
        End Sub

        ''' <summary>
        ''' Converts table text copied out of a PDF table into a DataTable of text columns.
        ''' </summary>
        ''' <param name="PDFTableText">PDF table text. String.</param>
        ''' <param name="Delimiter">Delimiter. Usually a space. String.</param>
        ''' <returns></returns>
        Public Shared Function GetDataTableFromPDFTableText(PDFTableText As String, Optional Delimiter As String = " ") As DataTable
            Dim DT As New DataTable
            Try
                'Get the PDFTableText into an array of lines
                Dim Lines As String() = PDFTableText.Trim.Split(New [Char]() {CChar(vbCrLf)})

                'Set up some counters so we know which line and column we are on.
                Dim LineNumber As Integer = 0
                Dim ColumnsCount As Integer = 0

                'Loop through each line in the PDFTableText.
                For Each Line As String In Lines

                    'If we are on the first line then we assume we are on the header line.
                    'Create a new DataColumn for each header and add it to the DataTable.
                    If Lines.Count > 0 Then
                        If LineNumber = 0 Then
                            For Each LineItem As String In Line.Split(Delimiter)
                                'Add the column to the DataTable
                                DT.Columns.Add(LineItem.Trim, GetType(String))
                            Next
                        Else
                            'We've moved beyond the first row and into the data.
                            'Make a new DataRow to hold the data.
                            Dim NewRow As DataRow = DT.NewRow
                            Dim CurrentItemNumber As Integer = 0
                            For Each LineItem As String In Line.Split(Delimiter)
                                'As we loop through the items in the line, add them to the new row in the place determined by CurrentItem
                                NewRow.Item(CurrentItemNumber) = LineItem.Trim
                                CurrentItemNumber = CurrentItemNumber + 1
                            Next
                            DT.Rows.Add(NewRow)
                        End If
                        LineNumber = LineNumber + 1
                    End If
                Next
            Catch ex As Exception
                MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End Try
            Return DT
        End Function

        Public Shared Function GetTransposedDataTable(DT As DataTable) As DataTable
            MsgBox("The GetTransposedDataTable function works but still need to get the headers transposed.")
            Dim dtnew As New DataTable
            Try
                For i As Integer = 0 To DT.Rows.Count - 1
                    dtnew.Columns.Add(i.ToString)
                Next
                For i As Integer = 0 To DT.Columns.Count - 1
                    Dim dr As DataRow = dtnew.NewRow
                    dtnew.Rows.Add(dr)
                Next
                For i As Integer = 0 To DT.Rows.Count - 1
                    For j As Integer = 0 To DT.Columns.Count - 1
                        dtnew.Rows(i).Item(j) =
                    DT.Rows(j).Item(i).ToString
                    Next
                Next
            Catch ex As Exception
                MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End Try
            Return dtnew
        End Function

    End Class

End Namespace
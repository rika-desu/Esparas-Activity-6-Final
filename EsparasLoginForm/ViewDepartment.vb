Imports MySql.Data.MySqlClient

Public Class ViewDepartment
    Private Sub adminDashboard_Click(sender As Object, e As EventArgs) Handles adminDashboard.Click
        Me.Close()
        adminDashboard.Show()
    End Sub

    Public sqlColumns As String = "deptID as Department_ID, deptName as Department_Name"

    Private Sub Load_Data_to_Grid(ByVal strsql As String)
        Dim myreader As MySqlDataReader
        Dim mycommand As New MySqlCommand
        Dim mydataAdapter As New MySqlDataAdapter
        Dim mydatatable As New DataTable

        Connect2DB()
        With Me
            Try
                mycommand.Connection = myconn
                mycommand.CommandText = strsql
                myreader = mycommand.ExecuteReader
                mydatatable = New DataTable

                myreader.Close()
                mydataAdapter.SelectCommand = mycommand

                mydataAdapter.Fill(mydatatable)
                DGdepartment.AutoSize = False
                .DGdepartment.Refresh()
                .DGdepartment.EndEdit()
                .DGdepartment.DataSource = mydatatable
                .DGdepartment.ReadOnly = True
                .DGdepartment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells

            Catch ex As MySqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error on SQL query")
            End Try
            myreader = Nothing
            mycommand = Nothing
            Disconnect2DB()
        End With
    End Sub

    Private Sub ViewDepartment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Load_Data_to_Grid("select " & Me.sqlColumns & " from department")
    End Sub

    Private Sub PrintBTN_Click(sender As Object, e As EventArgs) Handles PrintBTN.Click
        Call importToExcel(Me.DGdepartment, "department_report.xlsx")
    End Sub
End Class
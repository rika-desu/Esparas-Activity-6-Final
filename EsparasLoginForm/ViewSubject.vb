Imports MySql.Data.MySqlClient

Public Class ViewSubject
    Private Sub adminDashboard_Click(sender As Object, e As EventArgs) Handles adminDashboard.Click
        Me.Close()
        adminDashboard.Show()
    End Sub

    Public sqlColumns As String = "subjectID as Subject_ID, subjectName as Subject_Name, instructorID as Instructor_ID"

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
                DGsubject.AutoSize = False
                .DGsubject.Refresh()
                .DGsubject.EndEdit()
                .DGsubject.DataSource = mydatatable
                .DGsubject.ReadOnly = True
                .DGsubject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells

            Catch ex As MySqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error on SQL query")
            End Try
            myreader = Nothing
            mycommand = Nothing
            Disconnect2DB()
        End With
    End Sub

    Private Sub ViewSubject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Load_Data_to_Grid("select " & Me.sqlColumns & " from subject")
    End Sub

    Private Sub PrintBTN_Click(sender As Object, e As EventArgs) Handles PrintBTN.Click
        Call importToExcel(Me.DGsubject, "subject_report.xlsx")
    End Sub
End Class
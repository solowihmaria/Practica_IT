Imports System.Data.SqlClient
Public Class Меню
    Dim Adapter As New SqlDataAdapter()
    Dim Builder As New SqlCommandBuilder()
    Dim ConBuilder As New SqlConnectionStringBuilder()
    Dim Myconn As New SqlConnection(ConBuilder.ConnectionString)

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ConBuilder.UserID = $"SarantsevaA"
        ConBuilder.Password = $"als__.27052003"
        ConBuilder.DataSource = $"LAPTOP-VH1E26FG"
        ConBuilder.InitialCatalog = $"school database"
        Dim Myconn As New SqlConnection(ConBuilder.ConnectionString)
        Try
            Myconn.Open()
            MsgBox("Подключение получено")
        Catch
            MsgBox("Подключение не получено")
            Application.Exit()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Фильтры.Show()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Запросы.Show()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Просмотр_таблицы.Show()

    End Sub
End Class

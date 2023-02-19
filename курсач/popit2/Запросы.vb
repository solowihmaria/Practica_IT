Imports System.Data.SqlClient
Public Class Запросы
    Dim Adapter As New SqlDataAdapter()
    Dim Builder As New SqlCommandBuilder()
    Dim ConBuilder As New SqlConnectionStringBuilder()
    Dim Myconn As New SqlConnection(ConBuilder.ConnectionString)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ConBuilder.UserID = $"SarantsevaA"
        ConBuilder.Password = $"als__.27052003"
        ConBuilder.DataSource = $"LAPTOP-VH1E26FG"
        ConBuilder.InitialCatalog = $"school database"
        'делаем отдельные функции по запросам и дальше через if комбобокса вызываем эти функции
        If ComboBox1.Text = "Расписание занятий в школе" Then
            raspisaniezanyati()
        End If
        If ComboBox1.Text = "Список учеников" Then
            spisokuchenikov()
        End If
        If ComboBox1.Text = "Отдел кадров" Then
            otdelkadrov()
        End If
        If ComboBox1.Text = "Список классов" Then
            spisokclassov()
        End If
        If ComboBox1.Text = "Список предметов" Then
            spisokpredmetov()
        End If
    End Sub

    Public Sub otdelkadrov()
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()

            Dim sqlString = "SELECT Должности.[Наименование должности], Сотрудники.ФИО, Сотрудники.Пол
                            FROM     Должности INNER JOIN
                            Сотрудники ON Должности.[Код должности] = Сотрудники.[Код должности]"
            Dim command As New SqlCommand(sqlString, Myconn)
            Adapter = New SqlDataAdapter(command)
            Builder = New SqlCommandBuilder(Adapter)
            Adapter.Fill(dt)
            DataGridView1.DataSource = dt
            Myconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "error")
        End Try
    End Sub

    Public Sub spisokuchenikov()
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()

            Dim sqlString = "SELECT dbo.Ученики.ФИО, dbo.Классы.[Код сотрудника-класного руководителя], dbo.Классы.[Год создания], dbo.Классы.[Код вида], dbo.Классы.Буква
                             FROM     dbo.Ученики INNER JOIN
                             dbo.Классы ON dbo.Ученики.[Код класса] = dbo.Классы.[Код класса]"
            Dim command As New SqlCommand(sqlString, Myconn)
            Adapter = New SqlDataAdapter(command)
            Builder = New SqlCommandBuilder(Adapter)
            Adapter.Fill(dt)
            DataGridView1.DataSource = dt
            Myconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "error")
        End Try
    End Sub

    Public Sub spisokclassov()
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()

            Dim sqlString = "SELECT dbo.Сотрудники.ФИО, dbo.Классы.[Код вида], dbo.Классы.Буква, dbo.Классы.[Год обучения]
                             FROM     dbo.Классы INNER JOIN
                             dbo.[Виды классов] ON dbo.Классы.[Код вида] = dbo.[Виды классов].[Код вида] INNER JOIN
                             dbo.Сотрудники ON dbo.Классы.[Код сотрудника-класного руководителя] = dbo.Сотрудники.[Код сотрудника]"
            Dim command As New SqlCommand(sqlString, Myconn)
            Adapter = New SqlDataAdapter(command)
            Builder = New SqlCommandBuilder(Adapter)
            Adapter.Fill(dt)
            DataGridView1.DataSource = dt
            Myconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "error")
        End Try
    End Sub

    Public Sub spisokpredmetov()
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()

            Dim sqlString = "SELECT dbo.Предметы.Наименование AS Expr1, dbo.Сотрудники.ФИО
                             FROM   dbo.Предметы INNER JOIN
                             dbo.Сотрудники ON dbo.Предметы.[Код сотрудника-учителя] = dbo.Сотрудники.[Код сотрудника]"
            Dim command As New SqlCommand(sqlString, Myconn)
            Adapter = New SqlDataAdapter(command)
            Builder = New SqlCommandBuilder(Adapter)
            Adapter.Fill(dt)
            DataGridView1.DataSource = dt
            Myconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "error")
        End Try
    End Sub

    Public Sub raspisaniezanyati()
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()

            Dim sqlString = "SELECT dbo.Расписание.[День недели], dbo.Расписание.[Код класса], dbo.Расписание.[Время начала], dbo.Расписание.[Время окончания], dbo.Классы.[Код вида], dbo.Классы.Буква, dbo.Предметы.Наименование
                            FROM     dbo.Расписание INNER JOIN
                             dbo.Классы ON dbo.Расписание.[Код класса] = dbo.Классы.[Код класса] INNER JOIN
                             dbo.Предметы ON dbo.Расписание.[Код предмета] = dbo.Предметы.[Код предмета]"
            Dim command As New SqlCommand(sqlString, Myconn)
            Adapter = New SqlDataAdapter(command)
            Builder = New SqlCommandBuilder(Adapter)
            Adapter.Fill(dt)
            DataGridView1.DataSource = dt
            Myconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "error")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Меню.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class
Imports System.Data.SqlClient
Public Class Фильтры
    Dim Adapter As New SqlDataAdapter()
    Dim Builder As New SqlCommandBuilder()
    Dim ConBuilder As New SqlConnectionStringBuilder()
    Dim Myconn As New SqlConnection(ConBuilder.ConnectionString)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'делаем отдельные функции по запросам и дальше через if комбобокса вызываем эти функции
        ConBuilder.UserID = $"Sarantseva"
        ConBuilder.Password = $"als__.27052003"
        ConBuilder.DataSource = $"LAPTOP-VH1E26FG"
        ConBuilder.InitialCatalog = $"school database"

        If ComboBox1.Text = "Сотрудники отдельных должностей " Then
            otdelkadrov_otdelndolgnosti()
        End If
        If ComboBox1.Text = "Классы различных годов обучения" Then
            spisokuchenikov_classrazlgodov()
        End If
        If ComboBox1.Text = "Расписание для отдельных классов" Then
            spisokclassov_raspisanie()
        End If
        If ComboBox1.Text = "Предметы отдельных преподавателей" Then
            spisokpredmetov_otdprepod()
        End If
        If ComboBox1.Text = "Ученики отдельных классов" Then
            spisokclassov_uchenic()
        End If
        If ComboBox1.Text = "Отдельные виды классов" Then
            spisokclassov_otdvid()
        End If
    End Sub

    Public Sub otdelkadrov_otdelndolgnosti()
        Dim parametr As String = TextBox1.Text
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()
            Dim sqlString = "SELECT dbo.Должности.[Наименование должности], dbo.Сотрудники.ФИО, dbo.Сотрудники.Пол
                            FROM     dbo.Должности INNER JOIN
                             dbo.Сотрудники ON dbo.Должности.[Код должности] = dbo.Сотрудники.[Код должности]
                            WHERE  (dbo.Должности.[Наименование должности] = '" & parametr & "')"
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

    Public Sub spisokuchenikov_classrazlgodov()
        Dim parametr As String = TextBox1.Text
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()

            Dim sqlString = "SELECT dbo.Ученики.ФИО, dbo.Классы.[Код сотрудника-класного руководителя], dbo.Классы.[Код вида], dbo.Классы.[Год обучения]
                            FROM     dbo.Ученики INNER JOIN
                            dbo.Классы ON dbo.Ученики.[Код класса] = dbo.Классы.[Код класса]
                            WHERE  (dbo.Классы.[Год обучения] = '" & parametr & "')"
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

    Public Sub spisokclassov_raspisanie()
        Dim parametr As String = TextBox1.Text
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()

            Dim sqlString = "FROM     dbo.Расписание INNER JOIN
                  dbo.Классы ON dbo.Расписание.[Код класса] = dbo.Классы.[Код класса] INNER JOIN
                  dbo.Предметы ON dbo.Расписание.[Код предмета] = dbo.Предметы.[Код предмета]
                WHERE       (dbo.Предметы.Наименование = '" & parametr & "')"
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

    Public Sub spisokpredmetov_otdprepod()
        Dim parametr As String = TextBox1.Text
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()

            Dim sqlString = "SELECT dbo.Предметы.Наименование AS Expr1, dbo.Сотрудники.ФИО
                            FROM     dbo.Предметы INNER JOIN
                            dbo.Сотрудники ON dbo.Предметы.[Код сотрудника-учителя] = dbo.Сотрудники.[Код сотрудника]
                            WHERE  (dbo.Сотрудники.ФИО = '" & parametr & "')"
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

    Public Sub spisokclassov_otdvid()
        Dim parametr As String = TextBox1.Text
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()

            Dim sqlString = "SELECT dbo.Сотрудники.ФИО, dbo.Классы.[Код вида], dbo.Классы.Буква, dbo.Классы.[Год обучения]
                            FROM     dbo.Классы INNER JOIN
                            dbo.[Виды классов] ON dbo.Классы.[Код вида] = dbo.[Виды классов].[Код вида] INNER JOIN
                             dbo.Сотрудники ON dbo.Классы.[Код сотрудника-класного руководителя] = dbo.Сотрудники.[Код сотрудника]
                            WHERE  (dbo.Классы.[Код вида] = " & parametr & ")"
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

    Public Sub spisokclassov_uchenic()
        Try
            Myconn = New SqlConnection(ConBuilder.ConnectionString)
            Myconn.Open()
            Dim dt = New DataTable()

            Dim sqlString = "SELECT dbo.Ученики.ФИО, dbo.Классы.[Код вида], dbo.Классы.Буква
                            FROM     dbo.Ученики INNER JOIN
                            dbo.Классы ON dbo.Ученики.[Код класса] = dbo.Классы.[Код класса]
                            WHERE  (dbo.Классы.[Код вида] = " & TextBox1.Text(0) & ") AND (dbo.Классы.Буква = '" & TextBox1.Text(2) & "')"
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

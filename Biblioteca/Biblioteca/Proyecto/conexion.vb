Imports System.Data.SqlClient
Public Class conexion
    Public conexion As SqlConnection = New SqlConnection("Data Source= localhost;Initial Catalog=biblioteca; Integrated Security=True")
    Public ds As DataSet = New DataSet()
    Public da As SqlDataAdapter
    Public cmd As SqlCommand
    Public dr As SqlDataReader
    Public dt As New DataTable

    Public Function mostrarTablaPrestamos()
        Dim cmd As New SqlCommand("mostrarPrestamos", conexion)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        Try
            conexion.Open()
            cmd.CommandType = CommandType.StoredProcedure
            da.Fill(dt)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return dt
    End Function
    Public Function mostrarTablaRetornos()
        Dim cmd As New SqlCommand("mostrarRetornos", conexion)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        Try
            conexion.Open()
            cmd.CommandType = CommandType.StoredProcedure
            da.Fill(dt)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return dt
    End Function
    Public Function insertarRetornos(idretorno As Integer, alumnoid As String,
    libroid As Integer, prestamoid As Integer, fechaRetorno As Date, multa As Double, estadoMulta As String)
        Try
            conexion.Open()
            cmd = New SqlCommand("insertarRetorno", conexion)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idretorno", idretorno)
            cmd.Parameters.AddWithValue("@alumnoid", alumnoid)
            cmd.Parameters.AddWithValue("@libroid", libroid)
            cmd.Parameters.AddWithValue("@prestamoid", prestamoid)
            cmd.Parameters.AddWithValue("@fechaRetorno", fechaRetorno)
            cmd.Parameters.AddWithValue("@multa", multa)
            cmd.Parameters.AddWithValue("@estadoMulta", estadoMulta)
            If cmd.ExecuteNonQuery <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            conexion.Close()
        End Try
    End Function
    Public Function buscarRetornos(idretorno As Integer)
        Try
            conexion.Open()
            cmd = New SqlCommand("buscarRetorno", conexion)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idretorno", idretorno)
            If cmd.ExecuteNonQuery <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            conexion.Close()
        End Try
    End Function
    Public Function editarRetorno(idretorno As Integer, estadoMulta As String)
        Try
            conexion.Open()
            cmd = New SqlCommand("editarRetorno", conexion)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idretorno", idretorno)
            cmd.Parameters.AddWithValue("@estadoMulta", estadoMulta)
            If cmd.ExecuteNonQuery <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            conexion.Close()
        End Try
    End Function
End Class

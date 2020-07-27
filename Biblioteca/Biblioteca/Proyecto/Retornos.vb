Imports System.ComponentModel
Imports System.Data.SqlClient
Public Class Retornos
    Private Sub Retornos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mostrarDataGridPrestamos()
        mostrarPrestamos()
        obtenerFecha()
    End Sub
    Private Sub mostrarPrestamos()
        mostrarDataGridPrestamos()
        Dim cn As New conexion
        Dim dt As DataTable = cn.mostrarTablaPrestamos()
        DGlibros.DataSource = dt
    End Sub
    Private Sub mostrarRetornos()
        mostrarDataGridRetornos()
        Dim cn As New conexion
        Dim dt As DataTable = cn.mostrarTablaRetornos()
        DGRetornos.DataSource = dt
    End Sub
    Private Sub obtenerFecha()
        Dim fecha As Date = Date.Today
        txtFechaRetorno.Text = fecha
    End Sub
    Private Sub insertarRetorno()
        Dim cn As New conexion
        Dim idretorno, libroid, prestamoid As Integer
        Dim multa As Double
        Dim alumnoid, estadoMulta As String
        Dim fechaRetorno As Date
        idretorno = txtIdRetorno.Text
        alumnoid = txtIdAlumno.Text
        libroid = txtIdLibro.Text
        prestamoid = txtIdPrestamo.Text
        fechaRetorno = txtFechaRetorno.Text
        multa = txtMulta.Text
        estadoMulta = cmbEstadoMulta.Text
        Try
            If cn.insertarRetornos(idretorno, alumnoid, libroid, prestamoid, fechaRetorno, multa, estadoMulta) Then
                MessageBox.Show("Se guardó el registro de retorno", "Datos Ingresados", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Ocurrió un error al tratar de registrar los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub buscarRetorno()
        Dim cn As New conexion
        Dim idretorno As Integer
        idretorno = txtIdRetorno.Text
        Try
            If cn.buscarRetornos(idretorno) Then
                MsgBox("Encontrado")
                mostrarBusqueda()
            Else
                MsgBox("No se encontró ese registro")
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub mostrarBusqueda()
        Dim cn As New conexion
        Dim cmd As New SqlCommand("buscarRetorno", cn.conexion)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        Try
            cn.conexion.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idretorno", txtIdRetorno.Text)
            da.Fill(dt)
            If dt.Rows.Count <> 0 Then
                DGRetornos.DataSource = dt
                cn.conexion.Close()
            Else
                DGRetornos.DataSource = Nothing
                cn.conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub editarRetorno()
        Dim cn As New conexion
        Dim idretorno As Integer
        Dim estadoMulta As String
        idretorno = txtIdRetorno.Text
        estadoMulta = cmbEstadoMulta.Text
        Try
            If cn.editarRetorno(idretorno, estadoMulta) Then
                MessageBox.Show("Se modificó el registro de retorno correctamente", "Datos Modificados", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Ocurrió un error al tratar de modificar los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub DGlibros_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGlibros.CellContentClick
        Dim fechaVence As Date
        Try
            Dim dgdatos As DataGridViewRow = DGlibros.Rows(e.RowIndex)
            txtIdPrestamo.Text = dgdatos.Cells(0).Value.ToString
            txtIdAlumno.Text = dgdatos.Cells(1).Value.ToString
            txtIdLibro.Text = dgdatos.Cells(2).Value.ToString
            fechaVence = dgdatos.Cells(4).Value.ToString
            calcularMulta(fechaVence)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub calcularMulta(fechaVence As Date)
        If (Date.Compare(fechaVence, txtFechaRetorno.Text) < 0) Then
            txtMulta.Text = 100.0
        Else
            txtMulta.Text = 0
        End If
    End Sub
    Private Function validarCampos()
        If txtIdRetorno.Text <> String.Empty And IsNumeric(txtIdRetorno.Text) And txtIdPrestamo.Text <> String.Empty And txtIdAlumno.Text <> String.Empty And txtIdLibro.Text <> String.Empty And txtFechaRetorno.Text <> String.Empty And txtMulta.Text <> String.Empty Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub mostrarDataGridPrestamos()
        btnRetornos.Enabled = True
        btnPrestamos.Enabled = False
        DGlibros.Enabled = True
        DGlibros.Visible = True
        DGRetornos.Enabled = False
        DGRetornos.Visible = False
    End Sub
    Private Sub mostrarDataGridRetornos()
        btnRetornos.Enabled = False
        btnPrestamos.Enabled = True
        DGlibros.Enabled = False
        DGlibros.Visible = False
        DGRetornos.Enabled = True
        DGRetornos.Visible = True
    End Sub
    Private Sub limpiar()
        txtIdRetorno.Clear()
        txtIdAlumno.Clear()
        txtIdPrestamo.Clear()
        txtIdLibro.Clear()
        txtMulta.Clear()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        limpiar()
    End Sub
    Private Sub btnPrestamos_Click(sender As Object, e As EventArgs) Handles btnPrestamos.Click
        mostrarDataGridPrestamos()
    End Sub

    Private Sub btnRetornos_Click(sender As Object, e As EventArgs) Handles btnRetornos.Click
        mostrarRetornos()
    End Sub
    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        If validarCampos() = True Then
            insertarRetorno()
            mostrarRetornos()
        Else
            MessageBox.Show("No se pudo guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        buscarRetorno()
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        editarRetorno()
    End Sub
    Private Sub txtIdRetorno_MouseHover(sender As Object, e As EventArgs) Handles txtIdRetorno.MouseHover
        toolTip.SetToolTip(txtIdRetorno, "Ingrese el ID de retorno")
        toolTip.ToolTipTitle = "ID Retorno"
        toolTip.ToolTipIcon = ToolTipIcon.Info
    End Sub
    Private Sub txtIdAlumno_MouseHover(sender As Object, e As EventArgs) Handles txtIdAlumno.MouseHover
        toolTip.SetToolTip(txtIdAlumno, "Se asignará el ID del alumno")
        toolTip.ToolTipTitle = "ID Alumno"
        toolTip.ToolTipIcon = ToolTipIcon.Info
    End Sub
    Private Sub txtIdLibro_MouseHover(sender As Object, e As EventArgs) Handles txtIdLibro.MouseHover
        toolTip.SetToolTip(txtIdLibro, "Se asignará el ID del libro")
        toolTip.ToolTipTitle = "ID Libro"
        toolTip.ToolTipIcon = ToolTipIcon.Info
    End Sub
    Private Sub txtIdPrestamo_MouseHover(sender As Object, e As EventArgs) Handles txtIdPrestamo.MouseHover
        toolTip.SetToolTip(txtIdPrestamo, "Se asignará el ID del préstamo")
        toolTip.ToolTipTitle = "ID Préstamo"
        toolTip.ToolTipIcon = ToolTipIcon.Info
    End Sub
    Private Sub txtFechaRetorno_MouseHover(sender As Object, e As EventArgs) Handles txtFechaRetorno.MouseHover
        toolTip.SetToolTip(txtFechaRetorno, "Contiene la fecha en la que se realiza el retorno")
        toolTip.ToolTipTitle = "Fecha Retorno"
        toolTip.ToolTipIcon = ToolTipIcon.Info
    End Sub
    Private Sub txtMulta_MouseHover(sender As Object, e As EventArgs) Handles txtMulta.MouseHover
        toolTip.SetToolTip(txtMulta, "Calcula automáticamente el valor de  la multa")
        toolTip.ToolTipTitle = "Multa"
        toolTip.ToolTipIcon = ToolTipIcon.Info
    End Sub
    Private Sub cmbEstadoMulta_MouseHover(sender As Object, e As EventArgs) Handles cmbEstadoMulta.MouseHover
        toolTip.SetToolTip(cmbEstadoMulta, "Seleccione si la multa está pagada o no")
        toolTip.ToolTipTitle = "Estado Multa"
        toolTip.ToolTipIcon = ToolTipIcon.Info
    End Sub
    Private Sub txtIdRetorno_Validating(sender As Object, e As CancelEventArgs) Handles txtIdRetorno.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 And IsNumeric(txtIdRetorno.Text) Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "No pueden dejar campos vacíos ni ingresar datos que no sean números")
        End If
    End Sub
    Private Sub txtIdAlumno_Validating(sender As Object, e As CancelEventArgs) Handles txtIdAlumno.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "No pueden dejar campos vacíos")
        End If
    End Sub
    Private Sub txtIdLibro_Validating(sender As Object, e As CancelEventArgs) Handles txtIdLibro.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "No pueden dejar campos vacíos")
        End If
    End Sub
    Private Sub txtIdPrestamo_Validating(sender As Object, e As CancelEventArgs) Handles txtIdPrestamo.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "No pueden dejar campos vacíos")
        End If
    End Sub
    Private Sub txtFechaRetorno_Validating(sender As Object, e As CancelEventArgs) Handles txtFechaRetorno.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "No pueden dejar campos vacíos")
        End If
    End Sub
    Private Sub txtMulta_Validating(sender As Object, e As CancelEventArgs) Handles txtMulta.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "No pueden dejar campos vacíos")
        End If
    End Sub
End Class
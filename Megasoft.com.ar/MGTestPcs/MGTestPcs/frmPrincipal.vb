Imports System.Net
Imports System.IO
Imports System.Net.Sockets
Imports Microsoft.Win32
Imports System.Management
Imports System.Net.NetworkInformation

Public Class frmPrincipal


    Private PreviousClientSize As Size = Me.ClientSize

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Pone la lista de discos disponibles
        Dim allDrives As DriveInfo() = DriveInfo.GetDrives()

        For Each d In allDrives
            CBoxDiscos.Items.Add(d.Name)
        Next

        If CBoxDiscos.Items.Count > 0 Then
            CBoxDiscos.SelectedIndex = 0
        End If

    End Sub


    Private Sub btnComprobar_Click(sender As Object, e As EventArgs) Handles btnComprobar.Click

        Try


            'Procesador ******************************************

            Dim processorClockSpeed As Integer = GetProcessorClockSpeed()
            Dim processorCliente As Integer = 2000
            Dim processorServidor As Integer = 2500

            TextBox1.Text = GetProcessorName.ToString
            TextBox2.Text = GetProcessorName.ToString
            TextBox15.Text = GetProcessorName.ToString

            If processorClockSpeed >= processorCliente Then

                TextBox2.BackColor = Color.Green
                TextBox2.ForeColor = Color.White
                TextBox15.BackColor = Color.Green
                TextBox15.ForeColor = Color.White
            Else

                TextBox2.BackColor = Color.Red
                TextBox2.ForeColor = Color.White
                TextBox15.BackColor = Color.Red
                TextBox15.ForeColor = Color.White

            End If

            If processorClockSpeed >= processorServidor Then
                TextBox1.BackColor = Color.Green
                TextBox1.ForeColor = Color.White
            Else
                TextBox1.BackColor = Color.Red
                TextBox1.ForeColor = Color.White
            End If



            'Memoria *********************************************

            Dim totalMemory As Double = CDbl(My.Computer.Info.TotalPhysicalMemory) / (1024 * 1024 * 1024)

            TextBox3.Text = totalMemory.ToString("N2") & "Gb"
            TextBox4.Text = totalMemory.ToString("N2") & "Gb"
            TextBox16.Text = totalMemory.ToString("N2") & "Gb"

            If totalMemory > 8 Then ' Servidor Dedicado
                TextBox3.BackColor = Color.Green
                TextBox3.ForeColor = Color.White
            Else
                TextBox3.BackColor = Color.Red
                TextBox3.ForeColor = Color.White
            End If


            If totalMemory > 4 Then ' Servidor
                TextBox4.BackColor = Color.Green
                TextBox4.ForeColor = Color.White
            Else
                TextBox4.BackColor = Color.Red
                TextBox4.ForeColor = Color.White
            End If

            If totalMemory > 4 Then ' Cliente
                TextBox16.BackColor = Color.Green
                TextBox16.ForeColor = Color.White
            Else
                TextBox16.BackColor = Color.Red
                TextBox16.ForeColor = Color.White
            End If





            'Discos *****************************************************************

            Dim DiscoElegido As String = CBoxDiscos.SelectedItem.ToString
            Dim totalSize As Double = CDbl(My.Computer.FileSystem.GetDriveInfo(DiscoElegido).TotalSize) / (1024 * 1024 * 1024)
            Dim availableFreeSpace As Double = CDbl(My.Computer.FileSystem.GetDriveInfo(DiscoElegido).AvailableFreeSpace) / (1024 * 1024 * 1024)
            TextBox5.Text = availableFreeSpace.ToString("N2") & " GB"
            TextBox6.Text = availableFreeSpace.ToString("N2") & " GB"
            TextBox17.Text = availableFreeSpace.ToString("N2") & " GB"


            If availableFreeSpace > 2 Then 'Servidor Dedicado
                TextBox5.BackColor = Color.Green
                TextBox5.ForeColor = Color.White
            Else
                TextBox5.BackColor = Color.Red
                TextBox5.ForeColor = Color.White
            End If

            If availableFreeSpace > 1 Then  ' Servidor
                TextBox6.BackColor = Color.Green
                TextBox6.ForeColor = Color.White
            Else
                TextBox6.BackColor = Color.Red
                TextBox6.ForeColor = Color.White
            End If

            If availableFreeSpace > 1 Then  ' Cliente
                TextBox17.BackColor = Color.Green
                TextBox17.ForeColor = Color.White
            Else
                TextBox17.BackColor = Color.Red
                TextBox17.ForeColor = Color.White
            End If


            'Resolucion de Pantalla *********************************************************
            Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
            Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

            Dim resolucion = screenWidth.ToString & " x " & screenHeight.ToString
            TextBox7.Text = resolucion
            TextBox8.Text = resolucion
            TextBox18.Text = resolucion

            If screenHeight > 1024 Then  ' Alto
                TextBox7.BackColor = Color.Green
                TextBox7.ForeColor = Color.White
                TextBox8.BackColor = Color.Green
                TextBox8.ForeColor = Color.White
                TextBox18.BackColor = Color.Green
                TextBox18.ForeColor = Color.White
            Else
                TextBox7.BackColor = Color.Red
                TextBox7.ForeColor = Color.White
                TextBox8.BackColor = Color.Red
                TextBox8.ForeColor = Color.White
                TextBox18.BackColor = Color.Red
                TextBox18.ForeColor = Color.White
            End If

            If screenWidth > 720 Then  'Ancho
                TextBox7.BackColor = Color.Green
                TextBox7.ForeColor = Color.White
                TextBox8.BackColor = Color.Green
                TextBox8.ForeColor = Color.White
                TextBox18.BackColor = Color.Green
                TextBox18.ForeColor = Color.White
            Else
                TextBox7.BackColor = Color.Red
                TextBox7.ForeColor = Color.White
                TextBox8.BackColor = Color.Red
                TextBox8.ForeColor = Color.White
                TextBox18.BackColor = Color.Red
                TextBox18.ForeColor = Color.White
            End If

            'Sistema Operativo **************************************************************
            TextBox9.Text = My.Computer.Info.OSFullName.ToString
            TextBox10.Text = My.Computer.Info.OSFullName.ToString
            TextBox19.Text = My.Computer.Info.OSFullName.ToString


            If IsWindows10ProOrAbove() = True Then
                TextBox9.BackColor = Color.Green
                TextBox9.ForeColor = Color.White
                TextBox10.BackColor = Color.Green
                TextBox10.ForeColor = Color.White
                TextBox19.BackColor = Color.Green
                TextBox19.ForeColor = Color.White
            Else
                TextBox9.BackColor = Color.Red
                TextBox9.ForeColor = Color.White
                TextBox10.BackColor = Color.Red
                TextBox10.ForeColor = Color.White
                TextBox19.BackColor = Color.Red
                TextBox19.ForeColor = Color.White
            End If


            'Internet ************************************************************************************

            If HasInternetConnection() Then
                TextBox11.Text = "Ok"
                TextBox11.ForeColor = Color.White
                TextBox11.BackColor = Drawing.Color.Green
                TextBox14.Text = "Ok"
                TextBox14.ForeColor = Color.White
                TextBox14.BackColor = Drawing.Color.Green
                TextBox20.Text = "Ok"
                TextBox20.ForeColor = Color.White
                TextBox20.BackColor = Drawing.Color.Green
            Else
                TextBox11.Text = "Error"
                TextBox11.ForeColor = Color.White
                TextBox11.BackColor = Drawing.Color.Red
                TextBox14.Text = "Error"
                TextBox14.ForeColor = Color.White
                TextBox14.BackColor = Drawing.Color.Red
                TextBox20.Text = "Error"
                TextBox20.ForeColor = Color.White
                TextBox20.BackColor = Drawing.Color.Red
            End If

        Catch ex As Exception
            MessageBox.Show("Se produjo un error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--------------------------------------------- Estilos-----------------------------------------------

#Region "Esilos"
    Private Sub ResizeControls()
        ' Establece el ancho y alto de la ventana de formulario
        Dim formWidth As Integer = Me.ClientSize.Width
        Dim formHeight As Integer = Me.ClientSize.Height

        ' Recorre todos los controles en el formulario y ajusta su tamaño, posición y fuente
        For Each control As Control In Me.Controls
            ' Si el control es un contenedor (como un panel o un grupo), ajusta sus hijos
            If TypeOf control Is ContainerControl Then
                For Each childControl As Control In control.Controls
                    childControl.Width = CInt(childControl.Width / Me.PreviousClientSize.Width * formWidth)
                    childControl.Height = CInt(childControl.Height / Me.PreviousClientSize.Height * formHeight)
                    childControl.Left = CInt(childControl.Left / Me.PreviousClientSize.Width * formWidth)
                    childControl.Top = CInt(childControl.Top / Me.PreviousClientSize.Height * formHeight)
                    childControl.Font = New Font(childControl.Font.FontFamily, childControl.Font.SizeInPoints / (Me.PreviousClientSize.Width / formWidth))
                Next
            End If

            ' Ajusta el tamaño, la posición y la fuente de los controles que no son contenedores
            control.Width = CInt(control.Width / Me.PreviousClientSize.Width * formWidth)
            control.Height = CInt(control.Height / Me.PreviousClientSize.Height * formHeight)
            control.Left = CInt(control.Left / Me.PreviousClientSize.Width * formWidth)
            control.Top = CInt(control.Top / Me.PreviousClientSize.Height * formHeight)
            control.Font = New Font(control.Font.FontFamily, control.Font.SizeInPoints / (Me.PreviousClientSize.Width / formWidth))

            ' Si el control es un DataGridView, ajustamos el tamaño de las celdas
            If TypeOf control Is DataGridView Then
                Dim dgv As DataGridView = DirectCast(control, DataGridView)
                For Each columna As DataGridViewColumn In dgv.Columns
                    columna.Width = columna.Width * (formWidth / Me.PreviousClientSize.Width)
                Next
                For Each fila As DataGridViewRow In dgv.Rows
                    fila.Height = fila.Height * (formHeight / Me.PreviousClientSize.Height)
                Next
            End If

        Next

        ' Actualiza el tamaño previo de la ventana de formulario para la próxima vez que se llame a ResizeControls
        Me.PreviousClientSize = Me.ClientSize
    End Sub

#End Region






    '---------------------------------------Metdos---------------------------------------------------

#Region "Metodos"
    Public Function HasInternetConnection() As Boolean
        Dim ping As New Ping()
        Try
            Dim reply As PingReply = ping.Send("www.google.com", 1000)
            Return (reply.Status = IPStatus.Success)
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function ObtenerNombreSistemaOperativo() As String
        Dim osName As String = My.Computer.Info.OSFullName.ToString
        Return osName
    End Function

    Public Function IsWindows10ProOrAbove() As Boolean
        Dim osVersion As Version = Environment.OSVersion.Version
        Dim osName As String = ObtenerNombreSistemaOperativo()

        If (osName.Contains("7")) Or (osName.Contains(" Home ")) Or (osName.Contains("Home") Or (osVersion.Major >= 10)) Then
            Return False

        Else
            Return True

        End If
    End Function

    Public Function GetProcessorName() As String
        Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")
        For Each obj As ManagementObject In searcher.Get()
            Return CStr(obj("Name"))
        Next
        Return "Desconocido"
    End Function

    Protected Function GetProcessorClockSpeed() As Integer
        Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")
        For Each queryObj As ManagementObject In searcher.Get()
            Dim clockSpeed As Integer = CInt(queryObj("MaxClockSpeed"))
            Return clockSpeed
        Next
        Return 0
    End Function


#End Region



    '-------------------------------------Botones----------------------------------------------------

#Region "Botones"
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReq_Click(sender As Object, e As EventArgs) Handles btnReq.Click
        frmRequisitos.Show()
    End Sub

#End Region

End Class

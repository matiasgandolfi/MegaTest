Imports Microsoft.VisualBasic

Public Class ResponsiveForm
    Inherits Form

    Private _resizeInProgress As Boolean = False
    Private _initialWidth As Integer
    Private _initialHeight As Integer

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        ' Se guarda el tamaño inicial del formulario y de los controles
        _initialWidth = Me.Width
        _initialHeight = Me.Height

        For Each control As Control In Me.Controls
            control.Tag = New ControlSizeAndPosition(control.Width, control.Height, control.Left, control.Top)
        Next
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)

        ' Si ya se está ejecutando una redimensión, no se hace nada
        If _resizeInProgress Then
            Return
        End If

        ' Se marca que se está ejecutando una redimensión
        _resizeInProgress = True

        ' Se redimensionan todos los controles del formulario
        ResizeControls()

        ' Se marca que la redimensión ha finalizado
        _resizeInProgress = False
    End Sub

    Private Sub ResizeControls()
        Dim widthRatio As Double = Me.Width / _initialWidth
        Dim heightRatio As Double = Me.Height / _initialHeight

        For Each control As Control In Me.Controls
            ' Se obtiene la posición y tamaño inicial del control
            Dim initialSizeAndPosition As ControlSizeAndPosition = DirectCast(control.Tag, ControlSizeAndPosition)

            ' Se ajusta el tamaño y la posición del control manteniendo la relación inicial
            control.Width = CInt(initialSizeAndPosition.Width * widthRatio)
            control.Height = CInt(initialSizeAndPosition.Height * heightRatio)
            control.Left = CInt(initialSizeAndPosition.Left * widthRatio)
            control.Top = CInt(initialSizeAndPosition.Top * heightRatio)
        Next
    End Sub

End Class

Public Class ControlSizeAndPosition
    Public Property Width As Integer
    Public Property Height As Integer
    Public Property Left As Integer
    Public Property Top As Integer

    Public Sub New(width As Integer, height As Integer, left As Integer, top As Integer)
        Me.Width = width
        Me.Height = height
        Me.Left = left
        Me.Top = top
    End Sub
End Class

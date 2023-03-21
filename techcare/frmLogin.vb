Imports System.Windows.Forms

Public Class frmLogin

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        startLogin()
    End Sub

    Private Sub tbPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles tbPassword.KeyDown
        If e.KeyCode = Keys.Enter Then startLogin()
    End Sub

    Private Sub tbUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles tbUsername.KeyDown
        If e.KeyCode = Keys.Enter Then startLogin()
    End Sub

    Public Sub startLogin()
        ' This procedure is called when Enter key is pressed / Login button is pressed. First, the program checks for input in both
        ' text fields. If not, an error will show. If there is, the program will call the authentication function. This returns the  
        ' employee ID of the user intending to login, as long as the username and password are correct. As long as a valid employee
        ' ID has been returned, the user will be permitted access. This is achieved by displaying the side panel on the main window,
        ' and displaying the name and employee ID of the logged-on user.

        If tbUsername.TextLength > 0 And tbPassword.TextLength > 0 Then
            Dim empID As String = Convert.ToString(functions.authenticate(tbUsername.Text, tbPassword.Text))

            If empID = "0" Then
                MsgBox("Login error! Either the username or password is incorrect.", MsgBoxStyle.Critical, "techcare")
                tbPassword.Clear()
                tbPassword.Focus()
            Else
                frmMainWindow.sidePanel.Visible = True
                frmMainWindow.lblEmpID.Visible = True
                frmMainWindow.lblCurrentUser.Visible = True
                frmMainWindow.lblEmpID.Text = empID
                frmMainWindow.lblCurrentUser.Text = functions.obtainEmployeeDetails(empID, 2).ToString & " " & functions.obtainEmployeeDetails(empID, 3).ToString
                frmMainWindow.Refresh()
                Me.Close()
            End If
        Else
            MsgBox("Login error! A valid username and password is required to login.", MsgBoxStyle.Critical, "techcare")
            tbPassword.Clear()
            tbPassword.Focus()
        End If
    End Sub
End Class

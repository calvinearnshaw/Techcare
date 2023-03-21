Imports MySql.Data.MySqlClient
Public Class frmCreateUser

    Private Sub btnConfirmChanges_Click(sender As Object, e As EventArgs) Handles btnConfirmChanges.Click
        ' This procedure is called upon clicking the Confirm button. The program will send an SQL query to
        ' insert a new record in the Employee database. A username will also be generated, which is unique to
        ' this specific user only.

        If tbTitle.Text = Nothing Or tbFname.Text = Nothing Or tbSname.Text = Nothing Or (rbBasicAccess.Checked = False And rbFullAccess.Checked = False) _
            Or tbPassword.Text = Nothing Or tbConfirmPassword.Text = Nothing Then
            MsgBox("One or more fields are missing information. Please check that all fields have been filled in and try again.", MsgBoxStyle.Exclamation, "techcare")
            tbPassword.Clear()
            tbConfirmPassword.Clear()
        Else
            If tbPassword.Text = tbConfirmPassword.Text Then
                Try
                    Dim username As String = functions.generateUsername(tbFname.Text, tbSname.Text)
                    Dim ual As String = Nothing

                    If rbBasicAccess.Checked = True Then
                        ual = "Basic"
                    Else
                        ual = "Full"
                    End If

                    If username Is Nothing Then
                        Throw New System.Exception("Unable to generate user account. Process terminated.")
                    Else
                        Dim dbConnection As MySqlConnection = New MySqlConnection("Server=localhost;Database=techcare;Uid=techcare;Pwd=techcare;")
                        Dim dbCommand As MySqlCommand = New MySqlCommand("INSERT INTO Employees (employeeID, title, forename, surname, userAccessLevel, username, password) VALUES " &
                                                             "(@empID, @empTitle, @fname, @sname, @ual, @username, @password);", dbConnection)

                        dbConnection.Open()

                        dbCommand.Parameters.AddWithValue("@empID", functions.generateUid("Employees", "employeeID", 7))
                        dbCommand.Parameters.AddWithValue("@empTitle", tbTitle.Text)
                        dbCommand.Parameters.AddWithValue("@fname", tbFname.Text)
                        dbCommand.Parameters.AddWithValue("@sname", tbSname.Text)
                        dbCommand.Parameters.AddWithValue("@ual", ual)
                        dbCommand.Parameters.AddWithValue("@username", username)
                        dbCommand.Parameters.AddWithValue("@password", tbPassword.Text)

                        dbCommand.ExecuteNonQuery()

                        dbConnection.Close()
                        dbCommand.Dispose()
                        dbConnection.Dispose()

                        MsgBox("User Account Created!" & vbNewLine & vbNewLine & "The employee's username is: " & username &
                               ". Please write this down for future reference.", MsgBoxStyle.Information, "techcare")

                        frmUserMgmt.refreshEmpList()

                        Me.Close()
                    End If
                Catch ex As Exception
                    MsgBox("An error has occured whilst creating the new user account." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
                    Me.Close()
                End Try
            Else
                MsgBox("The passwords entered do not match. Please try again.", MsgBoxStyle.Exclamation, "techcare")
                tbPassword.Clear()
                tbConfirmPassword.Clear()
            End If
        End If
    End Sub

    Private Sub frmCreateUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' This procedure is needed due to the type of dialog used, and is a patch for a previous bug found in the program.
        ' This clears off any data previously entered on the form, if a new form is opened.
        tbTitle.Clear()
        tbFname.Clear()
        tbSname.Clear()
        tbPassword.Clear()
        tbConfirmPassword.Clear()
        rbBasicAccess.Checked = False
        rbFullAccess.Checked = False
    End Sub
End Class
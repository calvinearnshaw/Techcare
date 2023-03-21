Public Class frmMainWindow

    Private Sub frmMainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' This is called upon loading the techcare application. Since all aspects of the application are encompassed inside the 
        ' Main Window, this is only called once!

        System.Threading.Thread.Sleep(3000)                                                     ' There's a known bug with winforms that causes an issue with maximised
        Me.WindowState = FormWindowState.Maximized                                              ' windows and splash screens. This is a workaround until Microsoft fixes it.

        functions.log("Splash Screen initialised")                                              ' Debug function, only runs if user has given consent.

        For Each ctl As Control In Me.Controls                                                  ' This fixed loop sets the background colour of the MDI Parent to the
            If TypeOf ctl Is MdiClient Then                                                     ' dark blue colour set out in the design. Without this, the program would
                ctl.BackColor = Me.BackColor                                                    ' have a grey background colour, therefore not matching the design.
            End If
        Next ctl

        lblVersionID.Text = "Version " & My.Application.Info.Version.ToString

        If My.Settings.userAskedForConsent = False Then                                         ' This is extra code which will be removed once techcare is on a stable
            frmLogsConsent.MdiParent = Me                                                       ' release. This displays a window asking for consent to log information
            frmLogsConsent.Show()                                                               ' while techcare is running.
        Else
            ' This checks if the user is running techcare for the first time. If so, the initial setup wizard will be displayed.
            If My.Settings.userFirstRun = True Then
                frmInitialSetup.MdiParent = Me
                frmInitialSetup.Show()
                functions.log("Initial setup has started.")
            Else
                While functions.databaseCheck(My.Settings.dbName) = False
                    ' This conditional loop does not allow the login window to display unless a valid connection to the server has been made.
                    Dim conf As Integer = MsgBox("Cannot find techcare database on server." & vbNewLine & vbNewLine &
                                                 "Check that the MySQL server is running, then click OK to try again.", MsgBoxStyle.Exclamation, "techcare")
                End While

                frmLogin.MdiParent = Me
                frmLogin.Show()
            End If
        End If

        Try
            If functions.obtainBusinessDetails(0) = "" Then
                Me.Text = "techcare"
            Else
                Me.Text = "techcare: " & functions.obtainBusinessDetails(0) & ", " & functions.obtainBusinessDetails(1) &
                                ", " & functions.obtainBusinessDetails(2)
            End If
        Catch ex As Exception
            Me.Text = "techcare"
            functions.log("Exception occurred whilst adding business details: " & ex.Message)
        End Try
    End Sub

    Private Sub frmMainWindow_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' This small piece of code is to refresh the background every time the window is resized. It prevents
        ' the techcare logo in the background from becoming glitched every time the window is resized!
        Me.Refresh()
    End Sub

    Private Sub btnSignOut_Click(sender As Object, e As EventArgs) Handles btnSignOut.Click
        ' When the sign out button is pressed, this procedure is called. This hides the side panel, and closes all and any windows open EXCEPT the Login window
        ' and (of course) the main window itself.

        functions.log("Sign Out button pressed")

        Dim msg As Integer
        msg = MsgBox("Are you sure you wish to sign out?" & vbNewLine & vbNewLine & "Anything that is unsaved will be lost.", MsgBoxStyle.YesNo, "techcare")

        If msg = MsgBoxResult.Yes Then
            functions.log("Sign out sequence started")
            frmAppConfig.Close()
            frmAddRepairRemark.Close()
            frmCreateNewRepair.Close()
            frmRepairMgmt.Close()
            frmUpdateQuote.Close()
            frmUpdateRepairStatus.Close()
            frmCreateUser.Close()
            frmEditEmpDetails.Close()
            frmResetPassword.Close()
            frmUserMgmt.Close()

            lblCurrentUser.Text = ""
            lblEmpID.Text = ""
            lblCurrentUser.Visible = False
            lblEmpID.Visible = False
            sidePanel.Visible = False
            frmLogin.MdiParent = Me
            frmLogin.Show()
            Me.Refresh()
            functions.log("Sign out sequence completed")
        End If
    End Sub

    Private Sub btnNewRepair_Click(sender As Object, e As EventArgs) Handles btnNewRepair.Click
        functions.log("New Repair button pressed")
        frmCreateNewRepair.MdiParent = Me
        frmCreateNewRepair.Show()
    End Sub

    Private Sub btnRepairMgmt_Click(sender As Object, e As EventArgs) Handles btnRepairMgmt.Click
        functions.log("Repair Management button pressed")
        frmRepairMgmt.MdiParent = Me
        frmRepairMgmt.Show()
    End Sub

    Private Sub btnUserMgmt_Click(sender As Object, e As EventArgs) Handles btnUserMgmt.Click
        functions.log("User Management button pressed")

        If functions.obtainEmployeeDetails(lblEmpID.Text, 4) = "Full" Then
            functions.log("Current user has permission to access User Management, opening...")
            frmUserMgmt.MdiParent = Me
            frmUserMgmt.Show()
        Else
            functions.log("Current user does NOT have permission to access User Management")
            MsgBox("You do not have the required access level to access this area.", MsgBoxStyle.Exclamation, "techcare")
        End If
    End Sub

    Private Sub btnApplicationSettings_Click(sender As Object, e As EventArgs) Handles btnApplicationSettings.Click
        functions.log("Application Settings button pressed")

        If functions.obtainEmployeeDetails(lblEmpID.Text, 4) = "Full" Then
            functions.log("Current user has permission to access Application Settings, opening...")

            frmAppConfig.MdiParent = Me
            frmAppConfig.Show()
        Else
            functions.log("Current user does NOT permission to access Application Settings")
            MsgBox("You do not have the required access level to access this area.", MsgBoxStyle.Exclamation, "techcare")
        End If
    End Sub
End Class

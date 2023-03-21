Imports MySql.Data.MySqlClient
Public Class frmAppConfig

    Public Sub HideAllPanels()
        ' This procedure is called anytime the user wishes to view another area of the Application Settings.
        databaseConfigPanel.Visible = False
        databaseConfigPanel.Dock = DockStyle.None
        btnDatabaseConfig.BackColor = Color.FromArgb(44, 48, 55)

        dataMgmtPanel.Visible = False
        dataMgmtPanel.Dock = DockStyle.None
        btnDataMgmt.BackColor = Color.FromArgb(44, 48, 55)

        aboutTechcarePanel.Visible = False
        aboutTechcarePanel.Dock = DockStyle.None
        btnAboutTechcare.BackColor = Color.FromArgb(44, 48, 55)

        resetSoftwarePanel.Visible = False
        resetSoftwarePanel.Dock = DockStyle.None
        btnResetSoftware.BackColor = Color.FromArgb(44, 48, 55)
    End Sub

    ' *******************************************************************************************************
    ' DATABASE CONFIGURATION CODE
    ' *******************************************************************************************************

    Private Sub btnDatabaseConfig_Click(sender As Object, e As EventArgs) Handles btnDatabaseConfig.Click
        ' This procedure is called upon clicking the Database Config button. It retrieves various settings
        ' from the application's internal My.SETTINGS storage (a file located in the Appdata folder of the
        ' user's computer), and populates text boxes within the Database Config panel, so the user can adjust
        ' these if required. The Database Config button also turns blue to show the user that this button has
        ' been selected.

        HideAllPanels()

        databaseConfigPanel.Dock = DockStyle.Fill
        databaseConfigPanel.Visible = True

        tbDbName.Text = My.Settings.dbName
        tbDbName.ReadOnly = True
        tbDbLocation.Text = My.Settings.dbLocation
        tbDbUsername.Text = My.Settings.dbUsername
        tbDbPassword.Text = My.Settings.dbPassword

        btnDatabaseConfig.BackColor = Color.FromArgb(32, 129, 197)
    End Sub

    Private Sub btnSaveDbConfig_Click(sender As Object, e As EventArgs) Handles btnSaveDbConfig.Click
        Dim confirmation As DialogResult

        confirmation = MessageBox.Show("WARNING!!" & vbNewLine & vbNewLine &
                                       "If the settings listed here do not match your server, this software will stop working." &
                                       vbNewLine & "Techcare must restart to apply its changes. Do you wish to continue?",
                                       "techcare", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmation = DialogResult.Yes Then
            If tbDbName.TextLength = 0 Or tbDbLocation.TextLength = 0 Then
                MsgBox("You must have a valid MySQL Server location and database name to continue.", MsgBoxStyle.Critical, "techcare")
            Else
                My.Settings.dbLocation = tbDbLocation.Text
                My.Settings.dbName = tbDbName.Text
                My.Settings.dbUsername = tbDbUsername.Text
                My.Settings.dbPassword = tbDbPassword.Text
                My.Settings.Save()
                My.Settings.Reload()
                MsgBox("Database Configuration updated successfully. Techcare will now restart.", MsgBoxStyle.Information, "techcare")

                Application.Restart()
            End If
        End If
    End Sub

    Private Sub btnTestDbConnection_Click(sender As Object, e As EventArgs) Handles btnTestDbConnection.Click
        ' This procedure is called upon clicking the TEST CONNECTION button. It checks that the techcare database exists on the MySQL Server.
        ' If it does, the SAVE button is enabled, to allow the user to save their changes. This procedure acts as a "safety net" to ensure that
        ' the program doesn't fail to load afterwards.

        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & tbDbLocation.Text & ";Database=" & tbDbName.Text & ";Uid=" &
                                                                      tbDbUsername.Text & ";Pwd=" & tbDbPassword.Text & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT schema_name FROM information_schema.schemata WHERE schema_name = @dbName;",
                                                             dbConnection)

            dbCommand.Parameters.AddWithValue("@dbName", tbDbName.Text)

            dbConnection.Open()

            If dbCommand.ExecuteScalar = tbDbName.Text Then
                MsgBox("Connection to database has been successfully made!", MsgBoxStyle.Information, "techcare")
                btnSaveDbConfig.Enabled = True
                lblWarningDbConfig.Visible = True
                tbDbName.ReadOnly = True
                tbDbLocation.ReadOnly = True
                tbDbUsername.ReadOnly = True
                tbDbPassword.ReadOnly = True
            Else
                MsgBox("Connection to database has failed. Please check all information is correct and try again.", MsgBoxStyle.Critical, "techcare")
                btnSaveDbConfig.Enabled = False
                lblWarningDbConfig.Visible = False
                tbDbName.ReadOnly = True
                tbDbLocation.ReadOnly = False
                tbDbUsername.ReadOnly = False
                tbDbPassword.ReadOnly = False
            End If

            dbConnection.Close()
        Catch ex As Exception
            MsgBox("Connection to database has failed. Please check all information is correct and try again." & vbNewLine & vbNewLine &
                   "More information related to the problem is below:" & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
        End Try
    End Sub

    ' *******************************************************************************************************
    ' DATA MANAGEMENT CODE (experimental features)
    ' *******************************************************************************************************

    Private Sub btnDataMgmt_Click(sender As Object, e As EventArgs) Handles btnDataMgmt.Click
        ' This procedure is called upon clicking the Data Management button.
        HideAllPanels()

        dataMgmtPanel.Dock = DockStyle.Fill
        dataMgmtPanel.Visible = True

        btnDataMgmt.BackColor = Color.FromArgb(32, 129, 197)
    End Sub

    Private Sub btnDbMgmtBackupData_Click(sender As Object, e As EventArgs) Handles btnDbMgmtBackupData.Click
        ' This procedure is called on clicking the Backup Data button. To make this work, the XAMPP Server (alongside MySQL) must be running.
        ' First, the program prompts the user to find the location of the XAMPP directory. (The user does not need to find the MySQLDump executable)
        ' The program then checks that the MYSQLDUMP.EXE file exists. If it doesn't - the program will prompt the user to find the correct XAMPP folder.
        ' Otherwise, the user will then be shown a "Save As" dialog which allows the user to select where the backup will be stored.
        ' After this has been selected, a Process object is created (essentially an object which sends commands to a given program). In this case, we
        ' use Command Prompt to send a MySQLDump instruction to backup the server to an area of the user's choosing.

        Dim xamppLocation As String = ""
        Dim dbDumpLocation As String = ""

        xamppBackupToLocationDialog.FileName = "Techcare DB Backup " & DateTime.Now.ToString("dd-MM-yyyy HH-mm") & ".sql"
        xamppLocationDialog.Description = "Select XAMPP Server folder."

        If xamppLocationDialog.ShowDialog = DialogResult.OK Then
            xamppLocation = xamppLocationDialog.SelectedPath & "\mysql\bin\"

            If System.IO.File.Exists(xamppLocation & "mysqldump.exe") = True Then
                If xamppBackupToLocationDialog.ShowDialog = DialogResult.OK Then
                    Try
                        Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName & ";Uid=" &
                                                                                 My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
                        dbConnection.Open()

                        dbDumpLocation = xamppBackupToLocationDialog.FileName

                        Dim backup As New Process
                        backup.StartInfo.FileName = "cmd.exe"
                        backup.StartInfo.UseShellExecute = False
                        backup.StartInfo.WorkingDirectory = xamppLocation
                        backup.StartInfo.RedirectStandardInput = True
                        backup.StartInfo.RedirectStandardOutput = True

                        backup.Start()

                        Dim backupStream As System.IO.StreamWriter = backup.StandardInput
                        Dim myStreamReader As System.IO.StreamReader = backup.StandardOutput

                        backupStream.WriteLine("mysqldump --user=" & My.Settings.dbUsername & " --password=" & My.Settings.dbPassword & " --host=" &
                                               My.Settings.dbLocation & " --databases " & My.Settings.dbName & " > """ & dbDumpLocation & """")

                        backupStream.Close()
                        backup.WaitForExit()
                        backup.Close()

                        dbConnection.Close()

                        MsgBox("Backup complete!", MsgBoxStyle.Information, "techcare")
                    Catch ex As Exception
                        MsgBox("An error has occured while backing up the database." & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
                    End Try
                End If
            Else
                MsgBox("MySQLDump.exe was not found in the specified XAMPP directory. Please check that the XAMPP directory has been selected " &
                       "and try again.", MsgBoxStyle.Critical, "techcare")
            End If
        End If
    End Sub

    Private Sub btnDbMgmtRestoreData_Click(sender As Object, e As EventArgs) Handles btnDbMgmtRestoreData.Click
        ' This procedure is called upon clicking the RESTORE DATA button. In order for this to happen, techcare will erase all and any data
        ' currently on the database. The database itself will also be deleted. Then, the program will execute the mysql executable via a Process
        ' object similar to above. This writes the data back on to the server again. The program will automatically restart once this has been
        ' completed.

        Dim xamppLocation As String = ""
        Dim dbDumpLocation As String = ""

        xamppLocationDialog.Description = "Select XAMPP Server folder."

        If xamppLocationDialog.ShowDialog = DialogResult.OK Then
            xamppLocation = xamppLocationDialog.SelectedPath & "\mysql\bin\"

            If System.IO.File.Exists(xamppLocation & "mysqldump.exe") = True Then
                Dim confirmation As DialogResult

                confirmation = MessageBox.Show("Restoring data will restart techcare. Do you wish to continue?",
                                               "techcare", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If confirmation = DialogResult.Yes Then
                    If xamppRestoreFromLocationDialog.ShowDialog = DialogResult.OK Then
                        Try
                            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName & ";Uid=" &
                                                                                     My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")

                            dbConnection.Open()

                            Dim dbCommand As MySqlCommand = New MySqlCommand("DROP DATABASE techcare;", dbConnection)
                            dbCommand.ExecuteNonQuery()

                            dbConnection.Close()
                            dbConnection.Dispose()
                            dbCommand.Dispose()
                        Catch ex As Exception
                            MsgBox("Unable to drop previous database." & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
                        End Try

                        functions.rebuildDatabase()

                        Try
                            Dim dbConnection As MySqlConnection
                            functions.log("File found, restoring...")
                            dbConnection = New MySqlConnection("Server=" & My.Settings.dbLocation &     ' Setup connection to database as previously configured.
                                                           ";Database=" & My.Settings.dbName &
                                                           ";Uid=" & My.Settings.dbUsername &
                                                           ";Pwd=" & My.Settings.dbPassword & ";")

                            dbConnection.Open()

                            dbDumpLocation = xamppRestoreFromLocationDialog.FileName

                            Dim backup As New Process                                                   ' Setup a new Process object. This allows command-line executables
                            backup.StartInfo.FileName = "cmd.exe"                                       ' to be called by the program. In this case, we will send a command
                            backup.StartInfo.UseShellExecute = False                                    ' to the MySQL directory previously set up.
                            backup.StartInfo.WorkingDirectory = xamppLocation
                            backup.StartInfo.RedirectStandardInput = True                               ' RedirectStandardInput/Output allows the Process object to "take
                            backup.StartInfo.RedirectStandardOutput = True                              ' control" of the command prompt which is used.

                            backup.Start()                                                              ' Start the Process object (open command prompt)

                            Dim backupStream As System.IO.StreamWriter = backup.StandardInput           ' Setup the StreamWriter object (this is the command line input)
                            Dim myStreamReader As System.IO.StreamReader = backup.StandardOutput        ' Setup the StreamReader object (this is the command line output)

                            backupStream.WriteLine("mysql -u " & My.Settings.dbUsername &               ' Write this line to the command prompt and execute.
                                               " -p " & My.Settings.dbName & " < """ &
                                               dbDumpLocation & """")

                            backupStream.Close()                                                        ' Execute line written by the Stream Writer and wait for the
                            backup.WaitForExit()                                                        ' command to finish executing before closing.
                            backup.Close()

                            dbConnection.Close()                                                        ' Close connection to database.

                            MsgBox("Restore complete!" & vbNewLine & vbNewLine & "Techcare will now restart.", MsgBoxStyle.Information, "techcare")

                            Application.Restart()
                        Catch ex As Exception
                            MsgBox("An error has occured while restoring data from the database." &     ' Display error if there's an issue restoring from the file.
                               vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
                            functions.log("Restore failed. " & ex.InnerException.Message)
                        End Try
                    End If
                End If
            Else
                MsgBox("MySQL.exe was not found in the specified XAMPP directory. Please check that the XAMPP directory has been selected " &
                       "and try again.", MsgBoxStyle.Critical, "techcare")
            End If
        End If
    End Sub

    ' *******************************************************************************************************
    ' RESET SOFTWARE CODE
    ' *******************************************************************************************************

    Private Sub btnResetSoftware_Click(sender As Object, e As EventArgs) Handles btnResetSoftware.Click
        ' This procedure is called upon clicking the RESET SOFTWARE button.
        HideAllPanels()

        resetSoftwarePanel.Visible = True
        resetSoftwarePanel.Dock = DockStyle.Fill
        btnResetSoftware.BackColor = Color.FromArgb(32, 129, 197)
    End Sub

    Private Sub btnResetSoftwareAuthentication_Click(sender As Object, e As EventArgs) Handles btnResetSoftwareAuthentication.Click
        ' This procedure is called upon clicking the RESET (authenticate) button.
        ' To authenticate the user, a similar routine is used as in the login window. The authenticate function returns an employee number
        ' (or 0 if no employee was found). A confirmation then appears to confirm that all data will be removed and techcare will be reset. If YES
        ' is pressed, the DROP DATABASE command is executed, and all application local settings are reset to their default values. The program
        ' then restarts, and the user will be able to begin initial setup from there.

        Dim empID As Integer = functions.authenticate(tbResetSoftwareUsername.Text, tbResetSoftwarePassword.Text)

        If empID = 0 Then
            MsgBox("Incorrect username or password provided!", MsgBoxStyle.Critical, "techcare")
        Else
            If functions.obtainEmployeeDetails(empID, 4) = "Full" Then
                Dim confirmation As DialogResult

                confirmation = MessageBox.Show("Resetting techcare will remove ALL users, repairs, business details, and custom settings." & vbNewLine & vbNewLine &
                                               "This action CANNOT be undone. Do you wish to continue?", "techcare", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If confirmation = DialogResult.Yes Then
                    Dim accruedErrors As List(Of String) = New List(Of String)
                    Try
                        Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
                        Dim dbCommand As MySqlCommand = New MySqlCommand("DROP DATABASE techcare;", dbConnection)

                        dbConnection.Open()
                        dbCommand.ExecuteNonQuery()
                        dbConnection.Close()
                    Catch ex As Exception
                        accruedErrors.Add("MySQL FATAL ERROR: " & ex.Message)
                    End Try

                    Try
                        My.Settings.dbLocation = ""
                        My.Settings.dbName = ""
                        My.Settings.dbUsername = ""
                        My.Settings.dbPassword = ""
                        My.Settings.userFirstRun = True
                        My.Settings.userAskedForConsent = False
                        My.Settings.userHasConsented = False
                        My.Settings.Save()
                        My.Settings.Reload()
                    Catch ex As Exception
                        accruedErrors.Add("FATAL ERROR: " & ex.Message)
                    End Try

                    If accruedErrors.Count > 0 Then
                        MsgBox("TECHCARE RESET FAILED!" & vbNewLine & accruedErrors.ToString, MsgBoxStyle.Critical, "techcare")
                        Application.Restart()
                    Else
                        MsgBox("Reset completed! Techcare will now restart.", MsgBoxStyle.Information, "techcare")
                        Application.Restart()
                    End If
                End If
            Else
                MsgBox("Cannot reset techcare" & vbNewLine & vbNewLine & "Only users with Admin access are authorised to reset techcare.",
                       MsgBoxStyle.Critical, "techcare")
            End If
        End If
    End Sub

    ' *******************************************************************************************************
    ' ABOUT TECHCARE CODE
    ' *******************************************************************************************************

    Private Sub btnAboutTechcare_Click(sender As Object, e As EventArgs) Handles btnAboutTechcare.Click
        ' This procedure is called upon clicking the ABOUT TECHCARE button.
        HideAllPanels()

        aboutTechcarePanel.Visible = True
        aboutTechcarePanel.Dock = DockStyle.Fill
        btnAboutTechcare.BackColor = Color.FromArgb(32, 129, 197)

        lblVersionID.Text = "Version " & My.Application.Info.Version.ToString
    End Sub
End Class
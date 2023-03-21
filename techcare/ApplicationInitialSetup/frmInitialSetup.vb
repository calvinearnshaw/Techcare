Imports MySql.Data.MySqlClient
Public Class frmInitialSetup

    ' *******************************************************************************************************
    ' STEP 1
    ' *******************************************************************************************************

    Private Sub frmInitialSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' This procedure is called when the Initial Setup window is loading. In this case, it brings up the first page
        ' of the initial setup wizard.
        step1.Dock = DockStyle.Fill
        step1.Visible = True
    End Sub

    Private Sub btnStep1Next_Click(sender As Object, e As EventArgs) Handles btnStep1Next.Click
        ' This procedure is called when the NEXT button is pressed on Step 1.
        step1.Dock = DockStyle.None
        step1.Visible = False

        step2.Dock = DockStyle.Fill
        step2.Visible = True
    End Sub

    ' *******************************************************************************************************
    ' STEP 2
    ' *******************************************************************************************************

    Private Sub btnStep2Prev_Click(sender As Object, e As EventArgs) Handles btnStep2Prev.Click
        ' This procedure is called when the PREVIOUS button is pressed on Step 2.
        ' A small security feature (the removal of any entered text from the text boxes on Step 2) is also executed.
        step2.Dock = DockStyle.None
        step2.Visible = False
        tbStep2DbLocation.Clear()
        tbStep2DbPassword.Clear()
        tbStep2DbUsername.Clear()

        step1.Dock = DockStyle.Fill
        step1.Visible = True
    End Sub

    Private Sub btnStep2Next_Click(sender As Object, e As EventArgs) Handles btnStep2Next.Click
        ' This procedure is called when the NEXT button is pressed on Step 2.
        If tbStep2DbLocation.Text = "" Or tbStep2DbUsername.Text = "" Then
            MsgBox("One or more fields are missing information. Please check that all" &                ' Display error if DB Location or MySQL username haven't been
                   " fields are filled in and try again.", MsgBoxStyle.Exclamation, "techcare")         ' supplied.
        Else
            Dim connectionStatus As Boolean = False

            Try
                Dim dbConnection As MySqlConnection = New MySqlConnection(                              ' Check whether or not the details supplied by the user
                    "Server=" & tbStep2DbLocation.Text & ";Uid=" & tbStep2DbUsername.Text &             ' will allow the program to successfully login to the database
                    ";Pwd=" & tbStep2DbPassword.Text & ";")
                dbConnection.Open()                                                                     ' The procedure will throw a (handled) exception if it cannot
                connectionStatus = True                                                                 ' connect, otherwise, we'll set the connectionStatus to TRUE.
                functions.log("Connection to server successful...")
                dbConnection.Close()
            Catch ex As Exception                                                                       ' Exception is caught using the try/catch statement. We keep
                connectionStatus = False                                                                ' the connectionStatus variable set to FALSE in this case.
                functions.log("Connection to server failed...aborting")
            End Try

            If connectionStatus = True Then                                                             ' Execute the following instructions IF connection was achieved.
                If functions.databaseCheck("techcare") = True Then                                      ' If a database named TECHCARE exists on the server, then
                    functions.log("Techcare db found, must obtain confirmation from user...")
                    Dim confirmation As DialogResult
                    confirmation = MessageBox.Show("Techcare has detected another database named " &    ' Message box displayed warning the user of the existing database,
                        "'techcare' on your MySQL Server." & vbNewLine & "In order for techcare " &     ' and that any data on said database will be removed before
                        "to complete setup, this database must be removed. You will be given the " &    ' continuing. User must click YES to continue.
                        "option to restore from an existing backup in the next step. Do you wish " &
                        "to continue?", "techcare", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If confirmation = DialogResult.Yes Then                                             ' If the user pressed YES, then execute the following instructions:
                        functions.log("User confirmed removal of old techcare db, removing...")
                        Try
                            Dim dbConnection As MySqlConnection = New MySqlConnection(                  ' Connect to MySQL server, and execute the DROP DATABASE command.
                                "Server=" & tbStep2DbLocation.Text & ";Uid=" & tbStep2DbUsername.Text _ ' This removes all structures and data associated with the =
                                & ";Pwd=" & tbStep2DbPassword.Text & ";")                               ' old techcare database.
                            Dim dbCommand As MySqlCommand = New MySqlCommand("DROP DATABASE techcare;",
                                                                             dbConnection)

                            dbConnection.Open()
                            dbCommand.ExecuteNonQuery()                                                 ' ExecuteNonQuery used for everything EXCEPT reading back data.
                            dbConnection.Close()

                            My.Settings.dbLocation = tbStep2DbLocation.Text                             ' Everything was successful at this point, so update the Application's
                            My.Settings.dbUsername = tbStep2DbUsername.Text                             ' local settings with the username, password, and location of the
                            My.Settings.dbPassword = tbStep2DbPassword.Text                             ' database.

                            step2.Dock = DockStyle.None                                                 ' We can now move on to step 3. The following code hides the panel
                            step2.Visible = False                                                       ' containing Step 2, and displays the step 3 panel.
                            tbStep2DbLocation.Clear()
                            tbStep2DbPassword.Clear()
                            tbStep2DbUsername.Clear()

                            step3.Dock = DockStyle.Fill
                            step3.Visible = True
                            functions.log("Techcare Drop successful, moving onto step 3...")
                        Catch ex As Exception
                            MsgBox("Setup cannot continue due to an error communicating with the MySQL" &   ' Error displayed if there's an issue connecting to the server.
                                   " Server." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical,
                                                                                    "techcare")
                            functions.log("FATAL ERROR - Cannot continue with setup, program aborting... " & ex.InnerException.Message)
                            Application.Exit()
                        End Try
                    Else
                        functions.log("User rejected deletion of old DB, aborting...")
                        MsgBox("Setup cannot continue.", MsgBoxStyle.Exclamation, "techcare")
                        Application.Exit()
                    End If
                Else
                    My.Settings.dbLocation = tbStep2DbLocation.Text                                     ' This code block is executed if the program cannot find another
                    My.Settings.dbUsername = tbStep2DbUsername.Text                                     ' database named "techcare". I.e. we have a clean database. Just
                    My.Settings.dbPassword = tbStep2DbPassword.Text                                     ' update the application's local settings and move on to step 3.

                    step2.Dock = DockStyle.None
                    step2.Visible = False
                    tbStep2DbLocation.Clear()
                    tbStep2DbPassword.Clear()
                    tbStep2DbUsername.Clear()

                    step3.Dock = DockStyle.Fill
                    step3.Visible = True

                    functions.log("No previous DB found, moving onto step 3...")
                End If
            Else
                MsgBox("Unable to connect to MySQL Server using details provided." & vbNewLine & vbNewLine &                ' Error displayed when there's an issue
                       "Check that:" & vbNewLine & "A) The server is running and the connection details are correct." &     ' connecting to the server.
                       vbNewLine & "B) The username and password provided are registered with your MySQL Server." &
                       vbNewLine & "C) The username and password used are able to read and write to your MySQL Server.",
                       MsgBoxStyle.Exclamation, "techcare")
                functions.log("Connection to DB failed.")
            End If
        End If
    End Sub

    ' *******************************************************************************************************
    ' STEP 3
    ' *******************************************************************************************************

    Private Sub btnStep3Previous_Click(sender As Object, e As EventArgs) Handles btnStep3Previous.Click
        ' This procedure is called when the PREVIOUS button is pressed on step 3.
        step3.Dock = DockStyle.None
        step3.Visible = False

        step2.Dock = DockStyle.Fill
        step2.Visible = True
    End Sub

    Private Sub btnStep3RestoreFromBackup_Click(sender As Object, e As EventArgs) Handles btnStep3RestoreFromBackup.Click
        ' This procedure is called when the RESTORE FROM BACKUP button is pressed on step 3.

        If functions.databaseCheck("techcare") = False Then                                         ' Check if the database exists. If not, then call the
            functions.log("Rebuilding database...")
            functions.rebuildDatabase()                                                             ' database rebuild procedure. This check occurs because
        End If                                                                                      ' users may potentially come back to this step.

        functions.log("Beginning restore from backup...")

        My.Settings.dbName = "techcare"                                                             ' Set the databaseName in local application settings to
        My.Settings.Save()                                                                          ' techcare, then save. (This name was originally modifiable,
        My.Settings.Reload()                                                                        ' but that has since been removed due to issues with restore).

        Dim xamppLocation As String = ""                                                            ' Initialise variables required for restore process to take place
        Dim dbDumpLocation As String = ""

        xamppLocationDialog.Description = "Select XAMPP Server folder."                             ' Setup a Folder Browser Dialog to allow the user to locate their
        '                                                                                           ' XAMPP installation.
        If xamppLocationDialog.ShowDialog = DialogResult.OK Then                                    ' Set the XAMPP location variable to the location of the XAMPP
            xamppLocation = xamppLocationDialog.SelectedPath & "\mysql\bin\"                        ' installation, followed by the location of MYSQL within XAMPP.

            functions.log("Attempting to find mysqldump.exe")

            If System.IO.File.Exists(xamppLocation & "mysqldump.exe") = True Then                   ' Check if the MYSQLDUMP program exists. If it does, then display
                If xamppRestoreFromLocationDialog.ShowDialog = DialogResult.OK Then                 ' the Open File Dialog, and prompt user to look for the backup
                    Try                                                                             ' to restore from.
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

                        step3.Dock = DockStyle.None                                                 ' Hide step 3 panel, and move onto step 4a (setup via restore
                        step3.Visible = False                                                       ' complete).

                        step4a.Dock = DockStyle.Fill
                        step4a.Visible = True

                        functions.log("Restore successful, finishing...")
                    Catch ex As Exception
                        MsgBox("An error has occured while restoring data from the database." &     ' Display error if there's an issue restoring from the file.
                               vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
                        functions.log("Restore failed. " & ex.InnerException.Message)
                    End Try
                End If
            End If
        Else
            MsgBox("MySQL.exe was Not found in the specified XAMPP directory. Please check" &       ' Display error if the program cannot find the MYSQLDUMP files
                   " that the XAMPP directory has been selected " &                                 ' within the supplied directory from the user.
                   "and try again.", MsgBoxStyle.Critical, "techcare")
            functions.log("MySQL not found in directory specified by user.")
        End If
    End Sub

    Private Sub btnStep3StartFromScratch_Click(sender As Object, e As EventArgs) Handles btnStep3StartFromScratch.Click
        ' This procedure is called when the START FROM SCRATCH button is pressed on Step 3.
        If functions.databaseCheck("techcare") = False Then                                         ' Check if the database exists. If not, then call the
            functions.log("Database rebuilding...")
            functions.rebuildDatabase()                                                             ' database rebuild procedure. This check occurs because
        End If                                                                                      ' users may potentially come back to this step.

        My.Settings.dbName = "techcare"                                                             ' Set the databaseName in local application settings to
        My.Settings.Save()                                                                          ' techcare, then save. (This name was originally modifiable,
        My.Settings.Reload()                                                                        ' but that has since been removed due to issues with restore).

        step3.Dock = DockStyle.None                                                                 ' Hide step 3 panel, and move onto step 4b (setup admin account).
        step3.Visible = False

        step4b.Dock = DockStyle.Fill
        step4b.Visible = True

        functions.log("Moving on to step 4...")
    End Sub

    ' *******************************************************************************************************
    ' STEP 4A (RESTORE COMPLETE)
    ' *******************************************************************************************************

    Private Sub btnStep4aFinish_Click(sender As Object, e As EventArgs) Handles btnStep4aFinish.Click
        ' This procedure is called when the FINISH button is pressed on step 4a. This updates the application local settings to confirm
        ' that the setup has completed and the initial setup window no longer needs to show. Once saved, the application will restart.

        step4a.Dock = DockStyle.None
        step4a.Visible = False
        My.Settings.userFirstRun = False
        My.Settings.Save()
        My.Settings.Reload()

        functions.log("** Program Restarting **")

        Application.Restart()
    End Sub

    ' *******************************************************************************************************
    ' STEP 4B (STARTING FROM SCRATCH)
    ' *******************************************************************************************************

    Private Sub btnStep4BPrevious_Click(sender As Object, e As EventArgs) Handles btnStep4BPrevious.Click
        ' This procedure is called when the PREVIOUS button is pressed on step 4b. The step 4b panel is hidden, and step 3 panel is shown.
        ' For an added layer of security, any details entered on step 4 are cleared.
        step4b.Dock = DockStyle.None
        step4b.Visible = False
        tbStep4BTitle.Clear()
        tbStep4BFname.Clear()
        tbStep4BSurname.Clear()
        tbStep4BPassword.Clear()
        tbStep4BConfirmPassword.Clear()

        step3.Visible = True
        step3.Dock = DockStyle.Fill
    End Sub

    Private Sub btnStep4BNext_Click(sender As Object, e As EventArgs) Handles btnStep4BNext.Click
        ' This procedure is called when the NEXT button is pressed on step 4b.
        If tbStep4BTitle.Text = "" Or tbStep4BFname.Text = "" Or tbStep4BSurname.Text = "" Or tbStep4BPassword.Text = "" Or tbStep4BConfirmPassword.Text = "" Then
            MsgBox("One or more fields are missing information. Please check that" &                                                ' Check if there are any missing fields.
                   " all fields have been filled in and try again.", MsgBoxStyle.Exclamation, "techcare")                           ' If there are, display an error.
            tbStep4BPassword.Clear()                                                                                                ' Clear password / confirm password fields.
            tbStep4BConfirmPassword.Clear()
        Else
            If tbStep4BPassword.Text = tbStep4BConfirmPassword.Text Then                                                            ' Check if the password entered matches what was entered
                If functions.validateNewPassword(tbStep4BPassword.Text) = True Then                                                 ' on the confirm password field. If so, then check the
                    Try                                                                                                             ' password is strong enough. If yes, generate a username
                        Dim username As String = functions.generateUsername(tbStep4BFname.Text,                                     ' using the generateUsername function, and the supplied
                                                                            tbStep4BSurname.Text)                                   ' username and password.

                        If username Is Nothing Then                                                                                 ' If the generated username variable has nothing in it,
                            Throw New System.Exception("Unable to generate user account." &                                         ' terminate the process. This is VERY unlikely to happen.
                                                       " Process terminated.")
                        Else
                            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation &          ' Setup a connection to the database, using the details
                                                                                      ";Database=" & My.Settings.dbName &           ' previously set by the user.
                                                                                      ";Uid=" & My.Settings.dbUsername &
                                                                                      ";Pwd=" & My.Settings.dbPassword & ";")
                            Dim dbCommand As MySqlCommand = New MySqlCommand("INSERT INTO Employees (employeeID, title, " &         ' Setup an SQL query which inserts a new record into the
                                                                             "forename, surname, userAccessLevel, username," &      ' Employees database. We setup parameters using the @
                                                                             " password) VALUES (@empID, @empTitle, @fname," &      ' symbol. This prevents SQL injection attacks!
                                                                             "@sname, @ual, @username, @password);", dbConnection)

                            dbConnection.Open()

                            dbCommand.Parameters.AddWithValue("@empID", functions.generateUid("Employees", "employeeID", 7))        ' Give values for each of the parameters set out above.
                            dbCommand.Parameters.AddWithValue("@empTitle", tbStep4BTitle.Text)                                      ' empID is a 7-digit number generated by the generateUid
                            dbCommand.Parameters.AddWithValue("@fname", tbStep4BFname.Text)                                         ' function I have written in the functions class.
                            dbCommand.Parameters.AddWithValue("@sname", tbStep4BSurname.Text)
                            dbCommand.Parameters.AddWithValue("@ual", "Full")                                                       ' The user access level is set to Full by default for the
                            dbCommand.Parameters.AddWithValue("@username", username)                                                ' first account created.
                            dbCommand.Parameters.AddWithValue("@password", tbStep4BPassword.Text)

                            dbCommand.ExecuteNonQuery()                                                                             ' Execute the above query.

                            dbConnection.Close()
                            dbCommand.Dispose()
                            dbConnection.Dispose()

                            MsgBox("User Account Created!" & vbNewLine & vbNewLine & "The employee's username is: " & username &    ' Message which appears after a successful account
                                   ". Please write this down for future reference.", MsgBoxStyle.Information, "techcare")           ' creation, containing the user's username.

                            step4b.Dock = DockStyle.None                                                                            ' Hide step 4b panels, and clear all fields from the
                            step4b.Visible = False                                                                                  ' panel. Display step 5.
                            tbStep4BTitle.Clear()
                            tbStep4BFname.Clear()
                            tbStep4BSurname.Clear()
                            tbStep4BPassword.Clear()
                            tbStep4BConfirmPassword.Clear()

                            step5.Visible = True
                            step5.Dock = DockStyle.Fill
                        End If
                    Catch ex As Exception
                        MsgBox("An error has occured whilst creating the new user account." & vbNewLine & vbNewLine &               ' Error displayed if there's an exception thrown when
                               ex.Message, MsgBoxStyle.Critical, "techcare")                                                        ' creating the user account.
                        tbStep4BPassword.Clear()                                                                                    ' For security, the password field(s) are cleared.
                        tbStep4BConfirmPassword.Clear()
                    End Try
                Else
                    MsgBox("The password entered does not meet password strength requirements. Please try again.",                  ' Error displayed if the password does not meet the
                           MsgBoxStyle.Exclamation, "techcare")                                                                     ' password strength requirements.
                    tbStep4BConfirmPassword.Clear()                                                                                 ' Again, password field(s) are cleared for security.
                    tbStep4BPassword.Clear()
                End If
            Else
                MsgBox("The passwords entered do not match. Please try again.", MsgBoxStyle.Exclamation, "techcare")                ' Error displayed if the passwords entered do not
                tbStep4BPassword.Clear()                                                                                            ' match.
                tbStep4BConfirmPassword.Clear()                                                                                     ' Password field(s) cleared for security.
            End If
        End If
    End Sub

    ' *******************************************************************************************************
    ' STEP 5
    ' *******************************************************************************************************

    Private Sub btnStep5Next_Click(sender As Object, e As EventArgs) Handles btnStep5Next.Click
        ' This procedure is called when the NEXT button is pressed on step 5.
        If tbStep5BusName.Text = "" Or tbStep5BusAddress.Text = "" Or tbStep5BusContact.Text = "" Then
            MsgBox("One or more fields are missing information. Please check that all fields have been filled in and try again.",   ' Check if the business details have been entered. If
                   MsgBoxStyle.Exclamation, "techcare")                                                                             ' not, prompt the user to ensure all details are given.
        Else
            Try
                Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation &                      ' Setup connection to MySQL Server.
                                                                          ";Database=" & My.Settings.dbName &
                                                                          ";Uid=" & My.Settings.dbUsername &
                                                                          ";Pwd=" & My.Settings.dbPassword & ";")
                Dim dbCommand As MySqlCommand = New MySqlCommand("INSERT INTO Business (name, address, phoneNumber, repairTnC)" &   ' Setup query to insert new business information into
                                                                 "VALUES (@busName, @busAddr, @busPhn, @busRepairTNC);",            ' the database.
                                                                 dbConnection)

                dbConnection.Open()

                dbCommand.Parameters.AddWithValue("@busName", tbStep5BusName.Text)                                                  ' Use parameterised queries to avoid SQL injection
                dbCommand.Parameters.AddWithValue("@busAddr", tbStep5BusAddress.Text)                                               ' attacks.
                dbCommand.Parameters.AddWithValue("@busPhn", tbStep5BusContact.Text)
                dbCommand.Parameters.AddWithValue("@busRepairTNC", tbStep5BusRepairTnC.Text)

                dbCommand.ExecuteNonQuery()

                dbConnection.Close()
                dbCommand.Dispose()
                dbConnection.Dispose()

                step5.Visible = False                                                                                               ' Hide step 5 panel and display step 6.
                step5.Dock = DockStyle.None                                                                                         ' Clear all input fields on step 5.
                tbStep5BusName.Clear()
                tbStep5BusAddress.Clear()
                tbStep5BusContact.Clear()
                tbStep5BusRepairTnC.Clear()

                step6.Visible = True
                step6.Dock = DockStyle.Fill
            Catch ex As Exception
                MsgBox("Cannot add business details to techcare database." & vbNewLine & vbNewLine & ex.Message,                    ' Exception handling - error shown if the program
                       MsgBoxStyle.Critical, "techcare")                                                                            ' cannot add the business details.
            End Try
        End If
    End Sub

    Private Sub tbStep5BusContact_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbStep5BusContact.KeyPress
        ' This procedure is called whenever input is given to the contact number field on step 5. This checks if the input given
        ' is a number, and if so, allows the number to be inserted into the textbox. If it's not, then do nothing.
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    ' *******************************************************************************************************
    ' STEP 6
    ' *******************************************************************************************************

    Private Sub btnStep6Finish_Click(sender As Object, e As EventArgs) Handles btnStep6Finish.Click
        ' This procedure is called when the FINISH button is pressed on step 6. This updates the application local settings to confirm
        ' that the setup has completed and the initial setup window no longer needs to show. Once saved, the application will restart.
        step6.Dock = DockStyle.None
        step6.Visible = False
        My.Settings.userFirstRun = False
        My.Settings.Save()
        My.Settings.Reload()

        Application.Restart()
    End Sub
End Class
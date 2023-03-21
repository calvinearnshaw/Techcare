Imports MySql.Data.MySqlClient
Public Class frmRepairMgmt

    Private Sub frmRepairMgmt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' This sequence of code is called upon loading the repairs management window.
        ' It clears off any remaining row(s) on the comments table and search results grid.
        ' It then overlaps the repair details panel with a label prompting the user to select a
        ' repair to view.
        dgvRepairRemarks.Rows.Clear()
        dgvRepairSearchResults.Rows.Clear()
        lblUserPrompt.Show()
        lblUserPrompt.Dock = DockStyle.Fill
    End Sub

    Private Sub btnSearchRepairs_Click(sender As Object, e As EventArgs) Handles btnSearchRepairs.Click
        ' This procedure is called when the Search button is pressed. It first checks that the user only intends to search
        ' either by repair reference (which uses binary search), or surname (which uses SQL's search functionality).

        dgvRepairSearchResults.Rows.Clear()                                                                                     ' Removes all rows from previous search (if previous search was carried out!)

        If tbRepairRef.TextLength > 0 And tbSurname.TextLength > 0 Then                                                         ' This check allows the program to choose whether to use the binary search algorithm
            MsgBox("An error occured whilst searching repairs." & vbNewLine & vbNewLine &                                       ' (which can only return exact values), or the SQL search query (which makes use of
                   "You can only search by either surname or repair reference.", MsgBoxStyle.Exclamation, "techcare")           ' wildcards).
        Else
            If tbSurname.TextLength > 0 Then
                ' Search repairs list by surname. The query makes use of the SQL % Wildcard, which means that the user can
                ' enter part of a customer's surname to search the database. For example, typing "Sm" into surname box would
                ' return repairs under surnames Smith, Smart, etc.
                Try
                    Dim dbConnection As MySqlConnection = New MySqlConnection("Server=localhost;Database=techcare;Uid=techcare;Pwd=techcare;")
                    Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT repairReference, surname, assetMake, assetModel, currentRepairStatus FROM Repairs" &
                                                                     " WHERE surname Like @surname;", dbConnection)

                    dbCommand.Parameters.AddWithValue("@surname", tbSurname.Text & "%")

                    dbConnection.Open()

                    Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader

                    If dbReader.HasRows Then
                        While dbReader.Read
                            dgvRepairSearchResults.Rows.Add(New String() {dbReader(0).ToString, dbReader(1).ToString, dbReader(2).ToString & " " & dbReader(3).ToString,
                                dbReader(4).ToString})
                        End While
                    Else
                        MsgBox("No results found. Try refining your search results.", MsgBoxStyle.Exclamation, "techcare")
                    End If

                    dbConnection.Close()
                    dbCommand.Dispose()
                    dbConnection.Dispose()
                Catch ex As Exception
                    MsgBox("An error has occured while searching for repairs on the techcare database." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
                End Try

            ElseIf tbRepairRef.TextLength > 0 Then
                Try
                    ' This uses the binary search algorithm. First, a list of repair references are added to a list. An SQL
                    ' query is used to return all repair references currently on the system. To speed up this search I have
                    ' set the query to sort the results automatically. However, you could potentially incorporate a sort of
                    ' some description here too!

                    Dim repairRefs As List(Of String) = New List(Of String)

                    Try
                        Dim dbConnection As MySqlConnection = New MySqlConnection("Server=localhost;Database=techcare;Uid=techcare;Pwd=techcare;")
                        Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT repairReference FROM Repairs ORDER BY repairReference ASC;", dbConnection)

                        dbConnection.Open()

                        Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader

                        If dbReader.HasRows Then
                            While dbReader.Read
                                repairRefs.Add(dbReader(0).ToString)
                            End While
                        End If

                        dbConnection.Close()
                        dbConnection.Dispose()
                        dbCommand.Dispose()
                    Catch ex As Exception
                        MsgBox("An error has occured while searching for repairs on the techcare database." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
                    End Try

                    If repairRefs.Count > 0 Then                            ' This is the start of the binary search algorithm! We begin by defining 4 different variables, as below:
                        Dim minimum As Integer = 0                          ' Lowest possible position which the repair ref can be found is 0. We refer to this as the 'minimum' value.
                        Dim maximum As Integer = repairRefs.Count           ' Highest possible position which the repair ref can be found is the length of the repairRefs list.
                        Dim guess As Integer                                ' This variable is used as the position of the array by which the program initially guesses the target ref is located.
                        Dim position As Integer = -1                        ' This variable is updated as soon as the target has been found, with the position of the target value in the list.

                        While repairRefs.Count >= minimum
                            guess = Math.Floor((maximum + minimum) / 2)                 ' Floor division (round to nearest whole number less than or equal to value of guess).
                            If repairRefs(guess).ToString = tbRepairRef.Text Then       ' Check if value at position GUESS in the repairRefs list is equal to the target value.
                                position = guess                                        ' If yes, that is the position by which the target value is located. Exit conditional loop.
                                Exit While
                            ElseIf Convert.ToInt32(repairRefs(guess).ToString) > Convert.ToInt32(tbRepairRef.Text) Then
                                maximum = guess - 1                                     ' If the value at position GUESS is greater than the target value, then the position by which the target
                            Else                                                        ' value is located must be less than the guess position. Therefore, the max value it must be is the guess
                                minimum = guess + 1                                     ' position - 1. If this isn't the case, the position by which the target value is located must be greater
                            End If                                                      ' than the guess positon. In this case, the minimum is set to the guessed position + 1.
                        End While

                        If position = -1 Then
                            MsgBox("No results found. Try refining your search results.", MsgBoxStyle.Exclamation, "techcare")
                        Else
                            Try
                                Dim dbConnection As MySqlConnection = New MySqlConnection("Server=localhost;Database=techcare;Uid=techcare;Pwd=techcare;")
                                Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT repairReference, surname, assetMake, assetModel, currentRepairStatus FROM Repairs " &
                                                                                 "WHERE repairReference = @repairRef;", dbConnection)

                                dbCommand.Parameters.AddWithValue("@repairRef", repairRefs(position).ToString)

                                dbConnection.Open()

                                Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader

                                If dbReader.HasRows Then
                                    While dbReader.Read
                                        dgvRepairSearchResults.Rows.Add(New String() {dbReader(0).ToString, dbReader(1).ToString, dbReader(2).ToString & " " &
                                                                        dbReader(3).ToString, dbReader(4).ToString})
                                    End While
                                End If

                                dbConnection.Close()
                                dbConnection.Dispose()
                                dbCommand.Dispose()
                            Catch ex As Exception
                                MsgBox("An error has occured while searching for repairs on the techcare database." & vbNewLine & vbNewLine & ex.Message,
                                       MsgBoxStyle.Critical, "techcare")
                            End Try
                        End If
                    Else
                        MsgBox("No results found. Try refining your search results.", MsgBoxStyle.Exclamation, "techcare")
                    End If
                Catch ex As Exception
                    MsgBox("No results found. Try refining your search results.", MsgBoxStyle.Exclamation, "techcare")
                End Try
            Else
                ' Where search criteria have not been entered, the program will assume that the user is looking to see all current repairs.
                Try
                    Dim dbConnection As MySqlConnection = New MySqlConnection("Server=localhost;Database=techcare;Uid=techcare;Pwd=techcare;")
                    Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT repairReference, surname, assetMake, assetModel, currentRepairStatus FROM Repairs;", dbConnection)

                    dbConnection.Open()

                    Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader

                    If dbReader.HasRows Then
                        While dbReader.Read
                            dgvRepairSearchResults.Rows.Add(New String() {dbReader(0).ToString, dbReader(1).ToString, dbReader(2).ToString & " " & dbReader(3).ToString,
                                                            dbReader(4).ToString})
                        End While
                    Else
                        MsgBox("No repairs exist on the system.", MsgBoxStyle.Exclamation, "techcare")
                    End If

                    dbConnection.Close()
                    dbCommand.Dispose()
                    dbConnection.Dispose()
                Catch ex As Exception
                    MsgBox("An error has occured while searching for repairs on the techcare database." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
                End Try
            End If
        End If
    End Sub

    Private Sub dgvRepairSearchResults_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRepairSearchResults.CellClick
        ' This procedure is called when a row is selected from the repair search results. This will pull up all repair information that is stored.
        ' First, the program checks if the number of selected rows is equal to 1. This is because the datagridview tables we use in VB.net allow for
        ' more than 1 row to be selected at a time. This prevents an exception from occurring.

        If dgvRepairSearchResults.SelectedRows.Count = 1 Then
            Try
                Dim dbConnection As MySqlConnection = New MySqlConnection("Server=localhost;Database=techcare;Uid=techcare;Pwd=techcare;")
                Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT * FROM Repairs WHERE repairReference = @repairRef;", dbConnection)

                dbCommand.Parameters.AddWithValue("@repairRef", dgvRepairSearchResults.SelectedRows(0).Cells(0).Value.ToString)

                dbConnection.Open()                                             ' We search the database for the repair which corresponds with
                Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader       ' the repair reference in the datagridview, and return its record.

                If dbReader.HasRows Then
                    While dbReader.Read                                         ' This section of code populates the various labels in the Repair Details view
                        lblRepairRef.Text = dbReader(0).ToString                ' with the information pulled from the database query above.
                        lblCustomerNameAddress.Text = dbReader(1).ToString & " " & dbReader(2).ToString & " " & dbReader(3).ToString & vbNewLine &
                            dbReader(4).ToString & vbNewLine & dbReader(5).ToString & vbNewLine & dbReader(6).ToString
                        lblHomePhn.Text = dbReader(7).ToString
                        lblMobilePhn.Text = dbReader(8).ToString
                        lblEmailAddress.Text = dbReader(9).ToString
                        lblAssetDetails.Text = dbReader(10).ToString & vbNewLine & dbReader(11).ToString & vbNewLine & dbReader(12).ToString
                        lblCurrentRepairStatus.Text = dbReader(13).ToString
                        lblIntakeDate.Text = dbReader(14).ToString
                        lblFaultDesc.Text = dbReader(15).ToString
                        lblQuote.Text = dbReader(16).ToString
                    End While
                End If

                If lblCurrentRepairStatus.Text = "Collected" Or lblCurrentRepairStatus.Text = "Asset Removed" Then
                    btnAddRepairRemark.Enabled = False                          ' This is an addition to the program which was not included in the design
                    btnUpdateRepairQuote.Enabled = False                        ' stage. If the user previously marked a repair job as Removed or Collected, the user
                    btnUpdateRepairStatus.Enabled = False                       ' can no longer edit any details. Therefore, all buttons which could allow the current
                    btnCustomerCollection.Enabled = False                       ' repair details to be changed are simply greyed out.
                Else
                    btnAddRepairRemark.Enabled = True
                    btnUpdateRepairQuote.Enabled = True
                    btnUpdateRepairStatus.Enabled = True
                    btnCustomerCollection.Enabled = True
                End If

                dbConnection.Close()
                dbCommand.Dispose()
                dbConnection.Dispose()
            Catch ex As Exception
                MsgBox("An error has occured while searching for repairs on the techcare database." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
            End Try

            Try
                ' Since the repair remarks are stored in a separate table, we need to use a separate try/catch statement to retrieve these details. We simply
                ' return all records in the Remarks table where the job reference is equal to the job reference we're looking for.

                dgvRepairRemarks.Rows.Clear()

                Dim dbConnection As MySqlConnection = New MySqlConnection("Server=localhost;Database=techcare;Uid=techcare;Pwd=techcare;")
                Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT * FROM Remarks WHERE jobReference = @repairRef ORDER BY timestamp ASC;", dbConnection)

                dbCommand.Parameters.AddWithValue("@repairRef", dgvRepairSearchResults.SelectedRows(0).Cells(0).Value.ToString)

                dbConnection.Open()

                Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader

                If dbReader.HasRows Then
                    While dbReader.Read
                        dgvRepairRemarks.Rows.Add(New String() {functions.obtainEmployeeDetails(dbReader(1).ToString, 2) & " " &
                                                  functions.obtainEmployeeDetails(dbReader(1).ToString, 3), dbReader(3).ToString, dbReader(4).ToString})
                    End While
                End If

                dbConnection.Close()
                dbCommand.Dispose()
                dbConnection.Dispose()
            Catch ex As Exception
                MsgBox("An error has occured while searching for the selected repair's remarks on the techcare database." & vbNewLine & vbNewLine & ex.Message,
                       MsgBoxStyle.Critical, "techcare")
            End Try

            lblUserPrompt.Hide()                ' The User Prompt label serves as an overlay to all the repair detail labels on the Repair Management window.
            lblUserPrompt.Dock = DockStyle.None ' This stops the end-user from seeing repair detail placeholder text, which makes for a cleaner appearance at runtime.
        End If
    End Sub

    Private Sub btnUpdateRepairStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateRepairStatus.Click
        ' This procedure is called when the Update Repair Status button is pressed. It simply gets the Update Repair Status dialog to show.
        ' "ShowDialog" is used instead of "Show". This stops the user from focusing on another window whilst the Update Repair Status window is open!
        If lblCurrentRepairStatus.Text = "Booked In" Then
            frmUpdateRepairStatus.rbBookedIn.Checked = True
        ElseIf lblCurrentRepairStatus.Text = "Service in Progress" Then
            frmUpdateRepairStatus.rbServiceInProgress.Checked = True
        ElseIf lblCurrentRepairStatus.Text = "Transferred to External Body" Then
            frmUpdateRepairStatus.rbTransferred.Checked = True
        ElseIf lblCurrentRepairStatus.Text = "Service Completed" Then
            frmUpdateRepairStatus.rbServiceCompleted.Checked = True
        End If
        frmUpdateRepairStatus.ShowDialog()
    End Sub

    Private Sub btnUpdateRepairQuote_Click(sender As Object, e As EventArgs) Handles btnUpdateRepairQuote.Click
        ' This procedure is called when the Update Repair Quote button is pressed.
        frmUpdateQuote.tbNewRepairQuote.Text = lblQuote.Text
        frmUpdateQuote.ShowDialog()
    End Sub

    Private Sub btnAddRepairRemark_Click(sender As Object, e As EventArgs) Handles btnAddRepairRemark.Click
        ' This procedure is called when the Add Repair Remark button is pressed.
        frmAddRepairRemark.ShowDialog()
    End Sub

    Private Sub btnCustomerCollection_Click(sender As Object, e As EventArgs) Handles btnCustomerCollection.Click
        ' This procedure is called when the Customer Collection button is pressed.
        ' The procedure first confirms that the user wishes to book out the repair. If the user presses YES on the dialog box, the collection process begins.
        ' This is simply an SQL query which changes the repair status to COLLECTED. If the user wishes to view a collected repair, they'll notice that all action
        ' buttons are greyed out. This is because users cannot edit repairs which have previously been booked out, or marked as removed.

        Dim confirmCollection As DialogResult
        confirmCollection = MessageBox.Show("Customer Collection:" & vbNewLine & vbNewLine & "Has/is the customer:" & vbNewLine & "1) Paid for any part(s) or labour?" &
                                  vbNewLine & "2) Received their product and is happy with the condition?" & vbNewLine & "3) Satisfied that the repair is complete?" &
                                  vbNewLine & vbNewLine & "Click YES to confirm collection, or click NO to stop collection.",
                                  "techcare", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmCollection = DialogResult.Yes Then
            Try
                Dim dbConnection As MySqlConnection = New MySqlConnection("Server=localhost;Database=techcare;Uid=techcare;Pwd=techcare;")
                Dim dbCommand As MySqlCommand = New MySqlCommand("UPDATE Repairs SET currentRepairStatus='Collected' WHERE repairReference=@repairRef;", dbConnection)

                dbCommand.Parameters.AddWithValue("@repairRef", lblRepairRef.Text)

                dbConnection.Open()

                dbCommand.ExecuteNonQuery()

                dbConnection.Close()
                dbCommand.Dispose()
                dbConnection.Dispose()

                MsgBox("Repair marked as collected.", MsgBoxStyle.Information, "techcare")

                lblUserPrompt.Dock = DockStyle.Fill
                lblUserPrompt.Visible = True
            Catch ex As Exception
                MsgBox("An error has occured while completing the customer collection for this repair." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
            End Try
        End If
    End Sub

    Private Sub btnNewRepair_Click(sender As Object, e As EventArgs) Handles btnNewRepair.Click
        frmCreateNewRepair.MdiParent = frmMainWindow
        frmCreateNewRepair.Show()
    End Sub
End Class
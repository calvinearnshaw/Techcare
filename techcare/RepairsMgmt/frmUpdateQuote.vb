Imports MySql.Data.MySqlClient

Public Class frmUpdateQuote

    Private Sub tbNewRepairQuote_Leave(sender As Object, e As EventArgs) Handles tbNewRepairQuote.Leave
        ' This procedure is called anytime the user's cursor comes away from the quotation text box.
        ' In this instance we check if the user has entered a valid amount, and warn the user when this is not
        ' the case.
        Try
            tbNewRepairQuote.Text = CDec(tbNewRepairQuote.Text).ToString("c")       ' This line converts the value of the textbox into a currency.
        Catch ex As Exception
            tbNewRepairQuote.Clear()
            MsgBox("Invalid quotation entered. Please try again.", MsgBoxStyle.Exclamation, "techcare")
        End Try
    End Sub

    Private Sub btnConfirmChanges_Click(sender As Object, e As EventArgs) Handles btnConfirmChanges.Click
        ' This procedure is called when the user clicks the 'Confirm' button. The above procedure will call itself
        ' automatically due to the user losing focus on the textbox. This procedure simply updates the database with the
        ' new quotation as provided by the user.
        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=localhost;Database=techcare;Uid=techcare;Pwd=techcare;")
            Dim dbCommand As MySqlCommand = New MySqlCommand("UPDATE Repairs SET estimateQuote=@quote WHERE repairReference=@repairRef;", dbConnection)

            dbCommand.Parameters.AddWithValue("@quote", tbNewRepairQuote.Text)
            dbCommand.Parameters.AddWithValue("@repairRef", frmRepairMgmt.lblRepairRef.Text)

            dbConnection.Open()

            dbCommand.ExecuteNonQuery()

            dbConnection.Close()

            frmRepairMgmt.lblQuote.Text = tbNewRepairQuote.Text

            MsgBox("Quotation updated successfully.", MsgBoxStyle.Information, "techcare")

            dbConnection.Close()
            dbConnection.Dispose()
            dbCommand.Dispose()

            Me.Close()
        Catch ex As Exception
            MsgBox("An error occured while updating the repair quotation." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
            Me.Close()
        End Try
    End Sub

    Private Sub frmUpdateQuote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbNewRepairQuote.Text = frmRepairMgmt.lblQuote.Text
    End Sub
End Class
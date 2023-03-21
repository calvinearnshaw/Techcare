Public Class frmLogsConsent

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        My.Settings.userHasConsented = True
        My.Settings.userAskedForConsent = True
        My.Settings.Save()
        My.Settings.Reload()
        MsgBox("Thank you. The program will now restart.", MsgBoxStyle.Information, "techcare")
        Application.Restart()
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        My.Settings.userHasConsented = False
        My.Settings.userAskedForConsent = True
        My.Settings.Save()
        My.Settings.Reload()
        MsgBox("Thank you. The program will now restart.", MsgBoxStyle.Information, "techcare")
        Application.Restart()
    End Sub
End Class
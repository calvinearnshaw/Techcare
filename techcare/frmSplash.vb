Public NotInheritable Class frmSplash

    Private Sub frmSplash_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblVersion.Text = "Version: " & My.Application.Info.Version.ToString
    End Sub
End Class

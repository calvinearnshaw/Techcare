Imports MySql.Data.MySqlClient
Imports System.IO
Public Class functions

    ' This class contains any shared functions used within the program. This means similar parts of code can be called as many times as needed without
    ' having to rewrite the same code again. If a function or procedure requires SQL, a try/catch statement is used, as errors cannot be identified before runtime.


    ' =====================================================================================================================
    ' MYSQL DATABASE MANAGEMENT
    ' =====================================================================================================================

    Public Shared Function databaseCheck(ByVal dbName As String)
        ' This function checks that the techcare database exists on the MySQL Server. If the database exists, the function
        ' returns TRUE. If it doesn't, it returns FALSE.

        Dim hasDB As Boolean = False

        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT schema_name FROM information_schema.schemata WHERE schema_name = @dbName;", dbConnection)

            dbCommand.Parameters.AddWithValue("@dbName", dbName)

            dbConnection.Open()

            Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader

            If dbReader.HasRows Then
                While dbReader.Read
                    hasDB = True
                End While
            Else
                hasDB = False
            End If

            dbConnection.Close()
        Catch ex As Exception
            log("Error occurred during db check: " & ex.Message)
            hasDB = False
        End Try

        Return hasDB
    End Function

    Public Shared Sub rebuildDatabase()
        ' This procedure rebuilds the TECHCARE database in the event it is not present in the MySQL server.
        ' This procedure is only called with the consent of the end-user.

        Dim accruedErrors As List(Of String) = New List(Of String)

        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("CREATE DATABASE techcare;", dbConnection)

            My.Settings.dbName = "techcare"
            My.Settings.Save()
            My.Settings.Reload()

            dbConnection.Open()
            dbCommand.ExecuteNonQuery()
            dbConnection.Close()
        Catch ex As Exception
            accruedErrors.Add("Failed to create techcare database in server: " & ex.Message)
            log("Error occurred during db rebuild: " & ex.Message)
        End Try

        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("CREATE TABLE Employees (employeeID INT NOT NULL, title VARCHAR(256) NOT NULL, forename VARCHAR(256) NOT NULL, " &
                                                             "surname VARCHAR(256) NOT NULL, userAccessLevel VARCHAR(256) NOT NULL, username VARCHAR(256) NOT NULL," &
                                                             "password VARCHAR(256) NOT NULL);", dbConnection)

            dbConnection.Open()
            dbCommand.ExecuteNonQuery()
            dbConnection.Close()
        Catch ex As Exception
            accruedErrors.Add("Failed to create Employees table in techcare database: " & ex.Message)
            log("Error occurred during db rebuild: " & ex.Message)
        End Try

        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("CREATE TABLE Repairs (repairReference INT NOT NULL, title VARCHAR(256) NOT NULL, forename VARCHAR(256) NOT NULL, " &
                                                             "surname VARCHAR(256) NOT NULL, address VARCHAR(256) NOT NULL, city VARCHAR(256) NOT NULL, postcode VARCHAR(256) NOT NULL, " &
                                                             "homePhone VARCHAR(256) NULL, mobilePhone VARCHAR(256) NULL, emailAddress VARCHAR(256) NULL, " &
                                                             "assetMake VARCHAR(256) NOT NULL, assetModel VARCHAR(256) NOT NULL, assetSerialNumber VARCHAR(256) NOT NULL, " &
                                                             "currentRepairStatus VARCHAR(256) NOT NULL, intakeDate VARCHAR(256) NOT NULL, faultDescription VARCHAR(256) NOT NULL, " &
                                                             "estimateQuote VARCHAR(256) NOT NULL);", dbConnection)
            dbConnection.Open()
            dbCommand.ExecuteNonQuery()
            dbConnection.Close()
        Catch ex As Exception
            accruedErrors.Add("Failed to create Repairs table in techcare database: " & ex.Message)
            log("Error occurred during db rebuild: " & ex.Message)
        End Try

        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("CREATE TABLE Remarks (commentID VARCHAR(256) NOT NULL, employeeID VARCHAR(256) NOT NULL, jobReference VARCHAR(256) NOT NULL, " &
                                                             "timestamp VARCHAR(256) NOT NULL, comment VARCHAR(256) NOT NULL);", dbConnection)
            dbConnection.Open()
            dbCommand.ExecuteNonQuery()
            dbConnection.Close()
        Catch ex As Exception
            accruedErrors.Add("Failed to create Remarks table in techcare database: " & ex.Message)
            log("Error occurred during db rebuild: " & ex.Message)
        End Try

        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("CREATE TABLE Business (name VARCHAR(256) NOT NULL, address VARCHAR(256) NOT NULL, phoneNumber VARCHAR(256) NOT NULL, " &
                                                             "repairTnC VARCHAR(256) NOT NULL);", dbConnection)

            dbConnection.Open()
            dbCommand.ExecuteNonQuery()
            dbConnection.Close()
        Catch ex As Exception
            accruedErrors.Add("Failed to create Business table in techcare database: " & ex.Message)
            log("Error occurred during db rebuild: " & ex.Message)
        End Try

        If accruedErrors.Count > 0 Then
            Dim errors As String = Nothing
            For i As Integer = 0 To accruedErrors.Count - 1
                errors = errors & vbNewLine & vbNewLine & accruedErrors(i).ToString
            Next
            MsgBox("Error(s) have occurred whilst creating the techcare database. Techcare may not function as intended. Please review these errors below:" & errors, MsgBoxStyle.Critical, "techcare")
        Else
            If databaseCheck(My.Settings.dbName) = True Then
                log("Rebuild successful, connection has been achieved.")
            End If
        End If
    End Sub

    Public Shared Function generateUid(ByVal table As String, ByVal tablePk As String, ByVal length As Integer) As String
        ' This function generates a numerical unique identifier of length "length". The program checks the specified table
        ' under the column (passed in via the tablePk parameter) to confirm that the value generated is in fact unique. If
        ' it is, the hasUniqueIdentifier variable is set to True, so that the program can exit the conditional loop, and
        ' the uniqueIdentifier variable is set to the uniqueIdentifier generated. This value is then returned so that it
        ' can be used at any point in the program.

        Dim hasUniqueIdentifier As Boolean = False
        Dim uniqueIdentifier As String = Nothing

        While hasUniqueIdentifier = False
            Dim possibleChars As String = "1234567890"                  ' Define a set of possible characters to use in the creation of the unique identifier.
            Dim charArray() As Char = possibleChars.ToCharArray         ' Convert the defined string into an array of characters.
            Dim random As New Random                                    ' Initialise a new Random library object.
            Dim sb As New System.Text.StringBuilder                     ' Initialise a new String Builder object (this allows us to piece together the unique identifier).

            For index As Integer = 1 To length                          ' Start a fixed loop starting from position 1 and ending at position "length".
                sb.Append(charArray(random.Next(0, charArray.Length)))  ' Add a random character from the character array. Anything from position 0 in the array to max length.
            Next

            Dim generatedID As String = sb.ToString                     ' Convert the sb object into a string. This can now be used to check for matches in the database!

            Try
                Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
                Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT * FROM " & table & " WHERE " & tablePk & " = '" & generatedID & "';", dbConnection)


                dbConnection.Open()                                         ' Setup an SQL command which checks if a given table has a record containing the same unique identifier
                Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader   ' as the identifier which has been generated above.

                If dbReader.HasRows Then                                    ' If the query finds any matches, do not do anything, and restart the loop.
                Else
                    uniqueIdentifier = generatedID                          ' Otherwise, the loop can be stopped and the unique identifier can be used!
                    hasUniqueIdentifier = True
                End If

                dbConnection.Close()                                        ' A good habit to get into is to dispose connections and commands once finished. This resets the
                dbCommand.Dispose()                                         ' MySQL Connection and Command objects!
                dbConnection.Dispose()
            Catch ex As Exception
                MsgBox("An exception has occurred while generating a Unique Identifier." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
                log("Error occurred during UID creation: " & ex.Message)
            End Try
        End While

        Return uniqueIdentifier
    End Function

    ' =====================================================================================================================
    ' TECHCARE GENERAL MANAGEMENT
    ' =====================================================================================================================

    Public Shared Function authenticate(ByVal username As String, ByVal password As String)
        ' This function checks that a given username and password match up to an employee record on the EMPLOYEES table. If there is,
        ' the function will return the employee ID. If there isn't, 0 is returned.

        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT employeeID FROM Employees WHERE username=@uid AND password=@pwd", dbConnection)
            Dim validDetails As Integer = 0

            dbConnection.Open()

            dbCommand.Parameters.AddWithValue("@uid", username)
            dbCommand.Parameters.AddWithValue("@pwd", password)

            Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader

            If dbReader.HasRows Then
                While dbReader.Read
                    validDetails = Convert.ToInt32(dbReader(0).ToString)
                End While
            End If

            dbConnection.Close()
            dbCommand.Dispose()
            dbConnection.Dispose()

            Return validDetails
        Catch ex As Exception
            MsgBox("An exception has occurred whilst authenticating the user." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
            log("Error occurred during user authentication: " & ex.Message)
            Return 0
        End Try
    End Function

    Public Shared Function obtainEmployeeDetails(ByVal employeeID As String, ByVal col As Integer)
        ' This function "cherrypicks" the Employee table within the database, returning a given cell's value as a string.
        ' To do this, an empty "returningValue" variable is created, which the result of the query will be stored into.
        ' The row in the Employees table is found using the employee ID as criteria, and the column number is passed in as
        ' a parameter - which is used to select the column number that which the SQL Data Reader will be reading from.

        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT * FROM Employees WHERE employeeID=@empID", dbConnection)
            Dim returningValue As String = Nothing

            dbConnection.Open()

            dbCommand.Parameters.AddWithValue("@empID", employeeID)

            Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader

            If dbReader.HasRows Then
                While dbReader.Read
                    returningValue = dbReader(col).ToString
                End While
            End If

            dbConnection.Close()
            dbCommand.Dispose()
            dbConnection.Dispose()

            Return returningValue
        Catch ex As Exception
            MsgBox("An exception has occurred whilst obtaining employee details." & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "techcare")
            log("Error occurred while obtaining employee details: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Shared Function validateNewPassword(ByVal password As String) As Boolean
        ' This function is called internally, and checks a "password" string to ensure it's strong enough to be used.
        ' Across the techcare program, any new password created must have at least 8 characters, at least 1 uppercase
        ' letter, at least 1 number, and at least 1 symbol.

        Dim upper As New System.Text.RegularExpressions.Regex("[A-Z]")          ' Setup a "bank" of regex characters - this means any existing letters PLUS any variations in such letters
        Dim lower As New System.Text.RegularExpressions.Regex("[a-z]")          ' from different languages. This means that this function can look out for any characters the user may type,
        Dim number As New System.Text.RegularExpressions.Regex("[0-9]")         ' rather than a limited set of characters which would otherwise go unnoticed by the function.
        Dim special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]") ' This line is used so the program can look for any characters which are not uppercase or lowercase, i.e. symbols.

        If password.Length < 8 Then Return False                                ' Carry out password checks - first check that password is at least 8 characters long.
        If upper.Matches(password).Count < 1 Then Return False                  ' Check if password has at least 1 uppercase character. If not, return false result.
        If lower.Matches(password).Count < 1 Then Return False                  ' Check if password has at least 1 lowercase character. If not, return false result.
        If number.Matches(password).Count < 1 Then Return False                 ' Check if password has at least 1 number. If not, return false result.
        If special.Matches(password).Count < 1 Then Return False                ' Check if password has at least 1 special character. If not, return false result.

        Return True                                                             ' If function hasn't returned false result by now, the password meets all requirements, so return true result.
    End Function

    Public Shared Function generateUsername(ByVal forename As String, ByVal surname As String)
        ' This function creates a unique username. The format of the username is the first 5 letters of the employee's
        ' surname, followed by the first letter of the first name, then a two-digit number, starting at 01 for the first
        ' employee with this username. For example - John Smith = smithj01. If James Smith registers as an employee, his
        ' username would be smithj02, etc.

        Try
            Dim newUsername As String                                                   ' We define newUsername as an empty string, to allow us to build up a new username.

            surname = surname.Replace(" ", "")                                          ' Remove hyphens, spaces, and apostrophes from the surname. Examples:
            surname = surname.Replace("-", "")                                          ' Smith Green becomes SmithGreen, Smith-Green becomes SmithGreen, and O'Brien becomes
            surname = surname.Replace("'", "")                                          ' OBrien.

            If surname.ToString.Length > 5 Then                                         ' Check if the length of the surname is greater than 5. If it is, take the substring
                newUsername = surname.ToString.Substring(0, 5)                          ' of the first 5 letters of surname.
            Else
                newUsername = surname.ToString                                          ' Otherwise, simply set the (current) new username to the first part of the modified
            End If                                                                      ' surname.

            newUsername = newUsername + forename.ToString.Substring(0, 1)               ' We then add the first letter of the forename to the newUsername variable.

            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName & ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT * FROM Employees WHERE username LIKE @uid", dbConnection)
            Dim counter As Integer = 1

            dbCommand.Parameters.AddWithValue("@uid", "%" & newUsername.ToString.Substring(0, Len(newUsername)) & "%")     ' This command looks for any usernames on the database
            dbConnection.Open()                                                                                            ' with the same username.

            Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader

            If dbReader.HasRows Then                                                                        ' If there are any other users on the database with the same
                While dbReader.Read                                                                         ' alphabetical component of the username, the counter variable
                    counter += 1                                                                            ' counts how many rows the database reader returns. Since the 
                End While                                                                                   ' counter starts at 1 instead of 0, any username made with
            End If                                                                                          ' this function will always be unique.

            newUsername = newUsername + counter.ToString("00")                                              ' Add the two-digit number onto the end of the username.

            dbConnection.Close()
            dbConnection.Dispose()
            dbCommand.Dispose()

            Return newUsername.ToLower                                                                      ' Return the username as a lowercase string.
        Catch ex As Exception
            log("Error occurred during username generation: " & ex.StackTrace)
            Return Nothing
        End Try
    End Function

    Public Shared Function obtainBusinessDetails(ByVal col As Integer)
        Try
            Dim dbConnection As MySqlConnection = New MySqlConnection("Server=" & My.Settings.dbLocation & ";Database=" & My.Settings.dbName &
                                                                      ";Uid=" & My.Settings.dbUsername & ";Pwd=" & My.Settings.dbPassword & ";")
            Dim dbCommand As MySqlCommand = New MySqlCommand("SELECT * FROM Business", dbConnection)
            Dim returningValue As String = Nothing

            dbConnection.Open()

            Dim dbReader As MySqlDataReader = dbCommand.ExecuteReader

            If dbReader.HasRows Then
                While dbReader.Read
                    returningValue = dbReader(col).ToString
                End While
            End If

            dbConnection.Close()
            dbCommand.Dispose()
            dbConnection.Dispose()

            Return returningValue
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' =====================================================================================================================
    ' TECHCARE DEBUG TOOLS
    ' =====================================================================================================================

    Public Shared Sub log(ByVal infoToLog As String)
        If My.Settings.userHasConsented = True Then
            Dim strFile As String = "log.txt"
            Dim fileExists As Boolean = File.Exists(strFile)
            Using sw As New StreamWriter(File.Open(strFile, FileMode.Append))
                sw.WriteLine(If(fileExists, "[" & DateTime.Now.ToShortDateString & " - " & DateTime.Now.ToShortTimeString & "] " & infoToLog,
                             "====NEW ERROR LOG====" & vbNewLine & My.Computer.Info.OSFullName & vbNewLine & My.Computer.Info.TotalPhysicalMemory &
                             vbNewLine & My.Computer.Name & "====END OF COMPUTER INFO COLLECTION===="))
            End Using
        End If
    End Sub
End Class

Imports System.IO

Public Class FileAccessClass

    Public Enum FileJob
        Open = 1
        SaveAs = 2
        Directory = 0
    End Enum

    Shared Function FileStringAppend(ByVal OpenSave As FileJob, ByVal Title As String, ByVal Extension As String) As String

        If OpenSave = FileJob.SaveAs Then
            Try
                Dim StaticSaveAs As SaveFileDialog = New SaveFileDialog

                With StaticSaveAs
                    .ValidateNames = False
                    .ShowHelp = False
                    .AddExtension = True
                    .Title = Title
                    .Filter = Extension
                    .ShowDialog()
                End With

                If StaticSaveAs.FileName = "" Then

                    Return "Cancel"

                Else

                    Return StaticSaveAs.FileName

                End If

            Catch ex As Security.SecurityException

                MsgBox("Your OS will not let this program access any files." & vbCrLf & "This may because it is runing from a network drive," & vbCrLf & "copy the program to your desktop and retry.", MsgBoxStyle.Critical, "Security issue")
                Return "Cancel"

            Catch ex As Exception

                Return "Cancel"

            End Try

        End If

        Return "Cancel"

    End Function

    Shared Function FileString(ByVal OpenSave As FileJob, ByVal Title As String, ByVal Extension As String) As String
        Dim answer As MsgBoxResult

        If OpenSave = FileJob.SaveAs Then
            Try
                Dim StaticSaveAs As SaveFileDialog = New SaveFileDialog

                With StaticSaveAs
                    .ValidateNames = True
                    .ShowHelp = False
                    .AddExtension = True
                    .Title = Title
                    .Filter = Extension
                    .ShowDialog()
                End With

                If StaticSaveAs.FileName = "" Then

                    Return "Cancel"

                    'ElseIf File.Exists(StaticSaveAs.FileName) = True Then

                    '    answer = MsgBox("Do you want to overwrite this file?", MsgBoxStyle.YesNoCancel, "File exists.")

                    '    If answer = MsgBoxResult.Yes Then

                    '        Return StaticSaveAs.FileName

                    '    ElseIf answer = MsgBoxResult.No Then

                    '        Return FileString(FileJob.SaveAs, Title, Extension)

                    '    ElseIf answer = MsgBoxResult.Cancel Then

                    '        Return "Cancel"

                    '    End If

                Else

                    Return StaticSaveAs.FileName

                End If
            Catch ex As Security.SecurityException

                MsgBox("Your OS will not let this program access any files." & vbCrLf & "This may because it is runing from a network drive," & vbCrLf & "copy the program to your desktop and retry.", MsgBoxStyle.Critical, "Security issue")
                Return "Cancel"

            Catch ex As Exception

                Return "Cancel"

            End Try
        ElseIf OpenSave = FileJob.Open Then
            Try
                Dim staticOpenAs As OpenFileDialog = New OpenFileDialog
                With staticOpenAs
                    .ValidateNames = True
                    .ShowHelp = False
                    .AddExtension = True
                    .CheckFileExists = True
                    .Title = Title
                    .Filter = Extension
                    .ShowDialog()
                End With

                If staticOpenAs.FileName = "" Then

                    Return "Cancel"

                ElseIf File.Exists(staticOpenAs.FileName) = False Then

                    answer = MsgBox("Could not find file, do you want to select another file?", MsgBoxStyle.YesNoCancel, "File exists.")

                    If answer = MsgBoxResult.Yes Then

                        Return FileString(FileJob.SaveAs, Title, Extension)

                    ElseIf answer = MsgBoxResult.No Then

                        Return "Cancel"

                    ElseIf answer = MsgBoxResult.Cancel Then

                        Return "Cancel"

                    End If

                Else

                    Return staticOpenAs.FileName

                End If

            Catch ex As Security.SecurityException

                MsgBox("Your OS will not let this program access any files." & vbCrLf & "This may because it is runing from a network drive," & vbCrLf & "copy the program to your desktop and retry.", MsgBoxStyle.Critical, "Security issue")
                Return "Cancel"

            Catch ex As Exception

                Return "Cancel"

            End Try

        ElseIf OpenSave = FileJob.Directory Then
            Try
                Dim staticDirectory As FolderBrowserDialog = New FolderBrowserDialog

                With staticDirectory
                    If IO.Directory.Exists(Extension) = True Then
                        .SelectedPath = Extension
                    End If
                    .ShowNewFolderButton = False
                    .Description = Title
                    .ShowDialog()
                End With

                If staticDirectory.SelectedPath = "" Then

                    Return "Cancel"

                Else

                    Return staticDirectory.SelectedPath

                End If

            Catch ex As Security.SecurityException

                MsgBox("Your OS will not let this program access any files." & vbCrLf & "This may because it is runing from a network drive," & vbCrLf & "copy the program to your desktop and retry.", MsgBoxStyle.Critical, "Security issue")
                Return "Cancel"

            Catch ex As Exception

                Return "Cancel"

            End Try


        End If

        Return "Cancel"

    End Function

    Shared Function FileString_filter(ByVal OpenSave As FileJob, ByVal Title As String, ByVal Extension As String) As String
        Dim answer As MsgBoxResult

        If OpenSave = FileJob.SaveAs Then
            Try
                Dim StaticSaveAs As SaveFileDialog = New SaveFileDialog

                With StaticSaveAs
                    .ValidateNames = True
                    .ShowHelp = False
                    .AddExtension = True
                    .Title = Title
                    .Filter = Extension
                    .ShowDialog()
                End With

                If StaticSaveAs.FileName = "" Then

                    Return "Cancel"

                ElseIf File.Exists(StaticSaveAs.FileName) = True Then

                    answer = MsgBox("Do you want to overwrite this file?", MsgBoxStyle.YesNoCancel, "File exists.")

                    If answer = MsgBoxResult.Yes Then

                        Return StaticSaveAs.FileName

                    ElseIf answer = MsgBoxResult.No Then

                        Return FileString(FileJob.SaveAs, Title, Extension)

                    ElseIf answer = MsgBoxResult.Cancel Then

                        Return "Cancel"

                    End If

                Else

                    Return StaticSaveAs.FilterIndex.ToString & StaticSaveAs.FileName

                End If
            Catch ex As Security.SecurityException

                MsgBox("Your OS will not let this program access any files." & vbCrLf & "This may because it is runing from a network drive," & vbCrLf & "copy the program to your desktop and retry.", MsgBoxStyle.Critical, "Security issue")
                Return "Cancel"

            Catch ex As Exception

                Return "Cancel"

            End Try
        ElseIf OpenSave = FileJob.Open Then
            Try
                Dim staticOpenAs As OpenFileDialog = New OpenFileDialog
                With staticOpenAs
                    .ValidateNames = True
                    .ShowHelp = False
                    .AddExtension = True
                    .CheckFileExists = True
                    .Title = Title
                    .Filter = Extension
                    .ShowDialog()
                End With

                If staticOpenAs.FileName = "" Then

                    Return "Cancel"

                ElseIf File.Exists(staticOpenAs.FileName) = False Then

                    answer = MsgBox("Could not find file, do you want to select another file?", MsgBoxStyle.YesNoCancel, "File exists.")

                    If answer = MsgBoxResult.Yes Then

                        Return FileString(FileJob.SaveAs, Title, Extension)

                    ElseIf answer = MsgBoxResult.No Then

                        Return "Cancel"

                    ElseIf answer = MsgBoxResult.Cancel Then

                        Return "Cancel"

                    End If

                Else

                    Return staticOpenAs.FilterIndex.ToString & staticOpenAs.FileName

                End If

            Catch ex As Security.SecurityException

                MsgBox("Your OS will not let this program access any files." & vbCrLf & "This may because it is runing from a network drive," & vbCrLf & "copy the program to your desktop and retry.", MsgBoxStyle.Critical, "Security issue")
                Return "Cancel"

            Catch ex As Exception

                Return "Cancel"

            End Try

        ElseIf OpenSave = FileJob.Directory Then
            Try
                Dim staticDirectory As FolderBrowserDialog = New FolderBrowserDialog

                With staticDirectory
                    .ShowNewFolderButton = True
                    .Description = Title
                    .ShowDialog()
                End With

                If staticDirectory.SelectedPath = "" Then

                    Return "Cancel"

                Else

                    Return staticDirectory.SelectedPath

                End If

            Catch ex As Security.SecurityException

                MsgBox("Your OS will not let this program access any files." & vbCrLf & "This may because it is runing from a network drive," & vbCrLf & "copy the program to your desktop and retry.", MsgBoxStyle.Critical, "Security issue")
                Return "Cancel"

            Catch ex As Exception

                Return "Cancel"

            End Try


        End If

        Return "cancel"

    End Function

End Class

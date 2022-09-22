@echo off
setlocal

cd "%~dp0"

For %%a in (
"XwaWorkspaceViewer\bin\Release\net48\*.dll"
"XwaWorkspaceViewer\bin\Release\net48\*.exe"
"XwaWorkspaceViewer\bin\Release\net48\*.config"
) do (
xcopy /s /d "%%~a" dist\
)

For %%a in (
"XwaWorkspaceEditor\bin\Release\net48\*.dll"
"XwaWorkspaceEditor\bin\Release\net48\*.exe"
"XwaWorkspaceEditor\bin\Release\net48\*.config"
) do (
xcopy /s /d "%%~a" dist\
)

For %%a in (
"XwaShpInstaller\bin\Release\net48\*.dll"
"XwaShpInstaller\bin\Release\net48\*.exe"
"XwaShpInstaller\bin\Release\net48\*.config"
) do (
xcopy /s /d "%%~a" dist\
)

@echo off
echo . > dirs.txt
dir /a:d /s /b >> dirs.txt
REM Löscht alle temporären Visual Studio Dateien
FOR /F %%d IN (dirs.txt) DO (
  rd -r "%%d\bin"
)
pause

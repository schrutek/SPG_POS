@echo off
echo . > dirs.txt
dir /a:d /s /b >> dirs.txt
REM L�scht alle tempor�ren Visual Studio Dateien
FOR /F %%d IN (dirs.txt) DO (
  rd -r "%%d\bin"
)
pause

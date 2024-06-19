;!include MUI2.nsh
LoadLanguageFile "${NSISDIR}\Contrib\Language files\Russian.nlf"
; Переменные.
!define TEMP1 $R0 ;Temp variable
; Наименование инсталятора.
Name "утилита импорта данных"
; Директория инсталяции.
InstallDir "$PROGRAMFILES64\Directum Company\DirectumRX\ImportData"
; Наименование инсталятора.
OutFile "Setup.exe"

VIProductVersion 4.0.4937.1
VIAddVersionKey FileVersion 4.0.4937.1
VIAddVersionKey ProductVersion 4.0.4937.1
XPStyle on
; Управление страницами.
Page directory 
Page instfiles

Section "Components"
 SetOutPath $INSTDIR
 File /r "..\src\ImportData\bin\Debug\netcoreapp3.0\*.*" 
SectionEnd


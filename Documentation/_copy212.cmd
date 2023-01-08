echo off

:: Version identity
set t=2.12

:: Documentation, defaults
for /f "eol=:" %%s in (CoreBase.txt) do copy /y "default.md" "%appdata%\Dynamo\Dynamo Core\%t%\packages\DynamoCSharpSample\doc\%%s.md"
for /f "eol=:" %%s in (CoreExtensible.txt) do copy /y "default.md" "%appdata%\Dynamo\Dynamo Core\%t%\packages\DynamoCSharpSample\doc\%%s.md"

for /f "eol=:" %%s in (CoreBase.txt) do copy /y "default.md" "%appdata%\Dynamo\Dynamo Revit\%t%\packages\DynamoCSharpSample\doc\%%s.md"
for /f "eol=:" %%s in (CoreExtensible.txt) do copy /y "default.md" "%appdata%\Dynamo\Dynamo Revit\%t%\packages\DynamoCSharpSample\doc\%%s.md"
for /f "eol=:" %%s in (RevitBase.txt) do copy /y "default.md" "%appdata%\Dynamo\Dynamo Revit\%t%\packages\DynamoCSharpSample\doc\%%s.md"
for /f "eol=:" %%s in (RevitExtensible.txt) do copy /y "default.md" "%appdata%\Dynamo\Dynamo Revit\%t%\packages\DynamoCSharpSample\doc\%%s.md"



:: If specific md file is created, then copy them afterwards, this can be ordered
:: into subfoldes while the below command will flatten it when copying

:: Documentation
::for /r ".\Core\" %%f in (*.md,*.png) do xcopy /d /y "%%f" "%appdata%\Dynamo\Dynamo Core\%t%\packages\DynamoCSharpSample\doc\"
::for /r ".\Core\" %%f in (*.md,*.png) do xcopy /d /y "%%f" "%appdata%\Dynamo\Dynamo Revit\%t%\packages\DynamoCSharpSample\doc\"

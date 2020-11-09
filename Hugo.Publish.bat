color B

del  Publish\*.*   /s /q

dotnet restore

dotnet build

cd Hugo.Core.WebApi

dotnet publish -o ..\Hugo.Core.WebApi\bin\Debug\netcoreapp3.1\

md ..\Publish

xcopy ..\Hugo.Core.WebApi\bin\Debug\netcoreapp3.1\*.* ..\Publish\ /s /e 

echo "Successfully! Please see the file Publish"

cmd
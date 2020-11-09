git pull;
rm -rf Publish;
dotnet build;
dotnet publish -o /home/Hugo/Hugo.Core.WebApi/bin/Debug/netcoreapp3.1;
cp -r /home/Hugo/Hugo.Core.Api/bin/Debug/netcoreapp3.1 Publish;
echo "Successfully! Please see the file Publish";
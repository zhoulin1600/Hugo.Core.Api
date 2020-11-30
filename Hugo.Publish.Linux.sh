git pull;
rm -rf Publish;
dotnet build;
dotnet publish -o /home/Hugo.Core.Api/Hugo.Core.WebApi/bin/Debug/netcoreapp3.1;
cp -r /home/Hugo.Core.Api/Hugo.Core.WebApi/bin/Debug/netcoreapp3.1 Publish;
echo "Successfully! Please see the file Publish";
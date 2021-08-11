#Installar ef migrations
```powershell
dotnet tool install --global dotnet-ef --version 5.0.9
```
#Migrations
Executados na pasta do projeto Data:
```powershell
dotnet ef migrations add Initial --startup-project ..\Presentation\Presentation.csproj
dotnet ef database update  --startup-project ..\Presentation\Presentation.csproj
```
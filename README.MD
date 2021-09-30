# Installar ef migrations
```powershell
dotnet tool install --global dotnet-ef --version 5.0.9
```
# Migrations
Executados na pasta do projeto Data:
```powershell
dotnet ef migrations add Initial --startup-project ..\Processadores.WebApi\Processadores.WebAp.csproj
dotnet ef database update  --startup-project ..\Processadores.WebAp\Processadores.WebAp.csproj
```

#Migrations com Identity
```powershell
dotnet ef migrations add Initial --context LoginContext --output-dir Areas\Identity\Data\Migrations
```

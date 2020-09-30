# The Spaceport 

The Spaceport är en rymdskeppsparkering för Star Wars universumet med en separat frontend och backend. Personerna som får parkera måste vara en del av Star Wars. Syftet med projektet var att vi skulle lära oss hur följande resurser på Azure kan fungera sammankopplat:

* Azure Container Instance (ACI)
* Azure Container Registry (ACR)
* Azure SQL-Server
* Azure SQL-Database
* Azure KeyVaults
* Azure Application Insights
* Docker
* Azure DevOps Pipelines
* Azure Artifacts

Det här är flödet som vi tänkte att vi ville åstadkomma

### Planering







I början av projektet så diskuterade vi och kom fram till att vi skulle använda ett projekt ifrån den tidigare kursen, vi tänkte att eftersom att koden inte var fokus i detta projektet att vi kunde spara tid. Vi visste även i ett tidigt skede att vi kommer behöva arbeta med många olika benämningar så vi valde att göra ett dokument för namngivelse, så att vi alla lättare kunde hålla koll på vad alla olika resurser skulle ha för namn. Vi diskuterade lite kring hur vi skulle lägga upp vår frontend och eftersom ingen av oss direkt hade kodat i razor pages  innan så tänkte vi att detta skulle va ett bra tillfälle att lära sig detta, så vi bestämde att vi skulle köra på det för våran frontend. 

Vi bestämde oss för att använda AzureDevOps boards för planering, vilket vi använde flitigt till en början men vi blev sämre på att använda detta under projektets gång. 

### Backend

Vår backend är ett .NET-core API. Vi fick sitta i några dagar och refakturera kod, eftersom att projektet inte var byggt för detta ändamålet ifrån början. Det blev en hel del problem på grund av detta. Vi ville få till databasen snabbt så att vi kunde testa vårt api, så vi konfigurerade en databas och körde igång den i Azure så att alla kunde testa emot samma databas. Vi valde att köra en serverless databas eftersom att vi visste att vi inte skulle ha speciellt mycket trafik i detta projektet. 

#### Logger



**Vår appSettings.json konfiguration:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:spacepark-sql-dev-01.database.windows.net,1433;Initial Catalog=spacepark-sqldb-dev-01;Persist Security Info=False;User ID=spaceparkadmin;Password=*********;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
```

**Våran databas-struktur i SQL:**

​          ![Backend-01.PNG](https://github.com/PGBSNH19/spacepark-grupp-2-b02-b04/blob/master/Dokumentation/Blogg/img/Backend-01.PNG?raw=true)      

Vi visste även att vi ville komma igång med en pipeline snabbt så vi la till funktionalitet för Docker i tidigt skede. 

### Docker

Eftersom att vi skulle ha vår frontend separerad ifrån våran backend så visste vi att vi skulle behöva två olika images, som skulle laddas upp i två olika resurser på Azure. Till en början bestämde vi att vi skulle försöka att få våran backend att fungera i våran CI pipeline. 

**Vår Dockerfile konfiguration:**

```yaml
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./SpacePark/SpacePark/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./SpacePark/SpacePark/ ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SpacePark.dll"]
```

För att kunna testa vår så pekar vi på följande portar i vår launchSettings.json:

```json
    "SpacePark": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "https://localhost:443;http://localhost:80"
    }
```

Vi fick våran CI-pipeline att fungera relativt snabbt utan några större problem. 







Till en början var planen att vi skulle använda oss av MVC men det slutade med att vi använde oss av ett API och razor pages. Vi fokuserade främst på att få till en fungerande pipeline, vilket också då fick oss att göra en fungerande ACI. När detta låg uppe i Azure så konfigurerade vi vår databas. Vi valde att ha den i molnet ifrån början så att alla kunde arbeta med samma databas. 
# The Spacepark

## Introduktion

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

## Arbetssätt

* Att vi delade upp oss två och två.
* allt på azure gjordes tillsammans
* Att vi suttit ca: 09:30-16:30 alla dagar när vi inte haft lektion
* Bloggat i princip varje dag
* Skapat planering för kommande dagar utifrån var vi stått inför för problem alternativt vad vi behövt för att komma vidare i projektet som helhet.

## Planering

* Att vi utgick från ett gammalt projekt
* Skapade boards och sprints för att sätta igång och strukturera arbetet
* Tänkte MVC -> API + Razorpages
* Tidigt få databas till azure
* få projekten att fungera tillsammans lokalt (göra ett post-request till databas)
* Skapa alla resurser på Azure och fixa åtkomst, förklara att vi inte fattade det där hur vi skulle komma åt saker och ting.
* Flowcharts för att hjälpa oss med att visualisera flödet av vissa delar av projektet.

## Problem och lösningar

* RazorPages - Vad var det som var svårast? (hantera sidor och objekt)
* Azure KeyVault - Vad var det egentligen som var så svårt? Hur löste vi det?
* Azure Application Insights - Vi fick det till viss del att fungera. Hur kunde det fungerat bättre?
* App service vs. Container Instance. Varför valde vi ACI? Vilka problem uppstod?

## Resultat - genomgång av hela strukturen

* Vad är vi nöjda med?
* Vad skulle vi gjort annorlunda ifall vi hade gjort om projektet idag (Missnöje :( )?
* CI-/CD-Pipeline - Hur gjorde vi? (Använd separat dokument här)
* Artifacts, hur använde vi dem?
* Att vi inte delade första pipeline i två
* Flowchart över projektet i helhet
* Bara Dev-miljö inte prod, varför? Hur skulle vi gjort Prod?
* Tester, hur använde vi de? Hur skulle man egentligen använt sig av tester? Styrkor med tester i CI/CD.
* Pris, vad skulle hela kalaset kosta ifall vi hade deployat något likande?





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





## Planering och arbetsföljd

Till en början var planen att vi skulle använda oss av MVC men det slutade med att vi använde oss av ett API och RazorPages. Vi fokuserade främst på att få till en fungerande pipeline, vilket också då fick oss att göra en fungerande ACI. När detta låg uppe i Azure så konfigurerade vi vår databas. Vi valde att ha den i molnet ifrån början så att alla kunde arbeta med samma databas. 

Vi hade ju till en början tänkt att vi skulle göra ett MVC men det visade sig egentligen vara för enkelt/gå för fort för oss, vilket skulle kunna leda till att vi inte lärde oss lika bra. Vi gick därför över till ett API och tanken var nu istället att vi skulle  separera vår MVC till två separata projekt i vår solution och sedan få dem att arbeta tillsammans i målnet. Men vi såg till att få detta samarbete att fungera lokalt innan vi gick vidare.

Vi stötte här på vårt första problem egentligen, när vi inte hade använt oss av en .gitignore-fil och fick givetvis massor med konflikter vid merge.  

I våra pages var ett tidigt problem att vi inte kunde passera data mellan de olika sidorna. Mer specifikt uppstod det störst problem när mer komplicerade objekt som har egna listor etc. skulle skickas till nästa sida, eftersom att datan går förlorad. Vi hade inte tidigare använt oss utav RazorPages så det var svårt. Vi löste det genom att kasta med ett hidden input som ligger i html-formen som följer med vid submit.

Vid denna punkt fungerar båda projekten lokalt med databas på Azure. Innan vi lägger upp allting på Azure behöver vi  skapa och konfigurera vår Docker-fil för frontend. 



Vad vi har lärt oss:

1. Vi trodde att vi skulle spara tid genom att plocka kod ifrån ett gammalt, men det är mycket av koden i projektet som egentligen aldrig används. Det blev mestadels bara rörigt när vi plockade in så mycket kod för att få hela CRUD-delen att fungera. Det som vi borde ha gjort är att fokusera mer på att plocka ur dom delarna som vi faktiskt skulle komma att behöva. Egentligen var det bristfällande planering ifrån vårt håll som gjorde att det blev såhär. På grund av detta var vi tvingade att refaktorera mycket kod och det tog längre tid innan vi kunde testa hela flödet i projektet ifrån frontend till databas.
2. RazorPages, hade varit snyggt att använda någon form av cookie/session.
3. KeyVault - stora problem. Beskriv hela processen hur vi jobbade oss igenom och vad vi kom fram till. 
4. 
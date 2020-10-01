# Introduktion

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

Vi bestämde några förhållningsregler för hur projektet skulle fungera.

* Arbetstider 09:30 - 16:30 varje dag
* För blogg så mycket som möjligt
* Dela upp arbete i den mån möjligt
* Sitta mycket med Azure tillsammans

## Azure DevOps

Vi använde oss utav Azure DevOps-Boards för att planera upp projektet och lägga till Tasks på det vi behövde göra, vilket vi använde flitigt till en början men blev sämre på att använda under projektets gång. 

Tanken var att vi skulle använda oss utav sprintar och få till en bra planering runt detta, men det blev ändå mer att vi strukturerade om planeringen och använde oss utav bloggar istället.

# Planering

I början av projektet så diskuterade vi och kom fram till att vi skulle använda ett projekt ifrån den tidigare kursen, vi tänkte att eftersom att koden inte var fokus i detta projektet att vi kunde spara tid. Vi visste även i ett tidigt skede att vi kommer behöva arbeta med många olika benämningar så vi valde att göra ett dokument för [namngivelser](Namngivelsekonvention), så att vi alla lättare kunde hålla koll på vad alla olika resurser skulle ha för namn. Vi diskuterade lite kring hur vi skulle lägga upp vår *Frontend* och eftersom ingen av oss direkt hade kodat i *Razor Pages*  innan så tänkte vi att detta skulle va ett bra tillfälle att lära sig detta, så vi bestämde att vi skulle köra på det för våran *Frontend*. Tanken var att vi skulle få våran *Frontend* och *API* att köra mot varanda och att det skulle vara möjligt att göra en *Post* en person till *API*.

## Azure

Vi såg till att lägga upp vissa resurser i *Azure* i ett väldigt tidigt stadie, så som *Resource Group* och *SQL Server* och Databas för att kunna arbeta efter en utvecklingsmiljö uppe i molnet så tidigt som möjligt och därefter utöka det till att inkludera en produktionsmiljö också. När det var fixat såg vi till att ge *Azure DevOps* tillgång till den *Resource Group* vi skapat.

### Continuous Integration Pipeline(CI)

Vi såg till att lägga upp en pipeline i ett väldigt tidigt stadie där tanken var att det kontinuerligt skulle byggas nya images till *Azure Container Registry*.

Här kan du läsa mer om  [Continuous Integration](https://github.com/PGBSNH19/spacepark-grupp-2-b02-b04/blob/master/Dokumentation/CI%20Pipeline.md).



### Continuous Delivery Pipeline(CD)

Till en början hade vi inte riktigt någon koll på hur vi skulle vilja distribuera produkten, men hade en tanke på att leverera denna till en Azure Container Instance utifrån Azure Container Registry. Vi hade tanker på att första leverera till en utvecklingsmiljö för att sedan leverera till produktionsmiljö.

Här kan du läsa mer om [Continuous Delivery](https://github.com/PGBSNH19/spacepark-grupp-2-b02-b04/blob/master/Dokumentation/CD%20Pipeline.md).

### Docker

Eftersom att vi skulle ha vår *Frontend* separerad ifrån våran *API* så visste vi att vi skulle behöva två olika images, som skulle laddas upp i två olika resurser på Azure. Till en början bestämde vi att vi skulle försöka att få våran API att fungera i våran *CI Pipeline*. 

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

# Frontend

Vi diskuterade kring hur vi skulle skapa våran Frontend och funderade på att skapa ett Web App projekt med ModelViewController eller Razor Pages, det blev att vi valde att göra med Razor Pages, med tanken på att det skulle gå relativt smidigt och enkelt att få upp en presentation.

Nedan visar vi ett diagram som visar på flödet i våran applikation:

![](PresentationFlowchart.png) 

## API

Vår *API* är ett .NET-core *API*. Vi fick sitta i några dagar och refakturera kod, eftersom att projektet inte var byggt för detta ändamålet ifrån början. Det blev en hel del problem på grund av detta. Vi ville få till databasen snabbt så att vi kunde testa vårt *API*, så vi konfigurerade en databas och körde igång den i *Azure* så att alla kunde testa emot samma databas. Vi valde att köra en serverless databas eftersom att vi visste att vi inte skulle ha speciellt mycket trafik i detta projektet. 

## SQL Databas

Vi valde att använda oss av en SQL-Databas då vi kände att det var det självklara valet i och med att vi ville ha en stabil relations-databas till projektet.

**Våran databas-struktur i SQL:**

​          ![Backend-01.PNG](https://github.com/PGBSNH19/spacepark-grupp-2-b02-b04/blob/master/Dokumentation/Blogg/img/Backend-01.PNG?raw=true)

# Problem och lösningar

* RazorPages - Vad var det som var svårast? (hantera sidor och objekt)
* Azure KeyVault - Vad var det egentligen som var så svårt? Hur löste vi det?
* Azure Application Insights - Vi fick det till viss del att fungera. Hur kunde det fungerat bättre?
* App service vs. Container Instance. Varför valde vi ACI? Vilka problem uppstod?

1. # Resultat 

   Här är flödet över hela flödet CI/CD pipeline som den ser ut just nu.

   ![Continuous Integration + Deployment Final](E:/C%23/spacepark-grupp-2-b02-b04/Dokumentation/Bilder/Continuous Integration + Deployment Final.png)

   ## Priser

   Du kan läsa mer om priser och vad saker och ting på Azure kostar [här](Priser.md).

   ## Vad är vi mest nöjda med?

   Den huvudsakliga uppgiften tillsammans med vårt mål med hela projektet var att få hela vår Spacepark-lösning att köras i molnet med en viss del automatisering. Det känner vi att vi åstadkommit och det var en väldigt skön känsla när vi äntligen kunde [gå in och parkera våra rymdskepp](spaceparkwebapp.northeurope.azurecontainer.io) (antagligen är inte den uppe och kör vid tid för inlämning tyvärr) med allt liggandes och körandes på Azure.

   Efter mer än två dagar, många olika NuGet-paket hål, i väggarna efter allt huvud-dunkande lyckades vi äntligen få KeyVault att fungera, vilket man kan läsa om i [detta dokumentet](Key%20Vault.md).

   

   ## Vad skulle vi gjort annorlunda ifall vi gjort om projektet idag?

   Övergripande i projektet märkte vi att vi skulle lagt **mer fokus på planering**. Det blev rörigt och svårt att dela upp arbetet mot slutet utav projektet, vilket resulterade i mycket arbete för en person medans 3 andra satt och var mer passiva, vilket så klart inte är så effektivt och inte speciellt roligt heller. Vi hade också många olika små dokument som gjorde det rörigt att veta ifall informationen man arbetade på redan fanns utarbetad innan eller inte.

   Vi trodde att vi skulle spara tid genom att plocka kod ifrån ett gammalt projekt, men det var mycket av koden i projektet som egentligen aldrig används. Det blev mestadels bara rörigt när vi plockade in så mycket kod för att få hela CRUD-delen att fungera. Det som vi borde ha gjort är att fokusera mer på att plocka ur dom delarna som vi faktiskt skulle komma att behöva. Egentligen var det bristfällande planering ifrån vårt håll som gjorde att det blev såhär. På grund av detta var vi tvingade att refaktorera mycket kod och det tog längre tid innan vi kunde testa hela flödet i projektet ifrån frontend till databas

   Nu när man vet lite mer om hur Azure fungerar generellt hade det varit kul att skapa ett mer "verkligt" flöde från developement -> staging -> production där man då har med quality assurence som en integral del innan release (nämns lite i [vårt CD-dokument](https://github.com/PGBSNH19/spacepark-grupp-2-b02-b04/blob/master/Dokumentation/CD%20Pipeline.md)). Vi hade ju tänkt att vi skulle göra detta från början men insåg snart att vi inte hade tid med det.

   ### CI pipeline

   Egentligen skulle hela build pipelinen vara uppdelad i två, en för Backend och en för Frontend. Det hade gjort att vi istället för att hela tiden skapa två nya images och spara dem i två olika registrys, endast skapat en ny ifall det endast skett ändringar i en branch. Eftersom att pipelinen var skapad och tänkt utifrån ett MVC-projekt så fastnade vi lite i det tänket. Hade vi börjat om från början idag hade vi definitivt delat upp det i två separata CI-pipelines.

   ### CD pipeline

   Vi använde oss egentligen utav två release **artifacts**, som hade varsin källa, en för frontend och en för backend. Vi märkte i efterhand att det kanske hade varit bättre att bara ha en artifacts som använder sig av 2 stycken källor istället, för det känns som att det är med så det skulle gå till i verkligheten. Det var någonting som vi inte ville börja hålla på med det i slutet utav projektet så det blev som det blev helt enkelt.

   ### Service Bus

   Tanken var att vi skulle implementera en service bus, men vi insåg att så som vårt projekt ser ut i nuläget så finns det ingen anledning. Hade vi haft tid hade vi kunnat implementera en service bus för att hantera betalningar av användaren. 

   Man skulle kunna säga att Azure Service Bus kan orkestrera olika program och tjänster med hjälp av *meddelanden*. Den skulle också kunna fungera som ett kö-system för användare om tjänsten plötsligt skulle få en stor ström av användare på samma gång. Detta kallas för service bus queues. 

   Om vi hade haft tid skulle vi till exempel kunna göra det möjligt för användaren att få ett kvitto på sin betalning. I vårt fall hade vi fått presentera detta när användaren är inloggad på sidan, men i ett verkligt projekt så hade användaren eventuellt fått ett mail eller sms.  Vi skulle även här kunnat lägga till funktionalitet för att påminna användaren om att tiden för parkeringsplatsen är på väg att ta slut.

   Här är flödet över hela flödet CI/CD pipeline som den ser ut just nu.

   

   ![Continuous Integration + Deployment Final](E:/C%23/spacepark-grupp-2-b02-b04/Dokumentation/Bilder/Continuous Integration + Deployment Final.png)

   
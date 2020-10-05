# Priser

## SpacePark

För detta detta projektets ändamål och för att vi ska kunna göra en uträkning, så tänker vi att alla som är med i vårt Star Wars universum parkerar en gång per dag.

### Uppskattad trafik

**Antalet personer**: 70 st

**Check in/Check out**:(Alla checkar in och ut en gång/dag) 70 st x 2

**Tid för Check in/Check out**: 1 min x 2

**Öppettider för parkering**: Kl 06-23

### Azure Resurser

**Azure SQL Database Serverless**: 1GB Lagring (max 70 parkerade samtidigt) ungefär 5 dollar / månad

**Storage:** Data för inloggning av applikationen 5 gb / månad

**Azure Container Instance**: 2 dollar per instance / månad

**Azure Container Registry:** 5 dollar per registry / månad

**Azure Backup**: 2GB lagring per månad 5 dollar månad

**Azure DevOps**: 200 dollar per månad

**Azure KeyVault**: 1 dollar i månaden för 2 000 000 requests

**Azure Monitor** 12 dollar / 1 000 000 api calls



## Resultat

Den totala kostnaden för att skapa en Space Park kommer att landa på 237 dollar i månaden ungefär. Då har vi valt att inte använda oss av SQL Server Provisioned då vi anser att vi inte har ett behov av att ha en server uppe konstant då våra aktiva timmar är mellan 06-23 och mängden trafik kommer vara begränsad. En provisioned server kostar 1150 dollar per månad och då är databasen uppe hela tiden.

Vi använder oss utav Azure Container Instance istället för Azure App Service for Containers i av samma anledning som med databasen, då vi inte har ett behov av att ha något uppe hela tiden kan vi spara in mycket pengar på det. En Azure App Service hade kostat oss ungefär 50 dollar mer än en Container Instance.
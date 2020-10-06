# Priser

## SpacePark

För att kunna skapa oss en realistisk uppfattning om vad det skulle kunna kosta att köra hela vårt projekt på Azure har vi skapat följade senario. Vi gjorde denna uppskattning utifårn vår begränsade kunskap om trafik, så dessa siffor bör tas med en nypa salt!

### Uppskattad trafik

**Antalet personer**: 70 st

**Check in/Check out**:(Alla checkar in och ut en gång/dag) 70 st x 2 = 140

**Tid för Check in/Check out**: 1 min x 140 = 140 min/dag/användare

**Öppettider för parkering**: Kl 06-23

### Azure Resurser

**Azure SQL Database Serverless**: 1GB Lagring (max 70 parkerade samtidigt) ungefär $5/mån

**Storage:** Data för inloggning av applikationen 5 gb/mån

**Azure Container Instance**: $2 per instance/månad = $4

**Azure Container Registry:** $5 per registry/mån = $10

**Azure Backup**: 2GB lagring/mån = $5

**Azure DevOps**: $200/mån

**Azure KeyVault**: 2 000 000 requests/mån = $1

**Azure Monitor**: 1 000 000 api calls/mån = $12



## Resultat

Enligt våra uppskattningar ovan kommer den totala kostnaden för vår Space Park parkeringsapplikation att kosta ca: **$237 per månad**, med de resurser vi har i *Azure*. 

Eftersom vi inte har ett behov av att ha en server uppe konstant valde vi ej **SQL Server Provisioned**, utan har parkerings-tjänsten uppe mellan 06 och 23 och mängden trafik kommer vara begränsad. En provisioned server kostar ca: $150 per månad och då är databasen uppe hela tiden istället.

Vi använder oss utav Azure Container Instance istället för Azure App Service for Containers i av samma anledning som med databasen, då öppettiderna för vår Space Park är begränsad finns det inget behov att våran applikation ska vara aktiv dygnet runt i molnet. En Azure App Service hade kostat oss ungefär 50 dollar mer än en Container Instance.

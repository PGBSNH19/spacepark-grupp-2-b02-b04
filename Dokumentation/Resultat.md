# Resultat 

Här är flödet över hela flödet CI/CD pipeline som den ser ut just nu.

![Continuous Integration + Deployment Final](Bilder/Continuous%20Integration%20+%20Deployment%20Final.png)

## Priser

Du kan läsa mer om priser och vad saker och ting på Azure kostar [här](Priser.md).

## Vad är vi mest nöjda med?

Den huvudsakliga uppgiften tillsammans med vårt mål med hela projektet var att få hela vår Spacepark-lösning att köras i molnet med en viss del automatisering. Det känner vi att vi åstadkommit och det var en väldigt skön känsla när vi äntligen kunde [gå in och parkera våra rymdskepp](spaceparkwebapp.northeurope.azurecontainer.io) med allt liggandes och körandes på Azure.

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



![Continuous Integration + Deployment Final](Bilder/Continuous%20Integration%20+%20Deployment%20Final.png)


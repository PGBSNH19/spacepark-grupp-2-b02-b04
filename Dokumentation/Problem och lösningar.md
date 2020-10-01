**RazorPages**

Vi antog oss utmaningen att bygga vår frontend med RazorPages, dels för att defaultsidan som kom med när vi skapade projektet var uppbyggt med RazorPages men också för att lära oss något nytt sätt att hantera frontend.  Detta beslut gjorde att byggandet av vår frontend tog längre tid än som vad tänkt. Dels på grund utav att vi var tvungna att hantera flera sidor och passa runt de värden som våra modeller innehåll från sida till sida för att kunna hantera parkering av skepp och checka ut skepp. Detta skulle visa sig vara med komplicerat än vad vi hade räknat med, då bland annat RazorPages inte kan hantera komplexa objekt.

Vi är väl inte helt nöjda med hur koden till vår frontend är uppbyggd, för att spara tid och få till mer "clean code" hade det varit bättre att vi skulle hålla oss till vanlig HTML och JavaScript-kodning som vi är mer bekväma med redan från start, men då vi hade kommit en bit på vår RazorPages lösning fanns det inte utrymme för att ändra på arbetssättet.

Sammanfattningsvis kan man säga att vi är nöjda med att få till alla funktioner med RazorPages men vi är inte nöjda med hur det ser ut i koden.

**Azure KeyVault**

Vår KeyVault-lösning var också en del i projektet som tog mer tid än väntat, dels på grund utav vår egna bristfällighet i kunskap om just KeyVault men också att vi hittade väldigt många olika lösningar på internet som var kvalitetsvarierande i deras dokumentation. Vi testade väldigt många olika lösningsexempel som vi hittade på internet men inget fungerade och vi var väldigt nära på att slopa idéen om att använda oss utav KeyVault för att bevara vår "connectionsstring". Som tur var fick vi lite hjälpa från Grupp 1 på hur dom hade löst det i kod, då vi kombinerade en del utav deras lösning med ett lösningsexempel på internet fick vi det att fungera.

Att vi löste kopplingen mellan vårt API och vår Key Vault är vi såklart nöjda med men på andra sidan är det högst oklart för oss om det är en bra lösning eller inte. Det positiva är att vår känslig information hålls hemligt i molnet och kan nås av vår API och vad vi har sett så kommer den inte ut någonstans i applikationen. Till avgörande om det är en bra lösning eller inte är något vi får läsa på mer om samt testa och implementera andra lösningar för att jämföra dom mot varandra och dra en slutsats där ifrån.

**Azure Application Insights**

* Azure Application Insights - Vi fick det till viss del att fungera. Hur kunde det fungerat bättre?



* App service vs. Container Instance. Varför valde vi ACI? Vilka problem uppstod?
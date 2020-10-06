**Key Vault**

Key Vault är en säkerhetstjänst som Azure tillhandahåller i deras molntjänst och används främst till att bevara känslig information som inte bör expositionernas till allmänheten, till exempel lösenord eller andra accessnycklar. När man skapar en Key Vault ställer man in vad den ska bevara och vilken nivå av tillgång som ska vara tillåtet för de specifika användarna, så som publikt eller internt bland personen i arbetsgruppen som jobbar på ett projekt.

I vårt projekt kommer vi använda oss utav en Key Vault som säkerhetsåtgärd till att hålla "connectionstring" till databasen hemlig för allmänheten , då vi anser att ”connectsionstring” till databasen klassas som känslig information.

Vår lösning till att implementera Key Vault-tjänsten i vårt projekt började med att vi skapade en Key Vault i vår **Resource Group**. Efter skapandet av vår Key Vault behöver vi lägga till en Secret och dess Value kommer att bevara vår "connectionstring" till databasen.

![CreateSecret](https://github.com/PGBSNH19/spacepark-grupp-2-b02-b04/blob/Dokumentation/Dokumentation/Bilder/CreateSecret.PNG) 

För att vår applikation ska komma åt vår hemliga "connectionstring" behöver vi ha tillgång till vår Key Vault i koden, vår lösning var att vi skapade en "Azure Service Token Provider" klass som kommer att sköta hämtningen av värdet som vi deklarerade i vår Secret och returnera det som en sträng. Parametern secretName som vi skickar in till GetKeyVaultSecret är url:n till vår Key Vault, denna url används för att peka på vilken Key Vault i molnet som vi ska hämta information från.

##### Koden nedan är själva flödet på att hämta ut värdet i som är deklarerat i Key Vaultens Secret.

```c#
public class AzureKeyVaultService
{
    public string GetKeyVaultSecret(string secretName)
    {
        var azureServiceTokenProvider = new AzureServiceTokenProvider();
        var keyVault = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
        var secret = keyVault.GetSecretAsync(secretName).Result;
        return secret.Value;
    }
}
```
Den returnerade strängen kommer att innehålla databasens "connnectionstring" som vi använder oss utav för att koppla ihop databasen med vår applikationen i molnet.

##### Koden nedan är flödet på hur vi implementerar den hämtade Key Vault Secret i applikationen

```c#
    public AzureKeyVaultService _aKVService = new AzureKeyVaultService();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var azureDbCon = _aKVService.GetKeyVaultSecret("<Key Vault URL>");
        var builder = new ConfigurationBuilder();
        if (string.IsNullOrEmpty(azureDbCon))
        {
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var defaultConnectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(defaultConnectionString);

        }
        else
        {
            builder.Build();
            optionsBuilder.UseSqlServer(azureDbCon);
        }
    }
```


Koden ovan som vi har skapat kommer inte att returnera något värde från den Key Vault som vi pekar på om vi inte ger applikationen tillgång till själva Key Vaulten. Det vi behövde göra först var att publicera vår applikation i molnet och ge den en intern identitet i vår Resource Group, som vi gjorde på följande sätt: 

![ACIidentity](https://github.com/PGBSNH19/spacepark-grupp-2-b02-b04/blob/Dokumentation/Dokumentation/Bilder/ACIidentity.PNG) 



Efter att vi gick in på Identity så behövde vi sätta Status från Off till On, genom att sätta den till On skapades en identitet till vår publicerade applikation som genererades ut under Object ID. 



![KeyVaultAccess](https://github.com/PGBSNH19/spacepark-grupp-2-b02-b04/blob/Dokumentation/Dokumentation/Bilder/KeyVaultAccess.PNG) 

När vi väl har den interna identiteten till vår applikation kan vi lägga till den i vår Key Vault och ge den tillgång till dess innehåll.

![AddAccessPolicy](https://github.com/PGBSNH19/spacepark-grupp-2-b02-b04/blob/Dokumentation/Dokumentation/Bilder/AddAccessPolicy.PNG) 

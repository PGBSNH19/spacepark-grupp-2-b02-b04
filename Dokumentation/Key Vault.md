**Key Vault**

Key Vault är en säkerhetstjänst som Azure tillhandahåller i deras molntjänst och används främst till att bevara känslig information som inte bör expositionernas till allmänheten, till exempel lösenord eller andra accessnycklar. När man skapar en Key Vault ställer man in vad den ska bevara och vilken nivå av tillgång som ska vara tillåtet för de specifika användarna, så som publikt eller internt bland personen i arbetsgruppen som jobbar på ett projekt.

I vårt projekt kommer vi använda oss utav en Key Vault som säkerhetsåtgärd till att hålla "connectionstring" till databasen hemlig för allmänheten , då vi anser att ”connectsionstring” till databasen klassas som känslig information.
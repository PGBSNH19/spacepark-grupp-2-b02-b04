# Service Bus

Tanken var att vi skulle implementera en service bus, men vi insåg att så som vårt projekt ser ut i nuläget så finns det ingen anledning. Hade vi haft tid hade vi kunnat implementera en service bus för att hantera betalningar av användaren. 



Man skulle kunna säga att Azure Service Bus kan orkestrera olika program och tjänster med hjälp av *meddelanden*. Den skulle också kunna fungera som ett kö-system för användare om tjänsten plötsligt skulle få en stor ström av användare på samma gång. Detta kallas för service bus queues. 



Om vi hade haft tid skulle vi till exempel kunna göra det möjligt för användaren att få ett kvitto på sin betalning. I vårt fall hade vi fått presentera detta när användaren är inloggad på sidan, men i ett verkligt projekt så hade användaren eventuellt fått ett mail eller sms.  Vi skulle även här kunna lägga till funktionalitet för att påminna användaren om att tiden för parkeringsplatsen är på väg att ta slut.


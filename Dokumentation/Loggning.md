# Loggning

Vi har implementerat loggning i våra Repositories, som logger när en transaktion till databasen går igenom. 

````bash
var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            _logger.LogInformation($"Deleting object of type 		  {entity.GetType()}");
            _context.Set<T>().Remove(entity);
````

När något går fel så är det istället controllern som fångar upp en exception och skicka tillbaka en 404 statuskod med exceptionen inbakad.

##### Azure Application Insights

Genom att implementera ett enkelt kodstycke utifrån

https://chlee.co/how-to-add-application-insights-into-an-asp-net-core-web-and-console-app/


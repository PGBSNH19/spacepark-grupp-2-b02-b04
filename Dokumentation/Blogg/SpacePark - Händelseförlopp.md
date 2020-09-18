### Skapa en användare

- Lägga till en ny användare i databasen och kolla mot Swapi, samt ej lägga till vid dubblett.****

### Logga in användare

- Kolla om användaren finns i databasen OM Sant: Logga in. ANNARS: Tillbaka till inloggningen.

### Inloggad 

- Kolla om det finns ett skepp parkerat i databasen. OM Sant: Skapa en checkout miljö. ANNARS: Hämta skeppslista från Swapi och ge möjlighet att välja skepp för check in. 
- Därefter tillbaka till Startskärmen.



### Förloppet mot API

**Hämta en person**

- Kolla om **SpaceShipID** INTE LIKA med NULL:

  - OM Sant:
    Checka ut skeppet och ta bort **SpaceShipID** från Parkinglot.

  		- OM Falskt:
  			Hämta lediga parkeringsplatser
  			- OM Sant:
  					Hämta skepp från Swapi och användaren väljer skepp.
  					Parkera skeppet i en Parkinglot(Lägg till SpaceShip => Koppla till Personen => Lägg till i Parkinglot).
  			- OM Falskt:
  					Returnera meddelande: Det finns inga lediga platser.
### Skapa en användare

- Lägga till en ny användare i databasen och kolla mot Swapi, samt ej lägga till vid dubblett.****
  - *PostPerson(string personName)*

### Logga in användare

- Kolla om användaren finns i databasen 
  - OM Sant: Logga in. 
  - ANNARS: Tillbaka till inloggningen.
- Kolla om användaren finns i databasen OM Sant: Logga in. ANNARS: Tillbaka till inloggningen.
- Hämta skeppslista från Swapi och ge möjlighet att välja skepp för check in. 
  - *GetPersonByName(string name)*
  - *PostSpaceShip(string spaceShipName, int personId)*
  - *PutParkinglotBySpaceShipId*(int spaceShipId)
### Inloggad (med skepp parkerat)

- Kolla om det finns ett skepp parkerat i databasen. 
- Visa alla tillgängliga skepp för personen
  - *PutParkingLotBySpaceShipId(int spaceShipId)*
  - *DeletePerson(int personId)*
  - *DeleteSpaceShip(int spaceShipId)*
- Bekräftelse på utcheckning med hjälp av statuskod.



### Funktionalitet

SpaceShips som ligger i databasen är endast SpaceShips som är parkerade.

Valideringen är för SpaceShips och Persons sker genom att validera att namnet finns i Star Wars universumet.

När en Person hämtas från API:et ska SpaceShip inkluderas.

  		- OM Falskt:

    			Hämta lediga parkeringsplatser
    			- OM Sant:
      					Hämta skepp från Swapi och användaren väljer skepp.
      					Parkera skeppet i en Parkinglot(Lägg till SpaceShip => Koppla till Personen => Lägg till i Parkinglot).
      			- OM Falskt:
        					Returnera meddelande: Det finns inga lediga platser.



### Api-Anrop

##### GetPersonByName

##### GetSpaceShipByPersonName

##### PutParklinglotBySpaceShipId

##### PostPerson

##### PostSpaceShip

##### DeletePerson

##### DeleteSpaceShip

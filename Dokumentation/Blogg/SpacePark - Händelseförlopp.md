### Skapa en användare

- Lägga till en ny användare i databasen och kolla mot Swapi, samt ej lägga till vid dubblett.****

### Logga in användare

- Kolla om användaren finns i databasen 
  - OM Sant: Logga in. 
  - ANNARS: Tillbaka till inloggningen.

### Inloggad 

- Kolla om det finns ett skepp parkerat i databasen. 
  - OM Sant: Skapa en check-out miljö. 
  - ANNARS: Kontrollera om det finns parkering ledig.
    - OM Sant:
      -  Skapa en check-in miljö och ange Starshipnamn och kontrollera det mot Swapi. 
        - OM Sant:
          - Parkera Starship
        - ANNARS
          - Återgå till inmatning av Starship.
    - ANNARS
      - Tillbaka till Inloggningssskärmen.
- Därefter tillbaka till Startskärmen.



### Funktionalitet

SpaceShips som ligger i databasen är endast SpaceShips som är parkerade.

Valideringen är för SpaceShips och Persons sker genom att validera att namnet finns i Star Wars universumet.

När en Person hämtas från API:et ska SpaceShip inkluderas.


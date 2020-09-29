## I Azure:

- Resource Group
- Container registry
- Pushat första image
- Skapat server och databas
- Skapat pipeline för backend

## Backend:

- Lagt in kodbas, refaktorerat
- fixat alla modeller och gjort första migration
- Skapat en några post- och get-metoder i alla controllers
- Skapat de flesta metoder som behövs för att kunna använda API

## Frontend:

- Använt razor pages
- Skapat enklare koppling emot API
- Kan lägga till en ny "användare" i databasen via frontend -> API -> databas
- Hämta data ur databasen via en befintlig användare som skapar upp ett objekt i razor pages

Var vi står just nu:

- Vi behöver lösa hur inloggningen ska fungera mer exakt... om vi ska använda cookies och sessioner.
- se till att alla skepp som är parkerade syns i frontend
- att det finns en check out / log out-knapp
- Att vi har CI för Frontend
- Att både front- och backend körs från azure och inte bara lokalt




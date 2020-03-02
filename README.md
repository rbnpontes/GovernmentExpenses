# Government Expenses
## How To Build (Server)
1. Clone Repository
2. Go to folder "GovernmentExpenses\GovernmentExpenses-Back"
3. Open Solution with Visual Studio 2017 or newer
4. Build Solution
5. Run Application and test if running at http://localhost:5001/swagger or http://localhost:5001/api/expenses.
## Disabling Swagger
1. In Solution go to "GovernmentExpenses-Back" project
2. Right click and goto "Properties"
3. Click on "Build" and remove "SWAGGER" value from "Conditional compilation symbols".
## Use Google Spreadsheet Database
Currently the Application uses the db.json file located in the "Artifacts" folder as a Database, but the application can use Google Spreadsheets as a Data Source.
For enable this feature, see steps:
1. At solution, goto GovernmentExpenses.Expenses project
2. Right click and goto "Properties"
3. Click on "Build" and add "SPREADSHEET" on input "Conditional compilation symbols".
## How to Build (Front)
Make sure you use most recently version of NodeJS and Angular CLI.
This project uses Angular Ivy
1. Clone repo and goto to folder "GovernmentExpenses\government-expenses-front"
2. Run `ng build --prod`
## Running (Front)
1. At "GovernmentExpenses\government-expenses-front" folder
2. Run `ng run` command.

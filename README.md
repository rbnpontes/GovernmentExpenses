
# Government Expenses
Demo: https://governmentexpenses.firebaseapp.com/dashboard

Server API: https://governmentexpenses.azurewebsites.net/swagger
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
## About Server Arch
The architecture of the project was designed in a plugin system, basically when the application is started the system will scan the "Business" directory looking for DLL's, all the DLL's that the system finds will be automatically loaded into the system.
After this step, the system will search for its entry point where it will be instantiated and executed, this is where the developer configures his entire environment.
Instead of the sole responsibility for the main program, all responsibility for assigning "controllers and services" will be the responsibility of the DLL's.
So a DLL can have its "Mini System" something like Micro Service, this approach facilitates maintenance and does not hurt the SOLID paradigm.
If another DLL needs to communicate with another DLL, just by dependency injection, pass the interface referring to the DLL's service.
Example:
Let's say I have two DLL's, one is called "Books" and the other "Payments" and for some reason "Payments" needs to consult a particular book.
There are several ways to do this information exchange, but the easiest is, in the "Payments" service builder, I would add the interface related to the "Books" service and call a function to consult the books.
### Why not use a Database like MySql, MongoDB instead Local
Believe me, I asked myself that same question while I was developing. **(Why not use a lib as an entity framework to manage my repository data?)**
Much of the development was related to business rule and not to data management. So I could have used a framework like Entity Framework, but I wanted to present a more practical approach regarding decoupling. I am not saying that if you do it with entity framework there would be no decoupling, but the goal is to present something simple and "sophisticated" SOLID fundamentals on the system.
Currently there are two simple implementations of repositories, the first implementation I directly modify a json file where I do all the storage on the HD, while the second implementation I use a spreadsheet hosted on Google Drive for data management.
And if I wanted to implement it in a database or something like that I would not take more than 1 day to do this implementation.

## How to Build (Front)
Make sure you use most recently version of NodeJS and Angular CLI.
This project uses Angular Ivy
1. Clone repo and goto to folder "GovernmentExpenses\government-expenses-front"
2. Run `ng build --prod`
## Running (Front)
1. At "GovernmentExpenses\government-expenses-front" folder
2. Run `ng run` command.

# Eric's Toy Store

A simple Toy Store backend and frontend client.

The backend is a .NET Core solution targeting .NET 5.0. The solution contains two projects:

- A Web API project to handle CRUD operations against an Sqlite database.
- A Tests project that uses Xunit to perform some basic unit tests.

The Sqlite database is accessed trough Entity Framework Core using code first.

The frontend is a React application that lists toys in the toy store and lets you add/edit/delete toys from the store.

## To Setup and Run the Store

### Prerequisites:

- Visual Studio 2019 Community (https://visualstudio.microsoft.com/downloads/)

- .NET Core 5.0 SDK (https://dotnet.microsoft.com/download)
If you have .NET Core installed, use the 'dotnet --info' command to determine which SDK you're using.

- Docker Community Edition (https://www.docker.com/products/docker-desktop)
- Node.js (https://nodejs.org/en/download/)

## Running the Toy Store Backend

1. Clone the repository from GitHub.

2. Use Visual Studio to open the .sln file in the top-level folder.

3. Make WebAPI the startup project by right-clicking on it and selecting 'Set as Startup Project'.

4. Select either IIS Express or Docker from the dropdown in the toolbar (depending on whether you want to run it in IIS Express or inside a docker container).

5. Press the Green arrow in the toolbar to start the backend.

The application will open in a new browser window and you will see the swagger documentation for the API.

You can interact with the database through swagger.

When running the frontend, leave the backend running so the frontend has something to communicate with.

## Running the Store Frontend

1. Move to the client directory in a terminal window or Power Shell.

2. Enter 'npm i' to install all the dependent packages for the client application.

3. After the node packages are installed, you can run the frontend by entering 'npm run start'.

The React application will run in a browser window. Select a toy in the table to edit/delete it. Click the 'Add Toy' button to add a new toy.

## Running the Unit Tests

1. In Visual Studio, right-click the UnitTests project and select Run Tests.

The Test Explorer window will appear and the tests will run. Green check marks indicate a successful test.

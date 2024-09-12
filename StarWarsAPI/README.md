A simple API that simulates a registry of inhabitants of the planet Tatooine conducted by the Galactic Empire.
The inhabitants have the following attributes:
- ID
- Name
- Planet of origin
- Species
- Is rebel or not

Additionally, planets and species are also registered with an ID and a name.

## Endpoints
### Habitants
#### GET /Habitants
Returns a list of all Tatooine inhabitants.

#### GET /Habitants/{name}
Returns a specific inhabitant.

#### POST /Habitants
Creates a new inhabitant.

#### GET /Habitants/Rebels
Returns a list of all rebel inhabitants.

### Planets
#### GET /Planets
Returns a list of all registered planets.

#### POST /Planets
Creates a new planet.

### Species
#### GET /Species
Returns a list of all registered species.

#### POST /Species
Creates a new species.


## Using the API locally
To run this API on your machine, you need to install these technologies:
- [Visual Studio](https://visualstudio.microsoft.com/downloads/)
- [.NET](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

Once installed, you need to clone the repository.
To configure the server and the SQL Server database from Management Studio if you haven't done it before:
- Connect to your server from Windows
- Right-click on the server > Properties > Security
- Select the option SQL Server and Windows Authentication mode
- Click OK
- Right-click on the server > Restart
- Reconnect from Windows
- In the server folders > Security > Logins
- Right-click on the one named **sa** > Properties
- Go to the Status tab and select the Grant and Enabled options
- Go to the General tab, change the password, and uncheck Enforce password policy
- Click OK
- If you now try to connect with the **sa** user and the password, it should work

Then you have two options:
1. Create your own database, run the `script.sql` in the database, and modify the `appsettings.json` file with the connection string.
2. To avoid changing the connection string in `appsettings.json`, create a new database named **encamina** and run the `script.sql` inside it.

## Use LocalDB
- In `appsettings.json`, write a new connection string: Server=(localdb)\\MSSQLLocalDB;Integrated Security=True
- Change the connection string in `Program.cs` for the new one
- Install the NuGet **Microsoft.EntityFrameworkCore.Tools**
- Open Tools > NuGet Package Manager > Package Manager Console
- Execute the command **Add-Migration InitialCreate**
- Execute the command **Update-Database**
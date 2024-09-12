## Database-first scaffolding
To create the database-first scaffolding, you need to follow these steps:
- Install these NuGet packages:
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.EntityFrameworkCore.Tools
- Navigate to Tools > NuGet Package Manager > Package Manager Console
- Execute the command:
**Scaffold-DbContext "YourConnectionString" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context StarWarsContext -ContextDir Data -Namespace StarWarsAPI.DatabaseFirst.Models -DataAnnotations**
- Update Program.cs to use the new DbContext
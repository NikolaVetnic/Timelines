Migrations are to be added using the `dotnet ef migrations` tool while positioned in the `Infrastructure` layer of a particular module. In case of Nodes module, the working directory should be `Modules/Nodes/Nodes.Infrastructure`, and then the following is executed:

```shell
dotnet ef migrations add MigrationName -o ./Data/Migrations --startup-project ../../../Applications/Core/Core.Api
```

#migration #migrations #infrastructure #dotnet #ef 
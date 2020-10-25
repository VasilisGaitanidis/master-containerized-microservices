# Data Migration

```bash
$ dotnet ef migrations add InitialMigration
$ dotnet ef migrations database update
```

```bash
$ dotnet ef migrations script -o .\Scripts\InitialMigration.sql
```
# Data Migration

```bash
$ dotnet ef migrations add InitialMigration
$ dotnet ef database update
```

```bash
$ dotnet ef migrations script -o .\Scripts\Migration.sql
```
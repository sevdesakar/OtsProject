create migration file 
    -  dotnet ef migrations add MoneyTransfer --context OtsMsSqlDbContext --output-dir  Migrations/MsSql        
    -  dotnet ef migrations add MoneyTransfer --context OtsPostgreSqlDbContext --output-dir  Migrations/PostgreSql  

update database
    -  dotnet ef database update --context OtsMsSqlDbContext
    -  dotnet ef database update --context OtsPostgreSqlDbContext



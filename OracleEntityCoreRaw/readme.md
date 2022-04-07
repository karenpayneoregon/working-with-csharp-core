# Configuring your database



Path: S:\applications\VisualStudioTraining\OracleNorthWind


- Each database 1 through 15 are named NORTHWIND**01** with the last two digits pointing to a specific database
  - This needs to change in the code block below

```csharp
public partial class NorthwindContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("NORTHWIND01").HasAnnotation("Relational:Collation", "USING_NLS_COMP");
```

And also in appsetting.json

```json
{
  "ConnectionStrings": {
    "DatabaseConnection": "Data Source=aix-gridctl.emp.state.or.us:1521/northwind_demo;Persist Security Info=True;Enlist=false;Pooling=true;Statement Cache Size=10;User ID=northwind01;Password=!northwind!DEMO!;"
  }
}
```

# Developer List

| Developer | index |
| :--- | :--- |
| Karen Payne | 01 |
| Bill Rickman | 02 |
| Vince Buchheit | 03 |
| Lisa Smith | 04 |
| Garen Porter | 05 |
| Bick VU | 06 |
| Yelana Galante | 07 |
| Amelia Dinh | 08 |
| Francis Guarnes | 09 |
| Dino Guevara | 10 |
| Lindon Rose | 11 |
| Charlotte Williams | 12 |
| James Bennett | 13 |
| Reserved | 14 |
| Reserved | 15 |


## SQL-Server

The database is defined solely in appsettings.json and uses windows authenication but has the ability to use a defined user with roles.


# User Id and Password

All databases use the same `user id` and `password` in `appsettings.json`

---

# Scaffolding command

```
Scaffold-DbContext "Data Source=aix-gridctl.emp.state.or.us:1521/northwind_demo;Persist Security Info=True;Enlist=false;Pooling=true;Statement Cache Size=10;User ID=northwind01;Password=!northwind!DEMO!;" -Provider Oracle.EntityFrameworkCore -OutputDir Models  -Context NorthContext  -v -f  -project EntityCoreRaw -startupproject EntityCoreRaw -ContextDir Data -t "CATEGORIES","CUSTOMERS","EMPLOYEES","ORDERS","ORDER_DETAILS","PRODUCTS","SHIPPERS","SUPPLIERS"	
```
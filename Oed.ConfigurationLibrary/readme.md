# About

Simple example for obtaining connection strings.

Get current connection by `ActiveEnvironment`

```csharp
ConnectionString()
```



**appsettings.json**

```json
{
  "ConnectionsConfiguration": {
    "ActiveEnvironment": "Production",
    "Development": "Dev connection string goes here",
    "Stage": "Stage connection string goes here",
    "Production": "Prod connection string goes here"
  }
}
```

# Notes

For web applications we use the same logic, read a json file but using an environment variable like `ASPNETCORE_ENVIRONMENT`.

![image](../assets/systemEnvironments.png)

[Use multiple environments in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-5.0)



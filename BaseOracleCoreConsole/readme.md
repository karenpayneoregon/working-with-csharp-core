# About

- Setup for working with `Oracle` data provider
- Provides an appsettings file setup for database connections for development, staging and production along with two settings for title of the project and a second for showing the application regular or full screen.

```json
{
  "ConnectionsConfiguration": {
    "ActiveEnvironment": "Development",
    "Development": "Dev connection string goes here",
    "Stage": "Stage connection string goes here",
    "Production": "Prod connection string goes here"
  },
  "Settings": {
    "Title": "Code sample",
    "FullScreen":  "true" 
  } 
}
```

Just enough code to show getting the above settings.

```csharp
public class Operations
{
    public static string ConnectionString() => ConfigurationHelper.ConnectionString();

    public static Settings ApplicationSettings() => ConfigurationHelper.ApplicationSettings();
}
```
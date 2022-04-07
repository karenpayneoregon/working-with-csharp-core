# About


Visual for indexing

```csharp
private static string[] SomeOregonCities =>
    new[]
    {
        //              index from start    index from end                
        "Adams",        // 0                ^11
        "Albany",       // 1                ^10
        "Aloha",        // 2                ^9
        "Arlington",    // 3                ^8
        "Ashland",      // 4                ^7
        "Astoria",      // 5                ^6
        "Burns",        // 6                ^5
        "Jacksonville", // 7                ^4
        "Salem",        // 8                ^3
        "Portland",     // 9                ^2
        "Bend"          // 10               ^1 (or array length)
    };
```
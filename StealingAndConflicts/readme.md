# About

The idea here is a developer writes a language extension (in `.NET Core 5`) to chunk a string by a specific size.

```csharp
public static List<List<T>> Chunk<T>(this List<T> source, int chunkSize)
    => source
        .Select((value, index) => new { Index = index, Value = value })
        .GroupBy(item => item.Index / chunkSize)
        .Select(grp => grp.Select(item => item.Value).ToList())
        .ToList();
```

Then realizes there is a `Chunk` extension in `.NET Core 6` and drops the Microsoft soure code into their project.

If the developer keeps both there is an ambiguous reference to both and to fix this they need to write code that excludes using ` System.Linq` which means no `Select` method.

The real fix is to get rid of the first method and keep the second/new extension.

It may appear simple, get rid of the first extension method but novice developers tend to not think this is an option believe it or not it’s true.

How does a developer learn about the new extension method? Spend time examining source code in Microsoft's GitHub repository. The benefit is when moving from .NET Core 5 to a higher .NET Core Framework is not having to deal with conflicts between the older and newer extension method(s).

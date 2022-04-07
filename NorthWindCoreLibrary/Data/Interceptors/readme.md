# Interceptors

Entity Framework Core (EF Core) [interceptors](https://docs.microsoft.com/en-us/ef/core/logging-events-diagnostics/interceptors) enable interception, modification, and/or suppression of EF Core operations. This includes low-level database operations such as executing a command, as well as higher-level operations, such as calls to SaveChanges.

Interceptors are different from logging and diagnostics in that they allow modification or suppression of the operation being intercepted. Simple logging or [Microsoft.Extensions.Logging](https://docs.microsoft.com/en-us/ef/core/logging-events-diagnostics/extensions-logging?tabs=v3) are better choices for logging.

Interceptors are registered per DbContext instance when the context is configured. Use a [diagnostic listener](https://docs.microsoft.com/en-us/ef/core/logging-events-diagnostics/diagnostic-listeners) to get the same information but for all DbContext instances in the process.

While interceptors give us the ability to do anything within the context of an executing EF Core call, it also means WE CAN DO ANYTHING! Let’s look at some of the tasks we could do while a database query is happening:

- Store event information into audit tables
- Call a third party service for additional information
- Call a web service for authorization information

Each of these possibilities provides additional opportunities to break transactional guarantees and add performance overhead to each EF Core query. We need to consider each interceptor carefully and its impact on the overall user experience. More so, we need to consider failure states and whether any action that we place within an interceptor must be part of an overall transaction.

Each interceptor and DbContext share a lifetime, so thoughtful consideration needs to happen when putting functionality into an interceptor implementation.
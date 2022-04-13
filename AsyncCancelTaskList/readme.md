# Microsoft way

[Cancel a list of tasks (C#)](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/cancel-an-async-task-or-a-list-of-tasks)

You can cancel an async console application if you don't want to wait for it to finish. By following the example in this topic, you can add a cancellation to an application that downloads the contents of a list of websites. You can cancel many tasks by associating the [CancellationTokenSource](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource?view=net-5.0) instance with each task. 

# Karen's way

Do it in a windows form to get the real expereince and separation of concerns.

Many code samples from Microsoft and other entities will use a simple console project to demonstrate specific code.

With this example the code may run and perform properly but does not actually let you know if the asynchronous aspect is working and then there is notifications done via writing to the console which is not going to be the case with web or desktop solutions.

So I took Microsoft’s console code and wrapped it into a windows form with better separation of concerns along with a better example of cancelling a task. What I did not add with a time out for the task via the CancellationTokenSource which really is not appropriate for this example.


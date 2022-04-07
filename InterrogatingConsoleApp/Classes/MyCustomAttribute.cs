using System;

namespace InterrogatingConsoleApp.Classes
{
    public class MyCustomAttribute : Attribute
    {
        public string SomeProperty { get; set; }
    }
}

using InterrogatingConsoleApp.Interfaces;

namespace InterrogatingConsoleApp.Classes
{
    public class Person : IBase
    {
        public int Identifier { get; set; }
        public int Id => Identifier;
    }
}

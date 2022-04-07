namespace InterrogatingConsoleApp.Classes
{
    public class Customer
    {
        [MyCustom(SomeProperty = "Some value")]
        public string DummyMethod()
        {
            return "";
        }
    }
}
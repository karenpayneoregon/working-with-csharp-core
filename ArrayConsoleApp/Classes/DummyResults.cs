namespace ArrayConsoleApp.Classes
{
    public class DummyResults
    {
        public int Value { get; }
        public int Index { get; }

        public DummyResults(int value, int index)
        {
            Value = value;
            Index = index;
        }

        public override string ToString()
        {
            return $"{{ Value = {Value}, Index = {Index} }}";
        }
    }
}
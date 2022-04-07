namespace ArrayConsoleApp.Models
{
    public class IndexItem
    {
        public int Index { get; set; }
        public string Text { get; set; }

        public override string ToString() => $"{Index,3:D2} {Text}";
    }
}
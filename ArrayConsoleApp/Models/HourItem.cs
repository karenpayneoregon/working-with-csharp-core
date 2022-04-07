using System;

namespace ArrayConsoleApp.Models
{
    public class HourItem
    {
        public string Text { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public override string ToString() => Text;

    }
}

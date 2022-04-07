namespace Ranges_examples.Models
{
    public class CityIndexer
    {
        public int Index { get; }
        public string CityName { get; }

        public CityIndexer(int index, string cityName)
        {
            Index = index;
            CityName = cityName;
        }

        public override string ToString() => $"{{ Index = {Index}, CityName = {CityName} }}";
    }
}
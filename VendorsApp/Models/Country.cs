
using VendorsApp.Interfaces;

namespace VendorsApp.Models
{
    public class Country : IBaseReadOnly
    {
        public int Id { get;  }
        public string Name { get;  }
        public override string ToString() => Name;

        public Country(int identifier, string name)
        {
            Id = identifier;
            Name = name;
        }
    }
}

namespace VendorsApp.Models
{
    public class Vendor
    {
        public int Id { get;  }
        public string AccountNumber { get; init; }
        public string DisplayName { get; init; }
        public int CreditRating { get; init; }

        public override string ToString() => DisplayName;

        public Vendor(int id, string accountNumber, string displayName, int creditRating)
        {
            Id = id;
            AccountNumber = accountNumber;
            DisplayName = displayName;
            CreditRating = creditRating;
        }
    }
}

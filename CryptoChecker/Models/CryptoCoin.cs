namespace CryptoChecker.Models
{
    public class CryptoCoin
    {
        public string Id { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public double Current_Price { get; set; }
        public long Market_Cap { get; set; }
        public int Market_Cap_Rank { get; set; }
        public long Total_Volume { get; set; }
    }
}

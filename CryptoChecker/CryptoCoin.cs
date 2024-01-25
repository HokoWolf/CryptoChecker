namespace CryptoChecker
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

		public override string ToString()
		{
			return $"{Id} - {Symbol} - {Name} - {Image} - {Current_Price} - {Market_Cap} - {Market_Cap_Rank} - {Total_Volume}";
		}
	}
}

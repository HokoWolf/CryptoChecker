using System.Windows;
using System.Net.Http;
using Newtonsoft.Json;
using CryptoChecker.Models;

namespace CryptoChecker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		public string? Result { get; set; }
		public List<CryptoCoin> CoinObjectList { get; set; } = [];
		public List<string> CoinsList { get; set; } = [];

		public MainWindow()
		{
			InitializeComponent();
			Loaded += async (sender, e) => await FetchData();
			DataContext = this;
		}

		private async Task FetchData()
		{
			using HttpClient client = new();

			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.75 Safari/537.36");

			var response = await client.GetAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&per_page=10&page=1&precision=full");

			if (response.IsSuccessStatusCode)
			{
				Result = await response.Content.ReadAsStringAsync();
				CoinObjectList = JsonConvert.DeserializeObject<List<CryptoCoin>>(Result)!;
				CoinsList.AddRange(CoinObjectList.Select(x => x.ToString()));
			}
			else
			{
				Result = $"Error: {response.StatusCode}";
			}
		}
	}
}
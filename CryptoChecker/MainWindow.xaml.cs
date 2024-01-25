using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;

namespace CryptoChecker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public string? Result { get; set; }
		public List<CryptoCoin> CoinObjectList { get; set; } = new();
		public List<string> CoinsList { get; set; } = new();

		public MainWindow()
		{
			InitializeComponent();
			Loaded += async (sender, e) => await FetchData();
		}

		private async Task FetchData()
		{
			using (HttpClient client = new())
			{
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

				DataContext = this;
			}
		}
	}
}
using CryptoChecker.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

namespace CryptoChecker.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<CryptoCoin> _coinsList = new ObservableCollection<CryptoCoin>();
		public ObservableCollection<CryptoCoin> CoinsList
		{
			get => _coinsList;
			set
			{
				_coinsList = value;
				OnPropertyChanged(nameof(CoinsList));
			}
		}

		public async Task FetchData()
		{
			using (HttpClient client = new())
			{
				client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.75 Safari/537.36");

				var response = await client.GetAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&per_page=10&page=1&precision=full");

				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					CoinsList = new ObservableCollection<CryptoCoin>(JsonConvert.DeserializeObject<List<CryptoCoin>>(result)!);
				}
			}
		}

		// INotifyPropertyChanged
		public event PropertyChangedEventHandler? PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

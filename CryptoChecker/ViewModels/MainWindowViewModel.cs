using CryptoChecker.Models;
using CryptoChecker.MVVM;
using CryptoChecker.Views.Pages;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CryptoChecker.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private Page? _mainFrameContent;
		public Page? MainFrameContent
		{
			get => _mainFrameContent;
			set
			{	
				_mainFrameContent = value;
				OnPropertyChanged(nameof(MainFrameContent));
			}
		}

		private ObservableCollection<CryptoCoin> _coinsList = [];
		public ObservableCollection<CryptoCoin> CoinsList
		{
			get => _coinsList;
			set
			{
				_coinsList = value;
				OnPropertyChanged(nameof(CoinsList));
			}
		}

		public MainWindowViewModel()
		{
			MainFrameContent = new MainPage();
			OpenCoinPageCommand = new RelayCommand(OpenCoinPage);
		}

		public async Task FetchData()
		{
			using HttpClient client = new();

			client.DefaultRequestHeaders.Add("User-Agent",
				((App)Application.Current).AppSettings.HttpClient.UserAgent);

			var api = ((App)Application.Current).AppSettings.GeckoCoinApi;
			var response = await client.GetAsync(Path.Combine(api.MainApiUrl, api.TopCoinsUrl));

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				CoinsList = new(JsonConvert.DeserializeObject<List<CryptoCoin>>(result)!
					.OrderBy(coin => coin.Market_Cap_Rank));

				foreach (var coin in CoinsList)
				{
					coin.Symbol = coin.Symbol.ToUpper();

					coin.Price_Change_Percentage_1h_In_Currency =
						Math.Round(coin.Price_Change_Percentage_1h_In_Currency, 1);

					coin.Price_Change_Percentage_24h_In_Currency =
						Math.Round(coin.Price_Change_Percentage_24h_In_Currency, 1);

					coin.Price_Change_Percentage_7d_In_Currency =
						Math.Round(coin.Price_Change_Percentage_7d_In_Currency, 1);
				}
			}
		}

		// Commands
		public ICommand OpenCoinPageCommand { get; }

		private void OpenCoinPage(object? parameter)
		{
			// TODO: Make OpenCoinPage command
		}

		// INotifyPropertyChanged
		public event PropertyChangedEventHandler? PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

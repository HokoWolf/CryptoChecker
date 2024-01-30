using System.Windows;
using CryptoChecker.ViewModels;
using CryptoChecker.Views.Pages;

namespace CryptoChecker.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Loaded += OnLoaded;
		}

		private async void OnLoaded(object sender, RoutedEventArgs e)
		{
			var viewModel = new MainWindowViewModel();
			DataContext = viewModel;
			await viewModel.FetchData();
		}
	}
}
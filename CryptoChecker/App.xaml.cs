using CryptoChecker.Config;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.IO;
using System.Windows;

namespace CryptoChecker
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public IConfiguration Configuration { get; private set; } = null!;
		public AppSettings AppSettings { get; private set; } = null!;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			ConfigureAppSettings();
		}

		private void ConfigureAppSettings()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

			Configuration = builder.Build();

			AppSettings = new AppSettings();
			Configuration.GetSection("HttpClient").Bind(AppSettings.HttpClient);
			Configuration.GetSection("GeckoCoinApi").Bind(AppSettings.GeckoCoinApi);
		}
	}
}

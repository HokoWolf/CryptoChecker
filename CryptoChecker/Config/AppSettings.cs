namespace CryptoChecker.Config
{
	public class AppSettings
	{
		public HttpClientSettings HttpClient { get; set; } = new();
		public GeckoCoinApiSettings GeckoCoinApi { get; set; } = new();
	}
}

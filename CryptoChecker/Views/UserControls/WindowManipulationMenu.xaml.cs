using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CryptoChecker.Views.UserControls
{
	/// <summary>
	/// Interaction logic for WindowManipulationMenu.xaml
	/// </summary>
	public partial class WindowManipulationMenu : UserControl
	{
		public WindowManipulationMenu()
		{
			InitializeComponent();
		}

		private void btMinimize_Click(object sender, RoutedEventArgs e)
		{
			if (Window.GetWindow(this) is Window window)
			{
				window.WindowState = WindowState.Minimized;
			}
		}

		private void btMaximize_Click(object sender, RoutedEventArgs e)
		{
			if (Window.GetWindow(this) is Window window)
			{
				window.WindowState = (window.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
			}
		}

		private void btExit_Click(object sender, RoutedEventArgs e)
		{
			if (Window.GetWindow(this) is Window window)
			{
				window.Close();
			}
		}

		private void ManipulationMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				btMaximize_Click(this, new RoutedEventArgs());
				return;
			}
			else if (Window.GetWindow(this).WindowState == WindowState.Maximized)
			{
				double x_prop = e.GetPosition(Window.GetWindow(this)).X / Window.GetWindow(this).Width;
				double y_prop = e.GetPosition(Window.GetWindow(this)).Y / Window.GetWindow(this).Height;

				btMaximize_Click(this, new RoutedEventArgs());

				Window.GetWindow(this).Left = e.GetPosition(Window.GetWindow(this)).X - (Window.GetWindow(this).Width * x_prop);
				Window.GetWindow(this).Top = e.GetPosition(Window.GetWindow(this)).Y - (Window.GetWindow(this).Height * y_prop);
			}

			Window.GetWindow(this).DragMove();
		}
	}
}

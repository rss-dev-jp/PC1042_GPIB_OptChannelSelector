using RssDev.Common.ApplicationUtility;
using RssDev.Project_Code.Defines;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RssDev.Project_Code.Windows
{

	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			Title = $"{ProgramDefine.PROGRAM_TITLE} Ver.{VersionInfo.GetVersion()}";
			DataContext = new MainWindowViewModel();
		}

		/// <summary>
		/// Window.Closingイベント
		/// </summary>
		/// <param name="sender">Window</param>
		/// <param name="e">キャンセルイベントデータ</param>
		private void OnWindowClosing(object sender, CancelEventArgs e)
		{
			if (DataContext is MainWindowViewModel viewModel)
			{
				viewModel.Shutdown();
			}
		}

		/// <summary>
		/// Button.Clickイベント
		/// </summary>
		/// <param name="sender">Button</param>
		/// <param name="e">イベントデータ</param>
		private void OnButtonClick(object sender, RoutedEventArgs e)
		{

			if (sender is Button button
				&& button.Tag != null
				&& DataContext is MainWindowViewModel viewModel)
			{

				var tag = button.Tag.ToString().ToLower();
				switch (tag)
				{

					case "connect":
						viewModel.Connect();
						break;

					case "disconnect":
						viewModel.Disconnect();
						break;

					case "channel":
						viewModel.SendChangeChannel();
						break;

					case "status":
						viewModel.SendCheckStatus();
						break;

					case "model":
						viewModel.SendCheckModel();
						break;

					case "send":
						viewModel.SendInputCommand();
						break;

					case "log":
						viewModel.ClearLog();
						break;

					case "exit":
						this.Close();
						break;

					default:
						throw new Exception($"{nameof(OnButtonClick)}():{tag}未実装");

				}

			}

		}

		/// <summary>
		/// TextBox.KeyDownイベント
		/// </summary>
		/// <param name="sender">TextBox</param>
		/// <param name="e">キーイベントデータ</param>
		private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
		{

			if (sender is TextBox textBox
				&& DataContext is MainWindowViewModel viewModel)
			{

				switch (e.Key)
				{

					case Key.Return:
						viewModel.SendInputCommand();
						break;

					default:
						break;

				}

			}

		}

		/// <summary>
		/// TextBox.TextChangedイベント
		/// </summary>
		/// <param name="sender">TextBox</param>
		/// <param name="e">Text変更イベントデータ</param>
		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{

			if (sender is TextBox textBox
				&& !string.IsNullOrEmpty(textBox.Text))
			{
				textBox.ScrollToEnd();
			}

		}

	}

}

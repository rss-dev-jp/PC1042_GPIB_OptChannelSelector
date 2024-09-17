using RssDev.Common.ApplicationUtility;
using RssDev.Project_Code.Threadings;
using RssDev.RuntimeLog;
using System.Windows;
using RssDev.Project_Code.Defines;
using RssDev.Project_Code.IniFiles;
using RssDev.Project_Code.Windows;

namespace RssDev
{

	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{

		/// <summary>
		/// プログラム起動
		/// </summary>
		/// <param name="e">起動イベント引数データ</param>
		protected override void OnStartup(StartupEventArgs e)
		{

			base.OnStartup(e);

			// 所有権の要求
			if (MutexManager.Request())
			{

				// ログ出力
				RuntimeLogger.Instance.Add(RuntimeLogger.Type.START, $"Program version " + VersionInfo.GetVersion());

				// iniファイル読込
				IniFileIF.Instance.Load();

				// 画面表示
				new MainWindow().ShowDialog();

			}
			// 多重起動のため終了
			else
			{
				var message = "多重起動禁止";
				RuntimeLogger.Instance.Add(RuntimeLogger.Type.EXCEPTION, message);
				MessageBoxEx.Show(message, $"{ProgramDefine.PROGRAM_TITLE} Ver.{VersionInfo.GetVersion()}", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				Application.Current.Shutdown();
			}

		}

		/// <summary>
		/// プログラム終了
		/// </summary>
		/// <param name="e">終了イベントデータ</param>
		protected override void OnExit(ExitEventArgs e)
		{

			base.OnExit(e);

			// 所有権を確保している
			if (MutexManager.IsHandle)
			{

				// iniファイル保存
				IniFileIF.Instance.Save();

				// ログ出力
				RuntimeLogger.Instance.Add(RuntimeLogger.Type.CLOSE, "");

			}

			// 所有権の解放
			MutexManager.Release();

		}

	}

}

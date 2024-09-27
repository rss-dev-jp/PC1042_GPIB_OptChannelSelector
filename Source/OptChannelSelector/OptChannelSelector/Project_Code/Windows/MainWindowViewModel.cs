using RssDev.Common.ApplicationUtility;
using RssDev.Common.ModelUtility;
using RssDev.Project_Code.Defines;
using RssDev.Project_Code.Defines.Enums;
using RssDev.Project_Code.GpibComms;
using RssDev.Project_Code.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace RssDev.Project_Code.Windows
{

	/// <summary>
	/// メイン画面ViewModel
	/// </summary>
	public class MainWindowViewModel : BindableBase
	{

		/// <summary>
		/// GPIB通信Task
		/// </summary>
		private readonly GpibCommsTask _gpibCommsTask;

		/// <summary>
		/// GPIB通信ログデータ
		/// </summary>
		private readonly GpibCommsLogData _logData = new GpibCommsLogData();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainWindowViewModel()
		{

			_gpibCommsTask = new GpibCommsTask(AddGpibCommsLog);

			var channels = new ObservableCollection<int>();
			for (var iLoop = ProgramDefine.CHANNEL_MIN; iLoop <= ProgramDefine.CHANNEL_MAX; iLoop++)
			{
				channels.Add(iLoop);
			}
			Channels = new ReadOnlyObservableCollection<int>(channels);

		}

		/// <summary>
		/// GPIB接続開始
		/// </summary>
		public void Connect()
		{

			if (string.IsNullOrWhiteSpace(VisaAddress))
			{
				MessageBoxEx.Show("VISA Addressを入力してください", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			_gpibCommsTask.StartTask();
			OnPropertyChanged(nameof(IsConnect));

		}

		/// <summary>
		/// GPIB接続終了
		/// </summary>
		public void Disconnect()
		{
			_gpibCommsTask.StopTask();
			OnPropertyChanged(nameof(IsConnect));
		}

		/// <summary>
		/// CH切替コマンド送信
		/// </summary>
		public void SendChangeChannel()
		{
			var command = CommandDefine.Instance.GetCommandChangeChannel(SelectedChannel);
			SendCommand(command);
		}

		/// <summary>
		/// 状態確認コマンド送信
		/// </summary>
		public void SendCheckStatus()
		{
			var command = CommandDefine.Instance.GetCommandCheckStatus();
			SendCommand(command);
		}

		/// <summary>
		/// 機種確認コマンド送信
		/// </summary>
		public void SendCheckModel()
		{
			var command = CommandDefine.Instance.GetCommandCheckModel();
			SendCommand(command);
		}

		/// <summary>
		/// 入力したコマンドを送信
		/// </summary>
		public void SendInputCommand()
		{

			if (string.IsNullOrWhiteSpace(InputCommand))
			{
				MessageBoxEx.Show("コマンドを入力してください", MessageBoxButton.OK);
				return;
			}

			SendCommand(InputCommand);
		}

		/// <summary>
		/// コマンド送信
		/// </summary>
		/// <param name="command">コマンド</param>
		private void SendCommand(string command)
		{

			// 送信フラグ更新
			IsSending = true;
			OnPropertyChanged(nameof(IsSending));

			// コマンド送信処理開始
			_gpibCommsTask.AddSendCommand(command);

		}

		/// <summary>
		/// GPIB通信ログ追加
		/// </summary>
		/// <param name="dateTime">通信日時</param>
		/// <param name="direction">送受信方向</param>
		/// <param name="message">送受信メッセージ</param>
		public void AddGpibCommsLog(DateTime dateTime, GpibCommsDirections direction, string message)
		{

			// 受信データまたはエラーデータの場合はコマンド送信処理終了
			if (direction != GpibCommsDirections.Tx)
			{
				IsSending = false;
				OnPropertyChanged(nameof(IsSending));
			}

			// 戻り値を要求しないコマンドの場合はmessageの値がstring.Empty
			// => ログには出力しない
			if (!string.IsNullOrEmpty(message))
			{
				_logData.AddLog(dateTime, direction, message);
				OnPropertyChanged(nameof(LogText));
				OnPropertyChanged(nameof(LogRowCount));
			}

		}

		/// <summary>
		/// ログクリア
		/// </summary>
		public void ClearLog()
		{

			_logData.ClearLog();
			OnPropertyChanged(nameof(LogText));
			OnPropertyChanged(nameof(LogRowCount));

		}

		/// <summary>
		/// アプリ終了
		/// </summary>
		public void Shutdown()
		{
			_gpibCommsTask.Dispose();
		}

		/****************************************************************************************/
		//　これ以降はバインドデータ

		/// <summary>
		/// VISAアドレス
		/// </summary>
		public string VisaAddress
		{
			get { return ProgramDefine.Instance.VisaAddress; }
			set { ProgramDefine.Instance.VisaAddress = value; }
		}

		/// <summary>
		/// 受信タイムアウト[ms]
		/// </summary>
		public int ReadTimeout
		{
			get { return ProgramDefine.Instance.ReadTimeout; }
			set { ProgramDefine.Instance.ReadTimeout = value; }
		}

		/// <summary>
		/// GPIB接続フラグ
		/// </summary>
		public bool IsConnect { get { return _gpibCommsTask.IsOpen; } }

		/// <summary>
		/// チャンネル一覧
		/// </summary>
		/// <remarks>
		/// コンストラクタにてインスタンス生成
		/// </remarks>
		public ReadOnlyObservableCollection<int> Channels { get; }

		/// <summary>
		/// 選択チャンネル
		/// </summary>
		public int SelectedChannel
		{
			get { return ProgramDefine.Instance.Channel; }
			set { ProgramDefine.Instance.Channel = value; }
		}

		/// <summary>
		/// コマンド入力
		/// </summary>
		public string InputCommand
		{
			get { return ProgramDefine.Instance.InputCommand; }
			set { ProgramDefine.Instance.InputCommand = value; }
		}

		/// <summary>
		/// コマンド送信中管理フラグ
		/// </summary>
		public bool IsSending { get; private set; } = false;

		/// <summary>
		/// 通信ログ表示内容
		/// </summary>
		public string LogText { get { return _logData.LogText; } }

		/// <summary>
		/// 通信ログ表示行数
		/// </summary>
		public int LogRowCount { get { return _logData.LogRowCount; } }

		/// <summary>
		/// 通信ログ出力フラグ
		/// </summary>
		public bool IsOutputLog
		{
			get { return ProgramDefine.Instance.IsOutputLog; }
			set { ProgramDefine.Instance.IsOutputLog = value; }
		}

	}

}

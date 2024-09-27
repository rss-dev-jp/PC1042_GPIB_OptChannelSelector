using RssDev.Common.ApplicationUtility;
using RssDev.Common.TaskUtility;
using RssDev.Project_Code.Containers;
using RssDev.Project_Code.Defines;
using RssDev.Project_Code.Defines.Enums;
using RssDev.Project_Code.GpibComms;
using RssDev.Project_Code.Tasks.Events;
using RssDev.RuntimeLog;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace RssDev.Project_Code.Tasks
{

	/// <summary>
	/// GPIB通信Task
	/// </summary>
	public class GpibCommsTask : ATask, IDisposable
	{

		/// <summary>
		/// GPIB通信機器
		/// 接続フラグ
		/// </summary>
		public bool IsOpen { get { return _gpibManager.IsOpen; } }

		/// <summary>
		/// 送信コマンド管理イベント
		/// </summary>
		private readonly TaskEventEx _commandEvent = new TaskEventEx(false);

		/// <summary>
		/// イベント通知
		/// </summary>
		private readonly TaskEventEx _sendEvent = new TaskEventEx(false);

		/// <summary>
		/// プロパティ更新Task
		/// </summary>
		private readonly UpdatePropertyTask _updatePropertyTask;

		/// <summary>
		/// GPIB通信管理
		/// </summary>
		private readonly GpibManager _gpibManager = new GpibManager();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="addGpibCommsLogMethod">GPIB通信ログ追加メソッド</param>
		public GpibCommsTask(UpdatePropertyTask.AddGpibCommsLogDelegate addGpibCommsLogMethod)
		{
			_updatePropertyTask = new UpdatePropertyTask(addGpibCommsLogMethod, _sendEvent);
		}

		/// <summary>
		/// クラス名取得
		/// </summary>
		/// <returns>クラス名</returns>
		protected override string GetClassName()
		{
			return nameof(GpibCommsTask);
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		public void Dispose()
		{
			_sendEvent.LastEvant();
			StopTask();
		}

		/// <summary>
		/// Task終了
		/// </summary>
		public void StopTask()
		{

			// 送信予定のコマンドクリア
			_commandEvent.ClearParameter();
			_commandEvent.LastEvant();

			// シリアル通信切断まで待機
			var stopwatch = Stopwatch.StartNew();
			while (IsOpen)
			{

				// コマンド送信完了 => シリアル通信切断
				if (!isProcess)
				{
					_gpibManager.Close();
				}

				DoEventWrapper.DoEvents();

				if (stopwatch.ElapsedMilliseconds > ProgramDefine.CLOSE_DISCONNECT_INTERVAL)
				{
					var message = $"GPIB通信が一定時間内で終了しませんでした";
					RuntimeLogger.Instance.Add(RuntimeLogger.Type.EXCEPTION, message);
					MessageBoxEx.Show(message, MessageBoxButton.OK);
					break;
				}

			}

		}

		/// <summary>
		/// Task開始
		/// </summary>
		public void StartTask()
		{

			if (isProcess)
			{
				return;
			}

			// GPIB通信接続
			_gpibManager.Open(ProgramDefine.Instance.VisaAddress);

			if (IsOpen)
			{
				_updatePropertyTask.StartTaskAsync();
				StartTaskAsync();   // Task起動
			}
			else
			{
				var message = $"{ProgramDefine.Instance.VisaAddress}への接続に失敗しました";
				RuntimeLogger.Instance.Add(RuntimeLogger.Type.EXCEPTION, message);
				MessageBoxEx.Show(message, "GPIB通信", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}

		/// <summary>
		/// Task開始(非同期)
		/// </summary>
		private async void StartTaskAsync()
		{
			await Task.Run(() => Process());
		}

		/// <summary>
		/// シリアル通信処理
		/// </summary>
		private void Process()
		{

			SetThreadID();
			ClearRate();

			isProcess = true;
			while (isProcess)
			{

				try
				{

					var param = _commandEvent.GetParameter1();
					if (param == null)
					{
						_commandEvent.WaitOne();
						continue;
					}
					else if (TaskEventEx.CheckLastEvent(param))
					{
						isProcess = false;
						continue;
					}
					else if (param is string command)
					{

						// コマンド送信
						_gpibManager.Write(command);
						_sendEvent.Set(new LogContainer(DateTime.Now, GpibCommsDirections.Tx, command));

						// 戻り値受信
						try
						{
							var value = _gpibManager.Read(ProgramDefine.Instance.ReadTimeout);
							_sendEvent.Set(new LogContainer(DateTime.Now, GpibCommsDirections.Rx, value));
						}
						catch (TimeoutException)
						{
							_sendEvent.Set(new LogContainer(DateTime.Now, GpibCommsDirections.Er, "Read Timeout."));
						}

					}

				}
				catch (Exception ex)
				{
					RuntimeLogger.Instance.Add(RuntimeLogger.Type.EXCEPTION, $"{GetClassName()}.{MethodBase.GetCurrentMethod().Name}() {ex.Message}");
				}

			}

			_sendEvent.LastEvant();

		}

		/// <summary>
		/// 送信コマンド追加
		/// </summary>
		/// <param name="command">送信コマンド</param>
		public void AddSendCommand(string command)
		{
			_commandEvent.Set(command);
		}

		/// <summary>
		/// 送信コマンド一覧初期化
		/// </summary>
		public void ClearSendCommands()
		{
			_commandEvent.ClearParameter();
		}

	}

}

using RssDev.Common.TaskUtility;
using RssDev.Project_Code.Containers;
using RssDev.Project_Code.Defines.Enums;
using RssDev.Project_Code.Tasks.Events;
using RssDev.RuntimeLog;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace RssDev.Project_Code.Tasks
{

	/// <summary>
	/// プロパティ更新Task
	/// </summary>
	public class UpdatePropertyTask : ATask
	{

		/// <summary>
		/// GBIB通信ログ追加デリゲート
		/// </summary>
		/// <param name="dateTime">通信日時</param>
		/// <param name="direction">送受信方向</param>
		/// <param name="message">送受信メッセージ</param>
		public delegate void AddGbibCommsLogDelegate(DateTime dateTime, GbibCommsDirections direction, string message);

		/// <summary>
		/// GBIB通信ログ追加メソッド
		/// </summary>
		private readonly AddGbibCommsLogDelegate _addSerialCommsLogMethod;

		/// <summary>
		/// イベント管理
		/// </summary>
		private readonly TaskEventEx _recvEvent;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="addGbibCommsLogMethod">GBIB通信ログ追加メソッド</param>
		/// <param name="recvEvent"></param>
		public UpdatePropertyTask(AddGbibCommsLogDelegate addGbibCommsLogMethod, TaskEventEx recvEvent)
		{
			_addSerialCommsLogMethod = addGbibCommsLogMethod;
			_recvEvent = recvEvent;
		}

		/// <summary>
		/// クラス名取得
		/// </summary>
		/// <returns>クラス名</returns>
		protected override string GetClassName()
		{
			return nameof(UpdatePropertyTask);
		}

		/// <summary>
		/// Task開始
		/// </summary>
		public async void StartTaskAsync()
		{

			if (isProcess)
			{
				return;
			}

			// 処理開始
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

					var param = _recvEvent.GetParameter1();
					if (param == null)
					{
						_recvEvent.WaitOne();
						continue;
					}
					else if (TaskEventEx.CheckLastEvent(param))
					{
						isProcess = false;
						continue;
					}
					else if (param is LogContainer logContainer)
					{
						Application.Current?.Dispatcher?.Invoke(
							new Action(() =>
							{
								_addSerialCommsLogMethod.Invoke(logContainer.CommsDateTime, logContainer.Direction, logContainer.Message);
							}));
					}

				}
				catch (Exception ex)
				{
					RuntimeLogger.Instance.Add(RuntimeLogger.Type.EXCEPTION, $"{GetClassName()}.{MethodBase.GetCurrentMethod().Name}() {ex.Message}");
					_addSerialCommsLogMethod.Invoke(DateTime.Now, GbibCommsDirections.Er, ex.Message);
				}

			}

		}

	}

}

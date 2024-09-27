using RssDev.Project_Code.Defines.Enums;
using System;

namespace RssDev.Project_Code.Containers
{

	/// <summary>
	/// 通信ログコンテナ
	/// </summary>
	public class LogContainer
	{

		/// <summary>
		/// 通信日時
		/// </summary>
		public DateTime CommsDateTime { get; }

		/// <summary>
		/// GPIB通信方向
		/// </summary>
		public GpibCommsDirections Direction { get; }

		/// <summary>
		/// 送受信メッセージ
		/// </summary>
		public string Message { get; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="commsDateTime">通信日時</param>
		/// <param name="direction">GPIB通信方向</param>
		/// <param name="message">送受信メッセージ</param>
		public LogContainer(DateTime commsDateTime, GpibCommsDirections direction, string message)
		{
			CommsDateTime = commsDateTime;
			Direction = direction;
			Message = message;
		}
	}

}

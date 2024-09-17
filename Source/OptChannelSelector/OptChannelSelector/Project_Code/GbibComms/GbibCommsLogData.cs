using RssDev.Project_Code.Defines;
using RssDev.Project_Code.Defines.Enums;
using System;

namespace RssDev.Project_Code.GbibComms
{

	/// <summary>
	/// GBIB通信ログデータ
	/// </summary>
	public class GbibCommsLogData
	{

		/// <summary>
		/// 通信ログ最大表示行数
		/// </summary>
		private const int LOG_MAX_LINE = 1000;

		/// <summary>
		/// 通信ログに表示する時間フォーマット
		/// </summary>
		private const string LOG_TIMESTAMP_FORMAT = "HH:mm:ss.fff";

		/// <summary>
		/// 通信ログ表示内容
		/// </summary>
		public string LogText { get; private set; }

		/// <summary>
		/// 通信ログ表示行数
		/// </summary>
		public int LogRowCount { get; private set; }

		/// <summary>
		/// ログ追加
		/// </summary>
		/// <param name="dateTime">通信日時</param>
		/// <param name="direction">GBIB通信方向</param>
		/// <param name="data">送受信データ</param>
		/// <returns>
		/// true:ログを追加した
		/// false:ログを追加していない
		/// </returns>
		public bool AddLog(DateTime dateTime, GbibCommsDirections direction, string data)
		{

			if (!ProgramDefine.Instance.IsOutputLog)
			{
				return false;
			}

			var line = GetLogLine(dateTime, direction, data);
			LogText += line;
			LogRowCount++;

			// 表示最大行数超過
			if (LogRowCount > LOG_MAX_LINE)
			{
				int index = LogText.IndexOf(Environment.NewLine);
				LogText = LogText.Remove(0, index + Environment.NewLine.Length);
				LogRowCount = LOG_MAX_LINE;
			}

			return true;

		}

		/// <summary>
		/// GBIB通信ログに表示する行データを取得
		/// </summary>
		/// <param name="direction">GBIB通信方向</param>
		/// <param name="data">送受信データ</param>
		/// <returns>行データ</returns>
		private string GetLogLine(DateTime dateTime, GbibCommsDirections direction, string data)
		{

			if (!data.Contains(Environment.NewLine))
			{
				data += Environment.NewLine;
			}

			return $"{dateTime.ToString(LOG_TIMESTAMP_FORMAT)}: {direction}> {data}";

		}

		/// <summary>
		/// ログクリア
		/// </summary>
		public void ClearLog()
		{
			LogText = string.Empty;
			LogRowCount = 0;
		}

	}

}

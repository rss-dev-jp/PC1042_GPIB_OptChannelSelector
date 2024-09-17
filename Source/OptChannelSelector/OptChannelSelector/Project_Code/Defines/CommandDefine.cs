using System;

namespace RssDev.Project_Code.Defines
{

	/// <summary>
	/// コマンド定義
	/// </summary>
	public class CommandDefine
	{

		/// <summary>
		/// 戻り値要求コマンドワード
		/// </summary>
		public const string RequestCommandWord = "?";

		/// <summary>
		/// インスタンス
		/// </summary>
		public static readonly CommandDefine Instance = new CommandDefine();

		/// <summary>
		/// コンストラクタ（隠蔽）
		/// </summary>
		private CommandDefine()
		{ }

		/// <summary>
		/// CH切替コマンド取得
		/// </summary>
		/// <param name="channel">チャンネル</param>
		/// <returns>コマンド</returns>
		public string GetCommandChangeChannel(int channel)
		{
			return $"CSEL:CHAN {channel}";
		}

		/// <summary>
		/// 状態確認コマンド取得
		/// </summary>
		/// <returns>コマンド</returns>
		public string GetCommandCheckStatus()
		{
			return "CSEL:CHAN?";
		}

		/// <summary>
		/// 機種確認コマンド取得
		/// </summary>
		/// <returns>コマンド</returns>
		public string GetCommandCheckModel()
		{
			return "*IDN?";
		}

		/// <summary>
		/// 指定コマンドが戻り値を要求するかチェック
		/// </summary>
		/// <param name="command">コマンド</param>
		/// <returns>
		/// true:戻り値要求
		/// false:戻り値は要求しない
		/// </returns>
		public bool CheckRequestCommand(string command)
		{

			if (string.IsNullOrWhiteSpace(command))
			{
				throw new ArgumentNullException($"{nameof(CheckRequestCommand)}()：引数エラー");
			}

			return command.Substring(command.Length - 1, 1) == RequestCommandWord;

		}

	}

}

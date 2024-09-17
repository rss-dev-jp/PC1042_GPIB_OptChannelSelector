using System.Diagnostics;

namespace RssDev.Project_Code.Defines
{

	/// <summary>
	/// プログラム定義
	/// </summary>
	public class ProgramDefine
	{

		/// <summary>
		/// プログラムタイトル
		/// </summary>
		public static readonly string PROGRAM_TITLE = Process.GetCurrentProcess().ProcessName;

		/// <summary>
		/// 切断処理監視時間[ms]
		/// </summary>
		public const int CLOSE_DISCONNECT_INTERVAL = 2000;

		/// <summary>
		/// 最小チャンネル
		/// </summary>
		public const int CHANNEL_MIN = 1;

		/// <summary>
		/// 最大チャンネル
		/// </summary>
		public const int CHANNEL_MAX = 16;

		/// <summary>
		/// インスタンス
		/// </summary>
		public static ProgramDefine Instance { get; }

		/// <summary>
		/// プログラム定義
		/// </summary>
		static ProgramDefine()
		{
			Instance = new ProgramDefine();
		}

		/// <summary>
		/// VISAアドレス
		/// </summary>
		public string VisaAddress { get; set; } = string.Empty;

		/// <summary>
		/// 受信タイムアウト[ms]
		/// </summary>
		public int ReadTimeout { get; set; } = 2000;

		/// <summary>
		/// チャンネル
		/// </summary>
		public int Channel { get; set; } = CHANNEL_MIN;

		/// <summary>
		/// コマンド入力
		/// </summary>
		public string InputCommand { get; set; } = string.Empty;

		/// <summary>
		/// 通信ログ出力フラグ
		/// </summary>
		public bool IsOutputLog { get; set; } = true;

		/// <summary>
		/// コンストラクタ（隠蔽）
		/// </summary>
		private ProgramDefine()
		{ }

	}

}

using RssDev.Project_Code.Defines;
using RssDev.Project_Code.IniFiles.Streams;

namespace RssDev.Project_Code.IniFiles
{

	/// <summary>
	/// iniファイルインターフェース
	/// </summary>
	public class IniFileIF
	{

		/// <summary>
		/// セクション名
		/// </summary>
		private static class SECTIONS
		{

			/// <summary>
			/// GPIB通信
			/// </summary>
			public const string GPIB = nameof(GPIB);

		}

		/// <summary>
		/// iniファイル名
		/// </summary>
		private const string INI_FILE_NAME = "Parameter.ini";

		/// <summary>
		/// インスタンス
		/// </summary>
		public static readonly IniFileIF Instance;

		/// <summary>
		/// iniファイルインターフェース
		/// </summary>
		static IniFileIF()
		{
			Instance = new IniFileIF();
		}

		/// <summary>
		/// コンストラクタ（隠蔽）
		/// </summary>
		private IniFileIF()
		{ }

		/// <summary>
		/// iniファイル読込
		/// </summary>
		public void Load()
		{

			var ini = new IniStream(INI_FILE_NAME);
			if (ini.Exists())
			{

				ini.SetSection(SECTIONS.GPIB);
				ProgramDefine.Instance.VisaAddress = ini.GetValue(nameof(ProgramDefine.Instance.VisaAddress), ProgramDefine.Instance.VisaAddress);
				ProgramDefine.Instance.ReadTimeout = ini.GetValue(nameof(ProgramDefine.Instance.ReadTimeout), ProgramDefine.Instance.ReadTimeout);
				ProgramDefine.Instance.Channel = ini.GetValue(nameof(ProgramDefine.Instance.Channel), ProgramDefine.Instance.Channel);
				ProgramDefine.Instance.InputCommand = ini.GetValue(nameof(ProgramDefine.Instance.InputCommand), ProgramDefine.Instance.InputCommand);
				ProgramDefine.Instance.IsOutputLog = ini.GetValue(nameof(ProgramDefine.Instance.IsOutputLog), ProgramDefine.Instance.IsOutputLog);

			}

		}

		/// <summary>
		/// iniファイル保存
		/// </summary>
		public void Save()
		{

			var ini = new IniStream(INI_FILE_NAME);

			ini.SetSection(SECTIONS.GPIB);
			ini.SetValue(nameof(ProgramDefine.Instance.VisaAddress), ProgramDefine.Instance.VisaAddress);
			ini.SetValue(nameof(ProgramDefine.Instance.ReadTimeout), ProgramDefine.Instance.ReadTimeout);
			ini.SetValue(nameof(ProgramDefine.Instance.Channel), ProgramDefine.Instance.Channel);
			ini.SetValue(nameof(ProgramDefine.Instance.InputCommand), ProgramDefine.Instance.InputCommand);
			ini.SetValue(nameof(ProgramDefine.Instance.IsOutputLog), ProgramDefine.Instance.IsOutputLog);

		}

	}

}

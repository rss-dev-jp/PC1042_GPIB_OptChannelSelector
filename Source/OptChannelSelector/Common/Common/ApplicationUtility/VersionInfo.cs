using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssDev.Common.ApplicationUtility
{
	/// <summary>
	/// ソフトのバージョンに関するクラス
	/// </summary>
	public class VersionInfo
	{
		/// <summary>
		/// バージョンを取得する(ファイルバージョン)
		/// </summary>
		/// <returns></returns>
		public static string GetVersion()
		{
			return System.Windows.Forms.Application.ProductVersion;
		}
	}
}

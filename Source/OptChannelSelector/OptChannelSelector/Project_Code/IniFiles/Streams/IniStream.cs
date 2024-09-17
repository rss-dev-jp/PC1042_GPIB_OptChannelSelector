using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace RssDev.Project_Code.IniFiles.Streams
{

	/// <summary>iniファイル管理クラス</summary>
	public class IniStream
	{

		[DllImport("kernel32.dll")]
		private static extern int GetPrivateProfileString(
			string lpApplicationName,
			string lpKeyName,
			string lpDefault,
			StringBuilder lpReturnedstring,
			int nSize,
			string lpFileName);

		[DllImport("kernel32.dll")]
		private static extern int WritePrivateProfileString(
			string lpApplicationName,
			string lpKeyName,
			string lpstring,
			string lpFileName);

		/// <summary>セクション名</summary>
		public string Section { get; private set; } = nameof(IniStream);

		/// <summary>ファイルパス</summary>
		private readonly string _IniFilePath;

		/// <summary>
		/// ファイル名を指定して初期化します。
		/// ファイルが存在しない場合は初回書き込み時に作成。
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <remarks>exeパスと同フォルダにファイルを作成</remarks>
		public IniStream(string fileName)
		{

			var directoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

			_IniFilePath = Path.Combine(directoryPath, fileName);

		}

		/// <summary>ファイルが存在するか確認</summary>
		/// <returns>
		/// true :存在する
		/// false:存在しない
		/// </returns>
		public bool Exists()
		{
			return File.Exists(_IniFilePath);
		}

		/// <summary>セクションの設定</summary>
		/// <param name="section">セクションの名称</param>
		public void SetSection(string section)
		{
			Section = section;
		}

		/// <summary>
		/// sectionとkeyからiniファイルの設定値を設定します。 
		/// </summary>
		public void SetValue<T>(string key, T value)
		{
			WritePrivateProfileString(Section, key, value.ToString(), _IniFilePath);
		}

		/// <summary>
		/// sectionとkeyからiniファイルの設定値を取得します。
		/// 指定したsectionとkeyの組合せが無い場合はdefaultvalueで指定した値が返ります。
		/// </summary>
		/// <returns>
		/// 指定したsectionとkeyの組合せが無い場合はdefaultvalueで指定した値が返ります。
		/// </returns>
		public T GetValue<T>(string key, T defaultvalue)
		{

			var sb = new StringBuilder(256);

			try
			{

				GetPrivateProfileString(Section, key, defaultvalue.ToString(), sb, sb.Capacity, _IniFilePath);

				var conv = TypeDescriptor.GetConverter(typeof(T));

				if (conv != null)
				{
					return (T)conv.ConvertFromString(sb.ToString());
				}

				return defaultvalue;

			}
			catch
			{
				return defaultvalue;
			}
			finally
			{
				sb.Clear();
			}

		}

	}

}

using System;
using System.IO;
using System.Text;
using System.Windows;

namespace RssDev.Common.FileUtility
{
    /// <summary>
    /// ファイルの入出力処理
    /// </summary>
    public class FileIOStream
    {

        /// <summary>
        /// 読込
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>読み込んだ文字列</returns>
        static public string ReadString(string fileName)
        {
            string text = "";
            using (StreamReader sr = new StreamReader(
                fileName, Encoding.Default))    // TODO:default Shift-Jis とりあえず。あとで調査する yoshinaga
            //Encoding.GetEncoding(csvEncoding)))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }

        /// <summary>
        /// 書込
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="text">保存する文字列</remarks>
        static public bool WriteString(string fileName, string text)
        {
            using (StreamWriter sw = new StreamWriter(
                fileName, false, Encoding.Default))    // TODO:default Shift-Jis とりあえず。あとで調査する yoshinaga
            //Encoding.GetEncoding(csvEncoding)))
            {
                sw.Write(text); // 例外は呼び出し側でハンドルすること
            }
            return true;
        }

		/// <summary>
		/// 書き込み(追加)
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <param name="text">保存する文字列</remarks>
		static public bool AddString(string fileName, string text)
		{
			using (StreamWriter sw = new StreamWriter(
				fileName, true, Encoding.Default))    // TODO:default Shift-Jis とりあえず。あとで調査する yoshinaga
			//Encoding.GetEncoding(csvEncoding)))
			{
				sw.Write(text); // 例外は呼び出し側でハンドルすること
			}
			return true;		
		}

    }
}


using System;
using System.IO;
namespace RssDev.Common.ApplicationUtility
{
    /// <summary>
    /// アプリケーションのパスに関するクラス
    /// </summary>
    public class ApplicationPath
    {
        /// <summary>
        /// アプリケーション起動ディリクトリ取得
        /// </summary>
        /// <returns></returns>
        static public string GetCurrentAppDir()
        {
            return System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// 相対パスファイル名をフルパスに変更
        /// </summary>
        /// <param name="fileName">相対ファイルパス</param>
        /// <returns>フルパス</returns>
        static public string GetFullPath(string fileName)
        {
            // フルパスに変換する必要あり
            string startPath = ApplicationPath.GetCurrentAppDir();
            fileName = startPath + @"\" + fileName;
            Uri uri = new Uri(fileName);
            //return uri.AbsolutePath; // これはNG、URLエンコードされた文字なので2バイト文字化ける
            return uri.LocalPath;
        }

        /// <summary>
        /// ファイル名がフルパスはチェックする
        /// </summary>
        /// <returns></returns>
        static public bool IsFullPath(string fileName)
        {
            return Path.IsPathRooted(fileName);
        }

        /// <summary>
        /// ファイル名がフルパスかチェックし、必要に応じ変換し返す
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static public string GetAndChecktFullPath(string fileName)
        {
            if (IsFullPath(fileName))
                return fileName;
            else
                return GetFullPath(fileName);
        }

        /// <summary>
        /// ファイル名が相対パスかチェックし、必要に応じ変換し返す
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static public string GetAndChecktRelativePath(string fileName)
        {
            if (IsFullPath(fileName))
                return GetRelativePath(fileName);
            else
                return fileName;
        }

        /// <summary>
        /// フルパスファイル名を相対パスに変更
        /// </summary>
        /// <param name="fileName">フルファイルパス</param>
        /// <returns>相対パス</returns>
        static public string GetRelativePath(string fileName)
        {
            Uri startPath = new Uri(ApplicationPath.GetCurrentAppDir() + @"\");
            //fileName = startPath + @"\" + fileName;
            Uri param = new Uri(fileName);
            Uri result = startPath.MakeRelativeUri(param);
            return result.ToString().Replace("/", @"\");
        }

    }
}

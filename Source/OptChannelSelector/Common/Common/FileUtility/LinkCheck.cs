using System.IO;

namespace RssDev.Common.FileUtility
{
    /// <summary>
    /// リンクチェック
    /// </summary>
    public class LinkCheck
    {
        /// <summary>
        /// 拡張子
        /// </summary>
        private const string LinkExt = ".lnk";

        /// <summary>
        /// チェック実行
        /// </summary>
        /// <param name="fileName">対象ファイル名</param>
        /// <param name="targetPath">本体ファイル名</param>
        /// <returns>Lnkファイルの場合はtrue</returns>
        static public bool Execute(string fileName, out string targetPath)
        {
            targetPath = "";
            // ファイルの拡張子を取得
            string ext = Path.GetExtension(fileName);
            // ファイルへのショートカットは拡張子".lnk"
            if (ext.Equals(".lnk", System.StringComparison.OrdinalIgnoreCase))
            {
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                // ショートカットオブジェクトの取得
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(fileName);

                // ショートカットのリンク先の取得
                targetPath = shortcut.TargetPath.ToString();

                return true;
            }
            return false;
        }
    }
}

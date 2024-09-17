using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssDev.Common.FileUtility
{
    /// <summary>
    /// 拡張子定義クラス
    /// </summary>
    public class ExtDefine
    {
        /// <summary>
        /// Player対応拡子
        /// </summary>
        public static string[] VideoExts = new string[2] { ".wmv", ".mp4" };

        /// <summary>
        /// ビデオダイアログのフィルタ
        /// </summary>
        public static string VideoDialogFilter = "Video Files (*.wmv, *.mp4)|*.wmv;*.mp4";
    }
}

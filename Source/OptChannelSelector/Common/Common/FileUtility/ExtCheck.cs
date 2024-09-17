using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssDev.Common.FileUtility
{
    /// <summary>
    /// 拡張子チェック
    /// </summary>
    public class ExtCheck
    {
        /// <summary>
        /// 有効な拡張子
        /// </summary>
        private string[] okExts;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="okExts">有効な拡張子</param>
        public ExtCheck(string[] okExts)
        {
            this.okExts = okExts;
        }

        /// <summary>
        /// ファイルチェック
        /// </summary>
        /// <param name="fileName">対象ファイル名</param>
        /// <returns></returns>
        public bool Execute(string fileName)
        {
            var ext = Path.GetExtension(fileName);
            var isOk = false;
            foreach (var ok in okExts)
            {
                if (ok.Equals(ext, StringComparison.OrdinalIgnoreCase))
                {
                    isOk = true;
                    break;
                }
            }
            return isOk;
        }
    }
}

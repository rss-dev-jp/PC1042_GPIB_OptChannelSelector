using System;

namespace RssDev.Common.ExceptionUtility
{
    /// <summary>
    /// 例外オブジェクト生成
    /// </summary>
    public class CreateException
    {
        /// <summary>
        /// 例外オブジェクト生成処理
        /// </summary>
        /// <param name="className">発生したクラス名</param>
        /// <param name="methodName">発生したメソッド名</param>
        /// <param name="message">発生要因メッセージ</param>
        /// <returns>例外オブジェクト</returns>
        /// <remarks>
        /// 以下のよう設定
        /// throw CreateException.Create(this.ToString(), MethodBase.GetCurrentMethod().Name, "メッセージ");
        /// </remarks>
        static public Exception Create(string className, string methodName, string message)
        {
            string str = String.Format("[{0}]\n{1}\n{2}", className, methodName, message);

            return new Exception(str);
        }

        static public string NullMessage(string message)
        {
            return message + " is Null.";
        }

        static public string OutOfRangeMessage<T>(string message, T value)
        {
            return message + " is out of range.(" + value.ToString() + ")";
        }

    }
}

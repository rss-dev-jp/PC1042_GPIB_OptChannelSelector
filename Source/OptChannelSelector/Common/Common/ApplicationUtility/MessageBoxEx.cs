using System;
using System.IO;
using System.Windows;

namespace RssDev.Common.ApplicationUtility
{
    /// <summary>
    /// メッセージBOX拡張
    /// </summary>
    public class MessageBoxEx
    {
        /// <summary>
        /// メッセージ
        /// </summary>
        /// <param name="messageBoxText">メッセージ</param>
        /// <param name="button">ボタン</param>
        /// <param name="icon">アイコンイメージ、デフォルトは情報</param>
        /// <returns>メッセージ選択</returns>
        static public MessageBoxResult Show(string messageBoxText, MessageBoxButton button, MessageBoxImage icon = MessageBoxImage.Information)
        {
            string appName = Environment.GetCommandLineArgs()[0];   // アプリケーション名
            appName = Path.GetFileNameWithoutExtension(appName);    // 拡張子取る
            return MessageBox.Show(messageBoxText, appName, button, icon, MessageBoxResult.Cancel, 
                MessageBoxOptions.DefaultDesktopOnly // アプリケーション最前面にする
                );
        }

        /// <summary>
        /// メッセージ(親ウィンドウ上に表示する)
        /// </summary>
        /// <param name="messageBoxText">メッセージ</param>
        /// <param name="button">ボタン</param>
        /// <param name="icon">アイコンイメージ、デフォルトは情報</param>
        /// <returns>メッセージ選択</returns>
        static public MessageBoxResult Show(string messageBoxText, MessageBoxButton button, Window owner, MessageBoxImage icon = MessageBoxImage.Information)
        {
            string appName = Environment.GetCommandLineArgs()[0];   // アプリケーション名
            appName = Path.GetFileNameWithoutExtension(appName);    // 拡張子取る
            return MessageBox.Show(owner, messageBoxText, appName, button, icon, MessageBoxResult.Cancel);
        }

        /// <summary>
        /// メッセージ
        /// </summary>
        /// <param name="messageBoxText">メッセージ</param>
        /// <param name="title">タイトル</param>
        /// <param name="button">ボタン</param>
        /// <param name="icon">アイコンイメージ、デフォルトは情報</param>
        /// <returns>メッセージ選択</returns>
        static public MessageBoxResult Show(string messageBoxText, string title, MessageBoxButton button, MessageBoxImage icon = MessageBoxImage.Information)
        {
            string appName = Environment.GetCommandLineArgs()[0];   // アプリケーション名
            appName = Path.GetFileNameWithoutExtension(appName);    // 拡張子取る
            return MessageBox.Show("【" + title + "】" + Environment.NewLine + messageBoxText, appName, button, icon, MessageBoxResult.Cancel,
                MessageBoxOptions.DefaultDesktopOnly // アプリケーション最前面にする
                );
        }
    }
}

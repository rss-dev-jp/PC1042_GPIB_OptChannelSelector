using RssDev.Common.ModelUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RssDev.RuntimeLog.Main
{
    /// <summary>
    /// ビューモデル
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        private MainWindow window;
        private string logFileName;
        private List<string> addLogList;
        private object lockObj = new object();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="window"></param>
        public MainWindowViewModel(MainWindow window)
        {
            this.window = window;
            addLogList = new List<string>();
        }
        /// <summary>
        /// ログ追加
        /// </summary>
        /// <param name="message"></param>
        public void Add(string message)
        {
            lock (lockObj)
            {
                addLogList.Add(message);
            }

            Application.Current.Dispatcher.BeginInvoke(
               new Action(() =>
               {
                   // 以下の処理はメインスレッドで処理
                   lock (lockObj)
                   {
                       foreach (var log in addLogList)
                       {
                           window.textBox.AppendText(log + "\n");
                       }
                       addLogList.Clear();

                       // 最終行に自動スクロール
                       window.textBox.CaretIndex = window.textBox.Text.Length;
                       window.textBox.ScrollToEnd();
                   }
               })
            ); 
        }

        /// <summary>
        /// ログファイル名セット
        /// </summary>
        /// <param name="logFileName"></param>
        public void SetLogFileName(string logFileName)
        {
            this.logFileName = logFileName;
        }

        /// <summary>
        /// ログファイルをオープン
        /// </summary>
        public void OpenLogFile()
        {
            System.Diagnostics.Process.Start("notepad.exe", logFileName);
        }

        /****************************************************************************************/
        //　これ以降はバインドデータ
    }
}

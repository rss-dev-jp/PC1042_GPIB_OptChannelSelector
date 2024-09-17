using RssDev.RuntimeLog.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RssDev.RuntimeLog
{
    /// <summary>
    /// ロガー
    /// </summary>
    public class RuntimeLogger
    {
        /// <summary>
        /// シングルトンインスタンス
        /// </summary>
        static private RuntimeLogger instance = null;
        static public RuntimeLogger Instance
        {
            get 
            {
                if (instance == null)
                    instance = new RuntimeLogger();
                return instance;
            }
        }

        /// <summary>
        /// ログの例外
        /// </summary>
        public enum Type
        { 
            START,
            CLOSE,
            REBOOT,
            COMMENT,
            EXCEPTION,
        }

        private string appName;
        private string logFileName;
        private object lockObj = new object();
		private DateTime TodayDate;
		private string LogDir;


		/// <summary>
		/// コンストラクタ
		/// </summary>
		public RuntimeLogger()
        { 
            appName = Environment.GetCommandLineArgs()[0];   // アプリケーション名
			LogDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\log\";
			logFileName = Path.GetDirectoryName(appName);
			TodayDate = DateTime.Now.Date;

			// アプリケーション名
			appName = Path.GetFileNameWithoutExtension(appName);    // 拡張子取る

			// ログフォルダの作成
			try
			{
				if (System.IO.Directory.Exists(LogDir) == false)
					System.IO.Directory.CreateDirectory(LogDir);
			}
			catch { }

			// ログファイル名の作成
			logFileName = GetFileName(TodayDate);

			// 古い日付のファイルがあれば削除する
			DeleteOldFile();
		}

		/// <summary>
		/// ロガー表示
		/// </summary>
		public void Show(Window ownerWindow)
        {
            MainWindow.Instance.Owner = ownerWindow;
            MainWindow.Instance.Show();
            MainWindow.Instance.Topmost = true; // 最前面にするため一時的にTopMostにする
            MainWindow.Instance.Topmost = false;
            MainWindow.Instance.SetTitle(appName + " runtime log.");
            MainWindow.Instance.ViewModel.SetLogFileName(logFileName);
        }

        /// <summary>
        /// ロガー非表示
        /// </summary>
        public void Close()
        {
            MainWindow.Instance.Close();
        }

        /// <summary>
        /// ログ追加
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public void Add(Type type, string message = null)
        {
			if (TodayDate.Day != DateTime.Now.Day)
			{
				TodayDate = DateTime.Now.Date;

				// ファイル名の更新
				logFileName = GetFileName(TodayDate);

				// 古い日付のファイルがあれば削除する
				DeleteOldFile();
			}

			// 以下処理は排他制御する
			lock (lockObj)
            {
                var time = DateTime.Now.ToString("yyyyMMdd-HHmmss-fff");
                var str = string.Format("{0} {1,-10} {2}", time, type.ToString(), message);

                SaveLog(str);
                // ログ表示ウィンドが表示中なら
                if (MainWindow.IsShown())
                {
                    MainWindow.Instance.ViewModel.Add(str);
                }
            }
        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        /// <param name="message"></param>
        private void SaveLog(string message)
        {
            using (var sw = new StreamWriter(logFileName, true))
            {
                sw.WriteLine(message);
            }
        }

		/// <summary>
		/// ファイル名の取得
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		private string GetFileName(DateTime date)
		{
			return LogDir + appName + date.ToString("_yyyyMMdd") + ".log";
		}

		/// <summary>
		/// 古いファイルの削除
		/// </summary>
		private void DeleteOldFile()
		{
            var workFile = "";
            try
            {
				// 古い日付のファイルがあれば削除する
				var files = System.IO.Directory.GetFiles(LogDir, appName + "*.log");
				foreach (var file in files)
				{
                    workFile = file;
					var file2 = file.Replace(LogDir, "").Replace(appName, "").Replace("_", "").Replace(".log", "");
					var old = DateTime.ParseExact(file2, "yyyyMMdd", null);
					if ((TodayDate - old).TotalDays >= 30)
					{
						try
						{
							System.IO.File.Delete(file);
						}
						catch { }
					}
				}
			}
			catch (Exception ex)
			{
				Add(Type.EXCEPTION, "ログファイルの日時経過計算に失敗." + ex.Message + " File=" + Path.GetFileName(workFile));
			}
		}
	}
}

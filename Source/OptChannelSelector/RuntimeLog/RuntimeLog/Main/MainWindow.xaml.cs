using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RssDev.RuntimeLog.Main
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// ビューモデル
        /// </summary>
        public MainWindowViewModel ViewModel { get; private set; }

        /// <summary>
        /// シングルトンインスタンス
        /// </summary>
        static public MainWindow Instance
        {
            get 
            {
                if (instance == null)
                    instance = new MainWindow();
                return instance;
            }
        }
        static private MainWindow instance = null;

        /// <summary>
        /// 表示状態チェック
        /// </summary>
        /// <returns></returns>
        static public bool IsShown()
        {
            if (instance != null && instance.IsVisible)
                return true;
            else
                return false;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            instance = this;

            // ビューモデルバインド
            ViewModel = new MainWindowViewModel(this);
            DataContext = ViewModel;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        /// <summary>
        /// タイトル設定
        /// </summary>
        /// <param name="appName">タイトル</param>
        public void SetTitle(string appName)
        {
            Title = appName;
        }

        /// <summary>
        /// 全ログ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenLogFile();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace RssDev.Common.WindowUtility
{
    /// <summary>
    /// Alt+F4で完了させない
    /// </summary>
    public class NotAltF4
    {
        /// <summary>
        /// Alt+F4キーのための定義
        /// </summary>
        const int WM_SYSKEYDOWN = 0x0104;
        const int VK_F4 = 0x73;

        private Window window;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="window"></param>
        public NotAltF4(Window window)
        {
            this.window = window;

            window.Loaded += Window_Loaded;
        }

        /// <summary>
        /// A+F4で終了しない
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if ((msg == WM_SYSKEYDOWN) &&
                (wParam.ToInt32() == VK_F4))
            {
                handled = true;
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// A+F4で終了しないための設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(window).Handle);
            source.AddHook(new HwndSourceHook(WndProc));
        }
    }
}

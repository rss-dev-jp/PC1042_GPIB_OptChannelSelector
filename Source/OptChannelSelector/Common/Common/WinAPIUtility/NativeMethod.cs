using System;
using System.Runtime.InteropServices;
using System.Text;

namespace RssDev.Common.WinAPIUtility
{
    /// <summary>
    /// ネイテイブなWinAPI定義
    /// </summary>
    public class NativeMethod
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint flags);


        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd,
            StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd,
            StringBuilder lpClassName, int nMaxCount);

        public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc,
            IntPtr lparam);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern uint SendInput(
                    uint nInputs,    // INPUT 構造体の数(イベント数)
                    INPUT[] pInputs, // INPUT 構造体
                    int cbSize       // INPUT 構造体のサイズ
        );

        /*
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        */

        [DllImport("user32.dll")]
        public static extern long SendMessage(
          IntPtr hWnd,    // 送信先ウィンドウのハンドル
          uint Msg,       // メッセージ
          IntPtr wParam,    // メッセージの最初のパラメータ       x64ではポインタとなるので注意
          IntPtr lParam     // メッセージの 2 番目のパラメータ    x64ではポインタとなるので注意
        );

        [DllImport("user32.dll")]
        public static extern long SendNotifyMessage(
          IntPtr hWnd,    // 送信先ウィンドウのハンドル
          uint Msg,       // メッセージ
          IntPtr wParam,    // メッセージの最初のパラメータ       x64ではポインタとなるので注意
          IntPtr lParam     // メッセージの 2 番目のパラメータ    x64ではポインタとなるので注意
        );

        // 座標から、その座標を含むディスプレイハンドルを取得
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr MonitorFromPoint(System.Drawing.Point point, uint dwFlags);

        // ウィンドウハンドルから、そのウィンドウが乗っているディスプレイハンドルを取得
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorDefaultTo dwFlags);
        // ディスプレイハンドルからDPIを取得
        [DllImport("SHCore.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void GetDpiForMonitor(IntPtr hmonitor, MonitorDpiType dpiType, ref uint dpiX, ref uint dpiY);

        /// <summary>
        /// ウィンドウがディスプレイ モニターと交差しない場合に、関数の戻り値を決定します。
        /// </summary>
        public enum MonitorDefaultTo
        { 
            Null,       // NULL を返します。
            Primary,    // プライマリ ディスプレイ モニターへのハンドルを返します。
            Nearest     // ウィンドウに最も近いディスプレイ モニターへのハンドルを返します
        }

        /// <summary>
        /// モニターの 1 インチあたりのドット数 (dpi) の設定を識別します。
        /// </summary>
        public enum MonitorDpiType 
        { 
            Effective,  // 有効な DPI。 この値は、UI 要素をスケーリングするための正しいスケール ファクターを決定するときに使用する必要があります。
            Angular,    // 角度 DPI。 この DPI により、画面上の準拠した角度解像度でレンダリングが保証されます。 これには、この特定の表示に対してユーザーが設定したスケール ファクターは含まれません。
            Raw,        // 生の DPI。 この値は、画面自体で測定された画面の線形 DPI です。 推奨されるスケーリング設定ではなく、ピクセル密度を読み取る場合は、この値を使用します。 これには、この特定のディスプレイに対してユーザーが設定したスケール ファクターは含まれません。サポートされている DPI 値であるとは限りません。
            Default = Effective 
        }

        /// <summary>
        /// SendMessageの定数
        /// </summary>
        public const int SC_CLOSE = 0xF060;
        public const int WM_SYSCOMMAND = 0x112;

        // Set、GetWindowsLongの定義
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_NOACTIVATE   = 0x08000000;
        public const int WS_EX_TOPMOST      = 0x00000008;
        public const int WS_EX_TOOLWINDOW   = 0x00000080;
        public const int WS_EX_TRANSPARENT  = 0x00000020;
        public const int WS_EX_APPWINDOW    = 0x00040000;

        // SetWindowPosの定義
        public const int HWND_TOP = 0;             //Z オーダーの先頭 ※だめだった
        public const int HWND_TOPMOST = -1;        //常に一番手前に表示される最前面ウィンドウにする
        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOACTIVATE = 0x0010; //アクティブにはしない 

        // GetWindowのコマンド定義
        public const uint GW_HWNDFIRST = 0;  // 基準となるWindowと同じ種類のうち最前面のWindow
        public const uint GW_HWNDLAST = 1;   // 基準となるWindowと同じ種類のうち最背面のWindow
        public const uint GW_HWNDNEXT = 2;   // 基準となるWindowの次のWindow
        public const uint GW_HWNDPREV = 3;   // 基準となるWindowの前のWindow
        public const uint GW_OWNER = 4;      // 基準となるWindowのオーナーWindow
        public const uint GW_CHILD = 5;      // 基準となるWindowの子WindowのうちトップレベルのWindow}

        // SendInputのType定義
        public const int INPUT_MOUSE = 0;
        public const int INPUT_KEYBOARD = 1;
        public const int INPUT_HARDWARE = 2;
        
        // SendInputの定義、構造体
        public const int MOUSEEVENTF_MOVED = 0x0001;
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const int MOUSEEVENTF_WHEEL = 0x0080;
        public const int MOUSEEVENTF_XDOWN = 0x0100;
        public const int MOUSEEVENTF_XUP = 0x0200;
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public const int SCREEN_LENGTH = 0x10000;

        // キーFlag
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const int KEYEVENTF_KEYUP = 0x0002;
        public const int KEYEVENTF_SCANCODE = 0x0008;
        public const int KEYEVENTF_UNICODE = 0x0004;


        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public int type; // 0 = INPUT_MOUSE(デフォルト), 1 = INPUT_KEYBOARD 
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public HARDWAREINPUT Hardware;
            [FieldOffset(0)]
            public KEYBDINPUT Keyboard;
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public uint Msg;
            public ushort ParamL;
            public ushort ParamH;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort Vk;
            public ushort Scan;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

    }
}

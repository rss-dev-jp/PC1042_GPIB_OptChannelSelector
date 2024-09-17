using System;
using System.Runtime.InteropServices;

namespace RssDev.Common.ApplicationUtility
{
    /// <summary>
    /// OSシャットダウン、再起動
    /// </summary>
    public class ShutDown
    {
        public enum ExitWindows : uint
        {
            EWX_LOGOFF = 0x00,
            EWX_SHUTDOWN = 0x01,
            EWX_REBOOT = 0x02,
            EWX_POWEROFF = 0x08,
            EWX_RESTARTAPPS = 0x40,
            EWX_FORCE = 0x04,
            EWX_FORCEIFHUNG = 0x10,
        }

        [DllImport("user32.dll")]
        public static extern bool ExitWindowsEx(ExitWindows uFlags, int dwReason);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle,
            uint DesiredAccess,
            out IntPtr TokenHandle);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true,
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool LookupPrivilegeValue(string lpSystemName,
            string lpName,
            out long lpLuid);

        [System.Runtime.InteropServices.StructLayout(
           System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
        private struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            public long Luid;
            public int Attributes;
        }

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
            bool DisableAllPrivileges,
            ref TOKEN_PRIVILEGES NewState,
            int BufferLength,
            IntPtr PreviousState,
            IntPtr ReturnLength);

        //シャットダウンするためのセキュリティ特権を有効にする
        public static void AdjustToken()
        {
            const uint TOKEN_ADJUST_PRIVILEGES = 0x20;
            const uint TOKEN_QUERY = 0x8;
            const int SE_PRIVILEGE_ENABLED = 0x2;
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                return;

            IntPtr procHandle = GetCurrentProcess();

            //トークンを取得する
            IntPtr tokenHandle;
            OpenProcessToken(procHandle,
                TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out tokenHandle);
            //LUIDを取得する
            TOKEN_PRIVILEGES tp = new TOKEN_PRIVILEGES();
            tp.Attributes = SE_PRIVILEGE_ENABLED;
            tp.PrivilegeCount = 1;
            LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, out tp.Luid);
            //特権を有効にする
            AdjustTokenPrivileges(
                tokenHandle, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);

            //閉じる
            CloseHandle(tokenHandle);
        }

        /// <summary>
        /// 強制終了
        /// </summary>
        public static void ShutdownForce()
        {
            AdjustToken();
            ExitWindowsEx(ExitWindows.EWX_SHUTDOWN | ExitWindows.EWX_FORCE, 0);
        }

        /// <summary>
        /// ノーマル終了
        /// </summary>
        public static void Shutdown()
        {
            AdjustToken();
            ExitWindowsEx(ExitWindows.EWX_SHUTDOWN, 0);
        }

        /// <summary>
        /// 強制再起動
        /// </summary>
        public static void RebootForce()
        {
            AdjustToken();
            ExitWindowsEx(ExitWindows.EWX_REBOOT | ExitWindows.EWX_FORCE, 0);
        }

        /// <summary>
        /// ノーマル再起動
        /// </summary>
        public static void Reboot()
        {
            AdjustToken();
            ExitWindowsEx(ExitWindows.EWX_REBOOT, 0);
        }
    }
}

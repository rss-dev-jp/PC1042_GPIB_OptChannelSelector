using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RssDev.Common.WindowUtility
{
    public class SystemCursor
    {
        [DllImport("user32.dll")]
        public static extern bool SetSystemCursor(IntPtr hcur, uint id);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32
            uiParam, String pvParam, UInt32 fWinIni);

        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr pcur);

        public static uint CROSS = 32515;
        public static uint NORMAL = 32512;
        public static uint IBEAM = 32513;
        public static uint HAND = 32649;

        static void Change()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            uint[] Cursors = { NORMAL, IBEAM };

            for (int i = 0; i < Cursors.Length; i++)
                SetSystemCursor(CopyIcon(LoadCursor(IntPtr.Zero, (int)CROSS)), Cursors[i]);

            //Application.Run(new Form1());
            SystemParametersInfo(0x0057, 0, null, 0);
        }

        /*
            OCR_APPSTARTING 32650   Standard arrow and small hourglass
            OCR_NORMAL  32512       Standard arrow
            OCR_CROSS   32515       Crosshair
            OCR_HAND    32649       Hand
            OCR_HELP    32651       Arrow and question mark
            OCR_IBEAM   32513       I-beam
            OCR_NO      32648       Slashed circle
            OCR_SIZEALL 32646       Four-pointed arrow pointing north, south, east, and west
            OCR_SIZENESW32643       Double-pointed arrow pointing northeast and southwest
            OCR_SIZENS  32645       Double-pointed arrow pointing north and south
            OCR_SIZENWSE32642       Double-pointed arrow pointing northwest and southeast
            OCR_SIZEWE  32644       Double-pointed arrow pointing west and east
            OCR_UP      32516       Vertical arrow
            OCR_WAIT    32514       Hourglass
         */
    }
}

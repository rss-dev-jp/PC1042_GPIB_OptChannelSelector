using System.Windows;

namespace RssDev.Common.ApplicationUtility
{
    /// <summary>
    /// デスクトップに関するクラス
    /// </summary>
    public class Desktop
    {

        /// <summary>
        /// プライマリモニタの領域を取得
        /// </summary>
        /// <returns>プライマリモニタの領域</returns>
        static public Rect GetPrimaryMonitorArea()
        {
            //var desktopWorkingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea; // こちらは使用しない、下のほうを使用しないと解像度によって不具合が出る
            //return System.Windows.SystemParameters.WorkArea; // これは下のタスクバーや右端が削られる
            return new Rect(0, 0,
                            System.Windows.SystemParameters.PrimaryScreenWidth,
                            System.Windows.SystemParameters.PrimaryScreenHeight);

        }

        /// <summary>
        /// プライマリモニタの作業領域
        /// </summary>
        /// <returns></returns>
        static public Rect GetPrimaryMonitorWork()
        {
            return System.Windows.SystemParameters.WorkArea; // これは下のタスクバーや右端が削られる
        }
    }
}

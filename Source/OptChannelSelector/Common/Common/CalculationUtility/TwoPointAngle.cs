using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace RssDev.Common.CalculationUtility
{
    /// <summary>
    /// ２点間の角度を計算するクラス
    /// </summary>
    public class TwoPointAngle
    {
        /// <summary>
        /// 角度計算
        /// </summary>
        /// <param name="src">開始点</param>
        /// <param name="dst">目的点</param>
        /// <returns>開始点から目的点への角度</returns>
        /// <remarks>
        /// 3時の方向が0
        /// 0時の方向が90
        /// 9時の方向が180
        /// 6時の方向が270
        /// </remarks>
        public static double Calculation(Point src, Point dst)
        {
            double x = dst.X - src.X;
            double y = dst.Y - src.Y;

            double angle = AngleUtility.Rad2Deg(Math.Atan2(y, x));

            return AngleUtility.Deg2_0_359(angle);
        }

        /// <summary>
        /// 角度計算
        /// </summary>
        /// <param name="src">開始点</param>
        /// <param name="dst">目的点</param>
        /// <returns>開始点から目的点への角度、Zは使用しない</returns>
        public static double Calculation(Point3D src, Point3D dst)
        {
            double x = dst.X - src.X;
            double y = dst.Y - src.Y;

            double angle = AngleUtility.Rad2Deg(Math.Atan2(y, x));

            return AngleUtility.Deg2_0_359(angle);
        }
    }
}

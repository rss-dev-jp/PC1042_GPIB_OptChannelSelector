using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RssDev.Common.CalculationUtility
{
    /// <summary>
    /// 座標回転クラス
    /// </summary>
    public class RotatePoint
    {
        /// <summary>
        /// 座標をBasePointで引く
        /// </summary>
        /// <param name="basePoint">基準ポイント</param>
        /// <param name="point">変換したいポイント</param>
        /// <returns></returns>
        static public Point SubBasePoint(Point basePoint, Point point)
        {
            var yy = point.Y - basePoint.Y;
            var xx = point.X - basePoint.X;
            return new Point(xx, yy);
        }

        /// <summary>
        /// 座標にBasePointを足す
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        static public Point AddBasePoint(Point basePoint, Point point)
        {
            var yy = point.Y + basePoint.Y;
            var xx = point.X + basePoint.X;
            return new Point(xx, yy);
        }

        /// <summary>
        /// 座標の回転
        /// </summary>
        /// <param name="rad">回転角度(ラジアン)</param>
        /// <param name="point">0,0を中心とした座標に変換していること</param>
        /// <returns></returns>
        static public Point Calculation(double rad, Point point)
        {
            var xx = point.X * Math.Cos(rad) - point.Y * Math.Sin(rad);
            var yy = point.X * Math.Sin(rad) + point.Y * Math.Cos(rad);
            return new Point(xx, yy);
        }

    }
}

using System.Windows;

namespace RssDev.Common.CalculationUtility
{
    /// <summary>
    /// ２直線の交点を計算する
    /// </summary>
    /// <remarks>２つの直線が平行でない前提</remarks>
    public class CrossPoint
    {
        /// <summary>
        /// 交点計算
        /// </summary>
        /// <param name="a1">直線１の傾き</param>
        /// <param name="b1">直線１の切片</param>
        /// <param name="a2">直線２の傾き</param>
        /// <param name="b2">直線２の切片</param>
        /// <returns></returns>
        static public Point Calcuration(double a1, double b1, double a2, double b2)
        {
            // y=a1x+b1とy=a2x+b2の交点
            // P(xp,yp)= (b2-b1)/(a1-a2), a1*xp+b1
            var xp = (b2 - b1) / (a1 - a2);
            var yp = a1 * xp + b1;

            return new Point(xp, yp);
        }


        public double A1 { get; private set; }
        public double B1 { get; private set; }

        public double A2 { get; private set; }
        public double B2 { get; private set; }

        public Point Point { get; private set; }

        public CrossPoint(Point line1Start, Point line1End, Point line2Start, Point line2End)
        {
            double xx = line1End.X - line1Start.X;
            double yy = line1End.Y - line1Start.Y;

            A1 = yy / xx; // 傾き
            B1 = -1*(A1 * line1Start.X) + line1Start.Y; // 切片

            xx = line2End.X - line2Start.X;
            yy = line2End.Y - line2Start.Y;

            A2 = yy / xx; // 傾き
            B2 = -1 * (A2 * line2Start.X) + line2Start.Y; // 切片

            Point = Calcuration(A1, B1, A2, B2);
        }
    }
}

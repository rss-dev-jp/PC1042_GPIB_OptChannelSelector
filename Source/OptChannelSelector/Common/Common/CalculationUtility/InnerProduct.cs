using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace RssDev.Common.CalculationUtility
{
    /// <summary>
    /// 内積（なす角）計算
    /// </summary>
    public class InnerProduct
    {
        /// <summary>
        /// 処理結果も角度モード
        /// </summary>
        public enum AngleMode
        {
            DEGREE,
            RADIAN
        }

        /// <summary>
        /// 外積方向成分
        /// </summary>
        public enum Unevenness
        {
            Convex,
            Concave,
        }

        /// <summary>
        /// 3点のなす角、外積方向成分（2次元）
        /// </summary>
        /// <param name="basePoint">基準点</param>
        /// <param name="toPoint1">点１</param>
        /// <param name="toPoint2">点２</param>
        /// <param name="angle">なす角出力</param>
        /// <param name="unevenness">外積方向</param>
        /// <param name="mode">角度出力モード</param>
        /// <returns></returns>
        static public bool Execute(Point basePoint, Point toPoint1, Point toPoint2, out double angle, out Unevenness unevenness,
                                    AngleMode mode = AngleMode.DEGREE)
        {
            unevenness = Unevenness.Convex; // 初期値
            // 0ベクトルでないかチェック
            if (basePoint.Equals(toPoint1))
            {
                angle = double.NaN;
                return false;
                //throw CreateException.Create("CalculationPlane", "Exexute", "toPoint1 is 0Vector");
            }
            if (basePoint.Equals(toPoint2))
            {
                angle = double.NaN;
                return false;
                //throw CreateException.Create("CalculationPlane", "Exexute", "toPoint2 is 0Vector");
            }

            var ax = toPoint1.X - basePoint.X;
            var ay = toPoint1.Y - basePoint.Y;
            var bx = toPoint2.X - basePoint.X;
            var by = toPoint2.Y - basePoint.Y;

            {
                // 外積の方向成分を計算
                var crossDir = ax * by - ay * bx;
                // 凸と凹みを判定、内積での判定と等価
                unevenness = (crossDir >= 0) ? Unevenness.Convex : Unevenness.Concave;
            }


            var cos = (ax * bx + ay * by) / (Math.Sqrt(ax * ax + ay * ay) * Math.Sqrt(bx * bx + by * by));
            // 演算誤差の±1範囲外になる場合の対策
            if (cos > 1)
                cos = 1;

            if (cos < -1)
                cos = -1;

            if(mode == AngleMode.DEGREE)
                angle = AngleUtility.Rad2Deg(Math.Acos(cos));
            else
                angle = Math.Acos(cos); // ラジアンで返す
            return true;

        }

        /// <summary>
        /// 3点のなす角、外積方向成分（3次元）
        /// </summary>
        /// <param name="basePoint">基準点</param>
        /// <param name="toPoint1">点１</param>
        /// <param name="toPoint2">点２</param>
        /// <param name="angle">なす角出力</param>
        /// <param name="mode">角度出力モード</param>
        /// <returns></returns>
        static public bool Execute(Point3D basePoint, Point3D toPoint1, Point3D toPoint2, out double angle, 
                                    AngleMode mode = AngleMode.DEGREE)
        {
            // 0ベクトルでないかチェック
            if (basePoint.Equals(toPoint1))
            {
                angle = double.NaN;
                return false;
                //throw CreateException.Create("CalculationPlane", "Exexute", "toPoint1 is 0Vector");
            }
            if (basePoint.Equals(toPoint2))
            {
                angle = double.NaN;
                return false;
                //throw CreateException.Create("CalculationPlane", "Exexute", "toPoint2 is 0Vector");
            }

            var vector1 = toPoint1 - basePoint;
            var vector2 = toPoint2 - basePoint;

            var cos = (vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z) / 
                      (
                      Math.Sqrt(Math.Pow(vector1.X, 2) + Math.Pow(vector1.Y, 2) + Math.Pow(vector1.Z, 2))
                      *
                      Math.Sqrt(Math.Pow(vector2.X, 2) + Math.Pow(vector2.Y, 2) + Math.Pow(vector2.Z, 2))
                      );
            // 演算誤差の±1範囲外になる場合の対策
            if (cos > 1)
                cos = 1;

            if (cos < -1)
                cos = -1;

            if (mode == AngleMode.DEGREE)
                angle = AngleUtility.Rad2Deg(Math.Acos(cos));
            else
                angle = Math.Acos(cos); // ラジアンで返す
            return true;

        }
    }
}

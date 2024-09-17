using RssDev.Common.Data;
using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace RssDev.Common.CalculationUtility
{
    /// <summary>
    /// 2点間の距離を計算するクラス
    /// </summary>
    public class TwoPointDistance
    {
        /// <summary>
        /// ２点間の距離を計算する
        /// </summary>
        /// <param name="point1">点１</param>
        /// <param name="point2">点２</param>
        /// <returns>２点間の距離</returns>
        public static double Calculation(Point point1, Point point2)
        {
            return Math.Sqrt(EazyCalculation(point1, point2));
        }

        /// <summary>
        /// ２点間の距離を計算する
        /// </summary>
        /// <param name="point1">点１</param>
        /// <param name="point2">点２</param>
        /// <returns>２点間の距離(Sqrt前)</returns>
        public static double EazyCalculation(Point point1, Point point2)
        {
            double width = point1.X - point2.X;
            double height = point1.Y - point2.Y;
            return width * width + height * height;
        }

        /// <summary>
        /// ２点間の距離を計算する
        /// </summary>
        /// <param name="point1">点１</param>
        /// <param name="point2">点２</param>
        /// <returns>２点間の距離(Sqrt前)</returns>
        public static double EazyCalculation(PointSS point1, PointSS point2)
        {
            double width = point1.X - point2.X;
            double height = point1.Y - point2.Y;
            return width * width + height * height;
        }

        /// <summary>
        /// ２点間の距離を計算する(3D)
        /// </summary>
        /// <param name="point1">点１</param>
        /// <param name="point2">点２</param>
        /// <returns>２点間の距離</returns>
        public static double Calculation(Point3D point1, Point3D point2)
        {
            return Math.Sqrt(EazyCalculation(point1, point2));
        }

        /// <summary>
        /// ２点間の距離を計算する（3D）
        /// </summary>
        /// <param name="point1">点１</param>
        /// <param name="point2">点２</param>
        /// <returns>２点間の距離(Sqrt前)</returns>
        public static double EazyCalculation(Point3D point1, Point3D point2)
        {
            double width = point1.X - point2.X;
            double height = point1.Y - point2.Y;
            double depth = point1.Z - point2.Z;
            return width * width + height * height + depth * depth;
        }

        /// <summary>
        /// ２点間の距離を計算する（3D）
        /// </summary>
        /// <param name="point1">点１</param>
        /// <param name="point2">点２</param>
        /// <returns>２点間の距離(Sqrt前)</returns>
        public static double EazyCalculation(Vector3D point1, Vector3D point2)
        {
            double width = point1.X - point2.X;
            double height = point1.Y - point2.Y;
            double depth = point1.Z - point2.Z;
            return width * width + height * height + depth * depth;
        }

        /// <summary>
        /// ２点間の距離を計算する(3D)
        /// </summary>
        /// <param name="point1">点１</param>
        /// <param name="point2">点２</param>
        /// <returns>２点間の距離</returns>
        public static double Calculation(Vector3D point1, Vector3D point2)
        {
            return Math.Sqrt(EazyCalculation(point1, point2));
        }

    }
}

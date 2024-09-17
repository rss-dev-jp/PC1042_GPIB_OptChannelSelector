using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace RssDev.Common.CalculationUtility
{
    /// <summary>
    /// 座標系のユーティリティクラス
    /// </summary>
    public class PointUtility
    {
        /// <summary>
        /// 輪郭の最大座標の取得
        /// </summary>
        /// <param name="contourList">輪郭座標リスト</param>
        static public void GetMinMax(List<Point> contourList, out int minX, out int maxX, out int minY, out int maxY)
        {
            minX = int.MaxValue;
            maxX = int.MinValue;
            minY = int.MaxValue;
            maxY = int.MinValue;
            foreach (var p in contourList)
            {

                if (p.X < minX)
                    minX = (int)p.X;
                if (p.X > maxX)
                    maxX = (int)p.X;

                if (p.Y < minY)
                    minY = (int)p.Y;
                if (p.Y > maxY)
                    maxY = (int)p.Y;

            }
        }

        /// <summary>
        /// 輪郭の最大座標の取得
        /// </summary>
        /// <param name="contourList">輪郭座標リスト</param>
        static public void GetMinMax(List<Point3D> contourList, out int minX, out int maxX, out int minY, out int maxY)
        {
            minX = int.MaxValue;
            maxX = int.MinValue;
            minY = int.MaxValue;
            maxY = int.MinValue;
            foreach (var p in contourList)
            {

                if (p.X < minX)
                    minX = (int)p.X;
                if (p.X > maxX)
                    maxX = (int)p.X;

                if (p.Y < minY)
                    minY = (int)p.Y;
                if (p.Y > maxY)
                    maxY = (int)p.Y;

            }
        }

        /// <summary>
        /// 3Dリストを2Dに変更
        /// </summary>
        /// <param name="list2D">3Dリスト</param>
        /// <returns>2Dリスト</returns>
        static public List<Point> List3Dto2D(List<Point3D> list3D)
        {
            List<Point> list2D = new List<Point>();
            foreach (var p3 in list3D)
            {
                list2D.Add(new Point(p3.X, p3.Y));
            }
            return list2D;
        }

        /// <summary>
        /// 2Dリストを3Dに変更
        /// </summary>
        /// <param name="depthData">深度データ</param>
        /// <param name="list2D">2Dリスト</param>
        /// <returns>3Dリスト</returns>
        static public List<Point3D> List2Dto3D(ushort[,] depthData, List<Point> list2D, int parallelCount = 8)
        {
            /*
            List<Point3D> list3D = new List<Point3D>();
            foreach (var p in list2D)
            {
                list3D.Add(new Point3D(p.X, p.Y, depthData[(int)p.Y, (int)p.X]));
            }
             */
            List<Point3D> list3D = new List<Point3D>();
            Object thisLock = new object();
            Parallel.For(0, parallelCount, (n) =>
            {
                var loopCnt = list2D.Count;
                List<Point3D> list = new List<Point3D>();
                for (int i = n; i < loopCnt; i += parallelCount)
                {
                    var p = list2D[i];
                    list.Add(new Point3D(p.X, p.Y, depthData[(int)p.Y, (int)p.X]));
                }
                lock (thisLock)
                {
                    list3D.AddRange(list);
                }
            });

            return list3D;
        }

        /// <summary>
        /// 3DPointの作成
        /// </summary>
        /// <param name="point">2Dポイント</param>
        /// <param name="depth">深度</param>
        /// <returns>3Dポイント</returns>
        public static Point3D MakePoint2Point3D(Point point, ushort depth)
        {
            return new Point3D(point.X, point.Y, depth);
        }

        /// <summary>
        /// 3D座標を2D座標に変換
        /// </summary>
        /// <param name="point3D">3D座標</param>
        /// <returns>2D座標</returns>
        public static Point MakePoint3DPoint2D(Point3D point3D)
        {
            Point p = new Point(point3D.X, point3D.Y);
            return p;
        }

        /// <summary>
        /// ２点間の中心を取得（2D）
        /// </summary>
        /// <param name="point1">座標１</param>
        /// <param name="point2">座標２</param>
        /// <returns>中心</returns>
        public static Point GetMiddle(Point point1, Point point2)
        {
            Point point = new Point();
            point.X = (point1.X + point2.X) / 2;
            point.Y = (point1.Y + point2.Y) / 2;
            return point;
        }

        /// <summary>
        /// ２点間の中心を取得（3D）
        /// </summary>
        /// <param name="point1">座標１</param>
        /// <param name="point2">座標２</param>
        /// <returns>中心</returns>
        public static Point3D GetMiddle(Point3D point1, Point3D point2)
        {
            Point3D point = new Point3D();
            point.X = (point1.X + point2.X) / 2;
            point.Y = (point1.Y + point2.Y) / 2;
            point.Z = (point1.Z + point2.Z) / 2;
            return point;
        }

        /// <summary>
        /// ２点間の中心を取得（3D）
        /// </summary>
        /// <param name="point1">座標１</param>
        /// <param name="point2">座標２</param>
        /// <returns>中心</returns>
        public static Point GetMiddle2D(Point3D point1, Point3D point2)
        {
            Point point = new Point();
            point.X = (point1.X + point2.X) / 2;
            point.Y = (point1.Y + point2.Y) / 2;
            return point;
        }
    }
}

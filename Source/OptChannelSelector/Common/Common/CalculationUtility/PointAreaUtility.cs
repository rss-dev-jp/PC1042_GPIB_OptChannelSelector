using System.Collections.Generic;
using System.Windows;

namespace RssDev.Common.CalculationUtility
{
    /// <summary>
    /// 座標のエリアチェックユーティリティ
    /// </summary>
    public class PointAreaUtility
    {

        /// <summary>
        /// 範囲外座標を範囲内に変える
        /// </summary>
        /// <param name="pointList">座標リスト</param>
        /// <param name="imageWidth">範囲サイズ幅</param>
        /// <param name="imageHeight">範囲サイズ高さ</param>
        /// <returns></returns>
        static public List<Point> OutAreaCheck(List<Point> pointList, int imageWidth, int imageHeight)
        {
            List<Point> result = new List<Point>();
            foreach (var pt in pointList)
            {
                var y = pt.Y;
                if (y < 0)
                    y = 0;
                if (y >= imageHeight)
                    y = imageHeight - 1;

                var x = pt.X;
                if (x < 0)
                    x = 0;
                if (x >= imageWidth)
                    x = imageWidth - 1;

                result.Add(new Point(x, y));
            }
            return result;
        }
    }
}

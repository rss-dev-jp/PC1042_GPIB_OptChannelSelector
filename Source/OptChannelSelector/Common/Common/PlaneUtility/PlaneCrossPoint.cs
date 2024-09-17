using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RssDev.Common.PlaneUtility
{
    /// <summary>
    /// 平面と直線の交点を計算
    /// </summary>
    public class PlaneCrossPoint
    {
        PlaneParameter planeData;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="planeData">平面データ</param>
        public PlaneCrossPoint(PlaneParameter planeData)
        {
            this.planeData = planeData;
        }

        /// <summary>
        /// 平面と交わる直線の２点
        /// </summary>
        /// <param name="p1">開始点</param>
        /// <param name="p2">終了点</param>
        /// <returns>平面と交わる点</returns>
        public Point3D GetCrossPoint(Point3D p1, Point3D p2)
        {
            // P1～P2のベクトルを求める
            Vector3D v = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            double l = Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2) + Math.Pow(v.Z, 2));
            // Vの単位ベクトルを求める --- (4)
            double ex = v.X / l;
            double ey = v.Y / l;
            double ez = v.Z / l;

            // P1から距離 k にある直線上の点Qは(5)式となる。
 　         // Q = P1 + k E = ( x1 + k・ex,　y1 + k・ey,　z1 + k・ez) 　　・・・・・　(5)


            // (Q点が(1)式の平面上に含まれる条件式)はQ点の座標を(1)式に代入した(6)式となる。
 　         // a( x1 + k・ex) ＋ b( y1 + k・ey ) + c( z1 + k・ez ) + d = 0 　　・・・・・　(6)

            // (6)式より、 (7)式が導出され、ｋが算出できる。
 　　       // k = - ( a・x1+ b・y1 + c・z1 + d )/( a・ex + b・ey + x・ez )　　　・・・・・　(7)
            var k = - (planeData.A * p1.X + planeData.B * p1.Y + planeData.C * p1.Z + planeData.D) / 
                        (planeData.A * ex + planeData.B * ey + planeData.C * ez );

            // k（点P1から交点Qまでの距離）を(5)式に代入すると、直線と平面の交点Qが求まる。
            var qx = p1.X + k * ex;
            var qy = p1.Y + k * ey;
            var qz = p1.Z + k * ez;

            return new Point3D(qx, qy, qz);
        }
    }
}

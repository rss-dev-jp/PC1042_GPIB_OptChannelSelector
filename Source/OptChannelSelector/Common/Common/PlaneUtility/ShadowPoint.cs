using RssDev.Common.CalculationUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RssDev.Common.PlaneUtility
{
    /// <summary>
    /// 床平面へ直線的に落とした影の座標を計算するクラス
    /// </summary>
    public class ShadowPoint
    {
        private double angleRad;
        private int sensorDistance;

        private double a;
        private double b;
        private double c;

        /// <summary>
        /// センサの縦方向の傾き
        /// </summary>
        /// <param name="sensorVerticalAngle">センサ垂直方向の傾き</param>
        /// <param name="sensorDistance">画像中心、センサと平面との距離</param>
        public ShadowPoint(double sensorVerticalAngle, int sensorDistance)
        {
            this.angleRad = AngleUtility.Deg2Rad(sensorVerticalAngle);
            this.sensorDistance = sensorDistance;

            this.a = -Math.Tan(angleRad);
            this.b = -1;
            this.c = sensorDistance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1">影の座標</param>
        /// <returns></returns>
        public Point3D Calculation(Vector3D p1, ref int height)
        {
            return calculation(p1.X, p1.Y, p1.Z, ref height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1">影の座標</param>
        /// <returns></returns>
        public Point3D Calculation(Point3D p1, ref int height)
        {
            return calculation(p1.X, p1.Y, p1.Z, ref height);
        }

        private Point3D calculation(double pX, double pY, double pZ, ref int height)
        {
            /**
             * 傾きθの直線式を求める
             * ax + by + c = 0
             * 
             * tanθが傾きであるため、床平面は (xにはY、yにはZで置き換える)
             * y = -tanθ*x + SensorDistance
             * 
             * 0 = -tanθ*x -1*y + SensorDistance)
             * 
             */

            /**
             * 点と直線の距離を求める公式
             * h = abs(a*x + b*y + c) / sqort(a^2 + b^2)
             * 
             */

            /**
             * a=-tanθ
             * b=-1 
             * c=SensorDustance
             */
            double h = Math.Abs(a * pY + b * pZ + c) / Math.Sqrt(a * a + b * b);
            height = (int)h;

            if (angleRad == 0)
            {
                // YとZの算出はこれでもよいが、公式に置き換える　参考：https://keisan.casio.jp/exec/system/1173755018{
                var ay = Math.Sin(angleRad) * h + pY;
                var az = Math.Cos(angleRad) * h + pZ;

                var shadow3D = new Point3D(pX, ay, az);
                return shadow3D;
            }
            else
            {
                // 角度0では0除算でNANが発生するので注意
                // y=a1x+b1とy=a2x+b2の交点
                // P(xp,yp)= (b2-b1)/(a1-a2), a1*xp+b1
                var a1 = a;
                var b1 = c;
                var a2 = -1 / a1;
                var b2 = a2 * (-pY) + pZ;
                var xp = (b2 - b1) / (a1 - a2);
                var yp = a1 * xp + b1;

                var shadow3D = new Point3D(pX, xp, yp);
                return shadow3D;
            }
        }
    }
}

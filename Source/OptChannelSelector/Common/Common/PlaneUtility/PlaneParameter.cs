using System;
using System.Windows.Media.Media3D;

namespace RssDev.Common.PlaneUtility
{
    /// <summary>
    /// 平面データ
    /// </summary>
    public class PlaneParameter
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }
        public double D { get; private set; }

        public Vector3D BaseP { get; private set; }
        public Vector3D P1 { get; private set; }
        public Vector3D P2 { get; private set; }

        public double Length { get; private set; }

        public Vector3D VectorUnit { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        public PlaneParameter(double a, double b, double c, double d)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="baseP">基準座標</param>
        /// <param name="p1">点１</param>
        /// <param name="p2">点２</param>
        /// <param name="requestD">Dパラメータの計算有無</param>
        /// <remarks>base、p1、p2が時計回りになること</remarks>
        public PlaneParameter(Vector3D baseP, Vector3D p1, Vector3D p2, bool requestD)
        {
            CalcParameter(ref baseP, ref p1, ref p2);

            if (requestD)
            {
                // Dも計算
                this.D = -(A * BaseP.X + B * BaseP.Y + C * BaseP.Z);
            }
        }

        /// <summary>
        /// パラメータ計算
        /// </summary>
        /// <param name="baseP"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        private void CalcParameter(ref Vector3D baseP, ref Vector3D p1, ref Vector3D p2)
        {
            this.BaseP = baseP;
            this.P1 = p1;
            this.P2 = p2;

            var baseX = baseP.X;
            var baseY = baseP.Y;
            var baseZ = baseP.Z;

            // S、Tベクトルの計算とその外積計算(S×T)
            
            // S
            /*
            double sx = p1.X - baseX;
            double sy = p1.Y - baseY;
            double sz = p1.Z - baseZ;
            var v1 = new Vector3D(sx, sy, sz);
             */
            Vector3D v1 = Vector3D.Subtract(p1, baseP);

            // T
            /*
            double tx = p2.X - baseX;
            double ty = p2.Y - baseY;
            double tz = p2.Z - baseZ;
            var v2 = new Vector3D(tx, ty, tz);
             */
            Vector3D v2 = Vector3D.Subtract(p2, baseP);

            // S X T
            /*
            this.A = sy * tz - sz * ty;
            this.B = sz * tx - sx * tz;
            this.C = sx * ty - sy * tx;
             */
            var crossProduct = Vector3D.CrossProduct(v1, v2); // .Netライブラリの外積計算を使用する、多少の速度向上があった
            this.A = crossProduct.X;
            this.B = crossProduct.Y;
            this.C = crossProduct.Z;

            // ベクトル大きさ
            this.Length = crossProduct.Length;

            // 単位ベクトルの取得
            crossProduct.Normalize();
            this.VectorUnit = crossProduct;
        }

    }
}

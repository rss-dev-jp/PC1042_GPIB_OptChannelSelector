using System;

namespace RssDev.Common.CalculationUtility
{
    /// <summary>
    /// 角度計算等の汎用メソッドクラス
    /// </summary>
    public class AngleUtility
    {
        /// <summary>
        /// ラジアンを角度に変換
        /// </summary>
        /// <param name="rad">ラジアン</param>
        /// <returns>角度</returns>
        public static double Rad2Deg(double rad)
        {
            return (rad * 180.0 / Math.PI);
        }

        /// <summary>
        /// 角度をラジアンに変換
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double Deg2Rad(double angle)
        {
            return (angle / 180.0 * Math.PI);
        }

        /// <summary>
        /// 角度を0-359に変換する
        /// </summary>
        /// <param name="deg">角度</param>
        /// <returns>0-359の角度</returns>
        public static double Deg2_0_359(double deg)
        {
            if (deg < 0)
            {
                double mod = deg % 360;
                var result = (mod == 0) ? 0 : 360 + mod;
                if (result == 360) // degが限りなく小さい場合にこれがあり得る
                    result = 0;
                return result;
            }
            else
            {
                return deg % 360;
            }
        }

        /// <summary>
        /// 角度を0-179に変換する 180以上の角度は反対角を採用する
        /// </summary>
        /// <param name="deg">角度</param>
        /// <returns>0-179の角度<</returns>
        public static double Deg2_0_179(double deg)
        {
            var deg1 = AngleUtility.Deg2_0_359(deg); // 0..359にまず変換する
            var deg2 = AngleUtility.Deg2_0_359(deg1 + 180); // 180足しこんだ角度を計算
            return deg1 < deg2 ? deg1 : deg2; // 小さいほうが 180 > 決定角度 >= 0　である
        }

        /// <summary>
        /// 角度の差分を計算する
        /// </summary>
        /// <param name="srcAngle">基準角度</param>
        /// <param name="dstAngle">判定角度</param>
        /// <returns>角度差</returns>
        public static double Difference(double srcAngle, double dstAngle)
        {
            // 角度を0-359に揃える
            srcAngle = Deg2_0_359(srcAngle);
            dstAngle = Deg2_0_359(dstAngle);
            double result = Deg2_0_359(dstAngle - srcAngle);

            // 差分を±180に変換する
            if (result > 180)
            {
                return -360 + result;
            }
            else
            {
                return result;
            }
        }
    }
}

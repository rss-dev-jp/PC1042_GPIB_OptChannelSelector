
using System.Windows;
namespace RssDev.Common.Data
{
    /// <summary>
    /// 座標クラス、符号付16bit
    /// </summary>
    public class PointSS
    {
        public short X { get { return x; } }
        public short Y { get { return y; } }

        protected short x;
        protected short y;

        /// <summary>
        /// コンストラクタ1
        /// </summary>
        public PointSS(int dummy = 0)
        {
            this.x = 0;
            this.y = 0;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PointSS(short x, short y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <remarks>
        /// ループ内などで毎回shortキャストが面倒な場合のため用意。
        /// 内部保持値は16bitになるので、承知で使用すること。
        /// </remarks>
        public PointSS(int x, int y)
        {
            this.x = (short)x;
            this.y = (short)y;
        }

        /// <summary>
        /// System.Windows.Point型に変換
        /// </summary>
        /// <returns></returns>
        public Point GetPointD()
        {
            return new Point(X, Y);
        }
    }
}

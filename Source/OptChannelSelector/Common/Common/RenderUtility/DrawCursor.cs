using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace RssDev.Common.RenderUtility
{
    /// <summary>
    /// カーソル描画
    /// </summary>
    public class DrawCursor
    {
        /// <summary>
        /// クロスカーソル（＋）描画
        /// </summary>
        /// <param name="ctx">ジオメトリ</param>
        /// <param name="p">ポイント</param>
        static public void Pulus(StreamGeometryContext ctx, Point p)
        {
            int len = 2;
            Point p1 = new Point(p.X - len, p.Y);
            Point p2 = new Point(p.X + len, p.Y);
            Point p3 = new Point(p.X, p.Y - len);
            Point p4 = new Point(p.X, p.Y + len);

            ctx.BeginFigure(p1, true /* is filled */, true /* is closed */);
            ctx.LineTo(p2, true /* is stroked */, false /* is smooth join */);

            ctx.BeginFigure(p3, true /* is filled */, true /* is closed */);
            ctx.LineTo(p4, true /* is stroked */, false /* is smooth join */);
        }

        /// <summary>
        /// クロスカーソル（×）描画
        /// </summary>
        /// <param name="ctx">ジオメトリ</param>
        /// <param name="p">ポイント</param>
        static public void Cross(StreamGeometryContext ctx, Point p)
        {
            int len = 2;
            Point p1 = new Point(p.X - len, p.Y - len);
            Point p2 = new Point(p.X + len, p.Y + len);
            Point p3 = new Point(p.X - len, p.Y + len);
            Point p4 = new Point(p.X + len, p.Y - len);

            ctx.BeginFigure(p1, true /* is filled */, true /* is closed */);
            ctx.LineTo(p2, true /* is stroked */, false /* is smooth join */);

            ctx.BeginFigure(p3, true /* is filled */, true /* is closed */);
            ctx.LineTo(p4, true /* is stroked */, false /* is smooth join */);
        }

        /// <summary>
        /// クロスカーソル（×）描画
        /// </summary>
        /// <param name="drawContext">描画コンテキスト</param>
        /// <param name="pen">ペン</param>
        /// <param name="p">ポイント</param>
        /// <param name="len">カーソル超</param>
        static public void Cross(DrawingContext drawContext, Pen pen, Point p, int len = 4)
        {
            Point p1 = new Point(p.X - len, p.Y - len);
            Point p2 = new Point(p.X + len, p.Y + len);
            Point p3 = new Point(p.X - len, p.Y + len);
            Point p4 = new Point(p.X + len, p.Y - len);

            drawContext.DrawLine(pen, p1, p2);
            drawContext.DrawLine(pen, p3, p4);
        }

		/// <summary>
		/// クロスカーソル（+）描画
		/// </summary>
		/// <param name="drawContext">描画コンテキスト</param>
		/// <param name="pen">ペン</param>
		/// <param name="p">ポイント</param>
		/// <param name="len">カーソル超</param>
		static public void Cross2(DrawingContext drawContext, Pen pen, Point p, int len = 4)
		{
			Point p1 = new Point(p.X - len, p.Y);
			Point p2 = new Point(p.X + len, p.Y);
			Point p3 = new Point(p.X, p.Y + len);
			Point p4 = new Point(p.X, p.Y - len);

			drawContext.DrawLine(pen, p1, p2);
			drawContext.DrawLine(pen, p3, p4);
		}

		/// <summary>
		/// カーソルの描画
		/// </summary>
		/// <param name="drawContext">描画コンテキスト</param>
		/// <param name="size">イメージサイズ</param>
		/// <param name="pen">ペン</param>
		/// <param name="cursor">カーソル位置</param>
		static public void Cursor(DrawingContext drawContext, Size size, Pen pen, Point cursor)
        {
            // 純粋な1dotの線を引くために0.5を足している
            cursor.X += 0.5;
			cursor.Y += 0.5;

			// 横線
			drawContext.DrawLine(pen, new Point(0, cursor.Y), new Point(cursor.X - 0.5, cursor.Y));
            drawContext.DrawLine(pen, new Point(cursor.X + 0.5, cursor.Y), new Point(size.Width, cursor.Y));

            // 縦線
            drawContext.DrawLine(pen, new Point(cursor.X, 0), new Point(cursor.X, cursor.Y - 0.5));
            drawContext.DrawLine(pen, new Point(cursor.X, cursor.Y + 0.5), new Point(cursor.X, size.Height));
        }
    }
}

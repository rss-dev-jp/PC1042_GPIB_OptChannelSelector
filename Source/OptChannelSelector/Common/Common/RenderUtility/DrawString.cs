using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace RssDev.Common.RenderUtility
{
    /// <summary>
    /// 解析情報描画
    /// </summary>
    public class DrawString
    {
        private int defaultFontSize = 10;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="defaultFontSize">デフォルトフォントサイズ</param>
        public DrawString(int defaultFontSize)
        {
            this.defaultFontSize = defaultFontSize;
        }   

        /// <summary>
        /// 2重の文字描画
        /// </summary>
        /// <param name="drawContext">描画先</param>
        /// <param name="st">描画文字列</param>
        public void DrawDoubleStr(DrawingContext drawContext, string st, int x, int y, Color color, TextAlignment alignment = TextAlignment.Left)
        {
            DrawStr(drawContext, st, Colors.White, defaultFontSize, x, y, alignment);
            DrawStr(drawContext, st, color, defaultFontSize, x + 1, y + 1, alignment);
        }

        /// <summary>
        /// 文字描画
        /// </summary>
        /// <param name="drawContext">描画先</param>
        /// <param name="st">描画文字列</param>
        public void DrawStr(DrawingContext drawContext, string st, Color color, int size, int x, int y, TextAlignment alignment = TextAlignment.Left)
        {
            var text = StringUtility.GetFormattedText(st, size, color);
            text.TextAlignment = alignment;
            //y -= (int)(text.Height / 2); // 垂直方向中央
            drawContext.DrawText(text, new Point(x, y));
        }

        /// <summary>
        /// 文字描画（垂直中央）
        /// </summary>
        /// <param name="drawContext">描画先</param>
        /// <param name="st">描画文字列</param>
        public void DrawStrVCenter(DrawingContext drawContext, string st, Color color, Brush backGround, int size, int x, int y, TextAlignment alignment = TextAlignment.Left, bool underLine = false)
        {
            var text = StringUtility.GetFormattedText(st, size, color);
            text.TextAlignment = alignment;
            if(underLine)
                text.SetTextDecorations(CreateUnderLineDecoration(color));

            y -= (int)(text.Height / 2); // 垂直方向中央

            if (backGround != null)
                drawContext.DrawRectangle(backGround, null, new Rect(x-2, y, text.Width+4, text.Height));

            drawContext.DrawText(text, new Point(x, y));
        }

        private TextDecorationCollection CreateUnderLineDecoration(Color color)
        {
            // Create an underline text decoration. Default is underline.
            TextDecoration myUnderline = new TextDecoration();

            // Create a linear gradient pen for the text decoration.
            Pen myPen = new Pen();
            myPen.Brush = new SolidColorBrush(color);
            myPen.Brush.Opacity = 1;
            myPen.Thickness = 1.0;
            myPen.DashStyle = DashStyles.Solid;
            myUnderline.Pen = myPen;
            myUnderline.PenThicknessUnit = TextDecorationUnit.FontRecommended;

            // Set the underline decoration to a TextDecorationCollection and add it to the text block.
            TextDecorationCollection myCollection = new TextDecorationCollection();
            myCollection.Add(myUnderline);
            return myCollection;
        }

        /// <summary>
        /// 出力文字のサイズを取得する
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontSize"></param>
        /// <param name="typeFace"></param>
        /// <returns></returns>
        public Size MeasureString(string text, double fontSize, string typeFace)
        {
            var ft = new FormattedText(text, CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface(typeFace),
            fontSize,
            Brushes.White,
            1); // pixels per dip 暫定で1としておく　　VisualTreeHelper.GetDpi(this).PixelsPerDipとかを使うようである

            return new Size(ft.Width, ft.Height);
        }

    }
}

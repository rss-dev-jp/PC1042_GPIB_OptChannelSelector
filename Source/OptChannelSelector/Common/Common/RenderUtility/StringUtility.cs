using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace RssDev.Common.RenderUtility
{
    /// <summary>
    /// 文字列描画ユーティリティクラス
    /// </summary>
    public class StringUtility
    {
        public const string FONT_NAME = "MS Gothic";

        /// <summary>
        /// 文字列描画用クラスを作成
        /// </summary>
        /// <param name="text">描画する文字列</param>
        /// <param name="fontSize">文字サイズ</param>
        /// <returns>文字列描画用クラス</returns>
        public static FormattedText GetFormattedText(string text, int fontSize)
        {
            return GetFormattedText(text, fontSize, Colors.White);
        }

        private static CultureInfo cultureInfo = new CultureInfo("ja-jp");

        /// <summary>
        /// 文字列描画用クラスを作成
        /// </summary>
        /// <param name="text">描画する文字列</param>
        /// <param name="fontSize">文字サイズ</param>
        /// <param name="color">色</param>
        /// <returns>文字列描画用クラス</returns>
        public static FormattedText GetFormattedText(string text, int fontSize, Color color, string fontName = FONT_NAME)
        {
            var yAxisText = new FormattedText(text,
                                            cultureInfo,
                                            FlowDirection.LeftToRight,
                                            GetTypeface(fontName),
                                            fontSize, new SolidColorBrush(color),
                                            1); // pixels per dip 暫定で1としておく　　VisualTreeHelper.GetDpi(this).PixelsPerDipとかを使うようである

            return yAxisText;
        }

        /// <summary>
        /// 文字情報生成
        /// </summary>
        /// <param name="fontName">フォント名</param>
        /// <returns></returns>
        private static Typeface GetTypeface(string fontName)
        {
            return new Typeface(new FontFamily(fontName), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
        }

    }
}

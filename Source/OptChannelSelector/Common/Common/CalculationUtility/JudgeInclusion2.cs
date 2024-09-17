using System;
using System.Windows;

namespace RssDev.Common.CalculationUtility
{
    /// <summary>
    /// 座標、四角形内の内外判定
    /// </summary>
    public class JudgeInclusion
    {
		/// <summary>
		/// WindingNumberAlgorithmを使った内外判定　最終的な高速のお進め、新規案件ではこちらを使用することを薦める
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="comparisonArr"></param>
		/// <returns></returns>
		/// <remarks>
		/// 点 w から見て、多角形 P 上を移動する時に何回転するかによって包含判定を行う。この時、回転しなければ包含されず、1回転以上すれば包含していることを意味する。
		/// 半直線 wvi と 半直線 wvi+1 のなす角 θi を求め、その総和 ∑1≤i≤Nθi を確認する。
        /// この値が 0 以外(この時は 2π またはその倍数) になれば包含され、0の場合は包含されないことが分かる。
        /// </remarks>
        static public bool Execute(Point p1, Point[] comparisonArr)
        {
			var p1x = p1.X;
			var p1y = p1.Y;
            var theta = 0.0;

			for (var index = 0; index < comparisonArr.Length; index++)
            {
				var p2x = comparisonArr[index].X;
				var p2y = comparisonArr[index].Y;
				double p3x;
				double p3y;

				if (index < comparisonArr.Length - 1)
				{
					p3x = comparisonArr[index + 1].X;
					p3y = comparisonArr[index + 1].Y;
				}
				else
				{
					p3x = comparisonArr[0].X;
					p3y = comparisonArr[0].Y;
				}

				p2x -= p1x;
				p2y -= p1y;
				p3x -= p1x;
				p3y -= p1y;

                var cv = p2x * p3x + p2y * p3y;
                var sv = p2x * p3y - p3x * p2y;
                if( sv == 0 && cv <= 0)
                {
                    // 点上にあるため内側
                    return true;
                }

                theta += Math.Atan2(sv, cv);
			}

            return Math.Abs(theta) > 1;
		}

    }
}

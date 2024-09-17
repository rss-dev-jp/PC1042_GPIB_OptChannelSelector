using System;
using System.Windows;
using System.Windows.Documents;

namespace RssDev.Common.CalculationUtility
{
	/// <summary>
	/// 座標、四角形内の内外判定
	/// </summary>
	/// <remarks>
	/// Execute2が一番高速、
	/// 2 > 3 > 1 の順で早い
	/// </remarks>
	public class JudgeInclusion2
	{
		/// <summary>
		/// 差分計算の格納用配列
		/// </summary>
		private static Point[] _subArray = new Point[100];		

		/// <summary>
		/// 内外判定(使用してOK)
		/// </summary>
		/// <param name="p1">判定したい点</param>
		/// <param name="comparisonArr">四角形座標、左上、右上、右下、左下　座標は左上0,0の画像座標とする</param>
		/// <returns>内ならtrue</returns>
		/// <remarks>
		/// 各点とのなす角の合計が360°ならば四角形内と判定する
		/// comparisonArrは反時計回りでも正常に動作するのでコストをかけて時計回りにしなくてもよい
		/// </remarks>
		static public bool Execute(Point p1, Point[] comparisonArr)
		{
			double deg = 0;
			var p1x = p1.X;
			var p1y = p1.Y;

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

				var ax = p2x - p1x;
				var ay = p2y - p1y;
				var bx = p3x - p1x;
				var by = p3y - p1y;


				var cos = (ax * bx + ay * by) / (Math.Sqrt(ax * ax + ay * ay) * Math.Sqrt(bx * bx + by * by));
				// 演算誤差の±1範囲外になる場合の対策
				if (cos > 1)
					cos = 1;

				if (cos < -1)
					cos = -1;


				// 外積の方向成分を計算
				var crossDir = ax * by - ay * bx;
				// 凸と凹みを判定、内積での判定と等価
				var unevenness = (crossDir >= 0) ? 1/*Unevenness.Convex*/ : /*Unevenness.Concave*/-1;

				var value = AngleUtility.Rad2Deg(Math.Acos(cos)) * unevenness;
				deg += value;
			}

			if (Math.Abs(Math.Round(deg)) == 360) // -360もありうるので絶対値で判定
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// WindingNumberAlgorithmを使った内外判定（使用してOK）
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="comparisonArr"></param>
		/// <returns></returns>
		/// <remarks>
		/// 点 w から見て、多角形 P 上を移動する時に何回転するかによって包含判定を行う。この時、回転しなければ包含されず、1回転以上すれば包含していることを意味する。
		/// 半直線 wvi と 半直線 wvi+1 のなす角 θi を求め、その総和 ∑1≤i≤Nθi を確認する。
		/// この値が 0 以外(この時は 2π またはその倍数) になれば包含され、0の場合は包含されないことが分かる。
		/// </remarks>
		static public bool Execute2(Point p1, Point[] comparisonArr)
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

				if (sv == 0 && cv <= 0)
				{
					// 点上にあるため内側
					return true;
				}

				theta += Math.Atan2(sv, cv);
			}

			return Math.Abs(theta) > 1;
		}

		/// <summary>
		/// WindingNumberAlgorithmを使った内外判定(配列使用版)　※あえて使う必要なし
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="comparisonArr"></param>
		/// <returns></returns>
		/// <remarks>
		/// 点 w から見て、多角形 P 上を移動する時に何回転するかによって包含判定を行う。この時、回転しなければ包含されず、1回転以上すれば包含していることを意味する。
		/// 半直線 wvi と 半直線 wvi+1 のなす角 θi を求め、その総和 ∑1≤i≤Nθi を確認する。
		/// この値が 0 以外(この時は 2π またはその倍数) になれば包含され、0の場合は包含されないことが分かる。
		/// </remarks>
		static public bool Execute3(Point p1, Point[] comparisonArr)
		{
			var p1x = p1.X;
			var p1y = p1.Y;
			var theta = 0.0;

			for(int i = 0; i < comparisonArr.Length; i++)
			{
				_subArray[i].X = comparisonArr[i].X - p1x;
				_subArray[i].Y = comparisonArr[i].Y - p1y;
			}
			_subArray[comparisonArr.Length].X = _subArray[0].X;
			_subArray[comparisonArr.Length].Y = _subArray[0].Y;


			for (var index = 0; index < comparisonArr.Length; index++)
			{
				var p2x = _subArray[index].X;
				var p2y = _subArray[index].Y;
				var p3x = _subArray[index + 1].X;
				var p3y = _subArray[index + 1].Y;

				var cv = p2x * p3x + p2y * p3y;
				var sv = p2x * p3y - p3x * p2y;

				if (sv == 0 && cv <= 0)
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

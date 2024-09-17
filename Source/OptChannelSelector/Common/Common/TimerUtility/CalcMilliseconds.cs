using System.Diagnostics;

namespace RssDev.Common.TimerUtility
{
    /// <summary>
    /// ミリ秒の計算ユーティリティ
    /// </summary>
    public class CalcMilliseconds
    {

        /// <summary>
        /// Stopwatchからミリ秒を計算
        /// </summary>
        /// <param name="stopWatch">ストップウォッチ</param>
        /// <returns>ミリ秒</returns>
        /// <remarks>StopwatchのMillisecondsより正確</remarks>
        static public double Get(Stopwatch stopWatch)
        {
            double sec = (double)stopWatch.ElapsedTicks / Stopwatch.Frequency;
            return sec * 1000;  // ミリ秒に変換
        }

    }
}

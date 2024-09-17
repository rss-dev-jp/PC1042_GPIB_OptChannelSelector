using System.Diagnostics;

namespace RssDev.Common.TimerUtility
{
    /// <summary>
    /// アプリケーション起動時間計算クラス
    /// </summary>
    public static class ApplicationTimer
    {
        /// <summary>
        /// 使用するストップウォッチ
        /// </summary>
        static private Stopwatch time = Stopwatch.StartNew();

        /// <summary>
        /// 現在時刻
        /// </summary>
        static public long CurrentTime { get { return (time.ElapsedMilliseconds & long.MaxValue); } }

        /// <summary>
        /// 指定時刻からの経過ms
        /// </summary>
        static public long GetSubTime(long srcTime, long toTime)
        {
            long sub = toTime - srcTime;
            if (sub < 0)
            {
                // マイナスの場合、long.MaxValueを考慮して計算
                return long.MaxValue + sub + 1;
            }
            else
            {
                return sub;
            }
        }
    }
}

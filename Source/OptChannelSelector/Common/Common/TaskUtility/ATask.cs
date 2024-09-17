
namespace RssDev.Common.TaskUtility
{
    /// <summary>
    //　タスクの抽象クラス
    /// </summary>
    public abstract class ATask
    {
        /// <summary>
        /// タスク処理実行フラグに使用
        /// </summary>
        protected bool isProcess = false;

        /// <summary>
        /// タスクスレッドID保存
        /// </summary>
        public int TaskThreadID { get; private set; }

        /// <summary>
        /// タスク実行回数
        /// </summary>
        protected int taskCount;

        /// <summary>
        /// タスク処理時間最大値
        /// </summary>
        protected int maxTime = 0;

        /// <summary>
        /// タスク処理時間平均値
        /// </summary>
        protected int averageTime = 0;

        /// <summary>
        /// タスク処理時間総合
        /// </summary>
        protected int totalTime = 0;

        /// <summary>
        /// スレッドID設定
        /// </summary>
        public void SetThreadID()
        {
            TaskThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
        }

        /// <summary>
        /// タスク処理レートカウント取得
        /// クリアされる
        /// </summary>
        /// <returns>レート値</returns>
        public int GetTaskRate()
        {
            int t = taskCount;
            if (t == 0)
                averageTime = 0;
            else
                averageTime = totalTime / t; // 平均を計算する

            totalTime = 0;
            taskCount = 0;
            return t;
        }

        /// <summary>
        /// 処理レート初期化
        /// </summary>
        public void ClearRate()
        {
            taskCount = 0;
        }

        /// <summary>
        /// 処理最大時間取得
        /// およびクリア
        /// </summary>
        /// <returns>処理最大時間</returns>
        public int GetMaxTime()
        {
            int t = maxTime;
            maxTime = 0;
            return t;
        }

        /// <summary>
        /// 処理最大時間初期化
        /// </summary>
        /// <returns></returns>
        public void ClearMaxTime()
        {
            maxTime = 0;
        }

        /// <summary>
        /// 処理平均時間取得
        /// およびクリア
        /// </summary>
        /// <returns>処理平均時間</returns>
        public int GetAverageTime()
        {
            int t = averageTime;
            averageTime = 0;
            return t;
        }

        /// <summary>
        /// クラス名称取得
        /// </summary>
        /// <returns>タスククラス名</returns>
        protected abstract string GetClassName();

        /// <summary>
        /// 情報取得関数
        /// </summary>
        /// <returns>タスク情報</returns>
        public TaskInformation GetInformation()
        {
            var info = new TaskInformation()
            {
                TaskName = this.GetType().Name,
                ID = TaskThreadID,
                Fps = GetTaskRate(),
                Max = GetMaxTime(),
                Tip = GetAverageTime(),
            };
            return info;
        }


    }
}

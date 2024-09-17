using System;
using System.Collections.Generic;
using System.Threading;

namespace RssDev.Common.TaskUtility
{
    /// <summary>
    /// AutoResetEventクラスのカスタム
    /// </summary>
    /// <remarks>
    /// AutoResetEventは、Waitの前にSetが呼ばれていればWaitはブロックされない
    /// Waitの前にSetを複数回実行しても、その回数はキューイングされない、１回だけ有効
    /// </remarks>
    public class TaskEvent
    {
        /// <summary>
        /// イベント
        /// </summary>
        private AutoResetEvent evt = new AutoResetEvent(false);

        /// <summary>
        /// 排他用
        /// </summary>
        private Object thisLock = new Object();

        /// <summary>
        /// パラメータ1
        /// </summary>
        private List<Object> parameterList1 = new List<Object>();

        /// <summary>
        /// 最終データフラグ用クラス
        /// </summary>
        private class LastEvent { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TaskEvent()
        {
        }

        /// <summary>
        /// セット（シグナル通知）
        /// </summary>
        /// <param name="parameter1">パラメータ</param>
        public void Set(Object parameter1)
        {
            lock (thisLock)
            {
                this.parameterList1.Add(parameter1);
                // リアルタイム性を重視するので2つめ以降は先頭を削除する
                if (this.parameterList1.Count >= 2)
                {
                    // ただし、LastEventは削除しない
                    if(!(this.parameterList1[0] is LastEvent))
                        this.parameterList1.RemoveAt(0);
                }
                this.evt.Set();
            }
        }

        /// <summary>
        /// セット（シグナル通知）、リスト上限チェックせずため込む
        /// </summary>
        /// <param name="parameter1">パラメータ</param>
        public void SetQueing(Object parameter1)
        {
            lock (thisLock)
            {
                this.parameterList1.Add(parameter1);
                this.evt.Set();
            }
        }

        /// <summary>
        /// 最終イベント通知
        /// </summary>
        /// <remarks>タスクやスレッドの最終の意味で使用する</remarks>
        public void LastEvant()
        {
            lock (thisLock)
            {
                this.parameterList1.Add(new LastEvent());
                this.evt.Set();
            }
        }

        /// <summary>
        /// イベント待ち
        /// </summary>
        /// <returns>シグナル受信でtrue</returns>
        public bool WaitOne()
        {
            // データがある場合はすぐに返す
            lock (thisLock)
            {
                if (parameterList1.Count > 0)
                    return true;

                evt.Reset(); // カウントが0の時はResetしてからWaitOneをコールすること
            }
            
            return evt.WaitOne();
        }

        /// <summary>
        /// イベント待ち
        /// </summary>
        /// <param name="timeoutms">タイムアウト</param>
        /// <returns>シグナル受信でtrue</returns>
        public bool WaitOne(int timeoutms)
        {
            // データがある場合はすぐに返す
            lock (thisLock)
            {
                if (parameterList1.Count > 0)
                    return true;

                evt.Reset(); // カウントが0の時はResetしてからWaitOneをコールすること
            }

            return evt.WaitOne(timeoutms);
        }

        /// <summary>
        /// パラメータの取得
        /// </summary>
        /// <returns>パラメータ</returns>
        public Object GetParameter1()
        {
            Object result = null;
            lock (thisLock)
            {
                if (parameterList1.Count > 0)
                {
                    result = parameterList1[0];
                    parameterList1.RemoveAt(0);
                }
            }
            return result;
        }

        /// <summary>
        /// オブジェクトの最終イベントチェック
        /// </summary>
        /// <param name="obj">チェックオブジェクト</param>
        /// <returns>最終イベントならtrue</returns>
        static public bool CheckLastEvent(Object obj)
        {
            if (obj as LastEvent == null)
                return false;
            else
                return true;
        }
    }
}

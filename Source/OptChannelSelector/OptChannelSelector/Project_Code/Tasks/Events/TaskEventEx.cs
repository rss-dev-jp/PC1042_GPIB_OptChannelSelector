using System;
using System.Collections.Generic;
using System.Threading;

namespace RssDev.Project_Code.Tasks.Events
{
	/// <summary>
	/// AutoResetEventクラスのカスタム
	/// </summary>
	/// <remarks>
	/// AutoResetEventは、Waitの前にSetが呼ばれていればWaitはブロックされない
	/// Waitの前にSetを複数回実行しても、その回数はキューイングされない、１回だけ有効
	/// </remarks>
	public class TaskEventEx
	{

		/// <summary>
		/// イベント
		/// </summary>
		private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

		/// <summary>
		/// 排他用
		/// </summary>
		private readonly object _lock = new object();

		/// <summary>
		/// パラメータ一覧
		/// </summary>
		private readonly List<object> _parameters = new List<object>();

		/// <summary>
		/// 最終データフラグ用クラス
		/// </summary>
		private class LastEvent { }

		/// <summary>リアルタイム性を重視するか</summary>
		private readonly bool _isRealTime = true;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="isRealTime">リアルタイム性を重視するか</param>
		public TaskEventEx(bool isRealTime)
		{
			_isRealTime = isRealTime;
		}

		/// <summary>
		/// セット（シグナル通知）
		/// </summary>
		/// <param name="parameter1">パラメータ</param>
		public void Set(Object parameter1)
		{
			lock (_lock)
			{

				_parameters.Add(parameter1);

				// リアルタイム性を重視する場合
				// 2つめ以降は先頭を削除する
				if (_isRealTime && _parameters.Count >= 2)
				{

					// ただし、LastEventは削除しない
					if (!(_parameters[0] is LastEvent))
					{
						_parameters.RemoveAt(0);
					}

				}

				_autoResetEvent.Set();

			}
		}

		/// <summary>
		/// 最終イベント通知
		/// </summary>
		/// <remarks>タスクやスレッドの最終の意味で使用する</remarks>
		public void LastEvant()
		{
			lock (_lock)
			{
				_parameters.Add(new LastEvent());
				_autoResetEvent.Set();
			}
		}

		/// <summary>
		/// イベント待ち
		/// </summary>
		/// <returns>シグナル受信でtrue</returns>
		public bool WaitOne()
		{
			// データがある場合はすぐに返す
			lock (_lock)
			{
				if (_parameters.Count > 0)
					return true;

				_autoResetEvent.Reset(); // カウントが0の時はResetしてからWaitOneをコールすること
			}

			return _autoResetEvent.WaitOne();
		}

		/// <summary>
		/// イベント待ち
		/// </summary>
		/// <param name="timeoutms">タイムアウト</param>
		/// <returns>シグナル受信でtrue</returns>
		public bool WaitOne(int timeoutms)
		{
			// データがある場合はすぐに返す
			lock (_lock)
			{
				if (_parameters.Count > 0)
					return true;

				_autoResetEvent.Reset(); // カウントが0の時はResetしてからWaitOneをコールすること
			}

			return _autoResetEvent.WaitOne(timeoutms);
		}

		/// <summary>
		/// パラメータの取得
		/// </summary>
		/// <returns>パラメータ</returns>
		public object GetParameter1()
		{
			object result = null;
			lock (_lock)
			{
				if (_parameters.Count > 0)
				{
					result = _parameters[0];
					_parameters.RemoveAt(0);
				}
			}
			return result;
		}

		/// <summary>
		/// オブジェクトの最終イベントチェック
		/// </summary>
		/// <param name="obj">チェックオブジェクト</param>
		/// <returns>最終イベントならtrue</returns>
		static public bool CheckLastEvent(object obj)
		{
			if (obj as LastEvent == null)
				return false;
			else
				return true;
		}

		/// <summary>
		/// パラメータ解放
		/// </summary>
		public void ClearParameter()
		{
			lock (_lock)
			{
				_parameters.Clear();
			}
		}

		/// <summary>
		/// 待機パラメータが存在するかチェック
		/// </summary>
		/// <returns>
		/// true:存在する
		/// false:存在しない
		/// </returns>
		public bool ExistsParameters()
		{
			return !_parameters.Count.Equals(0);
		}

	}
}

using System.Reflection;
using System.Threading;

namespace RssDev.Project_Code.Threadings
{

	/// <summary>
	/// Mutex所有権管理
	/// </summary>
	public static class MutexManager
	{

		/// <summary>
		/// Mutexの所有権を得ているか
		/// </summary>
		public static bool IsHandle { get; private set; } = false;

		/// <summary>
		/// Mutex
		/// </summary>
		private static Mutex _mutex;

		/// <summary>
		/// Mutexの所有権を要求する
		/// </summary>
		/// <returns>
		/// true:所有権を取得した
		/// false:所有権を取得できなかった(多重起動)
		/// </returns>
		public static bool Request()
		{

			var name = Assembly.GetEntryAssembly().GetName().Name;
			_mutex = new Mutex(false, name);

			try
			{

				// Mutexの所有権を要求する
				IsHandle = _mutex.WaitOne(0, false);

			}
			// 別のアプリケーションがMutexを解放しないで終了した時
			catch (AbandonedMutexException)
			{
				IsHandle = true;
			}

			return IsHandle;

		}

		/// <summary>
		/// Mutex解放
		/// </summary>
		public static void Release()
		{

			// 所有権を取得している場合はMutexを解放する
			if (IsHandle)
			{
				_mutex.ReleaseMutex();
			}
			_mutex?.Close();

		}

	}

}

namespace RssDev.Project_Code.Containers
{

	/// <summary>
	/// Task処理完了コンテナ
	/// </summary>
	public class CompletedContainer
	{

		/// <summary>
		/// 通信ログコンテナ
		/// </summary>
		public LogContainer LogContainer { get; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="logContainer">通信ログコンテナ</param>
		public CompletedContainer(LogContainer logContainer)
		{
			LogContainer = logContainer;
		}

	}

}

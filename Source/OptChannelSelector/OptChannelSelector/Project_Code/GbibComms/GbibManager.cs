#undef NoComms  // 非通信定義          

using Ivi.Visa;
using RssDev.Project_Code.Defines;
using RssDev.RuntimeLog;
using System;

namespace RssDev.Project_Code.GbibComms
{

	/// <summary>
	/// GBIB通信管理
	/// </summary>
	public class GbibManager
	{

#if NoComms

		/// <summary>
		/// GBIB通信機器
		/// 接続フラグ
		/// </summary>
		private bool _isOpen = false;

		/// <summary>
		/// GBIB通信機器
		/// 接続フラグ
		/// </summary>
		public bool IsOpen { get { return _isOpen; } }

		/// <summary>
		/// 送信コマンド
		/// </summary>
		private string _sendCommand;

#else

		/// <summary>
		/// GBIB通信機器
		/// 接続フラグ
		/// </summary>
		public bool IsOpen { get { return _session != null; } }

		/// <summary>
		/// VISA.NETセッション
		/// </summary>
		private IMessageBasedSession _session;

#endif

		/// <summary>
		/// 戻り値要求コマンド送信フラグ
		/// </summary>
		private bool _isSendRequestCommand = false;

		/// <summary>
		/// 接続開始
		/// </summary>
		/// <param name="address">VISAアドレス</param>
		public void Open(string address)
		{
			if (!IsOpen)
			{
#if NoComms
				_isOpen = true;
#else
				_session = (IMessageBasedSession)GlobalResourceManager.Open(address);
#endif
				RuntimeLogger.Instance.Add(RuntimeLogger.Type.COMMENT, $"GBIB接続開始：{address}");
			}
		}

		/// <summary>
		/// 接続終了
		/// </summary>
		public void Close()
		{
			if (IsOpen)
			{
#if NoComms
				_isOpen = false;
#else
				_session.Dispose();
				_session = null;
#endif
				RuntimeLogger.Instance.Add(RuntimeLogger.Type.COMMENT, $"GBIB切断");
			}
		}

		/// <summary>
		/// コマンド送信
		/// </summary>
		/// <param name="command">コマンド</param>
		public void Write(string command)
		{
			if (IsOpen)
			{

				_isSendRequestCommand = CommandDefine.Instance.CheckRequestCommand(command);

#if NoComms
				_sendCommand = command;
#else
				_session.FormattedIO.DiscardBuffers();  // 送受信バッファクリア
				_session.FormattedIO.WriteLine(command);
#endif
				RuntimeLogger.Instance.Add(RuntimeLogger.Type.COMMENT, $"GBIBコマンド送信：{command}");
			}
		}

		/// <summary>
		/// 送信したコマンドに対する戻り値受信
		/// </summary>
		/// <param name="timeout">タイムアウト[ms]</param>
		/// <returns>受信メッセージ</returns>
		public string Read(int timeout)
		{

			if (IsOpen)
			{

				// 送信コマンド
				if (!_isSendRequestCommand)
				{
					return string.Empty;
				}

				// 受信処理開始
				try
				{
					return _session.FormattedIO.ReadLine();
				}
				catch (Ivi.Visa.IOTimeoutException ex)
				{
					throw new TimeoutException("Timeout発生");
				}

			}

			RuntimeLogger.Instance.Add(RuntimeLogger.Type.EXCEPTION, $"GBIB未接続時に{nameof(Read)}()メソッドを実行");
			return string.Empty;

		}

	}

}

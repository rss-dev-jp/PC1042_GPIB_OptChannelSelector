#undef NoComms  // 非通信定義          

using Ivi.Visa;
using RssDev.Project_Code.Defines;
using RssDev.RuntimeLog;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RssDev.Project_Code.GbibComms
{

	/// <summary>
	/// GBIB通信管理
	/// </summary>
	public class GbibManager
	{

		/// <summary>
		/// フッタ
		/// </summary>
		private const string DATA_FOOTER = "\r\n";

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
		/// 読み込み実行フラグ
		/// </summary>
		private bool _isReading = false;

		/// <summary>
		/// 取得した戻り値
		/// </summary>
		private string _readMessage = string.Empty;

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
				_session.FormattedIO.Write(command + DATA_FOOTER);
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
				_isReading = true;
				_readMessage = string.Empty;
				Task.Run(() => ReadProcess());

				// 戻り値受信まで待機
				var stopwatch = Stopwatch.StartNew();
				while (_isReading)
				{

					Thread.Sleep(1);

					if (stopwatch.ElapsedMilliseconds > timeout)
					{
						_isReading = false;
						RuntimeLogger.Instance.Add(RuntimeLogger.Type.EXCEPTION, $"GBIB通信エラー：Read Timeout");
						throw new TimeoutException("Timeout発生");
					}

				}

				return _readMessage;

			}

			RuntimeLogger.Instance.Add(RuntimeLogger.Type.EXCEPTION, $"GBIB未接続時に{nameof(Read)}()メソッドを実行");
			return string.Empty;

		}

		/// <summary>
		/// 送信したコマンドに対する戻り値受信
		/// </summary>
		private void ReadProcess()
		{

			var message = new StringBuilder();
			while (_isReading)
			{

#if NoComms

				Thread.Sleep(100);

				// CSEL:CHAN?
				if (_sendCommand == CommandDefine.Instance.GetCommandCheckStatus())
				{
					message.Append("1").Append(DATA_FOOTER);
				}
				// *IDN?
				else if (_sendCommand == CommandDefine.Instance.GetCommandCheckModel())
				{
					message.Append("OPTHUB,XXXXXXX,Ver.YY").Append(DATA_FOOTER);
				}
				// 戻り値がある入力コマンド
				else
				{
					message.Append("Return value for Input command").Append(DATA_FOOTER);
				}

#else
				message.Append(_session.FormattedIO.ReadString());
#endif

				var index = message.ToString().IndexOf(DATA_FOOTER);
				if (index != -1)
				{
					_readMessage = message.ToString(0, index);
					RuntimeLogger.Instance.Add(RuntimeLogger.Type.COMMENT, $"GBIB受信：{_readMessage}");
					_isReading = false;
				}

			}

		}

	}

}

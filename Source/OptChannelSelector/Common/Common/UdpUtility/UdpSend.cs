using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;

namespace RssDev.Common.UdpUtility
{
    /// <summary>
    /// UDPでの送信処理クラス
    /// </summary>
    public class UdpSend
    {
        /// <summary>
        /// 排他用オブジェクト
        /// </summary>
        private object thislock = new object();

        /// <summary>
        /// スレッド終了フラグ
        /// </summary>
        private bool isProcess = true;

        /// <summary>
        /// 送信データリスト
        /// </summary>
        private List<SendData> sendDataList;

        /// <summary>
        /// 送信先のポート番号
        /// </summary>
        private int toPort;

        /// <summary>
        /// シングルトンインスタンス
        /// </summary>
        static private UdpSend instance = null;

        /// <summary>
        /// インスタンス取得
        /// </summary>
        static public UdpSend GetInstance(int toPort)
        {
            if (instance == null)
                instance = new UdpSend();

            instance.toPort = toPort;
            return instance;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private UdpSend()
        {
            instance = this;
            sendDataList = new List<SendData>();

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// タスク終了
        /// </summary>
        public void Dispose()
        {
            isProcess = false;
        }

        /// <summary>
        /// 送信処理
        /// </summary>
        /// <param name="command">コマンド</param>
        /// <param name="remoteHost">送信先IP</param>
        /// <remarks>排他される</remarks>
        public void Transfer(string command, string remoteHost)
        {
            lock (thislock)
            {
                sendDataList.Add(new SendData(command, remoteHost));
            }
            /* バックグラウンドで送信を行う、下記コードではブロッキングされてしまう
            lock (thislock)
            {
                //データを送信するリモートホストとポート番号
                int remotePort = Define.UDP_PORT;

                //UdpClientオブジェクトを作成する
                UdpClient udp = new UdpClient();

                //送信データ作成
                byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(command.GetString());

                //リモートホストを指定してデータを送信する 送れない場合の再送はしない
                udp.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);

                udp.Close();
            }
             **/
        }

        /// <summary>
        /// UDP送信処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isProcess)
            {
                SendData send = null;
                lock (thislock)
                {
                    if (sendDataList.Count > 0)
                    {
                        send = sendDataList[0];
                        sendDataList.RemoveAt(0);
                    }
                }
                if (send == null)
                {
                    Thread.Sleep(1); // CPU負荷軽減
                    continue;
                }
                // 送信処理
                Execute(send.Command, send.RemoteHost, toPort);
            }
        }

        /// <summary>
        /// UDP送信
        /// </summary>
        /// <param name="command">コマンド</param>
        /// <param name="remoteHost">送信先</param>
        /// <param name="toPort">送信先ポート番号</param>
        static public void Execute(string command, string remoteHost, int toPort)
        {
            //データを送信するリモートホストとポート番号
            int remotePort = toPort;

            //UdpClientオブジェクトを作成する
            UdpClient udp = new UdpClient();

            byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(command);

            //リモートホストを指定してデータを送信する 送れない場合の再送はしない
            udp.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);

            udp.Close();
        }

        /// <summary>
        /// 送信データ
        /// </summary>
        class SendData
        {
            public string Command { get; private set; }
            public string RemoteHost { get; private set; }

            public SendData(string command, string remoteHost)
            {
                this.Command = command;
                this.RemoteHost = remoteHost;
            }
        }
    }
}

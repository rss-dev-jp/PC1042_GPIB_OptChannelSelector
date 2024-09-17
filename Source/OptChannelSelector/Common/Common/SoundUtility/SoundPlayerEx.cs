using RssDev.Common.ApplicationUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RssDev.Common.SoundUtility
{
    /// <summary>
    /// 音声再生プレイヤー
    /// </summary>
    /// <remarks>
    /// 標準のSoundPlayerが、多重再生に対応していないため、
    /// MediaPlayerベースの再生クラスを作成する
    /// </remarks>
    public class SoundPlayerEx
    {
        /// <summary>
        /// インスタンス取得、シングルトン
        /// </summary>
        static private SoundPlayerEx instance = null;
        static public SoundPlayerEx Instance 
        {
            get
            {
                if (instance == null)
                    instance = new SoundPlayerEx();
                return　instance;
            }
        }

        /// <summary>
        /// 再生データリスト
        /// </summary>
        private List<PlayData> playDataList = new List<PlayData>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private SoundPlayerEx()
        {
            instance = this;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < playDataList.Count; i++)
            {
                playDataList[i].Dispose();
            }
            instance = null;
        }

        /// <summary>
        /// 再生する音源登録
        /// </summary>
        /// <param name="uri">音声データ</param>
        /// <param name="count">保持数（同時再生最大）</param>
        public void Regist(Uri uri, int count)
        {
            bool isReg = false;
            for (int i = 0; i < playDataList.Count; i++)
            {
                // 上書きチェック
                if (playDataList[i].IsEqual(uri))
                {
                    isReg = true;
                    playDataList[i].Dispose();
                    playDataList[i] = new PlayData(uri, count);
                    break;
                }
            }
            if (!isReg)
            {
                playDataList.Add(new PlayData(uri, count));
            }
        }

        /// <summary>
        /// 再生処理
        /// </summary>
        /// <param name="uri">再生したいURL</param>
        /// <param name="volume">ボリューム　0.5が規定値 0..1</param>
        public void Play(Uri uri, double volume = 0.5)
        {
            foreach (var playData in playDataList)
            {
                if (playData.IsEqual(uri))
                {
                    playData.Play(volume);
                    break;
                }
            }
        }

        /// <summary>
        /// 再生停止処理
        /// </summary>
        /// <param name="uri">したいURL</param>
        /// <remarks>
        /// この機能を使う場合は登録は１つにすること
        /// </remarks>
        public void Stop(Uri uri)
        {
            foreach (var playData in playDataList)
            {
                if (playData.IsEqual(uri))
                {
                    playData.Stop();
                    break;
                }
            }
        }

        /// <summary>
        /// 再生データ
        /// </summary>
        private class PlayData
        {
            private MediaPlayer[] mediaPlayers;
            private int index = 0;
            private Uri uri;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="uri">音声データ</param>
            /// <param name="count">保持数（同時再生最大）</param>
            public PlayData(Uri uri, int count)
            {
                this.uri = uri;
                mediaPlayers = new MediaPlayer[count];
                for (int i = 0; i < mediaPlayers.Length; i++)
                {
                    mediaPlayers[i] = new MediaPlayer();
                    mediaPlayers[i].IsMuted = true;  // Open直後にノイズ音が発生する不具合があるようなのでMuteする
                    //mp.MediaEnded += mp_MediaEnded;   // 割り込みは使用するとMainスレッドに負荷がかかるので使用しない
                    //mp.MediaOpened += mp_MediaOpened; //
                    mediaPlayers[i].MediaFailed += mp_MediaFailed;   // エラーはハンドリング
                    mediaPlayers[i].Open(uri);                       // WAVファイルオープン
                    mediaPlayers[i].Position = new TimeSpan(0);      // 先頭にシークする
                }
            }

            /// <summary>
            /// Url比較
            /// </summary>
            /// <param name="uri">音源</param>
            /// <returns>一致ならtrue</returns>
            public bool IsEqual(Uri uri)
            {
                return this.uri.Equals(uri);
            }

            /// <summary>
            /// 解放処理
            /// </summary>
            public void Dispose()
            {
                for (int i = 0; i < mediaPlayers.Length; i++)
                {
                    mediaPlayers[i].Stop();
                    mediaPlayers[i].Close();
                }
                mediaPlayers = null;
            }

            /// <summary>
            /// 再生エラー時
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void mp_MediaFailed(object sender, ExceptionEventArgs e)
            {
                // ログメッセージ等
                Debug.WriteLine("PlayData " + uri + "\n" +
                    e.ErrorException.Message);
                // 解放
                var mp = (MediaPlayer)sender;
                mp.Close();
            }

            /// <summary>
            /// 再生処理
            /// </summary>
            public void Play(double volume)
            {
                mediaPlayers[index].IsMuted = true;  // Open直後にノイズ音が発生する不具合があるようなのでMuteする
                mediaPlayers[index].Stop();
                mediaPlayers[index].Volume = volume;    
                mediaPlayers[index].Position = new TimeSpan(0);      // 先頭にシークする
                mediaPlayers[index].IsMuted = false;  // Open直後にノイズ音が発生する不具合があるようなのでMuteする
                mediaPlayers[index].Play();

                index++;
                index %= mediaPlayers.Length;
            }

            /// <summary>
            /// 再生処理
            /// </summary>
            public void Stop()
            {
                mediaPlayers[index].IsMuted = true;  // Open直後にノイズ音が発生する不具合があるようなのでMuteする
                mediaPlayers[index].Stop();
                mediaPlayers[index].Position = new TimeSpan(0);      // 先頭にシークする
            }
        }
    }
}

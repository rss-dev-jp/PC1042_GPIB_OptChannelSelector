using System.Windows.Threading;

namespace RssDev.Common.ApplicationUtility
{
    /// <summary>
    /// WPFではApplication.DoEvent()相当の処理がないのでそれを行うクラス
    /// </summary>
    public class DoEventWrapper
    {
        /// <summary>
        /// Application.DoEvent()相当の処理
        /// </summary>
        static public void DoEvents()
        {
            var frame = new DispatcherFrame();
            var callback = new DispatcherOperationCallback(ExitFrames);
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame);
            Dispatcher.PushFrame(frame);
        }

        /// <summary>
        /// Application.DoEvent()相当の処理
        /// </summary>
        static private object ExitFrames(object obj)
        {
            ((DispatcherFrame)obj).Continue = false;
            return null;
        }
    }
}

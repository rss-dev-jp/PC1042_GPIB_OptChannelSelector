using System.Windows.Input;

namespace RssDev.Common.ApplicationUtility
{
    /// <summary>
    /// キー押下状態チェック
    /// </summary>
    public class KeyCheck
    {
        /// <summary>
        /// シフトキー押下チェック
        /// </summary>
        /// <returns></returns>
        public static bool IsShiftKeyDown()
        {
            return ((Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) == KeyStates.Down ||
                    (Keyboard.GetKeyStates(Key.RightShift) & KeyStates.Down) == KeyStates.Down);
        }

        /// <summary>
        /// コントロールキー押下チェック
        /// </summary>
        /// <returns></returns>
        public static bool IsCtrlKeyDown()
        {
            return ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) == KeyStates.Down ||
                    (Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) == KeyStates.Down);
        }

        /// <summary>
        /// Altキー押下チェック
        /// </summary>
        /// <returns></returns>
        public static bool IsAltKeyDown()
        {
            return ((Keyboard.GetKeyStates(Key.LeftAlt) & KeyStates.Down) == KeyStates.Down ||
                    (Keyboard.GetKeyStates(Key.RightAlt) & KeyStates.Down) == KeyStates.Down);
        }

    }
}

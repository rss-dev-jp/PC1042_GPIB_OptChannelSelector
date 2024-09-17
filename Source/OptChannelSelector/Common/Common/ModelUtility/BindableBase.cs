using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RssDev.Common.ModelUtility
{
    /// <summary>
    /// モデルを簡略化するための INotifyPropertyChanged 継承クラス
    /// </summary>
    public abstract class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティの変更を通知するためのマルチキャスト イベント。
        /// </summary>
        /// <remarks>これはINotifyPropertyChanged継承で必要宣言</remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// プロパティが目的の値と一致しているかどうかを確認。
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="field">フィールドメンバ名</param>
        /// <param name="value">格納値</param>
        /// <param name="propertyName">プロパティ名称（省略してよい）</param>
        /// <returns>値が変更された場合は true、既存の値が目的の値に一致した場合はfalse</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberNameAttribute] string propertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// プロパティ値が変更されたことをリスナーに通知します。
        /// </summary>
        /// <param name="propertyName">リスナーに通知するために使用するプロパティの名前。
        /// この値は省略可能で、
        /// <see cref="CallerMemberNameAttribute"/> をサポートするコンパイラから呼び出す場合に自動的に指定できます。</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>指定クラスの全プロパティに対しOnPropertyChanged()を実行</summary>
        /// <typeparam name="T">指定クラス</typeparam>
        protected void OnPropertyChangedAll<T>() where T : class
        {
            foreach (var item in typeof(T).GetProperties())
            {
                OnPropertyChanged(item.Name);
            }
        }
    }
}

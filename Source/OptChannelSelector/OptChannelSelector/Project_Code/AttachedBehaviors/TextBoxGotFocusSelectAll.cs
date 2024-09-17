using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RssDev.Project_Code.AttachedBehaviors
{

	/// <summary>
	/// TextBoxにてフォーカス取得時にTextを全選択状態にする添付ビヘイビア
	/// </summary>
	public class TextBoxGotFocusSelectAll
	{

		/// <summary>
		/// Text全選択状態有効フラグの値変更イベント
		/// </summary>
		/// <param name="sender">TextBox</param>
		/// <param name="e">プロパティ変更イベントデータ</param>
		private static void OnIsSelectAllChanged(object sender, DependencyPropertyChangedEventArgs e)
		{

			if (e.NewValue is bool value
				&& value
				&& sender is TextBox textBox)
			{
				textBox.GotFocus += OnGotFocus;
				textBox.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
			}
			else
			{
				try
				{
					OnUnloaded(sender, null);
				}
				catch { }
			}

		}

		/// <summary>イベント解除</summary>
		/// <param name="sender">TextBox</param>
		/// <param name="e">イベントデータ</param>
		private static void OnUnloaded(object sender, RoutedEventArgs e)
		{

			if (sender is TextBox textBox)
			{
				textBox.GotFocus -= OnGotFocus;
				textBox.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
			}

		}

		/// <summary>
		/// フォーカス取得イベント
		/// </summary>
		/// <param name="sender">TextBox</param>
		/// <param name="e">イベントデータ</param>
		private static void OnGotFocus(object sender, RoutedEventArgs e)
		{

			if (sender is TextBox textBox)
			{
				textBox.SelectAll();
			}

		}

		/// <summary>
		/// マウス左ボタン押下イベント
		/// </summary>
		/// <param name="sender">TextBox</param>
		/// <param name="e">マウスボタンイベントデータ</param>
		private static void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is TextBox textBox
				&& !textBox.IsFocused
				&& textBox.IsEnabled
				&& textBox.IsHitTestVisible
				&& textBox.Focusable)
			{
				// フォーカス取得 => OnFocus()実行
				Keyboard.Focus(textBox);
				e.Handled = true;
			}
		}

		/****************************************************************************************/
		//　これ以降はバインドデータ

		/// <summary>
		/// Text全選択状態有効フラグ
		/// </summary>
		public static readonly DependencyProperty IsSelectAllProperty
			= DependencyProperty.RegisterAttached(
				"IsSelectAll",
				typeof(bool),
				typeof(TextBoxGotFocusSelectAll),
				new UIPropertyMetadata(false, OnIsSelectAllChanged));

		/// <summary>
		/// Text全選択状態有効フラグの現在値を取得
		/// </summary>
		/// <param name="sender">TextBox</param>
		/// <returns>現在値</returns>
		[AttachedPropertyBrowsableForType(typeof(TextBox))]
		public static bool GetIsSelectAll(DependencyObject sender)
		{
			return (bool)sender.GetValue(IsSelectAllProperty);
		}

		/// <summary>
		/// Text全選択状態有効フラグを設定
		/// </summary>
		/// <param name="sender">TextBox</param>
		/// <param name="value">設定値</param>
		[AttachedPropertyBrowsableForType(typeof(TextBox))]
		public static void SetIsSelectAll(DependencyObject sender, bool value)
		{
			sender.SetValue(IsSelectAllProperty, value);
		}

	}

}

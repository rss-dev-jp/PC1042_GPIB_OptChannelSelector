using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace RssDev.Project_Code.AttachedBehaviors
{

	/// <summary>
	/// Enter押下でTextBox.Textプロパティを更新させる添付ビヘイビア
	/// </summary>
	public class TextBoxUpdateProperty
	{

		/// <summary>Enterキー押下判定フラグ値の更新イベント</summary>
		/// <param name="sender">TextBox</param>
		/// <param name="e">プロパティ変更イベントデータ</param>
		private static void OnIsInputEnterChanged(object sender, DependencyPropertyChangedEventArgs e)
		{

			if (e.NewValue is bool value
				&& value
				&& sender is TextBox textBox)
			{

				textBox.PreviewKeyDown += OnPreviewKeyDown;
				textBox.Unloaded += OnUnloaded;

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

				textBox.PreviewKeyDown -= OnPreviewKeyDown;
				textBox.Unloaded -= OnUnloaded;

			}

		}

		/// <summary>キー押下イベント</summary>
		/// <param name="sender">TextBox</param>
		/// <param name="e">キーイベントデータ</param>
		private static void OnPreviewKeyDown(object sender, KeyEventArgs e)
		{

			if (sender is TextBox textBox)
			{

				switch (e.Key)
				{

					case Key.Return:
						textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
						//e.Handled = true;
						break;

					default:
						break;

				}

			}

		}

		/****************************************************************************************/
		//　これ以降はバインドデータ

		/// <summary>Enterキー押下でTextプロパティを更新するか</summary>
		public static readonly DependencyProperty IsInputEnterProperty
			= DependencyProperty.RegisterAttached(
				"IsInputEnter",
				typeof(bool),
				typeof(TextBoxUpdateProperty),
				new UIPropertyMetadata(false, OnIsInputEnterChanged));

		/// <summary>Enterキー押下判定フラグ値を取得</summary>
		/// <param name="sender">TextBox</param>
		/// <returns>Enterキー押下判定フラグ値</returns>
		[AttachedPropertyBrowsableForType(typeof(TextBox))]
		public static bool GetIsInputEnter(DependencyObject sender)
		{
			return (bool)sender.GetValue(IsInputEnterProperty);
		}

		/// <summary>Enterキー押下判定フラグ値の設定</summary>
		/// <param name="sender">TextBox</param>
		/// <param name="value">設定値</param>
		[AttachedPropertyBrowsableForType(typeof(TextBox))]
		public static void SetIsInputEnter(DependencyObject sender, bool value)
		{
			sender.SetValue(IsInputEnterProperty, value);
		}

	}

}

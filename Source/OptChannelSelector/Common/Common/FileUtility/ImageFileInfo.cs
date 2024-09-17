using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shell32;
using System.Text.RegularExpressions;
using System.Windows;
using RssDev.Common.ApplicationUtility;

namespace RssDev.Common.FileUtility
{
	/// <summary>
	/// 画像、動画ファイルの情報を取得するクラス
	/// </summary>
	public class ImageFileInfo
	{
		public TimeSpan PlayTime { private set; get; }
		public int Width { private set; get; }
		public int Height { private set; get; }
        public bool IsMovie { private set; get; }

		private static ShellItem[] shellItem =
		{
			new ShellItem(){Item = "認識された種類"},
			new ShellItem(){Item = "大きさ"},
			new ShellItem(){Item = "長さ"},
			new ShellItem(){Item = "フレーム高"},
			new ShellItem(){Item = "フレーム幅"},
		};

		private enum ShellIndex
		{ 
			Type = 0,
			ImageSize, MovieTime, MovieWidth, MovieHeight,
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="path">ファイルのフルパス</param>
		public ImageFileInfo(string path)
		{
			// ファイルがない
			if (System.IO.File.Exists(path) == false)
			{
				throw new Exception("ファイルが見つからない");
			}
			// フルパス変換
			// path = ApplicationPath.GetAndChecktFullPath(path);
			path = System.IO.Path.GetFullPath(path);


			//　インデックスの取得をするのは初回だけ
			if (shellItem[0].Index == -1)
			{
				GetIndex(path);
			}

			var shell = new Shell();
			var dir = System.IO.Path.GetDirectoryName(path);
			var nameSpace = shell.NameSpace(System.IO.Path.GetDirectoryName(path));
			var item = nameSpace.ParseName(System.IO.Path.GetFileName(path));

            IsMovie = false;

			switch (nameSpace.GetDetailsOf(item, shellItem[(int)ShellIndex.Type].Index))
			{ 
				case "イメージ":
					// サイズは "幅 x 高"のフォーマット
					var data = nameSpace.GetDetailsOf(item, shellItem[(int)ShellIndex.ImageSize].Index);
					var split = data.Split('x');
					var re = new Regex(@"[^0-9]");
					Width = int.Parse(re.Replace(split[0], ""));
					Height = int.Parse(re.Replace(split[1], ""));
					break;

				case "ビデオ":
                    IsMovie = true;
					PlayTime = TimeSpan.Parse(nameSpace.GetDetailsOf(item, shellItem[(int)ShellIndex.MovieTime].Index));
					Width = int.Parse(nameSpace.GetDetailsOf(item, shellItem[(int)ShellIndex.MovieWidth].Index));
					Height = int.Parse(nameSpace.GetDetailsOf(item, shellItem[(int)ShellIndex.MovieHeight].Index));			
					break;
			}
		}

		/// <summary>
		/// 必要なインデックスを取得する
		/// </summary>
		private void GetIndex(string path)
		{
			var shell = new Shell();
			var dir = System.IO.Path.GetDirectoryName(path);
			var nameSpace = shell.NameSpace(System.IO.Path.GetDirectoryName(path));

			foreach (var item in shellItem)
			{
				for (int i = 0; i < 500; i++)
				{
					var name = nameSpace.GetDetailsOf(null, i);
//					Console.WriteLine(i.ToString() + ":" + name);
					if (name == item.Item)
					{
						item.Index = i;
						break;
					}
				}
			}
		}

		/// <summary>
		/// シェルで取得するのに必要なインデックスと項目名
		/// </summary>
		private class ShellItem
		{
			public int Index = -1;
			public string Item = "";

		}
	}
}

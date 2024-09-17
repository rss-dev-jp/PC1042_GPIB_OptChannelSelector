using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssDev.Common.UsbUtility
{
	/// <summary>
	/// USBメモリを検出する
	/// </summary>
	public class UsbMemoryDetect
	{
		/// <summary>
		/// 現在接続されているUSBドライブのパスを取得する
		/// </summary>
		/// <returns></returns>
		public static List<string> GetUsbDrive()
		{
			var allDrives = DriveInfo.GetDrives();
			var usbList = new List<string>();
			foreach (var drive in allDrives)
			{
				if (drive.DriveType == DriveType.Removable && drive.IsReady)
				{
					usbList.Add(drive.RootDirectory.FullName);
				}
			}

			return usbList;
		}
	}
}

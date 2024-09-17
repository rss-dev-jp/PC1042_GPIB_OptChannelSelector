using RssDev.Common.ApplicationUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RssDev.Common.FileUtility
{
    public static class FileThumbnailExtractor
    {
        [StructLayoutAttribute(LayoutKind.Sequential)]
        struct SIZE
        {
            public int cx;
            public int cy;
        }

        [Flags]
        enum SIIGBF
        {
            RESIZETOFIT = 0,
            BIGGERSIZEOK = 1,
            MEMORYONLY = 2,
            ICONONLY = 4,
            THUMBNAILONLY = 8,
            INCACHEONLY = 0x10,
            CROPTOSQUARE = 0x20,
            WIDETHUMBNAILS = 0x40,
            ICONBACKGROUND = 0x80,
            SCALEUP = 0x100,
        }

        [ComImport, Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface IShellItemImageFactory
        {
            [PreserveSig]
            void GetImage(SIZE size, SIIGBF flags, out IntPtr phbm);
        }

        [DllImport("shell32"), PreserveSig]
        extern static void SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IntPtr pbc, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object iunk);

        [DllImport("gdi32")]
        extern static int DeleteObject(IntPtr hObject);

        public static BitmapSource GetThumbnail(string fileName, int desiredWidth, int desiredHeight, ThumbnailType type)
        {
            object iunk = null;
            IntPtr hBmp = IntPtr.Zero;
            try
            {
                var IID_IShellItemImageFactory = new Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b");
                SHCreateItemFromParsingName(fileName, IntPtr.Zero, ref IID_IShellItemImageFactory, out iunk);
                var factory = (IShellItemImageFactory)iunk;

                if (factory == null)
                {
                    MessageBoxEx.Show("FileThumbnailExtractor.GetThumbnail\n" + fileName + "\nファイルが見つかっていない、設定確認すること", MessageBoxButton.OK, MessageBoxImage.Error);// TODO:
                    return null;
                }

                SIIGBF flags = SIIGBF.BIGGERSIZEOK;
                if (type == ThumbnailType.Icon) flags |= SIIGBF.ICONONLY;
                else if (type == ThumbnailType.Thumbnail) flags |= SIIGBF.THUMBNAILONLY;

                factory.GetImage(new SIZE { cx = desiredWidth, cy = desiredHeight }, flags, out hBmp);
                if (hBmp == IntPtr.Zero)
                {
                    /* フォーマットが不正な場合はここに来る
                    MessageBoxEx.Show("FileThumbnailExtractor.GetThumbnail\n" + fileName + "\nイメージが取得できない");// TODO:
                     **/
                    return null;
                }

                var bmp = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBmp, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                bmp.Freeze();
                return bmp;
            }
            finally
            {
                DeleteObject(hBmp);
                if (iunk != null)
                    Marshal.ReleaseComObject(iunk);
            }
        }

        public static async Task<BitmapSource> GetThumbnailAsync(string fileName, int desiredWidth, int desiredHeight, ThumbnailType type)
        {
            return await Task.Run(() => GetThumbnail(fileName, desiredWidth, desiredHeight, type));
        }
    }

    [Flags]
    public enum ThumbnailType
    {
        Icon = 1,
        Thumbnail = 2,
        Any = 3,
    }
}

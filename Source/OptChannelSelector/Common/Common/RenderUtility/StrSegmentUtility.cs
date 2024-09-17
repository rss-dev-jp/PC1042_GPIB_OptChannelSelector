using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace RssDev.Common.RenderUtility
{
    /// <summary>
    /// 文字セグメント描画ユーティリティクラス
    /// </summary>
    public class StrSegmentUtility
    {
        public enum ALIGHMENT { LEFT, RIGHT }

        public StrSegment StrSegment { get; private set; }
        private Array bitmapData;

        public StrSegmentUtility(StrSegment strSegment, Array bitmapData)
        {
            this.StrSegment = strSegment;
            this.bitmapData = bitmapData;
        }

        public void DrawValueStr(double value, int xx, int yy, Array color, ALIGHMENT alignment = ALIGHMENT.RIGHT)
        {
            int vv = (int)value;
            string s = vv.ToString();
            char[] chArray = s.ToCharArray();

            int align = alignment == ALIGHMENT.RIGHT ? -1 : 1;
            RenderNum(xx, yy, chArray, s.Length, align * (StrSegment.NumTextWidth + 2), color);
        }

        public void DrawValueStr2(double value, int xx, int yy, Array color, ALIGHMENT alignment = ALIGHMENT.RIGHT)
        {
            string str = string.Format("({0:f2})", value);
            char[] chArray = str.ToCharArray();

            int align = alignment == ALIGHMENT.RIGHT ? -1 : 1;
            RenderNum(xx, yy, chArray, str.Length, align * (StrSegment.NumTextWidth + 2), color);
        }
/*
        public void RenderDir(int xx, int yy, MoveDirection moveDir)
        {
            var list = strSegment.GetDirList(moveDir);
            DrawPoints(xx, yy, list, Colors.Aqua);
        }
*/
        public void DrawStr(string value, int xx, int yy, Array color, ALIGHMENT alignment = ALIGHMENT.RIGHT)
        {
            char[] chArray = value.ToCharArray();

            int align = alignment == ALIGHMENT.RIGHT ? -1 : 1;
            RenderNum(xx, yy, chArray, value.Length, align * (StrSegment.NumTextWidth + 2), color);
        }

        public void RenderNum(int xx, int yy, char[] chArray, int len, int charMove, Array color)
        {
            if (charMove > 0)
            {
                // 左よせ
                xx += 2; // オフセット
            }
            else
            {
                // 右よせ
                charMove *= -1;
                xx -= (charMove * len);
            }

            //for (int i = len - 1; i >= 0; i--)
            for (int i = 0; i < len; i++, xx += charMove)
            {
                List<Point> list = null;
                if (chArray[i] >= '0' && chArray[i] <= '9')
                {
                    list = StrSegment.GetPointList(chArray[i] - '0');
                }
                else
                if (chArray[i] == '-')
                {
                    list = StrSegment.GetMinusList();
                }
                else
                if (chArray[i] == '.')
                {
                    list = StrSegment.GetDotList();
                }
                else
                if (chArray[i] == '=')
                {
                    list = StrSegment.GetEqualList();
                }
                else
                if (chArray[i] == ' ')
                {
                    list = StrSegment.GetSpaceList();
                }
                else
                if (chArray[i] == ',')
                {
                    list = StrSegment.GetCommaList();
                }
                else
                if (chArray[i] >= 'A' && chArray[i] <= 'Z')
                {
                    list = StrSegment.GetAlphabetList(chArray[i] - 'A');
                }
                DrawPoints(xx, yy, list, color);
            }
        }

        private void DrawPoints(int xx, int yy, List<Point> list, Array color)
        {
            if (list != null)
            {
                foreach (var p in list)
                {
                    /*
                    int indexY = (int)(yy + p.Y) * imageWidth * typeSize;
                    int index = indexY + ((int)(xx + p.X)) * typeSize;
                    */
                    WriteColor(bitmapData, (int)(xx + p.X), (int)(yy + p.Y), color);
                }
            }
        }

        /// <summary>
        /// 色書き込み
        /// </summary>
        /// <param name="bitmapData"></param>
        /// <param name="index">開始位置</param>
        /// <param name="color">色</param>
        /// <returns></returns>
        private void WriteColor(Array bitmapData, int x, int y, Array color)
        {
            if (x < 0 || y < 0)
                return;

            if (y >= bitmapData.GetLength(0)) // 範囲チェック
                return;

            if (x >= bitmapData.GetLength(1)) // 範囲チェック
                return;
                
            for(int i = 0; i < color.Length; i++)
            {
                var ss =color.GetValue(i);
                bitmapData.SetValue(color.GetValue(i), y, x);
            }
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssDev.Common.FileUtility
{
    /// <summary>
    /// CSVファイルアクセス用
    /// </summary>
    public class CsvStream
    {
        /// <summary>
        /// CSVをArrayListに変換
        /// </summary>
        /// <param name="csvText">CSVの内容が入ったString</param>
        /// <returns>変換結果のArrayList</returns>
        public static ArrayList CsvToArrayList(string csvText)
        {
            ArrayList csvRecords =
                new ArrayList();

            //前後の改行を削除しておく
            csvText = csvText.Trim(new char[] { '\r', '\n' });

            //一行取り出すための正規表現
            System.Text.RegularExpressions.Regex regLine =
                new System.Text.RegularExpressions.Regex(
                "^.*(?:\\n|$)",
                System.Text.RegularExpressions.RegexOptions.Multiline);

            //1行のCSVから各フィールドを取得するための正規表現
            System.Text.RegularExpressions.Regex regCsv =
                new System.Text.RegularExpressions.Regex(
                "\\s*(\"(?:[^\"]|\"\")*\"|[^,]*)\\s*,",
                System.Text.RegularExpressions.RegexOptions.None);

            System.Text.RegularExpressions.Match mLine = regLine.Match(csvText);
            while (mLine.Success)
            {
                //一行取り出す
                string line = mLine.Value;
                //改行記号が"で囲まれているか調べる
                while ((CountString(line, "\"") % 2) == 1)
                {
                    mLine = mLine.NextMatch();
                    if (!mLine.Success)
                    {
                        throw new ApplicationException("不正なCSV");
                    }
                    line += mLine.Value;
                }
                //行の最後の改行記号を削除
                line = line.TrimEnd(new char[] { '\r', '\n' });
                //最後に「,」をつける
                line += ",";

                //1つの行からフィールドを取り出す
                System.Collections.ArrayList csvFields =
                    new System.Collections.ArrayList();
                System.Text.RegularExpressions.Match m = regCsv.Match(line);
                while (m.Success)
                {
                    string field = m.Groups[1].Value;
                    //前後の空白を削除
                    field = field.Trim();
                    //"で囲まれている時
                    if (field.StartsWith("\"") && field.EndsWith("\""))
                    {
                        //前後の"を取る
                        field = field.Substring(1, field.Length - 2);
                        //「""」を「"」にする
                        field = field.Replace("\"\"", "\"");
                    }
                    csvFields.Add(field);
                    m = m.NextMatch();
                }

                csvFields.TrimToSize();
                csvRecords.Add(csvFields);

                mLine = mLine.NextMatch();
            }

            csvRecords.TrimToSize();
            return csvRecords;
        }

        /// <summary>
        /// 指定された文字列内にある文字列が幾つあるか数える
        /// </summary>
        /// <param name="strInput">strFindが幾つあるか数える文字列</param>
        /// <param name="strFind">数える文字列</param>
        /// <returns>strInput内にstrFindが幾つあったか</returns>
        public static int CountString(string strInput, string strFind)
        {
            int foundCount = 0;
            int sPos = strInput.IndexOf(strFind);
            while (sPos > -1)
            {
                foundCount++;
                sPos = strInput.IndexOf(strFind, sPos + 1);
            }

            return foundCount;
        }

    }
}

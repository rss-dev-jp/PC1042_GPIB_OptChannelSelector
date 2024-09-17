using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace RssDev.Common.FileUtility
{
    /// <summary>
    /// Ini ファイル読み書きのベースクラス
    /// </summary>
    public abstract class IniFileBase
    {
        // 例外用の定数、書き込みミスの場合はコンパイルエラーになるから不要
        const string ERROR_READ = "Configuration file READ ERROR\n[{0}]\n{1}=\nvalue is not {2}";
        /// <summary>
        /// セクションとキーを指定して値を文字列で戻すメソッドベース
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        //protected virtual string GetValue(string section, string key)
        public virtual string GetValue(string section, string key, string defaultvalue)
        {
            return "";
        }

        /// <summary>
        /// プロパティ名の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns>プロパティ名</returns>
        /// <remarks>
        /// sample = GetName(() => setting.UserName));
        /// </remarks>
        public string GetName<T>(Expression<Func<T>> e)
        {
            var member = (MemberExpression)e.Body;
            return member.Member.Name;
        }

        /*
        /// <summary>
        /// Ini ファイルから整数値を読み取る
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <param name="defaultValue">キーが見つからない場合に戻すデフォルト値</param>
        public int ReadInteger(string section, string key, int defaultValue)
        {
            string result = GetValue(section, key);
            if (result == null)
                return defaultValue;
            try
            {
                return int.Parse(result);
            }
            catch (FormatException)
            {
                string s = String.Format(ERROR_READ, section, key, "int");
                var ex = new FormatException(s);
                throw ex;
            }
        }
        /// <summary>
        /// Ini ファイルから浮動小数点数値を読み取る
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <param name="defaultValue">キーが見つからない場合に戻すデフォルト値</param>
        public double ReadDouble(string section, string key, double defaultValue)
        {
            string result = GetValue(section, key);
            if (result == null)
                return defaultValue;
            try
            {
                return double.Parse(result);
            }
            catch (FormatException)
            {
                string s = String.Format(ERROR_READ, section, key, "double");
                var ex = new FormatException(s);
                throw ex;
            }
        }
        /// <summary>
        /// Ini ファイルから真偽（しんぎ）値を読み取る
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <param name="defaultValue">キーが見つからない場合に戻すデフォルト値</param>
        public bool ReadBool(string section, string key, bool defaultValue)
        {
            string result = GetValue(section, key);
            if (result == null)
                return defaultValue;
            //if (result == "1")
            if (result.Equals("True", StringComparison.OrdinalIgnoreCase))
                return true;
            //else if (result == "0")
            else if (result.Equals("False", StringComparison.OrdinalIgnoreCase))
                return false;
            else
            {
                string s = String.Format(ERROR_READ, section, key, "bool");
                var ex = new FormatException(s);
                throw ex;
            }
        }
        /// <summary>
        /// Ini ファイルから文字列を読み取る
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <param name="defaultValue">キーが見つからない場合に戻すデフォルト値</param>
        public string ReadString(string section, string key, string defaultValue)
        {
            string result = GetValue(section, key);
            if (result == null)
                return defaultValue;
            return result;
        }
        */
    }
    /// <summary>
    /// Ini ファイル中の特定値を取り出す軽量な読み取り専用クラス
    /// </summary>
    public class IniFileReader : IniFileBase
    {
        private string _fileName;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileName">読み込む INI ファイルのフルパス</param>
        public IniFileReader(string fileName)
        {
            _fileName = fileName;
        }
        /// <summary>
        /// セクションとキーを指定してストリームから値を文字列で戻すメソッド
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <returns>値の文字列</returns>
        //protected override string GetValue(string section, string key)
        public override string GetValue(string section, string key, string defaultvalue)
        {
            bool bInSection = false;
            int len;
            string buf;
            using (StreamReader sr = new StreamReader(_fileName))
            {
                while ((buf = sr.ReadLine()) != null)
                {
                    if (buf == "") continue;
                    if (buf[0] == ';') continue;
                    len = buf.Length;
                    if (bInSection)
                    {
                        int lPos = buf.IndexOf('=');
                        if (lPos > 0)
                        {
                            if (key == buf.Substring(0, lPos))
                            {
                                return buf.Substring(lPos + 1, len - lPos - 1);
                            }
                        }
                        if (len > 2 && buf[0] == '[' && buf[len - 1] == ']')
                        {
                            return null; //次のセクションが始まってしまったので終了
                        }
                    }
                    else
                    {
                        if (len > 2 && buf[0] == '[' && buf[len - 1] == ']')
                        {
                            if (section == buf.Substring(1, len - 2))
                                bInSection = true;
                        }
                    }
                }
            }
            return defaultvalue;
        }
    }
    /// <summary>
    /// Ini ファイルを読み書きするクラス
    /// 保存時にセクションおよびキーの順番は保持される
    /// </summary>
    public class IniFile : IniFileBase
    {
        /// <summary>
        /// Byte Order Mark の有無
        /// </summary>
        //public bool Bom { get; set; }


        /// <summary>
        /// 保存時に先頭にコメントを入れる場合
        /// </summary>
        public string Header { get; set; }
        List<Dictionary<string, List<Dictionary<string, string>>>> ini;
        string _fileName;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileName">読み込む INI ファイルのフルパス</param>
        public IniFile(string fileName)
        {
            //Bom = false;
            Header = "";
            _fileName = fileName;
            ini = new List<Dictionary<string, List<Dictionary<string, string>>>>();
            if (File.Exists(_fileName))
            {
                bool bInSection = false;
                int len;
                string buf;
                string section = "";
                using (StreamReader sr = new StreamReader(_fileName)) // 読込はそのファイルの文字コードの合わせて読み込む
                {
                    while ((buf = sr.ReadLine()) != null)
                    {
                        if (buf == "") continue;
                        if (buf[0] == ';') continue;
                        len = buf.Length;
                        if (bInSection)
                        {
                            int lPos = buf.IndexOf('=');
                            if (lPos > 0)
                            {
                                string t1 = buf.Substring(0, lPos);
                                string t2 = buf.Substring(lPos + 1, len - lPos - 1);
                                Add(section, t1, t2);
                            }
                            if (len > 2 && buf[0] == '[' && buf[len - 1] == ']')
                            {
                                section = buf.Substring(1, len - 2);
                            }
                        }
                        else
                        {
                            /* オリジナルコードをコメント、間違っている
                            // 最初のセクションが始まるまでガン無視させる
                            if (len > 2 && buf[0] == '[' && buf[len - 1] == ']')
                            {
                                if (section == buf.Substring(1, len - 2))
                                    bInSection = true;
                            }
                            */
                            // 最初のセクションを見つける
                            if (len > 2 && buf[0] == '[' && buf[len - 1] == ']')
                            {
                                section = buf.Substring(1, len - 2);
                                bInSection = true;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Ini オブジェクトに追加を行う内部メソッド
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <param name="value">値の文字列</param>
        private void Add(string section, string key, string value)
        {
            foreach (var dic1 in ini)
            {
                if (dic1.ContainsKey(section))
                {
                    foreach (var dic2 in dic1[section])
                    {
                        if (dic2.ContainsKey(key))
                        {
                            dic2[key] = value;
                            return;
                        }
                    }
                    var dic3 = new Dictionary<string, string>();
                    dic3.Add(key, value);
                    dic1[section].Add(dic3);
                    return;
                }
            }
            var dic4 = new Dictionary<string, string>();
            dic4.Add(key, value);
            var l1 = new List<Dictionary<string, string>>();
            l1.Add(dic4);
            var dic5 = new Dictionary<string, List<Dictionary<string, string>>>();
            dic5.Add(section, l1);
            ini.Add(dic5);
        }
        /// <summary>
        /// セクションとキーを指定して Ini オブジェクトから値を文字列で戻すメソッド
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <returns>値の文字列</returns>
        //protected override string GetValue(string section, string key)
        public override string GetValue(string section, string key, string defaultvalue)
        {
            foreach (var dic1 in ini)
            {
                if (dic1.ContainsKey(section))
                {
                    foreach (var dic2 in dic1[section])
                    {
                        if (dic2.ContainsKey(key))
                        {
                            return dic2[key];
                        }
                    }
                }
            }
            //return null;
            return defaultvalue;
        }
        /// <summary>
        /// Ini オブジェクトをファイルに書き出すメソッド
        /// </summary>
        /// <remarks>UTF-8で保存する、カメラ名称にユニコードが使用される場合があるため</remarks>
        public void Save()
        {
            string crlf = Environment.NewLine; // Linux は LF になる
            StreamWriter sw = null;
            try
            {
                //if (this.Bom)
                //    sw = new StreamWriter(_fileName, false, Encoding.UTF8);
                //else
                sw = new StreamWriter(_fileName, false);　// UTF8Encodingが規定値である

                if (this.Header != "")
                    sw.Write(this.Header);
                foreach (var d1 in ini)
                {
                    foreach (string section in d1.Keys)
                    {
                        sw.Write(String.Format("[{0}]{1}", section, crlf));
                        foreach (var d2 in d1[section])
                        {
                            foreach (string key in d2.Keys)
                            {
                                sw.Write(String.Format("{0}={1}{2}", key, d2[key], crlf));
                            }
                        }
                        sw.Write(crlf);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
        /// <summary>
        /// セクションの存在確認
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <returns>存在する場合は true</returns>
        public bool SectionExists(string section)
        {
            foreach (var dic1 in ini)
            {
                if (dic1.ContainsKey(section))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// セクションを削除する
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        public void EraseSection(string section)
        {
            foreach (var dic1 in ini)
            {
                if (dic1.ContainsKey(section))
                {
                    dic1.Remove(section);
                }
            }
        }

        /*
        /// <summary>
        /// Ini オブジェクト内容を書き換え、又は追加
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <param name="Value">整数値</param>
        public void WriteInteger(string section, string key, int Value)
        {
            this.Add(section, key, Value.ToString());
        }
        /// <summary>
        /// Ini オブジェクト内容を書き換え、又は追加
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <param name="Value">浮動小数点数値</param>
        public void WriteDouble(string section, string key, double Value)
        {
            this.Add(section, key, Value.ToString());
        }
        // <summary>
        /// Ini オブジェクト内容を書き換え、又は追加
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <param name="Value">真偽値</param>
        public void WriteBool(string section, string key, bool Value)
        {
            if (Value)
                this.Add(section, key, "True");
            else
                this.Add(section, key, "False");
        }
        */
        // <summary>
        /// Ini オブジェクト内容を書き換え、又は追加
        /// </summary>
        /// <param name="section">セクションの文字列</param>
        /// <param name="key">キーの文字列</param>
        /// <param name="Value">文字列</param>
        public void WriteString(string section, string key, string Value)
        {
            this.Add(section, key, Value);
        }
        /// <summary>
        /// sectionとkeyからiniファイルの設定値を取得、設定します。 
        /// </summary>
        /// <returns>指定したsectionとkeyの組合せが無い場合は""が返ります。</returns>
        /// 
        public string this[string section, string key]
        {
            set
            {
                //WritePrivateProfileString(section, key, value, filePath);
                WriteString(section, key, value);
            }
            get
            {
                /*
                StringBuilder sb = new StringBuilder(256);
                GetPrivateProfileString(section, key, string.Empty, sb, sb.Capacity, filePath);
                return sb.ToString();
                */
                return GetValue(section, key, "");
            }
        }

    }
}

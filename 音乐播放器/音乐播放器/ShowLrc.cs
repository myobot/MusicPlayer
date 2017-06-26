using TXTClass;
using System.Text.RegularExpressions;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Data;
using System.Text;
using System.IO;

namespace MumiMusic
{
    class ShowLrc
    {
        txtclass txt = new txtclass();
        string excTime = @"(?<=\[).*?(?=\])";//匹配时间的正则
        string excText = @"(?<=\])(?!\[).*";//匹配歌词的正则
        string[] lrcTime = new string[200];//保存歌曲时间
        string[] lrcText = new string[200];//保存歌词文字
        int t1=0;
        int t2 = 0;
        string zj;//中间变量

        /// <summary>
        /// 解析lrc文件
        /// </summary>
        /// <param name="FileName">文件路径</param>
        public void getLrc(string FileName)
        {
            try
            {
                t1 = 0;
                t2 = 0;
                string[] strs = System.IO.File.ReadAllLines(FileName, Encoding.GetEncoding("GB2312"));
                lrcTime = new string[strs.Length];
                lrcText = new string[strs.Length];
                int hasline = strs.Length;
                MatchCollection match1;
                MatchCollection match2;
                for (int i = 0; i < hasline; i++)
                {
                    if (strs[i].StartsWith("[ti:") | strs[i].StartsWith("[ar:") | strs[i].StartsWith("[al:") | strs[i].StartsWith("[by:") | strs[i].StartsWith("[offset:")) { }
                    else
                    {
                        match1 = Regex.Matches(strs[i], excTime);
                        match2 = Regex.Matches(strs[i], excText);
                        foreach (var v in match1)
                        {
                            StringBuilder sb = new StringBuilder(v.ToString());
                            sb.Replace(".", ":");
                            zj = sb.ToString();
                            try
                            {
                                lrcTime[t1] = zj;
                                foreach (var t in match2)
                                {
                                    lrcText[t2] = t.ToString();
                                }
                                t1++;
                                t2++;
                            }
                            catch (Exception e1)
                            {
                                Console.WriteLine(e1.Message);
                            }

                        }

                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //返回数组
        public string[] returnTime()
        {
           
            return this.lrcTime;
        }

        public string[] returnText()
        {
            
            return this.lrcText;
        }
    }
}

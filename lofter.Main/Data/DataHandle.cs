using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace lofter.Main.Data
{
    public class DataHandle
    {
        public static List<string> GetAllOriginalText(string content, int startSearchPoint, List<string> lastResult)
        {
            int startSubPoint = content.IndexOf(@"<div class=""block photo"">", startSearchPoint);
            if (startSubPoint != -1)
            {
                int endSubPoint = content.IndexOf("查看全文", startSubPoint);
                //textBox1.AppendText(content.Substring(startSubPoint, endSubPoint - startSubPoint));
                lastResult.Add(content.Substring(startSubPoint, endSubPoint - startSubPoint));
                if (content.IndexOf(@"<div class=""block photo"">", endSubPoint + 1) != -1)
                {
                    Console.WriteLine($"还有,递归开始的起点是{endSubPoint}");
                    return GetAllOriginalText(content, endSubPoint, lastResult); //进入下一层的递归
                }
                else
                {
                    Console.WriteLine("已经没有了");
                    return lastResult;
                }
            }
            else
            {
                Console.WriteLine("已经没有了");
                return lastResult;
            }
        }

        /// <summary>
        /// 从string格式的集合中提取信息并转换为SinglePost的集合
        /// </summary>
        /// <param name="originalText"></param>
        /// <returns></returns>
        public static List<SinglePost> ConvertToPosts(List<string> originalText)
        {
            List<SinglePost> list = new List<SinglePost>();
            foreach(var singleText in originalText)
            {
                SinglePost temp = new SinglePost();
                //取出日期的Day和Month
                MatchCollection timeMatchs = Regex.Matches(singleText, @">\d{1,2}</a></div>");
                if(timeMatchs.Count == 2)
                {
                    temp.Day = Convert.ToInt32(timeMatchs[0].Value.Replace(@"</a></div>", string.Empty).Replace(@">", string.Empty));
                    temp.Month = Convert.ToInt32(timeMatchs[1].Value.Replace(@"</a></div>", string.Empty).Replace(@">", string.Empty));
                }
                else
                {
                    temp.Day = -1; //-1则代表没有取出来 出错了
                    temp.Month = -1;
                }
                //取出贴文的图片Url
                if (Regex.IsMatch(singleText, @"<img src="".*?>"))
                    temp.ImgUrl = Regex.Match(singleText, @"<img src="".*?>").Value.Replace(@"<img src=""", string.Empty).Replace("\">", string.Empty);
                else
                    temp.ImgUrl = "Error";
                //取出贴文的tag
                if (singleText.Contains(@"<div class=""tag"">"))
                {
                    int startPoint = singleText.IndexOf(@"<div class=""tag"">");
                    int endPoint = singleText.IndexOf("</div>", startPoint);
                    MatchCollection tagMatchs = Regex.Matches(singleText.Substring(startPoint, endPoint - startPoint), @">.*?</a>");
                    foreach(Match i in tagMatchs)
                    {
                        temp.Tag += i.Value.Replace(@"</a>",string.Empty).Replace(@">",string.Empty);
                    }
                    //temp.Tag = tagMatchs.Count.ToString();
                }
                else
                    temp.Tag = "此贴文没有tag";
                
                //取出贴文的Text
                if (singleText.Contains(@"<div class=""text"">"))
                {
                    int startPoint = singleText.IndexOf(@"<div class=""text"">");
                    int endPoint = singleText.IndexOf("</div>", startPoint);
                    temp.Text = singleText.Substring(startPoint, endPoint - startPoint).Replace(@"<div class=""text"">",string.Empty).Replace(@"<p>",string.Empty).Replace(@"</p>",string.Empty).Replace(@"&nbsp;"," ").Replace(@"<br />",string.Empty);
                }
                else
                    temp.Text = "此贴文没有文本或者未获取到";
                list.Add(temp);
            }
            return list;
        }

        public static string ConvertToString(SinglePost post)
        {
            return $"发布日:{post.Day}\n发布月:{post.Month}\n贴文图片链接:{post.ImgUrl}\n贴文正文:{post.Text}\n贴文Tag{post.Tag}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using lofter.Main.Data;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace lofter.Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void button_Start_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox_TargetUrl.Text, @"https://.*\.lofter\.com"))//先检查网址是否规范
            {
                using (HttpClient client = new HttpClient())
                {
                    string TargetUrl = Regex.Match(textBox_TargetUrl.Text, @"https://.*\.lofter\.com").Value; //取出根地址
                    textBox_Status.AppendText("开始下载网页数据" + Environment.NewLine);
                    List<string> htmlContentCollection = new List<string>();
                    int StartPageIndex = 1;
                    while (true)
                    {
                        string content = await client.GetStringAsync(TargetUrl + "/?page=" + StartPageIndex);
                        textBox_Status.AppendText($"第{StartPageIndex}页数据已下载完毕" + Environment.NewLine);
                        htmlContentCollection.Add(content);
                        if (content.Contains("next disable") == true || content.Contains("下一页") == false)
                        {
                            textBox_Status.AppendText($"第{StartPageIndex}页数据已是最后一页,所有数据已下载完毕" + Environment.NewLine);
                            break;
                        }
                        StartPageIndex++;
                        await Task.Delay(200);
                    }
                    //解析、处理数据并放入集合中
                    List<PostCollection> postCollections = new List<PostCollection>();
                    for (int i = 0; i < htmlContentCollection.Count; i++)
                    {
                        List<SinglePost> list = DataHandle.ConvertToPosts(DataHandle.GetAllOriginalText(htmlContentCollection[i], 1, new List<string>()));
                        PostCollection temp = new PostCollection(i + 1, list);
                        postCollections.Add(temp);
                        textBox_Status.AppendText($"第{i + 1}页的数据已经处理完毕" + Environment.NewLine);
                    }
                    //保存数据到根目录
                    try
                    {
                        string TargetRootDirectory = Environment.CurrentDirectory + "\\" + $"{DateTime.Now.Month}月-{DateTime.Now.Day}日-{DateTime.Now.Hour}时-{DateTime.Now.Minute}分";
                        Directory.CreateDirectory(TargetRootDirectory);
                        textBox_Status.AppendText("创建用于保存数据的目录完毕" + Environment.NewLine);
                        for (int i = 0; i < postCollections.Count; i++)
                        {
                            string TargetSubDirectory = TargetRootDirectory + "\\" + $"第{i + 1}页内容";
                            Directory.CreateDirectory(TargetSubDirectory);
                            await File.WriteAllTextAsync(TargetSubDirectory + "\\" + "数据.json", ConvertJsonString(JsonConvert.SerializeObject(postCollections[i])));
                            for (int k = 0; k < postCollections[i].Posts.Count; k++)
                            {
                                using (var img = DownloadImage.UrlToImage(postCollections[i].Posts[k].ImgUrl))
                                {
                                    if (img != null)
                                    {
                                        if (postCollections[i].Posts[k].ImgUrl.Contains("type=jpg") == true)
                                        {
                                            WebClient mywebclient = new WebClient();
                                            mywebclient.DownloadFile(postCollections[i].Posts[k].ImgUrl, TargetSubDirectory + "\\" + $"第{k + 1}张.jpg");
                                        }
                                        else
                                            img.Save(TargetSubDirectory + "\\" + $"第{k + 1}张.png");
                                    }
                                }
                            }
                            textBox_Status.AppendText($"第{i + 1}页的数据和图片已经保存" + Environment.NewLine);
                        }
                        if(MessageBox.Show($"所有数据已下载至{TargetRootDirectory}目录下\n是否打开此目录查看？","EzLofter", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            Process.Start("Explorer.exe", TargetRootDirectory + "\\");
                    }
                    catch
                    {
                        
                    }
                }
            }
            else
                MessageBox.Show("输入的网址里不包含任何lofter博主地址\n示例：https://dongguaailaohan.lofter.com/");
        }

        /// <summary>
        /// 将json字符串格式化排版
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ConvertJsonString(string str)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
    }
}

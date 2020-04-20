using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

//（1）只爬取起始网站上的网页 
//（2）只有当爬取的是html文本时，才解析并爬取下一级URL。
// （3）相对地址转成绝对地址进行爬取。
//（4）尝试使用Winform来配置初始URL，启动爬虫，显示已经爬取的URL和错误的URL信息。
namespace simpleCrawler
{
    class SimpleCrawler
    {
        public event Action<string> Downloaded;
        public Hashtable urls = new Hashtable();
        public string startUrl;
        public int count = 0;
        /*
        static void Main(string[] args)
        {
            SimpleCrawler myCrawler = new SimpleCrawler();
            myCrawler.startUrl = "http://www.cnblogs.com/dstang2000";
            myCrawler.startUrl = myCrawler.startUrl.Trim('/') + '/';
            if (args.Length >= 1) myCrawler.startUrl = args[0];
            myCrawler.urls.Add(myCrawler.startUrl, false);//加入初始页面
            new Thread(myCrawler.Crawl).Start();
        }*/

        public void Crawl()
        {
            Console.WriteLine("开始爬行了.... ");
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                { //取出未爬的url
                    if ((bool)urls[url]) continue;
                    current = url;
                }

                if (current == null || count > 10) break;
                Console.WriteLine("爬行" + current + "页面!");
                string html = DownLoad(current); // 下载
                urls[current] = true;
                count++;
                Parse(html);//解析,并加入新的链接
                Console.WriteLine("爬行结束");
            }
        }

        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                Downloaded(url);
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        private void Parse(string html)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length == 0) continue;
                if (Regex.IsMatch(strRef, @"^\w+[^:]+/")) //不为直接链接（协议+：），并且链接的地址href=“blog/paper1.html”
                {
                    if (startUrl.EndsWith("/"))
                        strRef = startUrl + strRef;
                    else
                        strRef = startUrl + '/' + strRef;
                }
                else if (Regex.IsMatch(strRef, @"^/"))//链接的地址href=“/blog/paper1.html”
                {  //"http://www.cnblogs.com/dstang2000";
                    Match startMatch = Regex.Match(startUrl, @"^\w+://\w+\.\w+\.\w+");
                    String head = startMatch.ToString();
                    strRef = head + strRef;
                }
                else if (Regex.IsMatch(strRef, @"^../"))// 链接的地址href=“../blog/zhang/”//TODO多层
                {
                    MatchCollection matches1 = Regex.Matches(strRef, @"[.][.]/");
                    int times = matches1.Count;
                    String fatherUrl = GetAddress(times);//fahterUrl最后带有一个/
                    strRef = strRef.Remove(0, 3 * times);//startIndex,count,得到的strRef开头无/
                    strRef = fatherUrl + strRef;
                }
                Match matchStartUrl = Regex.Match(startUrl, @"\.[a-zA-Z0-9]+\..+");
                string get = matchStartUrl.ToString();
                string pattern = get + @".+\.(html|xml)";
                if (!Regex.IsMatch(strRef, pattern)) continue;
                if (urls[strRef] == null) urls[strRef] = false;
            }
        }

        public string GetAddress(int times)//根据../的数量返回正确的起始地址
        {
            if (times >= GetLevel(startUrl))
            {
                Match temp = Regex.Match(startUrl, @"(https:|http:)//[^/]+/");
                string t = temp.ToString();
                t = t.Substring(0, t.Length - 1);
                return t;
            }
            String fatherUrl = startUrl;
            for (int i = 0; i < times; i++)
            {
                int indexOfLastAdd = fatherUrl.LastIndexOf('/');
                fatherUrl = fatherUrl.Remove(indexOfLastAdd);
                indexOfLastAdd = fatherUrl.LastIndexOf('/');
                fatherUrl = fatherUrl.Remove(indexOfLastAdd + 1);//留下最后一个/
            }
            return fatherUrl;
        }
        public int GetLevel(string temp)
        {
            MatchCollection matches = Regex.Matches(temp, @"/");
            return matches.Count - 2;
        }

    }
}
/*
@"\.cnblogs\.com/dstang2000/.+\.(html|xml)"
相对地址转成绝对地址进行爬取。
链接的地址有的是以“http://…"这样的，有的是不带协议域名的相对地址。
例如：当前网页的地址  https://www.cnblogs.com/dstang2000/    链接的地址href=“blog/paper1.html”  实际地址是 https://www.cnblogs.com/dstang2000/blog/paper1.html
当前网页的地址  https://www.cnblogs.com/dstang2000/   链接的地址href=“/blog/paper1.html”  实际地址是 https://www.cnblogs.com/blog/paper1.html
当前网页的地址  https://www.cnblogs.com/dstang2000/    链接的地址href=“../blog/zhang/”  实际地址是 https://www.cnblogs.com/blog/zhang/
*/
//（1）只爬取起始网站上的网页 
//（2）只有当爬取的是html文本时，才解析并爬取下一级URL。
// （3）相对地址转成绝对地址进行爬取。
//（4）尝试使用Winform来配置初始URL，启动爬虫，显示已经爬取的URL和错误的URL信息。
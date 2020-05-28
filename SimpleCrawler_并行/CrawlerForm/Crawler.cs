using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlerForm {
  class Crawler {
    public event Action<Crawler> CrawlerStopped;
    public event Action<Crawler, string, string> PageDownloaded;
   
    //所有已下载和待下载URL，key是URL，value表示是否下载成功
    private ConcurrentDictionary<string, bool> urls = new ConcurrentDictionary<string, bool>();
    private ConcurrentBag<Task> tasks = new ConcurrentBag<Task>();
    //待下载队列
    private ConcurrentQueue<string> pending = new ConcurrentQueue<string>();


    //URL检测表达式，用于在HTML文本中查找URL
    private readonly string urlDetectRegex = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";

    //URL解析表达式
    public static readonly string urlParseRegex = @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";
    public string HostFilter { get; set; } //主机过滤规则
    public string FileFilter { get; set; } //文件过滤规则
    public int MaxPage { get; set; } //最大下载数量
    public string StartURL { get; set; } //起始网址
    public Encoding HtmlEncoding { get; set; } //网页编码
    public ConcurrentDictionary<string, bool> DownloadedPages { get => urls; }
    public int count = 0;
    public int filecount = 0;//防止多个进程调用同一个文件
    public bool flag = false;
    public Crawler() {
      MaxPage = 30;
      HtmlEncoding = Encoding.UTF8;
    }

    public void Start() {
      urls.Clear();
      //pending.Clear();
      pending.Enqueue(StartURL);
      int num = count;
      //int count = 0;
      //string firstUrl = pending.TryDequeue();
      //Task<string> task1 = Task.Run(() => DownLoad(firstUrl));
      //task1.Wait();//起始网页单独解析

      while (count <MaxPage) {
        if(pending.TryDequeue(out string url))
        {
            
            tasks.Add(Task.Run(() => DownLoad(url)));
            lock (this)
            {
              count++;
            }
        }
        /*尝试爬虫结束自动停止，但是如下方法可能会出现中途whenall就判定为结束了。
        Task temp = Task.WhenAll(tasks.ToArray());
        temp.ContinueWith(t => flag = true);
        if (flag == true)
            break; 
       */
      }
      CrawlerStopped(this);
    }

    private string DownLoad(string url) {
      urls[url] = true;
      WebClient webClient = new WebClient();
      webClient.Encoding = Encoding.UTF8;
      string html = webClient.DownloadString(url);//will wait
      string fileName;
      lock (this)
      {
         filecount++;
         fileName = filecount.ToString();
      }
      File.WriteAllText(fileName, html, Encoding.UTF8);
      PageDownloaded(this, url, "success");
      Parse(html, url);//will wait
      return html;
    }

    private void Parse(string html, string pageUrl) {

      var matches = new Regex(urlDetectRegex).Matches(html);
      foreach (Match match in matches) {
        string linkUrl = match.Groups["url"].Value;
        if (linkUrl == null || linkUrl == "") continue;
        linkUrl = FixUrl(linkUrl, pageUrl);//转绝对路径

        //解析出host和file两个部分，进行过滤
        Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
        string host = linkUrlMatch.Groups["host"].Value;
        string file = linkUrlMatch.Groups["file"].Value;
        if (file == "") file = "index.html";

        if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(file, FileFilter)
          && !urls.ContainsKey(linkUrl)) {
            pending.Enqueue(linkUrl);
            urls.TryAdd(linkUrl, false);
        }
      }
    }


    //将相对路径转为绝对路径
    static private string FixUrl(string url, string baseUrl) {
      if (url.Contains("://")) {
        return url;
      }
      if (url.StartsWith("//")) {
        return "http:" + url;
      }
      if (url.StartsWith("/")) {
        Match urlMatch = Regex.Match(baseUrl, urlParseRegex);
        String site = urlMatch.Groups["site"].Value;
        return site.EndsWith("/") ? site + url.Substring(1) : site + url;
      }

      if (url.StartsWith("../")) {
        url = url.Substring(3);
        int idx = baseUrl.LastIndexOf('/');
        return FixUrl(url, baseUrl.Substring(0, idx));
      }

      if (url.StartsWith("./")) {
        return FixUrl(url.Substring(2), baseUrl);
      }

      int end = baseUrl.LastIndexOf("/");
      return baseUrl.Substring(0, end) + "/" + url;
    }
  }

   
}

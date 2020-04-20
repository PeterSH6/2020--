using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using simpleCrawler;

namespace WebCrawler
{
    public partial class Form1 : Form
    {
        SimpleCrawler crawler = new SimpleCrawler();

        public Form1()
        {
            InitializeComponent();
            crawler.Downloaded += Crawler_PageDownloaded;
        }

        private void Crawler_PageDownloaded(string url)
        {
            if (this.listBox1.InvokeRequired)
            {
                Action<String> action = this.AddUrl;
                this.Invoke(action, new object[] { url });
            }
            else
            {
                AddUrl(url);
            }
        }

        private void AddUrl(string url)
        {
            listBox1.Items.Add(url);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            crawler.startUrl = txtUrl.Text;
            crawler.startUrl = crawler.startUrl.Trim('/') + '/';
            crawler.urls.Add(crawler.startUrl, false);//加入初始页面
            listBox1.Items.Clear();
            new Thread(crawler.Crawl).Start();
        }

    }
}


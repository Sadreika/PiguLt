using HtmlAgilityPack;
using System;
using System.Collections.Specialized;
using System.Net;

namespace CrawlerPiguLt
{
    public class Program
    {
        public int triesToConnect = 0;
        public NameValueCollection piguLtUrls = new NameValueCollection();

        public int crawling(String kategorija, String newUrlAddress)
        {
            WebClient client = new WebClient();

            NameValueCollection myNameValueCollection = new NameValueCollection();

            NameValueCollection piguLtInnerUrls = new NameValueCollection();

            String[] valueArray = null;

            myNameValueCollection.Add("Host", "pigu.lt");
            myNameValueCollection.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:76.0) Gecko/20100101 Firefox/76.0");
            myNameValueCollection.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            myNameValueCollection.Add("Accept-Language", "en-GB,en;q=0.5");

            foreach (String key in myNameValueCollection.Keys)
            {
                valueArray = myNameValueCollection.GetValues(key);
                foreach (String value in valueArray)
                {
                    client.Headers.Set(key, value);
                }
            }

            try
            {
                 Console.WriteLine("\nKategorija " + kategorija + "\n");
                 HtmlWeb hw = new HtmlWeb();
                 HtmlDocument dataInner = hw.Load(newUrlAddress);
                 try
                 {
                     foreach (HtmlNode link in dataInner.DocumentNode.SelectNodes("//div[@id='categoriesGrid']//a"))
                     {
                          piguLtInnerUrls.Add(link.InnerText, link.GetAttributeValue("href", string.Empty));
                          Console.WriteLine(link.InnerText.Trim());
                     }
                 }
                 catch (Exception response_exception)
                 {
                    Console.WriteLine("prekes_sarase");
                 }

                return 0;

            }
            catch (Exception response_exception)
            {
               // Console.WriteLine("ERROR\n" + response_exception);
                triesToConnect = triesToConnect + 1;
                return 1;
            }
        }

        static void Main(string[] args)
        {
            int methodRecursion = 1;
            Program crawlerObject = new Program();
            Sc scObject = new Sc();
            NameValueCollection piguLtUrls = new NameValueCollection();

            string newUrlAddress = scObject.createUrl();

            HtmlWeb hw = new HtmlWeb();
            HtmlDocument data = hw.Load(newUrlAddress);
            foreach (HtmlNode link in data.DocumentNode.SelectNodes("//div[@class='subcategory-list']//a"))
            {
                piguLtUrls.Add(link.InnerText, link.GetAttributeValue("href", string.Empty));
            }

            while (methodRecursion == 1)
            {
                foreach (String key in piguLtUrls.Keys)
                {
                    var valueArray = piguLtUrls.GetValues(key);
                    foreach (String value in valueArray)
                    {
                        methodRecursion = crawlerObject.crawling(key, value);
                    }
                }
            }

            Console.WriteLine("END");
        }
    }
}

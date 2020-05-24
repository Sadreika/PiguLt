using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerPiguLt
{
    public class Program
    {
        public int triesToConnect = 0;
        public int crawling(String newUrlAddress)
        {
            WebClient client = new WebClient();

            NameValueCollection myNameValueCollection = new NameValueCollection();

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
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument data = hw.Load(newUrlAddress);
                foreach (HtmlNode link in data.DocumentNode.SelectNodes("//div[@class='subcategory-list']//a"))
                {
                    Console.WriteLine(link.GetAttributeValue("href", string.Empty));
                }
                


                //string data = client.DownloadString(newUrlAddress);
                //Console.WriteLine(newUrlAddress);
                //Console.WriteLine(data);

                /*WebHeaderCollection myWebHeaderCollection = client.ResponseHeaders;
                for (int i = 0; i < myWebHeaderCollection.Count; i++)
                    Console.WriteLine("\t" + myWebHeaderCollection.GetKey(i) + " = " + myWebHeaderCollection.Get(i));

                Console.WriteLine("Duomenys\n" + data);*/
                return 0;

            }
            catch (Exception response_exception)
            {
                Console.WriteLine("ERROR\n" + response_exception);
                triesToConnect = triesToConnect + 1;
                if (triesToConnect == 1)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        static void Main(string[] args)
        {
            int methodRecursion = 1;
            Program crawlerObject = new Program();
            Sc scObject = new Sc();

           // for (int i = 30; i < days; i++) // reikes pakeisti (int i = 1; i < days + 1; i++)
           // {
                string newUrlAddress = scObject.createUrl();

                methodRecursion = crawlerObject.crawling(newUrlAddress);

                if (methodRecursion == 1)
                {
                    methodRecursion = crawlerObject.crawling(newUrlAddress);
                }
          //  }
            Console.WriteLine("END");
        }
    }
}

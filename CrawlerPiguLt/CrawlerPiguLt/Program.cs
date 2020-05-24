using HtmlAgilityPack;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Specialized;
using System.Net;


namespace CrawlerPiguLt
{
    public class Program
    {
        public int triesToConnect = 0;
        public NameValueCollection piguLtUrls = new NameValueCollection();

        public void gettingAllInfo(String newUrlAddress)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument info = hw.Load(newUrlAddress);
            String[] titleArray = new string[500];
            String[] priceArray = new string[500];
            String[] discountArray = new string[500];
            for (int k = 0; k < titleArray.Length; k++)
            {
                titleArray[k] = "";
                priceArray[k] = "";
                discountArray[k] = "";
            }
            int a = 0, b = 0, c = 0;

            foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//div[@class='product-price']//span[@class='price notranslate']"))
            {
             //   Console.WriteLine(information_about_product.InnerText.Trim());
                priceArray[b] = information_about_product.InnerText.Trim();
                b = b + 1;
            }
            foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//p[@class='product-name']"))
            {
                // Console.WriteLine(information_about_product.InnerText.Trim());
                titleArray[a] = information_about_product.InnerText.Trim();
                a = a + 1;
            }
            foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//span[@class='discount']"))
            {
                //Console.WriteLine(information_about_product.InnerText.Trim());
                discountArray[c] = information_about_product.InnerText.Trim();
                c = c + 1;
            }

            for(int j = 0; j < titleArray.Length; j++)
            {
                if(titleArray[j].Equals("") || priceArray.Equals(""))
                {

                }
                else
                {
                    Console.WriteLine(" KAINA: " + priceArray[j] + " TITLE: " + titleArray[j] + " DISCOUNT: " + discountArray[j]);
                }
            }
        }



    
        public int crawling(String kategorija, String newUrlAddress)
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
                 Console.WriteLine("\nKategorija " + kategorija + "\n");
                 HtmlWeb hw = new HtmlWeb();
                 HtmlDocument dataInner = hw.Load(newUrlAddress);
                 try
                 {
                     foreach (HtmlNode link in dataInner.DocumentNode.SelectNodes("//div[@id='categoriesGrid']//a"))
                     {
                        gettingAllInfo(link.GetAttributeValue("href", string.Empty));
                     }
                 }
                 catch (Exception response_exception)
                 {
                     Console.WriteLine("Prekes sarase");
                     gettingAllInfo(newUrlAddress);
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
            NameValueCollection piguLtUrls = new NameValueCollection();

            string newUrlAddress = "https://pigu.lt/lt/katalogas";

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

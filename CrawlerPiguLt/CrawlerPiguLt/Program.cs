using HtmlAgilityPack;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CrawlerPiguLt
{
    public class Program : Infomation_about_link
    {

        static void Main(string[] args)
        {
            Infomation_about_link linkObject = new Infomation_about_link();

            NameValueCollection piguLtUrls = new NameValueCollection();
            string newUrlAddress = "https://pigu.lt/lt/katalogas";

            HtmlWeb hw = new HtmlWeb();
            HtmlDocument data = hw.Load(newUrlAddress);
            foreach (HtmlNode link in data.DocumentNode.SelectNodes("//div[@class='subcategory-list']//a"))
            {
                piguLtUrls.Add(link.InnerText, link.GetAttributeValue("href", string.Empty));
            }

            foreach (String key in piguLtUrls.Keys)
            {
                var valueArray = piguLtUrls.GetValues(key);
                foreach (String value in valueArray)
                {
                    if (key.Equals("Tvoros")) // reikes padaryti, kad visiems
                    { 
                        linkObject.crawling(value);
                    }
                }
            }

        }
    }
}

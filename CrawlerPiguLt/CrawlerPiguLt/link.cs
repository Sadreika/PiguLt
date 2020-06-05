using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerPiguLt
{
    public class link
    {
        public List<string> priceList = new List<string>();
        public List<string> titleList = new List<string>();
        public List<string> discountList = new List<string>();
        public string originalLink = "";
        public int pageNumber = 0;

        public void settingOriginalLink(string givenOriginalLink)
        {
            this.originalLink = givenOriginalLink;
        }

        public void crawlingProcess(string newUrlAddress)
        {
            Console.WriteLine(newUrlAddress);
            try
            {
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument info = hw.Load(newUrlAddress);

                foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//div[@class='product-price']//span[@class='price notranslate']"))
                {
                    this.priceList.Add(information_about_product.InnerText.Trim());
                }
                foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//p[@class='product-name']"))
                {
                    this.titleList.Add(information_about_product.InnerText.Trim());
                }
                foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//div[@class='product-item__badges']"))
                {
                    var value = information_about_product.InnerHtml;
                    if (value.Contains("discount") == true)
                    {
                        this.discountList.Add(information_about_product.InnerText.Trim().Replace("\n", "").Replace("\r", ""));
                    }
                    else
                    {
                        this.discountList.Add("No discount");
                    }
                }
                pageNumber = pageNumber + 1;
                crawlingProcess(creatingNewUrl(newUrlAddress));
            }
           
            catch (Exception ex)
            {
                Console.WriteLine("FINAL PAGE");
            }
        }

        public string creatingNewUrl(string url)
        {
            string nextUrl = originalLink + "?page=" + pageNumber;
            return nextUrl;
        }
    }
}

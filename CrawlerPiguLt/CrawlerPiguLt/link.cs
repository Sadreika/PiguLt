using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerPiguLt
{
    public class link : codeExeptions
    {
        public List<string> priceList = new List<string>();
        public List<string> titleList = new List<string>();
        public List<string> discountList = new List<string>();
        public string originalLink = "";
        public int pageNumber = 1;
        public bool isThisPageIsExespion = false;

        public void settingOriginalLink(string givenOriginalLink)
        {
            this.originalLink = givenOriginalLink;
        }

        public void crawlingProcess(string newUrlAddress)
        {
            Console.WriteLine("THIS IS URL " + newUrlAddress);
            try
            {
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument info = hw.Load(newUrlAddress);

                foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//p[@class='product-name']"))
                {
                    this.titleList.Add(information_about_product.InnerText.Trim());
                }
                try
                {
                    foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//div[@class='product-price']//span[@class='price notranslate']"))
                    {
                        this.priceList.Add(information_about_product.InnerText.Trim());
                    }
                } catch (Exception ex)
                {
                    this.priceList.Add("No price");
                }
               
                try
                {
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
                } catch (Exception ex)
                {
                    this.discountList.Add("No discount");
                }
                try
                {
                    foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//span[@class='label-soldout']"))
                    {
                        this.priceList.Add("Sold out");
                    } 
                } catch(Exception ex)
                {
                    
                }
     
                pageNumber = pageNumber + 1;

                if(isThisPageIsExespion.Equals(true))
                {
                    crawlingProcess(creatingNewUrlWithOutPage(newUrlAddress));
                }
                else
                {
                    if (checkingForLinkExeption(newUrlAddress).Equals(""))
                    {
                        crawlingProcess(creatingNewUrl(newUrlAddress));
                    }
                    else
                    {
                        isThisPageIsExespion = true;
                        settingOriginalLink(checkingForLinkExeption(newUrlAddress));
                        crawlingProcess(creatingNewUrlWithOutPage(newUrlAddress));
                    }
                }
            }
           
            catch (Exception ex)
            {
                Console.WriteLine("FINAL PAGE");
            }
            isThisPageIsExespion = false;
        }

        public string creatingNewUrl(string url)
        {
            string nextUrl = originalLink + "?page=" + pageNumber;
            return nextUrl;
        }

        public string creatingNewUrlWithOutPage(string url)
        {
            string nextUrl = originalLink + pageNumber;
            return nextUrl;
        }
    }
}

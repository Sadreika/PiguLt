using HtmlAgilityPack;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerPiguLt
{
    public class Infomation_about_link : link
    {

        public List<string> canCrawl(string link)
        {
            List<string> categoriesLinks = new List<string>();
            try
            {
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument dataInner = hw.Load(link);
                foreach (HtmlNode categories in dataInner.DocumentNode.SelectNodes("//div[@id='categoriesGrid']//a"))
                {
                    categoriesLinks.Add(categories.GetAttributeValue("href", string.Empty));
                }
                return categoriesLinks;
            }
            catch (Exception response_exception)
            {
                categoriesLinks.Add(link);
                return categoriesLinks;
            }            
        }
        public void crawling(string link)
        {
            List<string> linksNeedToVisit = new List<string>();
            linksNeedToVisit = canCrawl(link);
            List<link> linkList = new List<link>(); 
            for(int i = 0; i < linksNeedToVisit.Count; i++)
            {
                link linkObject = new link();
                linkObject.settingOriginalLink(linksNeedToVisit[i]);
                linkObject.crawlingProcess(linksNeedToVisit[i]);
                fixingLinks(linkObject);
                linkList.Add(linkObject);
            }
            excel creatingExcel = new excel();
            creatingExcel.savingToDataBase(linkList);
        }

        public void fixingLinks(link linkObject)
        {
            if(linkObject.titleList.Count > linkObject.priceList.Count)
            {
                while(linkObject.titleList.Count > linkObject.priceList.Count)
                {
                    linkObject.priceList.Add("Null");
                }
            }
            if (linkObject.titleList.Count > linkObject.discountList.Count)
            {
                while (linkObject.titleList.Count > linkObject.discountList.Count)
                {
                    linkObject.discountList.Add("Null");
                }
            }
        }
    }
}

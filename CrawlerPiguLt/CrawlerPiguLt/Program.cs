using HtmlAgilityPack;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CrawlerPiguLt
{
    public class Program
    {
        public int index = 0;
        public int pageNumber = 1;
        public int triesToConnect = 0;
        public NameValueCollection piguLtUrls = new NameValueCollection();

        public List<string> priceList = new List<string>();
        public List<string> titleList = new List<string>();
        public List<string> discountList = new List<string>();

        public string originalUrl = "";

        public int gettingAllInfo(String newUrlAddress)
        {
            index = index + 1;
            Console.WriteLine(index);
            HtmlWeb hw = new HtmlWeb();
            try
            {
                HtmlDocument info = hw.Load(newUrlAddress);
                Console.WriteLine(newUrlAddress);
                if (pageNumber == 1)
                {
                    originalUrl = newUrlAddress;
                }
                pageNumber = pageNumber + 1;

                foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//div[@class='product-price']//span[@class='price notranslate']"))
                {
                    priceList.Add(information_about_product.InnerText.Trim());
                }
                foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//p[@class='product-name']"))
                {
                    titleList.Add(information_about_product.InnerText.Trim());
                }
                foreach (HtmlNode information_about_product in info.DocumentNode.SelectNodes("//div[@class='product-item__badges']"))
                {
                    var value = information_about_product.InnerHtml;
                    if (value.Contains("discount") == true)
                    {
                        discountList.Add(information_about_product.InnerText.Trim().Replace("\n", "").Replace("\r", ""));
                    }
                    else
                    {
                        discountList.Add("No discount");
                    }
                }
                gettingAllInfo(originalUrl + "?page=" + pageNumber);
                return 0;
            }
            catch(Exception ex)
            {
                return 2;
            }
        }
        // kai einama per puslapiu url prisideda ?page=3
        public void writingToExcel()
        {
            Console.WriteLine("EXCEL");
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            object misvalue = System.Reflection.Missing.Value;
            try
            {
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                oSheet.Cells[1, 1] = "Price";
                oSheet.Cells[1, 2] = "Title";
                oSheet.Cells[1, 3] = "Discount";

                for(int i = 2; i < priceList.Count; i++)
                {
                    oSheet.Cells[i, 1] = priceList[i - 2];
                    oSheet.Cells[i, 2] = titleList[i - 2];
                    oSheet.Cells[i, 3] = discountList[i - 2];
                }

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs("C:\\Users\\mariu\\Documents\\GitHub\\PiguLt\\Data.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, // sutvarkyti saugojimo l.
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();
                oXL.Quit();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Couldn't save to Excel");
            }
        }

        public int crawling(String kategorija, String newUrlAddress)
        {
            NameValueCollection myNameValueCollection = new NameValueCollection();

            try
            {
                // nepaema kitu puslpiu
                 HtmlWeb hw = new HtmlWeb();
                 HtmlDocument dataInner = hw.Load(newUrlAddress);
                 int value = 0;
                 try
                 {
                     foreach (HtmlNode link in dataInner.DocumentNode.SelectNodes("//div[@id='categoriesGrid']//a"))
                     {
                        while(value != 2)
                        {
                            value = gettingAllInfo(link.GetAttributeValue("href", string.Empty));
                            pageNumber = 0;
                        }
                    }
                 }
                 catch (Exception response_exception)
                 {
                     value = gettingAllInfo(newUrlAddress);
                     if(value == 2)
                     {
                        pageNumber = 0;
                     }
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
                        if (key.Equals("Žaislai vaikams"))
                        {
                            methodRecursion = crawlerObject.crawling(key, value);
                        }
                    }
                }
            }
            Console.WriteLine("Writing to excel");
            crawlerObject.writingToExcel();
            Console.WriteLine("END");
        }
    }
}

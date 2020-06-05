using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace CrawlerPiguLt
{
    public class excel : Infomation_about_link
    {
        public void savingToDataBase(List<link> linkList)
        {
            string connectionString = "";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"(LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\mariu\Documents\GitHub\PiguLt\CrawlerPiguLt\CrawlerPiguLt\AllData.mdf; Integrated Security = True";
                con.Open();
                Console.WriteLine("PAEJO");
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("nepaejo");
            }
         /*   for (int j = 0; j < linkList.Count; j++)
            {
                for (int i = 0; i < linkList[j].priceList.Count; i++)
                {
                  //  oSheet.Cells[i + 2 + fromWhereToStart, 1] = linkList[j].priceList[i];
                  //  oSheet.Cells[i + 2 + fromWhereToStart, 2] = linkList[j].titleList[i];
                  //  oSheet.Cells[i + 2 + fromWhereToStart, 3] = linkList[j].discountList[i];
                  //  fromWhereToStart = i;
                }
            } */
        }
    }
}

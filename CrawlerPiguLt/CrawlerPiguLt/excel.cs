using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerPiguLt
{
    public class excel : Infomation_about_link
    {
        public void savingToDataBase(List<link> linkList)
        {
            for (int j = 0; j < linkList.Count; j++)
            {
                for (int i = 0; i < linkList[j].priceList.Count; i++)
                {
                  //  oSheet.Cells[i + 2 + fromWhereToStart, 1] = linkList[j].priceList[i];
                  //  oSheet.Cells[i + 2 + fromWhereToStart, 2] = linkList[j].titleList[i];
                  //  oSheet.Cells[i + 2 + fromWhereToStart, 3] = linkList[j].discountList[i];
                  //  fromWhereToStart = i;
                }
            }
        }
    }
}

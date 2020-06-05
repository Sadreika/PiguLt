using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CrawlerPiguLt
{
    public class excel : Infomation_about_link
    {
        public void savingToDataBase(List<link> linkList)
        {
            try
            {
                cleaningDataBase();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Items;Integrated Security=True";
                con.Open();
                

                string price = "";
                string title = "";
                string discount = "";

                for (int j = 0; j < linkList.Count; j++)
                {
                    for (int i = 0; i < linkList[j].discountList.Count; i++)
                    {
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = con;
                        price = linkList[j].priceList[i];
                        title = linkList[j].titleList[i];
                        discount = linkList[j].discountList[i];
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Discount", discount);
                        cmd.CommandText = "INSERT [Items].[dbo].[AllData] (Price, Name, Discount) VALUES (@Price, @Title, @Discount)";
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with database");
            }
        }

        public void cleaningDataBase()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Items;Integrated Security=True";
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM [Items].[dbo].[AllData]";
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}

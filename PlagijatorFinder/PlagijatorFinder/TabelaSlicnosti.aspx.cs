using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

namespace PlagijatorFinder
{
    public partial class TabelaSlicnosti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //funkcija za kreiranje tabele
        protected void generateTable()
        {
            SqlConnection conn = new SqlConnection(_Default.GetConnectionString());
            conn.Open();

           

            try 
            {
                SqlCommand maxIDQuery = new SqlCommand("SELECT MAX(ID) FROM Rad", conn);
                int maxId = ((int)maxIDQuery.ExecuteScalar());

                SqlCommand minIDQuery = new SqlCommand("SELECT MIN(ID) FROM Rad", conn);
                int minId = ((int)minIDQuery.ExecuteScalar());

                Literal1.Text += "<table border=2px width=100%><tr><td>Fajl 1/2</td>";
                SqlCommand fileNames = new SqlCommand("SELECT Name FROM Rad", conn);
                SqlDataReader fileNamesReader = fileNames.ExecuteReader();
                while (fileNamesReader.Read())
                {
                    Literal1.Text += "<td>" + fileNamesReader.GetString(0) + "</td>";
                }
                fileNamesReader.Close();
                Literal1.Text += "</tr>";

                for (int i = minId; i <= maxId; i++)
                {
                    SqlCommand com1 = new SqlCommand("SELECT Name FROM Rad WHERE ID=" + i, conn);
                    string ime1 = com1.ExecuteScalar().ToString();
                    Literal1.Text +="<tr><td>"+ime1+"</td>";

                    for(int j=minId; j<=maxId; j++)
                    {
                        SqlCommand com2 = new SqlCommand("SELECT similarity FROM Slicnost WHERE IDrad1="+ i+" AND IDrad2="+j, conn);
                        decimal  slicnost= ((decimal)com2.ExecuteScalar());                        
                        Literal1.Text +="<td>"+slicnost+"</td>";
                    }
                   Literal1.Text +="</tr>";
                }
                Literal1.Text +="</table>";
                conn.Close();

            }
            catch (Exception ex)
            {
                Literal1.Text += ex.Message;
            }
        }

        //racunanje slicnosti izmedju radova/fajlova i upis u bazu u zasebnu tabelu Slicnost
        protected void countSimBtn_Click(object sender, EventArgs e)
        {
            decimal similarity;
            SqlConnection conn = new SqlConnection(_Default.GetConnectionString());
            conn.Open();

            SqlCommand dropTableData = new SqlCommand("DELETE FROM Slicnost", conn);
            dropTableData.ExecuteNonQuery();

            try
            {
                //mozda bi bilo dobro, izvuci mix i max id iz baze radova
                //i pri tome postaviti da brojaci idu od mix do max koeficijenta

                SqlCommand maxIDQuery = new SqlCommand("SELECT MAX(ID) FROM Rad", conn);
                int maxId = ((int)maxIDQuery.ExecuteScalar());
                //SqlCommand com1 = new SqlCommand("SELECT ID, fileTXTPath from Rad", conn);
                //SqlCommand com2 = new SqlCommand("SELECT ID, fileTXTPath from Rad", conn);
                //SqlDataReader reader1 = com1.ExecuteReader();
                //SqlDataReader reader2 = com2.ExecuteReader();
                

                for (int i = 1; i <= maxId; i++)
                {
                    SqlCommand com1 = new SqlCommand("SELECT fileTXTpath FROM Rad WHERE ID=" + i, conn);
                    //SqlDataReader reader1 = com1.ExecuteReader();
                    string tempPutanja = com1.ExecuteScalar().ToString();
                    //statusLbl.Text += tempPutanja;
                    string rad1 = _Default.getStringFromTxtFile(tempPutanja);
                    //reader1.Close();
                    

                    for (int j = 1; j <= maxId; j++)
                    {
                        SqlCommand com2 = new SqlCommand("SELECT fileTXTpath FROM Rad WHERE ID="+j, conn);
                        string tempPutanja2 = com2.ExecuteScalar().ToString();

                        //SqlDataReader reader2 = com2.ExecuteReader();
                        //if (reader2.HasRows)
                        //{
                            string rad2 = _Default.getStringFromTxtFile(tempPutanja2);
                            //reader2.Close();
                            similarity = (decimal)countSimilarity(rad1, rad2);

                            string tempSimilarity = similarity.ToString();
                            //var x = decimal.Parse(tempSimilarity, new NumberFormatInfo() { NumberDecimalSeparator = "," });
                            //decimal finalSim = Decimal.Parse(x.ToString());
                            
                            //SqlCommand cmn = new SqlCommand("INSERT INTO Slicnost(IDrad1, IDrad2, similarity) VALUES" +
                            //                                        " (@IDrad1, @IDrad2, @similarity)");
                            //SqlCommand cmn = new SqlCommand("INSERT INTO Slicnost VALUES(" + i + "," + j + ", CONVERT(DECIMAL(8,7), " + similarity + "))");
                            SqlCommand cmn = new SqlCommand("INSERT INTO Slicnost VALUES(" + i + "," + j + ", CONVERT(DECIMAL(8,4), REPLACE('" + tempSimilarity + "', ',','.')))");
                            cmn.CommandType = System.Data.CommandType.Text;
                            cmn.Connection = conn;
                            //cmn.Parameters.AddWithValue("@IDrad1", i);
                            //cmn.Parameters.AddWithValue("@IDrad2", j);
                            //cmn.Parameters.AddWithValue("@similarity", similarity);
                            cmn.ExecuteNonQuery();
                            
                        //}
                        //else
                            //continue;
                    }
                }

                    
                statusLbl.Text = "Uspeh";
                //reader1.Close();
                
                conn.Close();


            }
            catch (Exception ex) 
            {
                statusLbl.Text = ex.Message;
            }
        }

        protected float countSimilarity(string a, string b)
        {
            return Class1.StringSift2.Similarity(a, b);
        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            generateTable();
        }
    }
}
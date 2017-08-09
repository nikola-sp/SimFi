using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


namespace PlagijatorFinder
{
    public partial class testStrana : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
        }

        protected void testButton_Click(object sender, EventArgs e)
        {
            string putanja1 = null;
            string putanja2 = null;
            string rad1 = null;
            string rad2 = null;
            
           
            //int result;
            float result, resultCompare;
                //Unosimo rad u bazu
                SqlConnection conn = new SqlConnection(_Default.GetConnectionString());
                conn.Open();
                try
                {
                    SqlCommand com1 = new SqlCommand("SELECT fileTXTpath FROM Rad WHERE Name='" + DropDownList1.SelectedItem.Text+"'", conn);
                    putanja1 = com1.ExecuteScalar().ToString();

                    SqlCommand com2 = new SqlCommand("SELECT fileTXTpath FROM Rad WHERE Name='" + DropDownList2.SelectedItem.Text+"'", conn);
                    putanja2 = (string)(com2.ExecuteScalar());

                    SqlCommand com3 = new SqlCommand("SELECT Name, fileTXTPath FROM Rad", conn);
                    
                    rad1 = _Default.getStringFromTxtFile(putanja1);
                    rad2 = _Default.getStringFromTxtFile(putanja2);
                    compareLabel.Text = "";
                    //racunanje slicnosti koriscenjem Levenshtein rastojanja
                    //najcesci rezultat je outOfMemory izuzetak
                    //result = LevenshteinDistance.Compute(rad1, rad2);
                    //resultLabel.Text = "Levenstein distance between</br>" + DropDownList1.SelectedItem.Text +
                    //                  "and</br>" + DropDownList2.SelectedItem.Text + "</br>is " + result.ToString();

                    //racunanje rastojanja koriscenjem koda sa sajta 
                    //http://siderite.blogspot.com/2007/01/super-fast-string-distance-algorithm.html
                    result = Class1.StringSift2.Similarity(rad1, rad2);
                    resultLabel.Text = "StringSift distance between</br>" + DropDownList1.SelectedItem.Text +
                                          " and</br> " + DropDownList2.SelectedItem.Text + "</br>is " + result.ToString();

                    //Uporedjujemo izabrani fajl iz dropDown liste sa svim fajlovima u bazi i
                    //rezultate poredjenja ispisujemo na compareLabelu
                    SqlDataReader reader1 = com3.ExecuteReader();
                    while (reader1.Read())
                    {

                            string tempName = reader1.GetString(0);
                            string tempPath = reader1.GetString(1);
                            
                            //compareLabel.Text = "pobedaa!";
                           
                             
                                string rad3 = _Default.getStringFromTxtFile(tempPath);
                                resultCompare = Class1.StringSift2.Similarity(rad1, rad3);
                                compareLabel.Text += "Slicnost izmedju radova :" + tempName + " i " +
                                    DropDownList1.SelectedItem.Text + " je: " + resultCompare.ToString() + "</br>";

                                string[] proba = null;
                        
                                

                                //compareLabel.Text +=
                                //reader1.Close();
                            
                            
                    }
                    reader1.Close();
                    conn.Close();

                    //Diff.Item[] novo = null;
                    //novo = Diff.DiffText(rad1, rad2);
                    //resultLabel.Text += novo.ToString();
                }
                catch(Exception ex)
                {
                    resultLabel.Text = "Greska</br> " + ex.Message;
                }

            //pozivanje fajla koji je unet na Default strani
            //string putanjaZaProveru = (string)(Session["putanjaZaProveru"]);
            //testLabel.Text = _Default.getStringFromTxtFile(putanjaZaProveru);

        }
        /**
        protected void fileFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Panel2.Visible = fileFileCheckBox.Checked ? true : false;
            fileAllFilesCheckBox.Checked = false;
            categoryCheckBox.Checked = false;

        }

        protected void fileAllFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            Panel1.Visible = fileAllFilesCheckBox.Checked ? true : false;
            fileFileCheckBox.Checked = false;
            categoryCheckBox.Checked = false;
        }

        protected void categoryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Panel3.Visible = categoryCheckBox.Checked ? true : false;
            fileFileCheckBox.Checked = false;
            fileAllFilesCheckBox.Checked = false;
        }
         * */

        protected void panel1Button_Click(object sender, EventArgs e)
        {

            //fileFileCheckBox.Checked = false;
            //categoryCheckBox.Checked = false;

            string putanja=null;
            string rad = null;
            float resultCompare;
            resultLiteral.Text = "";
            SqlConnection conn = new SqlConnection(_Default.GetConnectionString());
            conn.Open();
            try
            {
                SqlCommand com1 = new SqlCommand("SELECT fileTXTpath FROM Rad WHERE Name='" + RadioButtonList1.SelectedItem.Text + "'", conn);
                putanja = com1.ExecuteScalar().ToString();
                rad = _Default.getStringFromTxtFile(putanja);

                SqlCommand com2 = new SqlCommand("SELECT Name, fileTXTPath FROM Rad", conn);
                SqlDataReader reader = com2.ExecuteReader();

                while (reader.Read())
                {
                    string tempName = reader.GetString(0);
                    string tempPath = reader.GetString(1);

                    string tempRad = _Default.getStringFromTxtFile(tempPath);
                    resultCompare = Class1.StringSift2.Similarity(rad, tempRad);
                    resultLiteral.Text += "Slicnost izmedju radova :" + tempName + " i " +
                        RadioButtonList1.SelectedItem.Text + " je: " + resultCompare.ToString() + "</br>";
                    
                }
                
                reader.Close();
                conn.Close();
                resultLiteral.Text += "</br>**************************************************************************</br>";


            }
            catch(Exception ex) 
            {
                resultLiteral.Text += ex.Message;
            }
            //Panel1.Visible = true;
            //fileAllFilesCheckBox.Checked = true;
        }

        protected void panel2Button_Click(object sender, EventArgs e)
        {
            //fileAllFilesCheckBox.Checked = false;
            //categoryCheckBox.Checked = false;
            string putanja1 = null;
            string putanja2 = null;
            string rad1 = null;
            string rad2 = null;


            //int result;
            float result, resultCompare;
            //Unosimo rad u bazu
            SqlConnection conn = new SqlConnection(_Default.GetConnectionString());
            conn.Open();
            try
            {
                SqlCommand com1 = new SqlCommand("SELECT fileTXTpath FROM Rad WHERE Name='" + DropDownList1.SelectedItem.Text + "'", conn);
                putanja1 = com1.ExecuteScalar().ToString();

                SqlCommand com2 = new SqlCommand("SELECT fileTXTpath FROM Rad WHERE Name='" + DropDownList2.SelectedItem.Text + "'", conn);
                putanja2 = (string)(com2.ExecuteScalar());

                SqlCommand com3 = new SqlCommand("SELECT Name, fileTXTPath FROM Rad", conn);

                rad1 = _Default.getStringFromTxtFile(putanja1);
                rad2 = _Default.getStringFromTxtFile(putanja2);
                
                //racunanje slicnosti koriscenjem Levenshtein rastojanja
                //najcesci rezultat je outOfMemory izuzetak
                //result = LevenshteinDistance.Compute(rad1, rad2);
                //resultLabel.Text = "Levenstein distance between</br>" + DropDownList1.SelectedItem.Text +
                //                  "and</br>" + DropDownList2.SelectedItem.Text + "</br>is " + result.ToString();

                //racunanje rastojanja koriscenjem koda sa sajta 
                //http://siderite.blogspot.com/2007/01/super-fast-string-distance-algorithm.html
                result = Class1.StringSift2.Similarity(rad1, rad2);
                resultLiteral.Text += "StringSift distance between</br>" + DropDownList1.SelectedItem.Text +
                                      " and</br> " + DropDownList2.SelectedItem.Text + "</br>is " + result.ToString();
                resultLiteral.Text += "</br>**************************************************************************</br>";

                conn.Close();
            }
            catch (Exception ex)
            {
                resultLiteral.Text += "Greska</br> " + ex.Message;
            }
            //fileFileCheckBox.Checked = true;
            //Panel2.Visible = true;
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Panel1.Visible = CheckBox1.Checked ? true : false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            Panel2.Visible = CheckBox2.Checked ? true : false;
            Panel1.Visible = false;
            Panel3.Visible = false;
            CheckBox1.Checked = false;
            CheckBox3.Checked = false;
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            Panel3.Visible = CheckBox3.Checked ? true : false;
            Panel1.Visible = false;
            Panel2.Visible = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
        }

        protected void panel3Button_Click(object sender, EventArgs e)
        {
            //SELECT  Distinct r.Name FROM Rad r JOIN RadKategorija rk ON r.ID= rk.RadID JOIN Kategorija ON
            //Kategorija.ID = rk.KatID WHERE Kategorija.Name='Kriptografija'
            //mozda napraviti funkciju za proveru sa svim ostalim radovima,koja prima dva razlicita tipa argumenata
            //u prvom slucaju to da bude samo ime fajla
            //a u drugom slucaju samo kategorija

        }

        
    }

   
}
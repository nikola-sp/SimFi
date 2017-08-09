using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using org.apache.pdfbox.util;
using org.apache.pdfbox.pdmodel;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace PlagijatorFinder
{
    public partial class _Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void saveAndConvertButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (FileUpload1.HasFile)
            {
                try
                {
                    sb.AppendFormat("uploading file: {0}", FileUpload1.FileName);
                    statusLabel.Text = sb.ToString();
                    SaveAndConvertFile(FileUpload1.PostedFile);
                    statusLabel.Text = "Fajl uspesno snimljen i konvertovan";
                    
                }
                catch(Exception ex) 
                {
                    statusLabel.Text = ex.Message;
                }
            }
            else
            {
                statusLabel.Text = "Ne postoji ulazni fajl";
            }
        }

        void SaveAndConvertFile(HttpPostedFile file)
        {
            // Putanja ka direktorijumu gde ce biti snimljen svaki uneti fajl
            string savePath = "C:\\Users\\Dusan\\Desktop\\PlagijatorFinder\\PlagijatorFinder\\uploadFiles\\";
            //string savePath = "C:\\Users\\DR.AKUL\\Documents\\Visual Studio 2010\\Projects\\PlagijatorFinder\\PlagijatorFinder\\uploadFiles\\";
            string fileName = null ;
            if(RequiredFieldValidator1.IsValid)
            { fileName = fileNameTextBox.Text; }
            
            // Kreiramo putanju i vrsimo proveru da li postoji fajl sa istim imenom
            //kao i fajl koji pokusavamo da upload-ujemo
            string pathToCheck = savePath + fileName;
            string tempfileName = "";
   
            if (File.Exists(pathToCheck))
            {
                int counter = 2;
                while (File.Exists(pathToCheck))
                {
                    // ukoliko postoji
                    // dodajemo odgovarajuci prefiks
                    tempfileName = counter.ToString() + fileName;
                    pathToCheck = savePath + tempfileName;
                    counter++;
                }

                fileName = tempfileName;

                // Pisemo poruku o postojanju fajla sa istim imenom, kao i novo ime pod kojim je snimljen
                statusLabel.Text = "Fajl sa istim imenom vec postoji.<br />Fajl je snimljen pod imenom " + fileName;
            }
            else
            {
                // Poruka o uspesnom snimanju fajla
                statusLabel.Text = "Your file was uploaded successfully." + FileUpload1.FileName;
            }

            // Dodajemo ime fajla na postojecu putanju za snimanje
            savePath += fileName;

            //provera da li su podaci korektno izabrani
            if (isChecked(CheckBoxList1) && RangeValidator1.IsValid)
            {
                checkBoxListLabel.Text = "kategorije dobro izabrane";
                //snimamo PDF fajl na zeljenu lokaciju
                FileUpload1.SaveAs(savePath);

                //KONVERTOVANJE FAJLA U TXT FORMAT
                string finalTxt = parseUsingPDFBox(savePath);
                TextWriter tw = new StreamWriter(savePath + ".txt", true);
                tw.WriteLine(finalTxt);
                tw.Close();
                
                //izdajemo poruku o uspesnoj konverziji
                statusLabel.Text += "<br/> Fajl uspesno konvertovan";

                int godina = Convert.ToInt32(godinaTextBox.Text);
                foreach (var e in getCheckedItems(CheckBoxList1))
                {
                    Label2.Text += e.ToString();
                }

                //Unosimo fajl u bazu
                SqlConnection conn = new SqlConnection(GetConnectionString());
                conn.Open();
                try
                {
                    //ubacivanje fajla u tabelu Rad
                    SqlCommand cmn = new SqlCommand("INSERT INTO Rad(Name, Year, filePDFPath, fileTXTPath) VALUES" +
                                                                      " (@name, @year, @filePDF, @fileTXT)");

                    cmn.CommandType = System.Data.CommandType.Text;
                    cmn.Connection = conn;
                    cmn.Parameters.AddWithValue("@name", fileNameTextBox.Text);
                    cmn.Parameters.AddWithValue("@year", godina);
                    cmn.Parameters.AddWithValue("@filePDF", savePath);
                    cmn.Parameters.AddWithValue("@fileTXT", savePath + ".txt");
                    cmn.ExecuteNonQuery();
                    checkBoxListLabel.Text = "URAAA!";

                    //dobijanje ID-a za poslednji ubacen fajl
                    SqlCommand newIDCommand = new SqlCommand("SELECT MAX(ID) FROM Rad", conn);
                    int radID = ((int)newIDCommand.ExecuteScalar());
                    
                    //ubacivanje veze Rad/Kategorija
                    foreach (var e in getCheckedItems(CheckBoxList1))
                    {
                        SqlCommand getKatID = new SqlCommand("SELECT ID FROM Kategorija WHERE Name='" + e.ToString()+"'", conn);
                        int katID = ((int)getKatID.ExecuteScalar());
                        SqlCommand updateCrossTable = new SqlCommand("INSERT INTO RadKategorija(RadID, KatID) VALUES (@radID, @katID)");
                        updateCrossTable.CommandType = System.Data.CommandType.Text;
                        updateCrossTable.Connection = conn;
                        updateCrossTable.Parameters.AddWithValue("@radID", radID);
                        updateCrossTable.Parameters.AddWithValue("@katID", katID);
                        updateCrossTable.ExecuteNonQuery();

                    }

                    //ubacivanje veze Rad/Autor i azuriranje tabele Autor

                    string autoriString = AutorTextBox.Text;
                    char[] charSeparators = new char[] { ';' };
                    string[] autori = autoriString.Split(charSeparators, StringSplitOptions.None);

                   // Conn.open();

                    foreach (var x in autori)
                    {
                        SqlCommand cmn2 = new SqlCommand("exec spInsertAutore @imeautora");

                        cmn2.CommandType = System.Data.CommandType.Text;
                        cmn2.Connection = conn;
                        cmn2.Parameters.AddWithValue("@imeautora", x);
                        cmn2.ExecuteNonQuery();

                    }
                    //Conn.close();
                    conn.Close();
                    

                }
                catch(Exception e)
                {
                    checkBoxListLabel.Text += "Greska: " + e.Source + "</br>" + e.Message;
                }
               
            }
            else
            {
                if (!isChecked(CheckBoxList1))
                    checkBoxListLabel.Text = "Izaberite kategoriju!";
            }

        }
/**
        string getFilePath(string path)
        {
            // Specify the path to save the uploaded file to.
            string savePath = "C:\\Users\\DR.AKUL\\Documents\\Visual Studio 2010\\Projects\\PlagijatorFinder\\PlagijatorFinder\\uploadFiles\\";

            // Get the name of the file to upload.
            string fileName = FileUpload1.FileName;

            // Create the path and file name to check for duplicates.
            path = savePath + fileName + ".txt";
            return path;
        }
        **/
        private static string parseUsingPDFBox(string filename)
        {
            
            PDDocument doc = PDDocument.load(filename);
            PDFTextStripper stripper = new PDFTextStripper();
            string text = stripper.getText(doc);
            doc.close();
            return text;
        }  

        /**
        protected void convertButton_Click(object sender, EventArgs e)
        {

            //string temp = FileUpload1.FileName;
            string path = "C:\\Users\\DR.AKUL\\Documents\\Visual Studio 2010\\Projects\\PlagijatorFinder\\PlagijatorFinder\\uploadFiles\\";
            string final = path + temp;
            string finalTxt = parseUsingPDFBox(final);
            // create a writer and open the file
            TextWriter tw = new StreamWriter(path+".txt", true);

            // write a line of text to the file
            tw.WriteLine(finalTxt);

            // close the stream
            tw.Close();

            statusLabel.Text += "<br/> Fajl uspesno konvertovan";


        }
         **/

        public static string GetConnectionString()
        {
            //return "Data Source=DRAKUL-PC;Initial Catalog=bazaRadova;Integrated Security=True";
            //return "Data Source=MEANMACHINE;Initial Catalog=bazaRadova;Integrated Security=True";
            return System.Configuration.ConfigurationManager.ConnectionStrings["bazaRadovaConnectionString2"].ConnectionString;
        }

        //provera da li je izabrana bar jedna kategorija
        public static bool isChecked(CheckBoxList ch)
        {
            int values =  ch.Items.Cast<ListItem>().Where(n => n.Selected).Select(n => n.Value ).Count();
            if (values > 0)
                return true;
            else
                return false;
        }
        
        //informacije o izabranim kategorijama
        public static List<string> getCheckedItems(CheckBoxList ch)
        {
            List<string> values = ch.Items.Cast<ListItem>().Where(n=>n.Selected).Select(n=>n.Value).ToList();
            return values;
        }

        //metoda vraca string koji predstavlja sadrzaj txt fajla sa zadate putanje
        public static string getStringFromTxtFile(string path)
        {
            string resultString = null;
            StreamReader str = new StreamReader(path, true);
            resultString = str.ReadToEnd();
            str.Close();
            return resultString;
        }



        protected void BtnDodajKategoriju_Click(object sender, ImageClickEventArgs e)
        {
            btnSnimiKategoriju.Visible = true;
            KategorijaTextBox.Visible = true;
        }

        protected void snimiKategorijuButton_Click(object sender, EventArgs e)
        {
            if (KategorijaTextBox.Text == "")
            { 
                
            }
            SqlConnection conn = new SqlConnection(GetConnectionString());
            conn.Open();
            SqlCommand cmn = new SqlCommand("INSERT INTO Kategorija(Name) VALUES" +
                                                    " (@name)");

            cmn.CommandType = System.Data.CommandType.Text;
            cmn.Connection = conn;
            cmn.Parameters.AddWithValue("@name", KategorijaTextBox.Text);
            cmn.ExecuteNonQuery();
            conn.Close();

            KategorijaTextBox.Text = "";
            SqlDataSource1.DataBind();
            CheckBoxList1.DataBind();



        }







    }
}

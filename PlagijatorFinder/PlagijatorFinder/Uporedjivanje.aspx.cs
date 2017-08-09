using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Web;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;
using Data;
using System.Data.SqlClient;


namespace PlagijatorFinder
{
	public partial class Uporedjivanje : System.Web.UI.Page
    {

        public int DisplayA;         //{ set { lblA.Text = String.Format( "File A: {0:N0} lines", value ); } }
		public int DisplayB ;        //{ set { lblB.Text = String.Format( "File B: {0:N0} lines", value ); } }
		public int DisplayRepeat;    //{ set { lblRepeat.Text = String.Format( "Repeat: x{0:N0}", value ); } }
		public float DisplayTime;    //{ set { lblTime.Text = String.Format( "Time: {0:N3} ms", value ); } }
		public int DisplaySpace;     //{ set { lblSpace.Text = String.Format( "Space: {0:N0} bytes", value ); } }
		public int DisplayDiff;      //{ set { lblDiff.Text = String.Format( "Diff: {0:N0} lines", value ); } }

          protected void Page_Load(object sender, EventArgs e)
          {

              // Run();
          }
              
		
        //{
        //    InitializeComponent();

        //    new WinFormsKickstart( this ).Loaded += HandlesLoaded;

        //    //MessageBox.Show( String.Format( "Frequency: {0:N} {1}", Stopwatch.Frequency, Stopwatch.IsHighResolution ) );
        //}

		void HandlesLoaded()
		{
			new Thread( Run ) { IsBackground = true }.Start();
		}

		void Run()
		{
			//uzimamo rad sa dropDownliste i uporedjujemo sa svim ostalima uz pomoc
            //metoda diff klase

            string putanja1 = null;
            string putanja2 = null;
            SqlConnection conn = new SqlConnection(_Default.GetConnectionString());
            conn.Open();
            
            try
			{
                //uzimamo fajl i prosledjujemo ga RunCore metodi

                SqlCommand com1 = new SqlCommand("SELECT fileTXTpath FROM Rad WHERE Name='" + DropDownList1.SelectedItem.Text + "'", conn);
                putanja1 = com1.ExecuteScalar().ToString();

                //Literal1.Text = "";
                SqlCommand com2 = new SqlCommand("SELECT Name, fileTXTPath FROM Rad", conn);
                
                SqlDataReader reader1 = com2.ExecuteReader();
                while (reader1.Read())
                {

                    string tempName = reader1.GetString(0);
                    string tempPath = reader1.GetString(1);

                    //compareLabel.Text = "pobedaa!";
                    if (DropDownList1.SelectedItem.Text != tempName)
                    {
                        string rad3 = _Default.getStringFromTxtFile(tempPath);
                        Literal1.Text += "<br>Poredjenje izmedju: " + DropDownList1.SelectedItem.Text + " i " + tempName + "</br>";
                        RunCore(putanja1, tempPath);
                        //resultCompare = Class1.StringSift2.Similarity(rad1, rad3);
                        //compareLabel.Text += "Slicnost izmedju radova :" + tempName + " i " +
                        //DropDownList1.SelectedItem.Text + " je: " + resultCompare.ToString() + "</br>";

                        //string[] proba = null;
                    }


                    //compareLabel.Text +=
                    //reader1.Close();


                }
                reader1.Close();
                conn.Close();
                //for petlja gde fiksiramo prvi string
                //a je fiksiran string b su svi ostali stringovi(fajlovi)
                //RunCore(putanja1, putanja2);
			}
			catch ( Exception x )
			{
                Literal1.Text = "Greska " + x.Message;
                //BeginInvoke( ( Action ) ( () => MessageBox.Show( x.ToString(), "Exception" ) ) );
			}
		}

		void RunCore(string path1, string path2)
		{
			string[] a = GetResource(path1);
            string[] b = GetResource(path2);

			DiffLinear.Diff.Compare( a, b ); // JIT

			{
				var args = Environment.GetCommandLineArgs();

				if ( args.Length == 3 )
				{
					a = File.ReadAllLines( args[ 1 ] );
					b = File.ReadAllLines( args[ 2 ] );
				}
			}

#if DEBUG
			const int REPEAT = 1;
#else
			const int REPEAT = 1;
#endif

			Results r = null;

			var sw = Stopwatch.StartNew();
			for ( int i = 0 ; i < REPEAT ; i++ )
			{

				//var pa = GetResource( "NLineDiffDemo.Files.grid1.html" ); var pb = GetResource( "NLineDiffDemo.Files.grid2.html" );
				//var qa = File.ReadAllText( @"D:\dev\LeanAndMean\Grid1.html" ); var qb = File.ReadAllText( @"D:\dev\LeanAndMean\Grid2.html" );
				//var ra = File.ReadAllLines( @"D:\dev\LeanAndMean\Grid1.html" ); var rb = File.ReadAllLines( @"D:\dev\LeanAndMean\Grid2.html" );

				//a = File.ReadAllLines( @"D:\dev\LeanAndMean\Grid1.html" ); b = File.ReadAllLines( @"D:\dev\LeanAndMean\Grid2.html" );
				//a = GetResource( "NLineDiffDemo.Files.grid1.html" ); b = GetResource( "NLineDiffDemo.Files.grid2.html" );
				//using ( var fs = File.OpenRead( @"D:\dev\LeanAndMean\Grid1.html" ) ) using ( var sr = new StreamReader( fs ) ) while ( !sr.EndOfStream ) sr.ReadLine(); using ( var fs = File.OpenRead( @"D:\dev\LeanAndMean\Grid2.html" ) ) using ( var sr = new StreamReader( fs ) ) while ( !sr.EndOfStream ) sr.ReadLine();
				//using ( var fs = File.OpenRead( @"D:\dev\LeanAndMean\Grid1.html" ) ) a = SplitStream( fs ); using ( var fs = File.OpenRead( @"D:\dev\LeanAndMean\Grid2.html" ) ) b = SplitStream( fs );

				//a = Gen(); b = Gen();
				//a = Gen(); b = a;
				//a = new string[] { "a", "a", "a", "a", "a", "a", "a", "a" }; b = new string[] { "b", "b", "b", "b", "b" };
				//a = new string[] { "a", "a", "a" }; b = a;
				//a = new string[] { "d", "s", "s", "r" }; b = new string[] { "s", "r" };

				r = DiffLinear.Diff.Compare( a, b );
			}
			sw.Stop();

			var sb = new StringBuilder();
            if (r != null)
            {
                sb.Append("<html>\n<head>\n<style>\n");
                sb.Append("BODY { font-family: Verdana; font-size: 10pt }");
                sb.Append(".deleted { background:white; color:black; }\n");
                sb.Append(".inserted { background:lightgreen; }\n");
                sb.Append(".bla { background:lightgreen; }\n");
                sb.Append("</style>\n</head>\n<body>\n");
                foreach (var snake in r.Snakes)
                {
                    //for (int i = snake.XStart; i < snake.XMid; i++) sb.Append("<span class='deleted'>" + HttpUtility.HtmlEncode(a[i]) + "</span><br />\n");
                   // for (int i = snake.YStart; i < snake.YMid; i++) sb.Append("<span class='inserted'>" + HttpUtility.HtmlEncode(b[i]) + "</span><br />\n");
                    for (int i = snake.XMid; i < snake.XEnd; i++) sb.Append("<span class='bla' title = 'bla'>" + HttpUtility.HtmlEncode(a[i]) + "</span><br />\n");
                }
                sb.Append("</body>\n</html>\n");
            }
			var html = sb.ToString();
       
            //div1.innerhtml = sb.ToString();
            Literal1.Text += sb.ToString();

            lblA.Text = a.Length.ToString();
            lblB.Text = b.Length.ToString();
            if (r != null) DisplayDiff = r.Snakes.Sum(s => s.ADeleted + s.BInserted);
            DisplayRepeat = REPEAT;
            DisplayTime = sw.ElapsedTicks * 1000f / Stopwatch.Frequency / (float)REPEAT;
            DisplaySpace = 2 * (2 * (a.Length + b.Length) + 1) * sizeof(int);
            //BeginInvoke( ( Action ) ( () =>
            //    {
            //        //wb.DocumentText = html;

            //        DisplayA = a.Length;
            //        DisplayB = b.Length;
            //        if ( r != null ) DisplayDiff = r.Snakes.Sum( s => s.ADeleted + s.BInserted );
            //        DisplayRepeat = REPEAT;
            //        DisplayTime = sw.ElapsedTicks * 1000f / Stopwatch.Frequency / ( float ) REPEAT;
            //        DisplaySpace = 2 * ( 2 * ( a.Length + b.Length ) + 1 ) * sizeof( int );
            //    } ) );
		}

//private void BeginInvoke(System.Action action)
//{
//    throw new System.NotImplementedException();
//}

		static Random r = new Random();
		string[] Gen()
		{
			var l = new List<string>();
			var lines = r.Next( 1000 ) + 500;
			for ( int i = 0 ; i < lines ; i++ )
				l.Add( ( ( char ) ( 'a' + r.Next( 26 ) ) ).ToString() );

			return l.ToArray();
		}

        string[] GetResource(string name)
        {
            using (var s = GenerateStreamFromString(getStringFromTxtFile(name))) return SplitStream(s);
        }

		public string[] SplitStream( Stream s )
		{
			var list = new List<string>();

			using ( var r = new StreamReader( s ) )
				while ( !r.EndOfStream )
					list.Add( r.ReadLine() );

			return list.ToArray();
		}

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string getStringFromTxtFile(string path)
        {
            string resultString = null;
            StreamReader str = new StreamReader(path, true);
            resultString = str.ReadToEnd();
            str.Close();
            return resultString;
        }

        protected void uporediButton_Click(object sender, System.EventArgs e)
        {
            Literal1.Text = "";
            Run();
        }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace PlagijatorFinder
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string RenderMenu()
        {
            var result = new StringBuilder();
            RenderMenuItem("Početna", "Pocetna.aspx", result);
            RenderMenuItem("Home", "Default.aspx", result);
            RenderMenuItem("Uporedjivanje", "Uporedjivanje.aspx", result);
            RenderMenuItem("Test", "testStrana.aspx", result);            
            RenderMenuItem("About", "About.aspx", result);
            RenderMenuItem("TabelaSlicnosti", "TabelaSlicnosti.aspx", result);
            RenderMenuItem("Kontakt", "Kontakt.aspx", result);
            return result.ToString();
        }

        void RenderMenuItem(string title, string address, StringBuilder output)
        {
            output.AppendFormat("<li><a  href=\"{0}\" ", address);

            var requestUrl = HttpContext.Current.Request.Url;
            if (requestUrl.Segments[requestUrl.Segments.Length - 1].Equals(address, StringComparison.OrdinalIgnoreCase)) // If the requested address is this menu item.
                //output.Append("class=\"ActiveMenuButton\"");
                output.Append("class=\"active\"");
            else
                //output.Append("class=\"MenuButton\"");
            output.Append("class=\"before\"");
            output.AppendFormat("><span>{0}</span></a></li> ", title);
        }
    }
}

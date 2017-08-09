using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlagijatorFinder
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string time = System.DateTime.Now.ToLongTimeString();
            Label2.Text = time;
            Label4.Text = time;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string time = System.DateTime.Now.ToLongTimeString();
            Label2.Text = time;
            Label4.Text = time;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Label2.Text = System.DateTime.Now.ToLongTimeString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISB42403_GroupProject
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Optional: Clear session or authentication state here if needed
            Response.Redirect("Login.aspx");
        }
    }
}
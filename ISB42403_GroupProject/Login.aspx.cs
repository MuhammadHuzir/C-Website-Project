using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISB42403_GroupProject
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT * FROM Users WHERE UserName = @username";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GameShopDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", txtUserName.Text);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // Username found
                object objPasswordHash = dt.Rows[0]["PasswordHash"];
                object objRole = dt.Rows[0]["Role"];
                object objEnabled = dt.Rows[0]["Enabled"];
                string password = txtPassword.Text;
                string storedPasswordHash = objPasswordHash.ToString();

                PBKDF2Hash hash = new PBKDF2Hash(password, storedPasswordHash);
                bool check = hash.PasswordCheck;
                bool enabled = Convert.ToBoolean(objEnabled);

                if (check == true && enabled == true)
                {
                    // Password correct and account enabled
                    Session["UserName"] = txtUserName.Text;
                    Session["Role"] = objRole;
                    if (Session["Role"].ToString() == "user")
                        Response.Redirect("HomePage.aspx");
                    else if (Session["Role"].ToString() == "admin")
                        Response.Redirect("Admin.aspx");
                }
                else
                {
                    // Password incorrect or account disabled
                    lblStatus.Text = "Incorrect password or account disabled.";
                }
            }
            else
            {
                // Username not found
                lblStatus.Text = "Incorrect username.";
            }


        }
    }
}
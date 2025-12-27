using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISB42403_GroupProject
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            PBKDF2Hash hash = new PBKDF2Hash(password);
            string passwordHash = hash.HashedPassword;
            bool enabled = true;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GameShopDB"].ConnectionString);

            string sql = @"INSERT INTO Users VALUES
                           (@username, @passwordhash, @email, @role, @enabled)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@username", txtUserName.Text);
            cmd.Parameters.AddWithValue("@passwordhash", passwordHash);
            cmd.Parameters.AddWithValue("@role", "user");
            cmd.Parameters.AddWithValue("@enabled", enabled);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                lblStatus.Text = "Status: Data successfully saved.";
            }
            catch (SqlException ex)
            {
                lblStatus.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
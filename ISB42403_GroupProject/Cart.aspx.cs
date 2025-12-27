using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ISB42403_GroupProject.Products; // Product class

namespace ISB42403_GroupProject
{
    public partial class Cart : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCart();
                GenerateTransactionId();
            }
        }

        protected void CartRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateQuantity")
            {
                var txtQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                if (txtQuantity != null && int.TryParse(txtQuantity.Text, out int newQuantity) && newQuantity > 0)
                {
                    string productId = e.CommandArgument.ToString();
                    UpdateCart(productId, newQuantity);
                    LoadCart();
                }
            }
            else if (e.CommandName == "RemoveItem")
            {
                string productId = e.CommandArgument.ToString();
                UpdateCart(productId, 0); // Remove item
                LoadCart();
            }
        }

        private void LoadCart()
        {
            if (Session["Cart"] != null)
            {
                List<Product> cart = (List<Product>)Session["Cart"];
                CartRepeater.DataSource = cart;
                CartRepeater.DataBind();

                lblTotal.Text = cart.Sum(p => p.Price * p.Quantity).ToString("F2");

                // Store cart items in DB
                foreach (var item in cart)
                {
                    decimal totalPrice = item.Price * item.Quantity;
                    int productId = Convert.ToInt32(item.ProductID);
                    ExecuteCartProcedure(lblCartId.Text, productId, item.Quantity, totalPrice);
                }
            }
            else
            {
                lblTotal.Text = "0.00";
            }
        }

        private void UpdateCart(string productId, int quantity)
        {
            if (Session["Cart"] != null)
            {
                var cart = (List<Product>)Session["Cart"];
                var item = cart.FirstOrDefault(p => p.ProductID == productId);

                if (item != null)
                {
                    if (quantity > 0)
                        item.Quantity = quantity;
                    else
                        cart.Remove(item);

                    Session["Cart"] = cart;
                }
            }
        }

        private void ExecuteCartProcedure(string transactionId, int productId, int quantity, decimal totalPrice)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GameShopDB"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand("spLoadCart", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        void GenerateTransactionId()
        {
            string hexTicks = DateTime.Now.Ticks.ToString("X");
            lblCartId.Text = hexTicks.Substring(hexTicks.Length - 15, 9);
            lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            string transactionId = lblCartId.Text;

            string query = "DELETE FROM Transactions WHERE transactionId = @transactionId";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GameShopDB"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@transactionId", transactionId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("Summary.aspx");
        }
    }
}

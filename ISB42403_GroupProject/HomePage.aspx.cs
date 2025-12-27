using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ISB42403_GroupProject.Products;

namespace ISB42403_GroupProject
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        private void BindSlideshow()
        {
            string query = "SELECT ProductID, Name, Price, Image FROM Products WHERE Category = 'Featured'";
            DataTable dt = GetData(query);
            rptSlideshow.DataSource = dt;
            rptSlideshow.DataBind();
        }

        private void BindGameGrid()
        {
            string query = "SELECT ProductID, Name, Price, Image FROM Products WHERE Category = 'Games'";
            DataTable dt = GetData(query);
            rptGameGrid.DataSource = dt;
            rptGameGrid.DataBind();
        }

        private DataTable GetData(string query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GameShopDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            var button = (System.Web.UI.WebControls.Button)sender;
            var productId = button.CommandArgument;

            // Retrieve product details
            var product = GetProductById(productId);
            if (product != null)
            {
                // Initialize cart session if null
                if (Session["Cart"] == null)
                {
                    Session["Cart"] = new List<Product>();
                }

                var cart = (List<Product>)Session["Cart"];

                // Check if product already exists in cart
                var existingProduct = cart.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (existingProduct != null)
                {
                    existingProduct.Quantity += 1; // Increment quantity if already in cart
                }
                else
                {
                    product.Quantity = 1; // Set default quantity
                    cart.Add(product);
                }

                // Save updated cart in session
                Session["Cart"] = cart;

                // Redirect to cart page
                Response.Redirect("Cart.aspx");
            }
        }

        private Product GetProductById(string productId)
        {
            // Fetch product from database based on ProductID
            // (Simplified for example; use a database query in real implementation)
            // Replace with your actual data access logic.
            using (var connection = new System.Data.SqlClient.SqlConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["GameShopDB"].ConnectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand(
                    "SELECT ProductID, Name, Price, Image FROM Products WHERE ProductID = @ProductID", connection);
                command.Parameters.AddWithValue("@ProductID", productId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            ProductID = reader["ProductID"].ToString(),
                            Name = reader["Name"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Image = reader["Image"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}
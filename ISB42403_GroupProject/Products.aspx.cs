using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace ISB42403_GroupProject
{
    public partial class Products : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // No additional code required for loading products
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

        // Product class for session storage
        public class Product
        {
            public string ProductID { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Image { get; set; }
            public int Quantity { get; set; } = 1;  // Default quantity to 1
            public double TotalPrice => (double)(Price * Quantity);  // Calculate total price based on quantity
        }

    }
}

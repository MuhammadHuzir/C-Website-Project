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
    public partial class Summary : System.Web.UI.Page
    {
        private const decimal TaxRate = 0.07m; // 7% tax
        private const decimal ServiceFee = 5.00m; // Fixed service fee

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalculateTotal();
            }
        }

        private void CalculateTotal()
        {
            if (Session["Cart"] != null)
            {
                var cart = (List<Products.Product>)Session["Cart"];

                // Calculate subtotal by multiplying price by quantity for each product
                decimal subtotal = cart.Sum(p => p.Price * p.Quantity); // Corrected calculation
                decimal tax = subtotal * TaxRate;
                decimal total = subtotal + tax + ServiceFee;

                lblSubtotal.Text = subtotal.ToString("0.00");
                lblTax.Text = tax.ToString("0.00");
                lblServiceFee.Text = ServiceFee.ToString("0.00");
                lblTotal.Text = total.ToString("0.00");
            }
            else
            {
                // In case there is no cart data in the session
                lblSubtotal.Text = "0.00";
                lblTax.Text = "0.00";
                lblServiceFee.Text = "0.00";
                lblTotal.Text = "0.00";
            }
        }


        protected void btnProceedToPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("Payment.aspx");
        }
    }
}